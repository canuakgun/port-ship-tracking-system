namespace PortShipTrackingSystem.Core.DTOs;

using System.ComponentModel.DataAnnotations;

public class CreateShipVisitDto
{
    [Required]
    public int ShipId { get; set; }

    [Required]
    public int PortId { get; set; }

    [Required]
    public DateTime ArrivalDate { get; set; }

    [Required]
    public DateTime DepartureDate { get; set; }

    [Required]
    public required string Purpose { get; set; }
}