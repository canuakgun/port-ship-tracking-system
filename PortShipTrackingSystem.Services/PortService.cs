namespace PortShipTrackingSystem.Services;

using PortShipTrackingSystem.Core.DTOs;
using PortShipTrackingSystem.Core.Entities;
using PortShipTrackingSystem.Core.Interfaces;

public class PortService : IPortService
{
    private readonly IPortRepository _portRepository;

    public PortService(IPortRepository portRepository)
    {
        _portRepository = portRepository;
    }

    public async Task<IEnumerable<PortReadDto>> GetAllPortsAsync()
    {
        var ports = await _portRepository.GetAllAsync();

        return ports.Select(p => new PortReadDto
        {
            PortId = p.PortId,
            Name = p.Name,
            Country = p.Country,
            City = p.City
        });
    }

    public async Task<PortReadDto?> GetPortByIdAsync(int id)
    {
        var port = await _portRepository.GetByIdAsync(id);
        if(port == null)
        {
            return null;
        }
        return new PortReadDto
        {
            PortId = port.PortId,
            Name = port.Name,
            Country = port.Country,
            City = port.City
        };
    }

    public async Task<PortReadDto> CreatePortAsync(CreatePortDto dto)
    {
        var port = new Port
        {
            Name = dto.Name,
            Country = dto.Country,
            City = dto.City
        };
        await _portRepository.AddAsync(port);
        await _portRepository.SaveChangesAsync();

        return new PortReadDto
        {
            PortId = port.PortId,
            Name = port.Name,
            Country = port.Country,
            City = port.City  
        };
    }

    public async Task<bool> UpdatePortAsync(int id, UpdatePortDto dto)
    { 
        var port = await _portRepository.GetByIdAsync(id);
        if(port == null)
        {
            return false;
        }
        port.Name = dto.Name;
        port.Country = dto.Country;
        port.City = dto.City;

        _portRepository.Update(port);
        return await _portRepository.SaveChangesAsync();
    }

    public async Task<bool> DeletePortAsync(int id)
    {
        var port = await _portRepository.GetByIdAsync(id);
        if(port == null)
        {
            return false;
        }
        _portRepository.Delete(port);
        return await _portRepository.SaveChangesAsync();
    }
}