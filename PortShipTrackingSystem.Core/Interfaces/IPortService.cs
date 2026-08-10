namespace PortShipTrackingSystem.Core.Interfaces;

using PortShipTrackingSystem.Core.DTOs;

public interface IPortService
{
    Task<IEnumerable<PortReadDto>> GetAllPortsAsync(PaginationParams pagination);
    Task<PortReadDto?> GetPortByIdAsync(int id);
    Task<PortReadDto> CreatePortAsync(CreatePortDto dto, string username);
    Task<bool> UpdatePortAsync(int id, UpdatePortDto dto, string username, bool isAdmin);
    Task<bool> DeletePortAsync(int id, string username, bool isAdmin);
}