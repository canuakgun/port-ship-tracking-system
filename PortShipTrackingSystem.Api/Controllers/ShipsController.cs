namespace PortShipTrackingSystem.Api.Controllers;

using Microsoft.AspNetCore.Mvc;
using PortShipTrackingSystem.Core.DTOs;
using PortShipTrackingSystem.Core.Interfaces;

[ApiController]
[Route("api/[controller]")]
public class ShipsController : ControllerBase
{
    private readonly IShipService _shipService;

    public ShipsController(IShipService shipService)
    {
        _shipService = shipService;
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ShipReadDto>>> GetAllShips()
    {
        var ships = await _shipService.GetAllShipsAsync();
        return Ok(ships);
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<ShipReadDto>> GetShipById(int id)
    {
        var ship = await _shipService.GetShipByIdAsync(id);
        return ship is null ? NotFound() : Ok(ship);
    }

    [HttpPost]
    public async Task<ActionResult<ShipReadDto>> CreateShip(CreateShipDto dto)
    {
        var created = await _shipService.CreateShipAsync(dto);
        return CreatedAtAction(nameof(GetShipById), new { id = created.ShipId }, created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateShip(int id, UpdateShipDto dto)
    {
        var updated = await _shipService.UpdateShipAsync(id, dto);
        return updated ? NoContent() : NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteShip(int id)
    {
        var deleted = await _shipService.DeleteShipAsync(id);
        return deleted ? NoContent() : NotFound();
    }
}