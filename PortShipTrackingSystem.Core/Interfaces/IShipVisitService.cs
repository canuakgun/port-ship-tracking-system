namespace PortShipTrackingSystem.Core.Interfaces;

using PortShipTrackingSystem.Core.DTOs;

public interface IShipVisitService
{
    Task<IEnumerable<ShipVisitReadDto>> GetAllVisitsAsync();
    Task<ShipVisitReadDto?> GetVisitByIdAsync(int id);
    Task<ShipVisitReadDto> CreateVisitAsync(CreateShipVisitDto dto, string currentUsername);
    Task<bool> UpdateVisitAsync(int id, UpdateShipVisitDto dto, string currentUsername, bool isAdmin);
    Task<bool> DeleteVisitAsync(int id, string currentUsername, bool isAdmin);
}