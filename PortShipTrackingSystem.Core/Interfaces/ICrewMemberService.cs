namespace PortShipTrackingSystem.Core.Interfaces;

using PortShipTrackingSystem.Core.DTOs;

public interface ICrewMemberService
{
    Task<IEnumerable<object>> GetAllCrewAsync(bool isAdmin, bool isPortManager);
    Task<object?> GetCrewByIdAsync(int id, bool isAdmin, bool isPortManager);
    Task<CrewMemberReadDto> CreateCrewAsync(CreateCrewMemberDto dto, string currentUsername);
    Task<bool> UpdateCrewAsync(int id, UpdateCrewMemberDto dto, string currentUsername, bool isAdmin);
    Task<bool> DeleteCrewAsync(int id, string currentUsername, bool isAdmin);
}