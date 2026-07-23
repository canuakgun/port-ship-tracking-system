namespace PortShipTrackingSystem.Services;

using PortShipTrackingSystem.Core.DTOs;
using PortShipTrackingSystem.Core.Entities;
using PortShipTrackingSystem.Core.Interfaces;
using PortShipTrackingSystem.Core.Exceptions;

public class CargoService : ICargoService
{
    private readonly ICargoRepository _cargoRepository;

    public CargoService(ICargoRepository cargoRepository)
    {
        _cargoRepository = cargoRepository;
    }

    public async Task<IEnumerable<CargoReadDto>> GetCargoesByShipIdAsync(int shipId)
    {
        var cargoes = await _cargoRepository.GetCargoesByShipIdAsync(shipId);
        return cargoes.Select(c => new CargoReadDto
        {
            CargoId = c.CargoId,
            ShipId = c.ShipId,
            Description = c.Description,
            WeightTon = c.WeightTon,
            CargoType = c.CargoType,
            ShipName = c.Ship.Name
        });
    }

    public async Task<CargoReadDto?> GetCargoByIdAsync(int id)
    {   
        var cargo = await _cargoRepository.GetByIdWithDetailsAsync(id);
        if(cargo == null)
        {
            return null;
        }
        return new CargoReadDto
        {
            CargoId = cargo.CargoId,
            ShipId = cargo.ShipId,
            Description = cargo.Description,
            WeightTon = cargo.WeightTon,
            CargoType = cargo.CargoType,
            ShipName = cargo.Ship.Name
        };
    }

    public async Task<CargoReadDto> CreateCargoAsync(CreateCargoDto dto)
    {
        if(dto.WeightTon <= 0)
        {
            throw new ValidationException("WeightTon must be bigger than 0.");
        }
        var cargo = new Cargo
        {
            ShipId = dto.ShipId,
            Description = dto.Description,
            WeightTon = dto.WeightTon,
            CargoType = dto.CargoType,
        };

        await _cargoRepository.AddAsync(cargo);
        await _cargoRepository.SaveChangesAsync();

        var createdCargo = await _cargoRepository.GetByIdWithDetailsAsync(cargo.CargoId);
        return new CargoReadDto
        {
            CargoId = createdCargo!.CargoId,
            ShipId = createdCargo.ShipId,
            Description = createdCargo.Description,
            WeightTon = createdCargo.WeightTon,
            CargoType = createdCargo.CargoType,
            ShipName = createdCargo.Ship.Name  
        };
    }

    public async Task<bool> UpdateCargoAsync(int id, UpdateCargoDto dto)
    {
        if(dto.WeightTon <= 0)
        {
            throw new ValidationException("WeightTon must be bigger than 0.");
        }
        var cargo = await _cargoRepository.GetByIdAsync(id);
        if(cargo == null)
        {
            return false;
        }

        cargo.ShipId = dto.ShipId;
        cargo.Description = dto.Description;
        cargo.WeightTon = dto.WeightTon;
        cargo.CargoType = dto.CargoType;

        _cargoRepository.Update(cargo);
        return await _cargoRepository.SaveChangesAsync();
    }

    public async Task<bool> DeleteCargoAsync(int id)
    {
        var cargo = await _cargoRepository.GetByIdAsync(id);
        if(cargo == null)
        {
            return false;
        }
        _cargoRepository.Delete(cargo);
        return await _cargoRepository.SaveChangesAsync();
    }
}