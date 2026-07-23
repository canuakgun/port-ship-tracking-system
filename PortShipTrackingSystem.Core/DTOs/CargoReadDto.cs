namespace PortShipTrackingSystem.Core.DTOs;

public class CargoReadDto
{
    public int CargoId { get; set;}
    public int ShipId { get; set;}
    public required string Description{ get; set;}
    public decimal WeightTon { get; set;}
    public required string CargoType { get; set;}
    public required string ShipName { get; set;}
}