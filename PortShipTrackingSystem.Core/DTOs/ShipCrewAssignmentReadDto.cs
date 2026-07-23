namespace PortShipTrackingSystem.Core.DTOs;

public class ShipCrewAssignmentReadDto
{
    public int AssignmentId { get; set; }
    public int ShipId { get; set; }
    public required string ShipName { get; set; }
    public int CrewId { get; set; }
    public required string CrewFullName { get; set; }
    public DateTime AssignmentDate { get; set; }
}