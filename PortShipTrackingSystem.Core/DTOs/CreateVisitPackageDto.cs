namespace PortShipTrackingSystem.Core.DTOs;

public class CreateVisitPackageDto
{
    public int ShipId { get; set; }
    public int PortId { get; set; }
    public DateTime ArrivalDate { get; set; }
    public DateTime DepartureDate { get; set; }
    public string Purpose { get; set; } = string.Empty;

    public string CargoDescription { get; set; } = string.Empty;
    public decimal CargoWeightTon { get; set; }
    public string CargoType { get; set; } = string.Empty;

    public int CrewId { get; set; }
    public DateTime AssignmentDate { get; set; }
}