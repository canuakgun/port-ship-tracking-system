namespace PortShipTrackingSystem.Core.DTOs;

public class UpdateShipVisitDto : CreateShipVisitDto
{
     public required byte[] RowVersion { get; set; }
}