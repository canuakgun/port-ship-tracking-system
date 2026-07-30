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
}