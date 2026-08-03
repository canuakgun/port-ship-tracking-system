namespace PortShipTrackingSystem.Services;

using PortShipTrackingSystem.Core.DTOs;
using PortShipTrackingSystem.Core.Entities;
using PortShipTrackingSystem.Core.Interfaces;
using PortShipTrackingSystem.Core.Exceptions;

public class ShipCrewAssignmentService : IShipCrewAssignmentService
{
    private readonly IShipCrewAssignmentRepository _shipCrewAssignmentRepository;

    public ShipCrewAssignmentService(IShipCrewAssignmentRepository shipCrewAssignmentRepository)
    {
        _shipCrewAssignmentRepository = shipCrewAssignmentRepository;
    }

    public async Task<IEnumerable<ShipCrewAssignmentReadDto>> GetAllAssignmentsAsync()
    {
        var shipCrewAssignments = await _shipCrewAssignmentRepository.GetAllWithDetailsAsync();
        
        return shipCrewAssignments.Select(sca => new ShipCrewAssignmentReadDto
        {
            AssignmentId = sca.AssignmentId,
            ShipId = sca.ShipId,
            ShipName = sca.Ship.Name,
            CrewId = sca.CrewId,
            CrewFullName = $"{sca.Crew.FirstName} {sca.Crew.LastName}",
            AssignmentDate = sca.AssignmentDate
        });
    }

    public async Task<ShipCrewAssignmentReadDto?> GetAssignmentByIdAsync(int id)
    {
        var shipCrewAssignment = await _shipCrewAssignmentRepository.GetByIdWithDetailsAsync(id);
        if(shipCrewAssignment == null)
        {
            return null;
        }
        return new ShipCrewAssignmentReadDto
        {
          AssignmentId = shipCrewAssignment.AssignmentId,
          ShipId = shipCrewAssignment.ShipId,
          ShipName = shipCrewAssignment.Ship.Name,
          CrewId = shipCrewAssignment.CrewId,
          CrewFullName = $"{shipCrewAssignment.Crew.FirstName} {shipCrewAssignment.Crew.LastName}",
          AssignmentDate = shipCrewAssignment.AssignmentDate
        };
    }

    public async Task<ShipCrewAssignmentReadDto> CreateAssignmentAsync(CreateShipCrewAssignmentDto dto, string currentUsername)
    {
        if(await _shipCrewAssignmentRepository.AssignmentExistsAsync(dto.ShipId, dto.CrewId, dto.AssignmentDate))
        {
            throw new ConflictException($"Crew member {dto.CrewId} is already assigned to ship {dto.ShipId} on {dto.AssignmentDate:yyyy-MM-dd}.");
        }
        var shipCrewAssign = new ShipCrewAssignment
        {
          ShipId = dto.ShipId,
          CrewId = dto.CrewId,
          AssignmentDate = dto.AssignmentDate,
          CreatedBy = currentUsername
        };

        await _shipCrewAssignmentRepository.AddAsync(shipCrewAssign);
        await _shipCrewAssignmentRepository.SaveChangesAsync();

        var createdShipCrewAssignment = await _shipCrewAssignmentRepository.GetByIdWithDetailsAsync(shipCrewAssign.AssignmentId);

        return new ShipCrewAssignmentReadDto
        {
            AssignmentId = createdShipCrewAssignment!.AssignmentId,
            ShipId = createdShipCrewAssignment.ShipId,
            ShipName = createdShipCrewAssignment.Ship.Name,
            CrewId = createdShipCrewAssignment.CrewId,
            CrewFullName =  $"{createdShipCrewAssignment.Crew.FirstName} {createdShipCrewAssignment.Crew.LastName}",
            AssignmentDate = createdShipCrewAssignment.AssignmentDate
        };
    }

    public async Task<bool> DeleteAssignmentAsync(int id, string currentUsername, bool isAdmin)
    {
        var shipCrewAssignment = await _shipCrewAssignmentRepository.GetByIdAsync(id);
        if(shipCrewAssignment == null)
        {
            return false;
        }
        if (!isAdmin && shipCrewAssignment.CreatedBy != currentUsername)
        {
            throw new ForbiddenException("You do not have permission to delete this record.");
        }
        _shipCrewAssignmentRepository.Delete(shipCrewAssignment, currentUsername);
        return await _shipCrewAssignmentRepository.SaveChangesAsync();
    }
}