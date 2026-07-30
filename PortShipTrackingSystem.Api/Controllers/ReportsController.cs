namespace PortShipTrackingSystem.Api.Controllers;

using Microsoft.AspNetCore.Mvc;
using PortShipTrackingSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class ReportsController : ControllerBase
{
    private readonly AppDbContext _context;

    public ReportsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("ship-summary-inefficient")]
    public async Task<IActionResult> GetShipSummaryInefficient()
    {
        var stopwatch = System.Diagnostics.Stopwatch.StartNew();

        var ships = await _context.Ships.ToListAsync();
        var result = new List<object>();

        foreach (var ship in ships)
        {
            var visitCount = await _context.ShipVisits.CountAsync(v => v.ShipId == ship.ShipId);
            var cargoCount = await _context.Cargoes.CountAsync(c => c.ShipId == ship.ShipId);
            var totalWeight = await _context.Cargoes.Where(c => c.ShipId == ship.ShipId).SumAsync(c => c.WeightTon);
            var crewCount = await _context.ShipCrewAssignments.CountAsync(a => a.ShipId == ship.ShipId);

            result.Add(new
            {
                ship.ShipId,
                ship.Name,
                VisitCount = visitCount,
                CargoCount = cargoCount,
                TotalWeight = totalWeight,
                CrewCount = crewCount
            });
        }

        stopwatch.Stop();

        return Ok(new
        {
            elapsedMilliseconds = stopwatch.ElapsedMilliseconds,
            shipCount = ships.Count,
            estimatedQueryCount = 1 + (ships.Count * 4),
            data = result
        });
    }

    [HttpGet("ship-summary-optimized")]
    public async Task<IActionResult> GetShipSummaryOptimized()
    {
        var stopwatch = System.Diagnostics.Stopwatch.StartNew();

        // 1. Visit sayıları — ShipId'ye göre grupla, tek sorgu
        var visitCounts = await _context.ShipVisits
            .GroupBy(v => v.ShipId)
            .Select(g => new { ShipId = g.Key, Count = g.Count() })
            .ToDictionaryAsync(x => x.ShipId, x => x.Count);

        // 2. Cargo sayısı + toplam ağırlık — ShipId'ye göre grupla, tek sorgu
        var cargoStats = await _context.Cargoes
            .GroupBy(c => c.ShipId)
            .Select(g => new { ShipId = g.Key, Count = g.Count(), TotalWeight = g.Sum(c => c.WeightTon) })
            .ToDictionaryAsync(x => x.ShipId, x => (x.Count, x.TotalWeight));

        // 3. Crew ataması sayısı — ShipId'ye göre grupla, tek sorgu
        var crewCounts = await _context.ShipCrewAssignments
            .GroupBy(a => a.ShipId)
            .Select(g => new { ShipId = g.Key, Count = g.Count() })
            .ToDictionaryAsync(x => x.ShipId, x => x.Count);

        // 4. Ships listesi — sadece ihtiyaç duyulan kolonlar, tracking kapalı
        var ships = await _context.Ships
            .AsNoTracking()
            .Select(s => new { s.ShipId, s.Name })
            .ToListAsync();

        // 5. Bellekte birleştirme (LEFT JOIN mantığı — veri yoksa 0/0m dön)
        var result = ships.Select(s =>
        {
            var (cargoCount, totalWeight) = cargoStats.GetValueOrDefault(s.ShipId, (0, 0m));
            return new
            {
                s.ShipId,
                s.Name,
                VisitCount = visitCounts.GetValueOrDefault(s.ShipId, 0),
                CargoCount = cargoCount,
                TotalWeight = totalWeight,
                CrewCount = crewCounts.GetValueOrDefault(s.ShipId, 0)
            };
        }).ToList();

        stopwatch.Stop();

        return Ok(new
        {
            elapsedMilliseconds = stopwatch.ElapsedMilliseconds,
            shipCount = ships.Count,
            estimatedQueryCount = 4,
            data = result
        });
    }
}