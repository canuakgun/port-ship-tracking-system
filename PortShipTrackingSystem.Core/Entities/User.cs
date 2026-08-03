namespace PortShipTrackingSystem.Core.Entities;

public class User : BaseEntity
{
    public int UserId { get; set; }
    public required string Username { get; set; }
    public required string PasswordHash { get; set; }
    public required string Role { get; set; } // "Admin", "PortManager", "Operator"
}