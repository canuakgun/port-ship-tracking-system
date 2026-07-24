namespace PortShipTrackingSystem.Api.Controllers;

using Microsoft.AspNetCore.Mvc;
using PortShipTrackingSystem.Core.DTOs;
using PortShipTrackingSystem.Core.Interfaces;

[ApiController]
[Route("api/[controller]")]
public class ShipVisitsController : ControllerBase
{
    private readonly IShipVisitService _shipVisitService;

    public ShipVisitsController(IShipVisitService shipVisitService)
    {
        _shipVisitService = shipVisitService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ShipVisitReadDto>>> GetAllVisits()
    {
        var visits = await _shipVisitService.GetAllVisitsAsync();
        return Ok(visits);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ShipVisitReadDto>> GetVisitById(int id)
    {
        var visit = await _shipVisitService.GetVisitByIdAsync(id);
        return visit is null ? NotFound() : Ok(visit);
    }

    [HttpPost]
    public async Task<ActionResult<ShipVisitReadDto>> CreateVisit(CreateShipVisitDto dto)
    {
        var created = await _shipVisitService.CreateVisitAsync(dto);
        return CreatedAtAction(nameof(GetVisitById), new { id = created.VisitId }, created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateVisit(int id, UpdateShipVisitDto dto)
    {
        var updated = await _shipVisitService.UpdateVisitAsync(id, dto);
        return updated ? NoContent() : NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteVisit(int id)
    {
        var deleted = await _shipVisitService.DeleteVisitAsync(id);
        return deleted ? NoContent() : NotFound();
    }
}