namespace PortShipTrackingSystem.Api.Controllers;

using Microsoft.AspNetCore.Mvc;
using PortShipTrackingSystem.Core.DTOs;
using PortShipTrackingSystem.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class CrewMembersController : ControllerBase
{
    private readonly ICrewMemberService _crewMemberService;

    public CrewMembersController(ICrewMemberService crewMemberService)
    {
        _crewMemberService = crewMemberService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CrewMemberReadDto>>> GetAllCrew()
    {
        var crew = await _crewMemberService.GetAllCrewAsync();
        return Ok(crew);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CrewMemberReadDto>> GetCrewById(int id)
    {
        var crewMember = await _crewMemberService.GetCrewByIdAsync(id);
        return crewMember is null ? NotFound() : Ok(crewMember);
    }

    [HttpPost]
    [Authorize(Roles = "Admin,PortManager")]
    public async Task<ActionResult<CrewMemberReadDto>> CreateCrew(CreateCrewMemberDto dto)
    {
        var created = await _crewMemberService.CreateCrewAsync(dto);
        return CreatedAtAction(nameof(GetCrewById), new { id = created.CrewId }, created);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin,PortManager")]
    public async Task<IActionResult> UpdateCrew(int id, UpdateCrewMemberDto dto)
    {
        var updated = await _crewMemberService.UpdateCrewAsync(id, dto);
        return updated ? NoContent() : NotFound();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteCrew(int id)
    {
        var deleted = await _crewMemberService.DeleteCrewAsync(id);
        return deleted ? NoContent() : NotFound();
    }
}