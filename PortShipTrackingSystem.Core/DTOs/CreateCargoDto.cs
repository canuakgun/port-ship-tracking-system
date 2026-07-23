namespace PortShipTrackingSystem.Core.DTOs;

using System.ComponentModel.DataAnnotations;

public class CreateCargoDto
{
    [Required]
    public int ShipId { get; set; }

    [Required]
    public required string Description { get; set; }

    [Range(0.01, double.MaxValue)]
    public decimal WeightTon { get; set; }

    [Required]
    public required string CargoType { get; set; }
}