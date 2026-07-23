namespace PortShipTrackingSystem.Core.DTOs;

using System.ComponentModel.DataAnnotations;

public class CreateShipDto
{
    [Required]
    public required string Name { get; set; }

    [Required, StringLength(10, MinimumLength = 7)]
    public required string IMO { get; set; }

    [Required]
    public required string Type { get; set; }

    [Required]
    public required string Flag { get; set; }

    [Range(1900, 2100)]
    public int YearBuilt { get; set; }
}