namespace PortShipTrackingSystem.Core.Interfaces;

using PortShipTrackingSystem.Core.Entities;

public interface ICargoRepository : IGenericRepository<Cargo>
{
    Task<IEnumerable<Cargo>> GetCargoesByShipIdAsync(int shipId);
    Task<Cargo?> GetByIdWithDetailsAsync(int id);
}