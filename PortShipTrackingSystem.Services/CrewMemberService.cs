namespace PortShipTrackingSystem.Services;

using PortShipTrackingSystem.Core.DTOs;
using PortShipTrackingSystem.Core.Entities;
using PortShipTrackingSystem.Core.Interfaces;
using PortShipTrackingSystem.Core.Exceptions;

public class CrewMemberService : ICrewMemberService
{
    private readonly ICrewMemberRepository _crewMemberRepository;

    public CrewMemberService(ICrewMemberRepository crewMemberRepository)
    {
        _crewMemberRepository = crewMemberRepository;
    }

    public async Task<IEnumerable<CrewMemberReadDto>> GetAllCrewAsync()
    {
        var crewMembers = await _crewMemberRepository.GetAllAsync();

        return crewMembers.Select(cm => new CrewMemberReadDto
        {
            CrewId = cm.CrewId,
            FirstName = cm.FirstName,
            LastName = cm.LastName,
            Email = cm.Email,
            PhoneNumber = cm.PhoneNumber,
            Role = cm.Role
        });
    }

    public async Task<CrewMemberReadDto?> GetCrewByIdAsync(int id)
    {
        var crewMember = await _crewMemberRepository.GetByIdAsync(id);
        if(crewMember == null)
        {
            return null;
        }
        return new CrewMemberReadDto
        {   
            CrewId = crewMember.CrewId,
            FirstName = crewMember.FirstName,
            LastName = crewMember.LastName,
            Email = crewMember.Email,
            PhoneNumber = crewMember.PhoneNumber,
            Role = crewMember.Role
        };
    }

    public async Task<CrewMemberReadDto> CreateCrewAsync(CreateCrewMemberDto dto, string currentUsername)
    {
        if(await _crewMemberRepository.GetByEmailAsync(dto.Email) != null)
        {
            throw new ConflictException("crew member with this email already exists");
        }
        var crewMember = new CrewMember
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            PhoneNumber = dto.PhoneNumber,
            Role = dto.Role,
            CreatedBy = currentUsername
        };

        await _crewMemberRepository.AddAsync(crewMember);
        await _crewMemberRepository.SaveChangesAsync();

        return new CrewMemberReadDto
        {
            CrewId = crewMember.CrewId,
            FirstName = crewMember.FirstName,
            LastName = crewMember.LastName,
            Email = crewMember.Email,
            PhoneNumber = crewMember.PhoneNumber,
            Role = crewMember.Role
        };
    }

    public async Task<bool> UpdateCrewAsync(int id, UpdateCrewMemberDto dto, string currentUsername, bool isAdmin)
    {
        var crewMember= await _crewMemberRepository.GetByIdAsync(id);
        if(crewMember == null)
        {
            return false;
        }
        if (!isAdmin && crewMember.CreatedBy != currentUsername)
        {
            throw new ForbiddenException("You do not have permission to delete this record.");
        }
        bool isEmailChanged = !string.Equals(crewMember.Email, dto.Email, StringComparison.OrdinalIgnoreCase);

        if (isEmailChanged)
        {
            var memberWithNewEmail = await _crewMemberRepository.GetByEmailAsync(dto.Email);

            if (memberWithNewEmail != null && memberWithNewEmail.CrewId != crewMember.CrewId)
            {
                throw new ConflictException("This email is being used by another crew member");
            }
        }
            crewMember.FirstName = dto.FirstName;
            crewMember.LastName = dto.LastName;
            crewMember.Email = dto.Email;
            crewMember.PhoneNumber = dto.PhoneNumber;
            crewMember.Role = dto.Role;

            _crewMemberRepository.Update(crewMember,currentUsername);
            return await _crewMemberRepository.SaveChangesAsync();
    }

    public async Task<bool> DeleteCrewAsync(int id, string currentUsername, bool isAdmin)
    {
        var crewMember = await _crewMemberRepository.GetByIdAsync(id);
        if(crewMember == null)
        {
            return false;
        }
        if (!isAdmin && crewMember.CreatedBy != currentUsername)
        {
            throw new ForbiddenException("You do not have permission to delete this record.");
        }
        _crewMemberRepository.Delete(crewMember,currentUsername);
        return await _crewMemberRepository.SaveChangesAsync();
    }
}