namespace PortShipTrackingSystem.Infrastructure.Repositories;

using Microsoft.EntityFrameworkCore;
using PortShipTrackingSystem.Core.Entities;
using PortShipTrackingSystem.Core.Interfaces;
using PortShipTrackingSystem.Infrastructure.Data;

public class ShipVisitRepository : GenericRepository<ShipVisit>, IShipVisitRepository
{
    private readonly AppDbContext _context;

    public ShipVisitRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }


    public async Task<IEnumerable<ShipVisit>> GetVisitsByShipIdAsync(int shipId)
    {
        return await _context.ShipVisits.Where(v => v.ShipId == shipId).ToListAsync();
    }
    public async Task<IEnumerable<ShipVisit>> GetVisitsByPortIdAsync(int portId)
    {
        return await _context.ShipVisits.Where(v => v.PortId == portId).ToListAsync();
    }
}