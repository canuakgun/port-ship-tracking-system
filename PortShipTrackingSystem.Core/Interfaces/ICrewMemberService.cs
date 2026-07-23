namespace PortShipTrackingSystem.Core.Interfaces;

using PortShipTrackingSystem.Core.DTOs;

public interface ICrewMemberService
{
    Task<IEnumerable<CrewMemberReadDto>> GetAllCrewAsync();
    Task<CrewMemberReadDto?> GetCrewByIdAsync(int id);
    Task<CrewMemberReadDto> CreateCrewAsync(CreateCrewMemberDto dto);
    Task<bool> UpdateCrewAsync(int id, UpdateCrewMemberDto dto);
    Task<bool> DeleteCrewAsync(int id);
}