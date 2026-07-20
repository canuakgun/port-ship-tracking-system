namespace PortShipTrackingSystem.Infrastructure.Repositories;

using Microsoft.EntityFrameworkCore;
using PortShipTrackingSystem.Core.Entities;
using PortShipTrackingSystem.Core.Interfaces;
using PortShipTrackingSystem.Infrastructure.Data;

public class CargoRepository : GenericRepository<Cargo>, ICargoRepository
{
    private readonly AppDbContext _context;

    public CargoRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Cargo>> GetCargoesByShipIdAsync(int shipId)
    {
        return await _context.Cargoes.Where(c => c.ShipId == shipId).ToListAsync();
    }
}