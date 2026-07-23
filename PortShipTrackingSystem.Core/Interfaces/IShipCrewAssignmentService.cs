namespace PortShipTrackingSystem.Core.Interfaces;

using PortShipTrackingSystem.Core.DTOs;

public interface IShipCrewAssignmentService
{
    Task<IEnumerable<ShipCrewAssignmentReadDto>> GetAllAssignmentsAsync();
    Task<ShipCrewAssignmentReadDto?> GetAssignmentByIdAsync(int id);
    Task<ShipCrewAssignmentReadDto> CreateAssignmentAsync(CreateShipCrewAssignmentDto dto);
    Task<bool> DeleteAssignmentAsync(int id);
}