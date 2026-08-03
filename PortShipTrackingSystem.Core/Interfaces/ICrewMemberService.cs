namespace PortShipTrackingSystem.Core.Interfaces;

using PortShipTrackingSystem.Core.DTOs;

public interface ICrewMemberService
{
    Task<IEnumerable<CrewMemberReadDto>> GetAllCrewAsync();
    Task<CrewMemberReadDto?> GetCrewByIdAsync(int id);
    Task<CrewMemberReadDto> CreateCrewAsync(CreateCrewMemberDto dto, string currentUsername);
    Task<bool> UpdateCrewAsync(int id, UpdateCrewMemberDto dto, string currentUsername, bool isAdmin);
    Task<bool> DeleteCrewAsync(int id, string currentUsername, bool isAdmin);
}