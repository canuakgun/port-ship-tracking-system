namespace PortShipTrackingSystem.Core.Interfaces;

using PortShipTrackingSystem.Core.DTOs;

public interface ICargoService
{
    Task<IEnumerable<CargoReadDto>> GetCargoesByShipIdAsync(int shipId);
    Task<CargoReadDto?> GetCargoByIdAsync(int id);
    Task<CargoReadDto> CreateCargoAsync(CreateCargoDto dto, string currentUsername);
    Task<bool> UpdateCargoAsync(int id, UpdateCargoDto dto, string currentUsername, bool isAdmin);
    Task<bool> DeleteCargoAsync(int id, string currentUsername, bool isAdmin);
}