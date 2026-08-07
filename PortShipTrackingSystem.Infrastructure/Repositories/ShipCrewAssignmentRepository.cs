namespace PortShipTrackingSystem.Infrastructure.Repositories;

using Microsoft.EntityFrameworkCore;
using PortShipTrackingSystem.Core.Entities;
using PortShipTrackingSystem.Core.Interfaces;
using PortShipTrackingSystem.Infrastructure.Data;
using PortShipTrackingSystem.Core.DTOs;

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
    public async Task<IEnumerable<ShipCrewAssignment>> GetAllWithDetailsAsync(PaginationParams pagination)
{
    return await _context.ShipCrewAssignments
        .Include(a => a.Ship)
        .Include(a => a.Crew)
        .Skip((pagination.PageNumber - 1) * pagination.PageSize)
        .Take(pagination.PageSize)
        .ToListAsync();
}


    public async Task<ShipCrewAssignment?> GetByIdWithDetailsAsync(int id)
    {
        return await _context.ShipCrewAssignments.Include(a => a.Ship).Include(a => a.Crew).FirstOrDefaultAsync(a => a.AssignmentId == id);
    }
}