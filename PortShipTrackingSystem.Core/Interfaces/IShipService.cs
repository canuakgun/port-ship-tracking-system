namespace PortShipTrackingSystem.Core.Interfaces;

using PortShipTrackingSystem.Core.DTOs;

public interface IShipService
{
    Task<IEnumerable<ShipReadDto>> GetAllShipsAsync();
    Task<ShipReadDto?> GetShipByIdAsync(int id);
    Task<ShipReadDto> CreateShipAsync(CreateShipDto dto, string currentUsername);
    Task<bool> UpdateShipAsync(int id, UpdateShipDto dto, string currentUsername, bool isAdmin);
    Task<bool> DeleteShipAsync(int id, string currentUsername, bool isAdmin);
}