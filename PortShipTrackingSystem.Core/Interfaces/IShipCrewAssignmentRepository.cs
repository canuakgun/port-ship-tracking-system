namespace PortShipTrackingSystem.Core.Interfaces;

using PortShipTrackingSystem.Core.Entities;

public interface IShipCrewAssignmentRepository : IGenericRepository<ShipCrewAssignment>
{
    Task<bool> AssignmentExistsAsync(int shipId, int crewId, DateTime date);
    Task<IEnumerable<ShipCrewAssignment>> GetAssignmentsByShipIdAsync(int shipId);
}