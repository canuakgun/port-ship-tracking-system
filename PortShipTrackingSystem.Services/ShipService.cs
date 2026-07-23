namespace PortShipTrackingSystem.Services;

using PortShipTrackingSystem.Core.DTOs;
using PortShipTrackingSystem.Core.Entities;
using PortShipTrackingSystem.Core.Interfaces;
using PortShipTrackingSystem.Core.Exceptions;

public class ShipService : IShipService
{
    private readonly IShipRepository _shipRepository;

    public ShipService(IShipRepository shipRepository)
    {
        _shipRepository = shipRepository;
    }

    public async Task<IEnumerable<ShipReadDto>> GetAllShipsAsync()
    {
        var ships = await _shipRepository.GetAllAsync();
        
        return ships.Select(s => new ShipReadDto
        {
            ShipId = s.ShipId,
            Name = s.Name,
            IMO = s.IMO,
            Type = s.Type,
            Flag = s.Flag,
            YearBuilt = s.YearBuilt
        });
    }

    public async Task<ShipReadDto?> GetShipByIdAsync(int id)
    {
        var ship = await _shipRepository.GetByIdAsync(id);
        if(ship == null)
        {
            return null;
        }
        return new ShipReadDto
        {
            ShipId = ship.ShipId,
            Name = ship.Name,
            IMO = ship.IMO,
            Type = ship.Type,
            Flag = ship.Flag,
            YearBuilt = ship.YearBuilt
        };

    }

    public async Task<ShipReadDto> CreateShipAsync(CreateShipDto dto)
    {
        bool imoExists = await _shipRepository.ImoExistsAsync(dto.IMO);
    if (imoExists)
    {
        throw new ConflictException($"A ship with IMO '{dto.IMO}' already exists.");
    }

    var ship = new Ship
    {
        Name = dto.Name,
        IMO = dto.IMO,
        Type = dto.Type,
        Flag = dto.Flag,
        YearBuilt = dto.YearBuilt
    };

    await _shipRepository.AddAsync(ship);
    await _shipRepository.SaveChangesAsync();

    return new ShipReadDto
    {
        ShipId = ship.ShipId,
        Name = ship.Name,
        IMO = ship.IMO,
        Type = ship.Type,
        Flag = ship.Flag,
        YearBuilt = ship.YearBuilt
    };

    }

    public async Task<bool> UpdateShipAsync(int id, UpdateShipDto dto)
    {
        var ship = await _shipRepository.GetByIdAsync(id);
        if(ship == null)
        {
            return false;
        }
        bool imoExists = await _shipRepository.ImoExistsAsync(dto.IMO, id);
        if (imoExists)
        {
             throw new ConflictException($"A ship with IMO '{dto.IMO}' already exists.");
        }
        ship.Name = dto.Name;
        ship.IMO = dto.IMO;
        ship.Type = dto.Type;
        ship.Flag = dto.Flag;
        ship.YearBuilt = dto.YearBuilt;

        _shipRepository.Update(ship);
        return await _shipRepository.SaveChangesAsync();
    }

    public async Task<bool> DeleteShipAsync(int id)
    {
        var ship = await _shipRepository.GetByIdAsync(id);
        if(ship == null)
        {
            return false;
        }
        _shipRepository.Delete(ship);
        return await _shipRepository.SaveChangesAsync();
    }
}