namespace PortShipTrackingSystem.Core.Interfaces;

using PortShipTrackingSystem.Core.DTOs;

public interface ICargoService
{
    Task<IEnumerable<CargoReadDto>> GetCargoesByShipIdAsync(int shipId);
    Task<CargoReadDto?> GetCargoByIdAsync(int id);
    Task<CargoReadDto> CreateCargoAsync(CreateCargoDto dto);
    Task<bool> UpdateCargoAsync(int id, UpdateCargoDto dto);
    Task<bool> DeleteCargoAsync(int id);
}