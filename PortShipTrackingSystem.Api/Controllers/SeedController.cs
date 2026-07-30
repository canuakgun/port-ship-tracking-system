namespace PortShipTrackingSystem.Api.Controllers;

using Microsoft.AspNetCore.Mvc;
using Bogus;
using PortShipTrackingSystem.Core.Entities;
using PortShipTrackingSystem.Infrastructure.Data;

[ApiController]
[Route("api/[controller]")]
public class SeedController : ControllerBase
{
    private readonly AppDbContext _context;

    public SeedController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost("generate")]
public async Task<IActionResult> GenerateTestData()
{

    _context.ShipCrewAssignments.RemoveRange(_context.ShipCrewAssignments);
    _context.Cargoes.RemoveRange(_context.Cargoes);
    _context.ShipVisits.RemoveRange(_context.ShipVisits);
    _context.CrewMembers.RemoveRange(_context.CrewMembers);
    _context.Ships.RemoveRange(_context.Ships);
    _context.Ports.RemoveRange(_context.Ports);
    await _context.SaveChangesAsync();

    // --- SHIPS ---
    var shipTypes = new[] { "Tanker", "Container Ship", "Bulk Carrier", "Cargo Ship", "Ro-Ro" };
    var flags = new[] { "Turkey", "Panama", "Liberia", "Marshall Islands", "Malta", "Greece" };

    var usedImos = new HashSet<string>();
    var shipFaker = new Faker<Ship>()
        .RuleFor(s => s.Name, f => $"{f.Company.CompanyName()} {f.Random.Int(1, 999)}")
        .RuleFor(s => s.IMO, f =>
        {
            string imo;
            do
            {
                imo = f.Random.Int(1000000, 9999999).ToString();
            } while (!usedImos.Add(imo));
            return imo;
        })
        .RuleFor(s => s.Type, f => f.PickRandom(shipTypes))
        .RuleFor(s => s.Flag, f => f.PickRandom(flags))
        .RuleFor(s => s.YearBuilt, f => f.Random.Int(1980, 2024));

    var ships = shipFaker.Generate(1000);
    await _context.Ships.AddRangeAsync(ships);

    // --- PORTS ---
    var portFaker = new Faker<Port>()
        .RuleFor(p => p.Name, f => $"{f.Address.City()} Port")
        .RuleFor(p => p.Country, f => f.Address.Country())
        .RuleFor(p => p.City, f => f.Address.City());

    var ports = portFaker.Generate(100);
    await _context.Ports.AddRangeAsync(ports);

    await _context.SaveChangesAsync();

    // --- CREW MEMBERS ---
    var roles = new[] { "Captain", "First Officer", "Engineer", "Deckhand", "Cook", "Navigator" };

    var crewFaker = new Faker<CrewMember>()
        .RuleFor(c => c.FirstName, f => f.Name.FirstName())
        .RuleFor(c => c.LastName, f => f.Name.LastName())
        .RuleFor(c => c.Email, (f, c) => f.Internet.Email(c.FirstName, c.LastName))
        .RuleFor(c => c.PhoneNumber, f => f.Phone.PhoneNumber("+90 5## ### ## ##"))
        .RuleFor(c => c.Role, f => f.PickRandom(roles));

    var crewMembers = crewFaker.Generate(5000);
    await _context.CrewMembers.AddRangeAsync(crewMembers);

    await _context.SaveChangesAsync();

    var shipIds = ships.Select(s => s.ShipId).ToList();
    var portIds = ports.Select(p => p.PortId).ToList();
    var crewIds = crewMembers.Select(c => c.CrewId).ToList();

    var purposes = new[] { "Loading", "Unloading", "Refueling", "Maintenance", "Inspection" };

    var visitFaker = new Faker<ShipVisit>()
        .RuleFor(v => v.ShipId, f => f.PickRandom(shipIds))
        .RuleFor(v => v.PortId, f => f.PickRandom(portIds))
        .RuleFor(v => v.ArrivalDate, f => f.Date.Past(2))
        .RuleFor(v => v.Purpose, f => f.PickRandom(purposes))
        .RuleFor(v => v.DepartureDate, (f, v) => v.ArrivalDate.AddDays(f.Random.Int(1, 14)));

    var visits = visitFaker.Generate(5000);
    await _context.ShipVisits.AddRangeAsync(visits);

    // --- CARGOES ---
    var cargoTypes = new[] { "Containers", "Grain", "Oil", "Coal", "Machinery", "Vehicles" };

    var cargoFaker = new Faker<Cargo>()
    .RuleFor(c => c.ShipId, f => f.PickRandom(shipIds))
    .RuleFor(c => c.Description, f => f.Commerce.ProductName())
    .RuleFor(c => c.WeightTon, f => f.Random.Decimal(1, 50000))
    .RuleFor(c => c.CargoType, f => f.PickRandom(cargoTypes));

    var cargoes = cargoFaker.Generate(10000);
    await _context.Cargoes.AddRangeAsync(cargoes);

    await _context.SaveChangesAsync();

       // --- SHIP CREW ASSIGNMENTS ---
var usedCombinations = new HashSet<(int ShipId, int CrewId, DateTime Date)>();

var assignmentFaker = new Faker<ShipCrewAssignment>()
    .CustomInstantiator(f =>
    {
        int shipId, crewId;
        DateTime date;
        do
        {
            shipId = f.PickRandom(shipIds);
            crewId = f.PickRandom(crewIds);
            date = f.Date.Past(2);
        } while (!usedCombinations.Add((shipId, crewId, date)));

        return new ShipCrewAssignment
        {
            ShipId = shipId,
            CrewId = crewId,
            AssignmentDate = date
        };
    });

var assignments = assignmentFaker.Generate(15000);
await _context.ShipCrewAssignments.AddRangeAsync(assignments);

await _context.SaveChangesAsync();

return Ok(new
{
    ships = ships.Count,
    ports = ports.Count,
    crewMembers = crewMembers.Count,
    visits = visits.Count,
    cargoes = cargoes.Count,
    assignments = assignments.Count
});
}
}