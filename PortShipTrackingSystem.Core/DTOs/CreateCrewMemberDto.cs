namespace PortShipTrackingSystem.Core.DTOs;

using System.ComponentModel.DataAnnotations;

public class CreateCrewMemberDto
{
    [Required]
    public required string FirstName { get; set; }

    [Required]
    public required string LastName { get; set; }

    [Required, EmailAddress]
    public required string Email { get; set; }

    [Required, Phone]
    public required string PhoneNumber { get; set; }

    [Required]
    public required string Role { get; set; }
}