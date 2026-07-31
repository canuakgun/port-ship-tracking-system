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
    private readonly ShipService _sut;

    public ShipServiceTests()
    {
        _mockRepo = new Mock<IShipRepository>();
        _sut = new ShipService(_mockRepo.Object);
    }

    [Fact]
    public async Task CreateShipAsync_ShouldThrowConflictException_WhenImoAlreadyExists()
    {
        var dto = new CreateShipDto
        {
            Name = "Test Ship",
            IMO = "1234567",
            Type = "Tanker",
            Flag = "Turkey",
            YearBuilt = 2020
        };

        _mockRepo.Setup(r => r.ImoExistsAsync(dto.IMO, null)).ReturnsAsync(true);

        await Assert.ThrowsAsync<ConflictException>(() => _sut.CreateShipAsync(dto));
    }

    [Fact]
    public async Task CreateShipAsync_ShouldReturnShipReadDto_WhenImoIsUnique()
    {
        var dto = new CreateShipDto
        {
            Name = "Test Ship 2",
            IMO = "7654321",
            Type = "Cargo",
            Flag = "Turkey",
            YearBuilt = 2021
        };

        _mockRepo.Setup(r => r.ImoExistsAsync(It.IsAny<string>(), null)).ReturnsAsync(false);

        var result = await _sut.CreateShipAsync(dto);

        Assert.Equal("Test Ship 2", result.Name);
        _mockRepo.Verify(r => r.AddAsync(It.IsAny<Ship>()), Times.Once);
    }

    [Fact]
    public async Task DeleteShipAsync_ShouldCallRepositoryDelete_WhenShipExists()
    {
        var ship = new Ship
        {
            ShipId = 1,
            Name = "Test Ship",
            IMO = "1234567",
            Type = "Tanker",
            Flag = "Turkey",
            YearBuilt = 2020
        };

        _mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(ship);
        _mockRepo.Setup(r => r.SaveChangesAsync()).ReturnsAsync(true);

        var result = await _sut.DeleteShipAsync(1);

        Assert.True(result);
        _mockRepo.Verify(r => r.Delete(ship), Times.Once);
    }
}