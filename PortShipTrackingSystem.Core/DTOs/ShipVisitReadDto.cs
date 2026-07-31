namespace PortShipTrackingSystem.Core.DTOs;

public class ShipVisitReadDto
{
    public int VisitId { get; set; }
    public int ShipId { get; set; }
    public required string ShipName { get; set; }
    public int PortId { get; set; }
    public required string PortName { get; set; }
    public DateTime ArrivalDate { get; set; }
    public DateTime DepartureDate { get; set; }
    public required string Purpose { get; set; }
    public required byte[] rowVersion { get; set; }
}