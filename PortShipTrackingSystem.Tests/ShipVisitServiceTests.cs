namespace PortShipTrackingSystem.Tests;

using Moq;
using Xunit;
using PortShipTrackingSystem.Core.DTOs;
using PortShipTrackingSystem.Core.Entities;
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

    [Fact]
    public async Task UpdateVisitAsync_ShouldThrowConcurrencyException_WhenRepositoryDetectsConflict()
    {
        // Arrange
        var existingVisit = new ShipVisit
        {
            VisitId = 1,
            ShipId = 1,
            PortId = 1,
            ArrivalDate = new DateTime(2026, 7, 1),
            DepartureDate = new DateTime(2026, 7, 10),
            Purpose = "Loading",
            RowVersion = new byte[] { 1, 2, 3 }
        };

        var dto = new UpdateShipVisitDto
        {
            ShipId = 1,
            PortId = 1,
            ArrivalDate = new DateTime(2026, 7, 1),
            DepartureDate = new DateTime(2026, 7, 12),
            Purpose = "Unloading",
            RowVersion = new byte[] { 9, 9, 9 }
        };

        _mockRepo.Setup(r => r.GetByIdWithDetailsAsync(1)).ReturnsAsync(existingVisit);
        _mockRepo
            .Setup(r => r.UpdateWithConcurrencyAsync(It.IsAny<ShipVisit>(), It.IsAny<byte[]>()))
            .ThrowsAsync(new ConcurrencyException("This record was modified by another user."));

        // Act & Assert
        await Assert.ThrowsAsync<ConcurrencyException>(() => _sut.UpdateVisitAsync(1, dto));
    }
}