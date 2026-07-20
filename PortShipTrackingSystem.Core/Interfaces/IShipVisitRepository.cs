namespace PortShipTrackingSystem.Core.Interfaces;

using PortShipTrackingSystem.Core.Entities;

public interface IShipVisitRepository : IGenericRepository<ShipVisit>
{
    Task<IEnumerable<ShipVisit>> GetVisitsByShipIdAsync(int shipId);
    Task<IEnumerable<ShipVisit>> GetVisitsByPortIdAsync(int portId);
}