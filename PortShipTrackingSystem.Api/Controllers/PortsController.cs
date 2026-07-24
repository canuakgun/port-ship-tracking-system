namespace PortShipTrackingSystem.Api.Controllers;

using Microsoft.AspNetCore.Mvc;
using PortShipTrackingSystem.Core.DTOs;
using PortShipTrackingSystem.Core.Interfaces;

[ApiController]
[Route("api/[controller]")]
public class PortsController : ControllerBase
{
    private readonly IPortService _portService;

    public PortsController(IPortService portService)
    {
        _portService = portService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PortReadDto>>> GetAllPorts()
    {
        var ports = await _portService.GetAllPortsAsync();
        return Ok(ports);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PortReadDto>> GetPortById(int id)
    {
        var port = await _portService.GetPortByIdAsync(id);
        return port is null ? NotFound() : Ok(port);
    }

    [HttpPost]
    public async Task<ActionResult<PortReadDto>> CreatePort(CreatePortDto dto)
    {
        var created = await _portService.CreatePortAsync(dto);
        return CreatedAtAction(nameof(GetPortById), new { id = created.PortId }, created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePort(int id, UpdatePortDto dto)
    {
        var updated = await _portService.UpdatePortAsync(id, dto);
        return updated ? NoContent() : NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePort(int id)
    {
        var deleted = await _portService.DeletePortAsync(id);
        return deleted ? NoContent() : NotFound();
    }
}