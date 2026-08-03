namespace PortShipTrackingSystem.Api.Controllers;

using Microsoft.AspNetCore.Mvc;
using PortShipTrackingSystem.Core.DTOs;
using PortShipTrackingSystem.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ShipCrewAssignmentsController : ControllerBase
{
    private readonly IShipCrewAssignmentService _shipCrewAssignmentService;

    public ShipCrewAssignmentsController(IShipCrewAssignmentService shipCrewAssignmentService)
    {
        _shipCrewAssignmentService = shipCrewAssignmentService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ShipCrewAssignmentReadDto>>> GetAllAssignments()
    {
        var assignments = await _shipCrewAssignmentService.GetAllAssignmentsAsync();
        return Ok(assignments);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ShipCrewAssignmentReadDto>> GetAssignmentById(int id)
    {
        var assignment = await _shipCrewAssignmentService.GetAssignmentByIdAsync(id);
        return assignment is null ? NotFound() : Ok(assignment);
    }

    [HttpPost]
    [Authorize(Roles = "Admin,PortManager")]
    public async Task<ActionResult<ShipCrewAssignmentReadDto>> CreateAssignment(CreateShipCrewAssignmentDto dto)
    {   
        var username = User.Identity!.Name!;
        var created = await _shipCrewAssignmentService.CreateAssignmentAsync(dto, username);
        return CreatedAtAction(nameof(GetAssignmentById), new { id = created.AssignmentId }, created);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteAssignment(int id)
    {   
        var username = User.Identity!.Name!;
        var isAdmin = User.IsInRole("Admin");
        var deleted = await _shipCrewAssignmentService.DeleteAssignmentAsync(id, username, isAdmin);
        return deleted ? NoContent() : NotFound();
    }
}