namespace PortShipTrackingSystem.Core.DTOs;

public class TokenResponseDto
{
    public required string Token { get; set; }
    public required string Username { get; set; }
    public required string Role { get; set; }
    public required string RefreshToken { get; set; }
}