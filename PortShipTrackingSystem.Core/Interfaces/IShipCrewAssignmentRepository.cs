namespace PortShipTrackingSystem.Core.Interfaces;

using PortShipTrackingSystem.Core.Entities;
using PortShipTrackingSystem.Core.DTOs;

public interface IShipCrewAssignmentRepository : IGenericRepository<ShipCrewAssignment>
{
    Task<bool> AssignmentExistsAsync(int shipId, int crewId, DateTime date);
    Task<IEnumerable<ShipCrewAssignment>> GetAssignmentsByShipIdAsync(int shipId);
    Task<IEnumerable<ShipCrewAssignment>> GetAllWithDetailsAsync(PaginationParams pagination);
    Task<ShipCrewAssignment?> GetByIdWithDetailsAsync(int id);
}