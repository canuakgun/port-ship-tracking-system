namespace PortShipTrackingSystem.Api.Controllers;

using Microsoft.AspNetCore.Mvc;
using PortShipTrackingSystem.Core.DTOs;
using PortShipTrackingSystem.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ShipsController : ControllerBase
{
    private readonly IShipService _shipService;

    public ShipsController(IShipService shipService)
    {
        _shipService = shipService;
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ShipReadDto>>> GetAllShips([FromQuery] PaginationParams pagination)
    {
        var ships = await _shipService.GetAllShipsAsync(pagination);
        return Ok(ships);
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<ShipReadDto>> GetShipById(int id)
    {
        var ship = await _shipService.GetShipByIdAsync(id);
        return ship is null ? NotFound() : Ok(ship);
    }

    [HttpPost]
[Authorize(Roles = "Admin,PortManager")]
public async Task<ActionResult<ShipReadDto>> CreateShip(CreateShipDto dto)
{
    var username = User.Identity!.Name!;
    var created = await _shipService.CreateShipAsync(dto, username);
    return CreatedAtAction(nameof(GetShipById), new { id = created.ShipId }, created);
}

[HttpPut("{id}")]
[Authorize(Roles = "Admin,PortManager")]
public async Task<IActionResult> UpdateShip(int id, UpdateShipDto dto)
{
    var username = User.Identity!.Name!;
    var isAdmin = User.IsInRole("Admin");
    var updated = await _shipService.UpdateShipAsync(id, dto, username, isAdmin);
    return updated ? NoContent() : NotFound();
}

[HttpDelete("{id}")]
[Authorize(Roles = "Admin")]
public async Task<IActionResult> DeleteShip(int id)
{
    var username = User.Identity!.Name!;
    var isAdmin = User.IsInRole("Admin");
    var deleted = await _shipService.DeleteShipAsync(id, username, isAdmin);
    return deleted ? NoContent() : NotFound();
}
}