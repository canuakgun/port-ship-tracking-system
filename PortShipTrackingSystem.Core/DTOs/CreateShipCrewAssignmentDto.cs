namespace PortShipTrackingSystem.Core.DTOs;

using System.ComponentModel.DataAnnotations;

public class CreateShipCrewAssignmentDto
{
    [Required]
    public int ShipId { get; set; }

    [Required]
    public int CrewId { get; set; }

    [Required]
    public DateTime AssignmentDate { get; set; }
}