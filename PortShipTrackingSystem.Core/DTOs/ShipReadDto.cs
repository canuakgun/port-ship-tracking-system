namespace PortShipTrackingSystem.Core.DTOs;

public class ShipReadDto
{
    public int ShipId{ get; set;}
    public required string Name{ get; set;}
    public required string IMO{ get; set;}
    public required string Type{ get; set;}
    public required string Flag{ get; set;}
    public  int YearBuilt{ get; set;}

}