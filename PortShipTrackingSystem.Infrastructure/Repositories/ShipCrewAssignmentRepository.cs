namespace PortShipTrackingSystem.Infrastructure.Repositories;

using Microsoft.EntityFrameworkCore;
using PortShipTrackingSystem.Core.Entities;
using PortShipTrackingSystem.Core.Interfaces;
using PortShipTrackingSystem.Infrastructure.Data;

public class ShipCrewAssignmentRepository : GenericRepository<ShipCrewAssignment>, IShipCrewAssignmentRepository
{
    private readonly AppDbContext _context;

    public ShipCrewAssignmentRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<bool> AssignmentExistsAsync(int shipId, int crewId, DateTime date)
    {

        return await _context.ShipCrewAssignments.AnyAsync(sca => sca.ShipId == shipId && sca.CrewId == crewId && sca.AssignmentDate == date);
    }

    public async Task<IEnumerable<ShipCrewAssignment>> GetAssignmentsByShipIdAsync(int shipId)
    {
        return await _context.ShipCrewAssignments.Where(sca => sca.ShipId == shipId).ToListAsync();
    }
}