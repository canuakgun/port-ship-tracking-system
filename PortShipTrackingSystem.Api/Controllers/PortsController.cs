namespace PortShipTrackingSystem.Api.Controllers;

using Microsoft.AspNetCore.Mvc;
using PortShipTrackingSystem.Core.DTOs;
using PortShipTrackingSystem.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;

[ApiController]
[Route("api/[controller]")]
[Authorize]
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
    [Authorize(Roles = "Admin,PortManager")]
    public async Task<ActionResult<PortReadDto>> CreatePort(CreatePortDto dto)
    {
        var username = User.Identity!.Name!;
        var created = await _portService.CreatePortAsync(dto, username);
        return CreatedAtAction(nameof(GetPortById), new { id = created.PortId }, created);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin,PortManager")]
    public async Task<IActionResult> UpdatePort(int id, UpdatePortDto dto)
    {   
        var username = User.Identity!.Name!;
        var isAdmin = User.IsInRole("Admin");
        var updated = await _portService.UpdatePortAsync(id, dto, username, isAdmin);
        return updated ? NoContent() : NotFound();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeletePort(int id)
    {   
        var username = User.Identity!.Name!;
        var isAdmin = User.IsInRole("Admin");
        var deleted = await _portService.DeletePortAsync(id, username, isAdmin);
        return deleted ? NoContent() : NotFound();
    }
}