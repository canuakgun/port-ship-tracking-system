    namespace PortShipTrackingSystem.Infrastructure.Repositories;

    using Microsoft.EntityFrameworkCore;
    using PortShipTrackingSystem.Core.Entities;
    using PortShipTrackingSystem.Core.Interfaces;
    using PortShipTrackingSystem.Infrastructure.Data;

    public class ShipRepository : GenericRepository<Ship>, IShipRepository
    {
        private readonly AppDbContext _context;

        public ShipRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<Ship?> GetByImoAsync(string imo)
        {
            return await _context.Ships.FirstOrDefaultAsync(s=> s.IMO == imo);
        }
        public async Task<bool> ImoExistsAsync(string imo, int? excludeShipId = null)
        {
            bool exists;
            if(excludeShipId != null)
            {
                exists = await _context.Ships.AnyAsync(s => s.ShipId !=excludeShipId && s.IMO == imo);
                return exists;
            }
            exists = await _context.Ships.AnyAsync(s => s.IMO == imo);
            return exists;
        }
    }