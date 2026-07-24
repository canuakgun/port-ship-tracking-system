namespace PortShipTrackingSystem.Tests;

using Moq;
using Xunit;
using PortShipTrackingSystem.Core.DTOs;
using PortShipTrackingSystem.Core.Interfaces;
using PortShipTrackingSystem.Core.Exceptions;
using PortShipTrackingSystem.Services;

public class ShipVisitServiceTests
{
    private readonly Mock<IShipVisitRepository> _mockRepo;
    private readonly ShipVisitService _sut;

    public ShipVisitServiceTests()
    {
        _mockRepo = new Mock<IShipVisitRepository>();
        _sut = new ShipVisitService(_mockRepo.Object);
    }

    [Fact]
    public async Task CreateVisitAsync_ShouldThrowValidationException_WhenArrivalIsAfterDeparture()
    {
        // Arrange
        var dto = new CreateShipVisitDto
        {
            ShipId = 1,
            PortId = 1,
            ArrivalDate = new DateTime(2026, 7, 20),
            DepartureDate = new DateTime(2026, 7, 15), // Arrival, Departure'dan SONRA — geçersiz
            Purpose = "Loading"
        };

        // Act & Assert
        await Assert.ThrowsAsync<ValidationException>(() => _sut.CreateVisitAsync(dto));
    }
}