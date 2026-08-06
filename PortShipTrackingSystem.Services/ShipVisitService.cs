namespace PortShipTrackingSystem.Services;

using PortShipTrackingSystem.Core.DTOs;
using PortShipTrackingSystem.Core.Entities;
using PortShipTrackingSystem.Core.Interfaces;
using PortShipTrackingSystem.Core.Exceptions;
using PortShipTrackingSystem.Core.Validation;


public class ShipVisitService : IShipVisitService
{
    private readonly IShipVisitRepository _shipVisitRepository;

    public ShipVisitService(IShipVisitRepository shipVisitRepository)
    {
        _shipVisitRepository = shipVisitRepository;
    }

    public async Task<IEnumerable<ShipVisitReadDto>> GetAllVisitsAsync()
    {   
        var visits = await _shipVisitRepository.GetAllWithDetailsAsync();

        return visits.Select(v => new ShipVisitReadDto
    {
        VisitId = v.VisitId,
        ShipId = v.ShipId,
        ShipName = v.Ship.Name,
        PortId = v.PortId,
        PortName = v.Port.Name,
        ArrivalDate = v.ArrivalDate,
        DepartureDate = v.DepartureDate,
        Purpose = v.Purpose,
        rowVersion = v.RowVersion
    });
    }

    public async Task<ShipVisitReadDto?> GetVisitByIdAsync(int id)
    {
        var visit = await _shipVisitRepository.GetByIdWithDetailsAsync(id);
        if(visit == null)
        {
            return null;
        }
        return new ShipVisitReadDto
        {
            VisitId = visit.VisitId,
            ShipId = visit.ShipId,
            ShipName = visit.Ship.Name,
            PortId = visit.PortId,
            PortName = visit.Port.Name,
            ArrivalDate = visit.ArrivalDate,
            DepartureDate = visit.DepartureDate,
            Purpose = visit.Purpose,
            rowVersion = visit.RowVersion
        };
    }

    public async Task<ShipVisitReadDto> CreateVisitAsync(CreateShipVisitDto dto, string currentUsername)
    {
        if (InputGuard.ContainsHtmlTags(dto.Purpose))
    {
        throw new ValidationException("Purpose cannot contain HTML tags.");
    }
        if (dto.ArrivalDate >= dto.DepartureDate)
        {
            throw new ValidationException("Arrival date must be before departure date.");
        }
        var visit = new ShipVisit
        {
            ShipId = dto.ShipId,
            PortId = dto.PortId,
            ArrivalDate = dto.ArrivalDate,
            DepartureDate = dto.DepartureDate,
            Purpose = dto.Purpose,
            CreatedBy = currentUsername
        };

        await _shipVisitRepository.AddAsync(visit);
        await _shipVisitRepository.SaveChangesAsync();

        var createdVisit = await _shipVisitRepository.GetByIdWithDetailsAsync(visit.VisitId);
        return new ShipVisitReadDto
        {
            VisitId = createdVisit!.VisitId,
            ShipId = createdVisit.ShipId,
            ShipName = createdVisit.Ship.Name,
            PortId = createdVisit.PortId,
            PortName = createdVisit.Port.Name,
            ArrivalDate = createdVisit.ArrivalDate,
            DepartureDate = createdVisit.DepartureDate,
            Purpose = createdVisit.Purpose,
            rowVersion = createdVisit.RowVersion
        };
    }

public async Task<bool> UpdateVisitAsync(int id, UpdateShipVisitDto dto, string currentUsername, bool isAdmin)
{
    if (dto.ArrivalDate >= dto.DepartureDate)
    {
        throw new ValidationException("Arrival date must be before departure date.");
    }
    var visit = await _shipVisitRepository.GetByIdWithDetailsAsync(id);
    if (visit == null)
    {
        return false;
    }
    if (!isAdmin && visit.CreatedBy != currentUsername)
    {
        throw new ForbiddenException("You do not have permission to delete this record.");
    }
    visit.ShipId = dto.ShipId;
    visit.PortId = dto.PortId;
    visit.ArrivalDate = dto.ArrivalDate;
    visit.DepartureDate = dto.DepartureDate;
    visit.Purpose = dto.Purpose;

    return await _shipVisitRepository.UpdateWithConcurrencyAsync(visit, dto.RowVersion, currentUsername); 
}

    public async Task<bool> DeleteVisitAsync(int id, string currentUsername, bool isAdmin)
    {   
        var visit = await _shipVisitRepository.GetByIdAsync(id);
        if(visit == null)
        {
            return false;
        }
        if (!isAdmin && visit.CreatedBy != currentUsername)
    {
        throw new ForbiddenException("You do not have permission to delete this record.");
    }
        _shipVisitRepository.Delete(visit, currentUsername);
        return await _shipVisitRepository.SaveChangesAsync();
    }
}