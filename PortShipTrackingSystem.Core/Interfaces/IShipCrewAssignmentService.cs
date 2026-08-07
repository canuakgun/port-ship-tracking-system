namespace PortShipTrackingSystem.Core.Interfaces;

using PortShipTrackingSystem.Core.DTOs;

public interface IShipCrewAssignmentService
{
    Task<IEnumerable<ShipCrewAssignmentReadDto>> GetAllAssignmentsAsync(PaginationParams pagination);
    Task<ShipCrewAssignmentReadDto?> GetAssignmentByIdAsync(int id);
    Task<ShipCrewAssignmentReadDto> CreateAssignmentAsync(CreateShipCrewAssignmentDto dto, string currentUsername);
    Task<bool> DeleteAssignmentAsync(int id, string currentUsername, bool isAdmin);
}