namespace PortShipTrackingSystem.Core.Interfaces;

using PortShipTrackingSystem.Core.DTOs;
using PortShipTrackingSystem.Core.Entities;

public interface IShipVisitRepository : IGenericRepository<ShipVisit>
{
    Task<IEnumerable<ShipVisit>> GetVisitsByShipIdAsync(int shipId);
    Task<IEnumerable<ShipVisit>> GetVisitsByPortIdAsync(int portId);
    Task<IEnumerable<ShipVisit>> GetAllWithDetailsAsync(PaginationParams pagination);
    Task<ShipVisit?> GetByIdWithDetailsAsync(int id);
    Task<bool> UpdateWithConcurrencyAsync(ShipVisit visit, byte[] originalRowVersion, string currentUsername);
}