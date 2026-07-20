namespace PortShipTrackingSystem.Core.Interfaces;

using PortShipTrackingSystem.Core.Entities;

    public interface IShipRepository : IGenericRepository<Ship>
    {
        Task<Ship?> GetByImoAsync(string imo);
        Task<bool> ImoExistsAsync(string imo, int? excludeShipId = null);
    }
