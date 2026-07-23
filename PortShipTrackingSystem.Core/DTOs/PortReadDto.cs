namespace PortShipTrackingSystem.Core.DTOs;

public class PortReadDto
{
    public int PortId{ get; set;}
    public required string Name{ get; set;}
    public required string Country{ get; set;}
    public required string City{ get; set;}
}