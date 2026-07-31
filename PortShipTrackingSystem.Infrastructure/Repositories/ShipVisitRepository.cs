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
    public async Task<IEnumerable<ShipVisit>> GetAllWithDetailsAsync()
{
    return await _context.ShipVisits
        .Include(v => v.Ship)
        .Include(v => v.Port)
        .ToListAsync();
}

    public async Task<ShipVisit?> GetByIdWithDetailsAsync(int id)
    {
        return await _context.ShipVisits.Include(v => v.Ship).Include(v => v.Port).FirstOrDefaultAsync(v => v.VisitId == id);
    }
    public async Task<bool> UpdateWithConcurrencyAsync(ShipVisit visit, byte[] originalRowVersion)
{
    Update(visit);
    _context.Entry(visit).Property(v => v.RowVersion).OriginalValue = originalRowVersion;
    return await SaveChangesAsync();
}
}