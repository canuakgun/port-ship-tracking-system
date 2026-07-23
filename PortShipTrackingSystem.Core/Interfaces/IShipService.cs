namespace PortShipTrackingSystem.Core.Interfaces;

using PortShipTrackingSystem.Core.DTOs;

public interface IShipService
{
    Task<IEnumerable<ShipReadDto>> GetAllShipsAsync();
    Task<ShipReadDto?> GetShipByIdAsync(int id);
    Task<ShipReadDto> CreateShipAsync(CreateShipDto dto);
    Task<bool> UpdateShipAsync(int id, UpdateShipDto dto);
    Task<bool> DeleteShipAsync(int id);
}