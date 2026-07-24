namespace PortShipTrackingSystem.Tests;

using Moq;
using Xunit;
using PortShipTrackingSystem.Core.DTOs;
using PortShipTrackingSystem.Core.Entities;
using PortShipTrackingSystem.Core.Interfaces;
using PortShipTrackingSystem.Core.Exceptions;
using PortShipTrackingSystem.Services;

public class ShipServiceTests
{
    private readonly Mock<IShipRepository> _mockRepo;
    private readonly ShipService _sut; // "sut" = System Under Test

    public ShipServiceTests()
    {
        _mockRepo = new Mock<IShipRepository>();
        _sut = new ShipService(_mockRepo.Object);
    }

    // Testler buraya gelecek
    [Fact]
    public async Task CreateShipAsync_ShouldThrowConflictException_WhenImoAlreadyExists()
    {
        // Arrange
        var dto = new CreateShipDto
        {
            Name = "Test Ship",
            IMO = "1234567",
            Type = "Tanker",
            Flag = "Turkey",
            YearBuilt = 2020
        };

        _mockRepo.Setup(r => r.ImoExistsAsync(dto.IMO, null)).ReturnsAsync(true);

        // Act & Assert
        await Assert.ThrowsAsync<ConflictException>(() => _sut.CreateShipAsync(dto));
    }
    [Fact]
    public async Task CreateShipAsync_ShouldReturnShipReadDto_WhenImoIsUnique()
    {
        // Arrange
        var dto = new CreateShipDto
        {
            Name = "Test Ship 2",
            IMO = "7654321",
            Type = "Cargo",
            Flag = "Turkey",
            YearBuilt = 2021
        };

        _mockRepo.Setup(r => r.ImoExistsAsync(dto.IMO, null)).ReturnsAsync(false);

        // Act
        var result = await _sut.CreateShipAsync(dto);

        // Assert
        Assert.Equal("Test Ship 2", result.Name);
        Assert.Equal("7654321", result.IMO);
        _mockRepo.Verify(r => r.AddAsync(It.IsAny<Ship>()), Times.Once);
    }
}