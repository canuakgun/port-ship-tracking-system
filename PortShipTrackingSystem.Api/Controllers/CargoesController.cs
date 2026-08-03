namespace PortShipTrackingSystem.Api.Controllers;

using Microsoft.AspNetCore.Mvc;
using PortShipTrackingSystem.Core.DTOs;
using PortShipTrackingSystem.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class CargoesController : ControllerBase
{
    private readonly ICargoService _cargoService;

    public CargoesController(ICargoService cargoService)
    {
        _cargoService = cargoService;
    }

    [HttpGet("ship/{shipId}")]
    public async Task<ActionResult<IEnumerable<CargoReadDto>>> GetCargoesByShipId(int shipId)
    {
        var cargoes = await _cargoService.GetCargoesByShipIdAsync(shipId);
        return Ok(cargoes);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CargoReadDto>> GetCargoById(int id)
    {
        var cargo = await _cargoService.GetCargoByIdAsync(id);
        return cargo is null ? NotFound() : Ok(cargo);
    }

    [HttpPost]
    [Authorize(Roles = "Admin,PortManager")]
    public async Task<ActionResult<CargoReadDto>> CreateCargo(CreateCargoDto dto)
    {
        var created = await _cargoService.CreateCargoAsync(dto);
        return CreatedAtAction(nameof(GetCargoById), new { id = created.CargoId }, created);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin,PortManager")]
    public async Task<IActionResult> UpdateCargo(int id, UpdateCargoDto dto)
    {
        var updated = await _cargoService.UpdateCargoAsync(id, dto);
        return updated ? NoContent() : NotFound();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteCargo(int id)
    {
        var deleted = await _cargoService.DeleteCargoAsync(id);
        return deleted ? NoContent() : NotFound();
    }
}