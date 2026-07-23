namespace PortShipTrackingSystem.Core.Interfaces;

using PortShipTrackingSystem.Core.DTOs;

public interface IShipVisitService
{
    Task<IEnumerable<ShipVisitReadDto>> GetAllVisitsAsync();
    Task<ShipVisitReadDto?> GetVisitByIdAsync(int id);
    Task<ShipVisitReadDto> CreateVisitAsync(CreateShipVisitDto dto);
    Task<bool> UpdateVisitAsync(int id, UpdateShipVisitDto dto);
    Task<bool> DeleteVisitAsync(int id);
}