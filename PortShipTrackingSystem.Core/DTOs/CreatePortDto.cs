namespace PortShipTrackingSystem.Core.DTOs;

using System.ComponentModel.DataAnnotations;

public class CreatePortDto
{
    [Required]
    public required string Name { get; set; }

    [Required]
    public required string Country { get; set; }

    [Required]
    public required string City { get; set; }
}