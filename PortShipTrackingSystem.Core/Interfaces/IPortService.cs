namespace PortShipTrackingSystem.Core.Interfaces;

using PortShipTrackingSystem.Core.DTOs;

public interface IPortService
{
    Task<IEnumerable<PortReadDto>> GetAllPortsAsync();
    Task<PortReadDto?> GetPortByIdAsync(int id);
    Task<PortReadDto> CreatePortAsync(CreatePortDto dto);
    Task<bool> UpdatePortAsync(int id, UpdatePortDto dto);
    Task<bool> DeletePortAsync(int id);
}