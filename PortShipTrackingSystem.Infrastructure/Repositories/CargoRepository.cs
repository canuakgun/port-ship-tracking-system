namespace PortShipTrackingSystem.Infrastructure.Repositories;

using Microsoft.EntityFrameworkCore;
using PortShipTrackingSystem.Core.Entities;
using PortShipTrackingSystem.Core.Interfaces;
using PortShipTrackingSystem.Infrastructure.Data;
using PortShipTrackingSystem.Core.DTOs;

public class CargoRepository : GenericRepository<Cargo>, ICargoRepository
{
    private readonly AppDbContext _context;

    public CargoRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Cargo>> GetCargoesByShipIdAsync(int shipId)
    {
        return await _context.Cargoes
            .Include(c => c.Ship)
            .Where(c => c.ShipId == shipId)
            .ToListAsync();
    }
   public async Task<IEnumerable<Cargo>> GetCargoesByShipIdAsync(int shipId, int pageNumber, int pageSize)
    {
        return await _context.Cargoes
            .Include(c => c.Ship)
            .Where(c => c.ShipId == shipId)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }
    public async Task<Cargo?> GetByIdWithDetailsAsync(int id)
    {
        return await _context.Cargoes
            .Include(c => c.Ship)
            .FirstOrDefaultAsync(c => c.CargoId == id);
    }
}