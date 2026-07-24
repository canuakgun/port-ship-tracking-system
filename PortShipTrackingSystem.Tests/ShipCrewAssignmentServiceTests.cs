namespace PortShipTrackingSystem.Tests;

using Moq;
using Xunit;
using PortShipTrackingSystem.Core.DTOs;
using PortShipTrackingSystem.Core.Interfaces;
using PortShipTrackingSystem.Core.Exceptions;
using PortShipTrackingSystem.Services;

public class ShipCrewAssignmentServiceTests
{
    private readonly Mock<IShipCrewAssignmentRepository> _mockRepo;
    private readonly ShipCrewAssignmentService _sut;

    public ShipCrewAssignmentServiceTests()
    {
        _mockRepo = new Mock<IShipCrewAssignmentRepository>();
        _sut = new ShipCrewAssignmentService(_mockRepo.Object);
    }

    [Fact]
    public async Task CreateAssignmentAsync_ShouldThrowConflictException_WhenDuplicateAssignmentExists()
    {
        // Arrange
        var assignmentDate = new DateTime(2026, 7, 20);
        var dto = new CreateShipCrewAssignmentDto
        {
            ShipId = 1,
            CrewId = 1,
            AssignmentDate = assignmentDate
        };

        _mockRepo
            .Setup(r => r.AssignmentExistsAsync(dto.ShipId, dto.CrewId, dto.AssignmentDate))
            .ReturnsAsync(true);

        // Act & Assert
        await Assert.ThrowsAsync<ConflictException>(() => _sut.CreateAssignmentAsync(dto));
    }
}