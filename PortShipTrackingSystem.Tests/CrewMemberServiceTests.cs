namespace PortShipTrackingSystem.Tests;

using Moq;
using Xunit;
using PortShipTrackingSystem.Core.Entities;
using PortShipTrackingSystem.Core.Interfaces;
using PortShipTrackingSystem.Services;

public class CrewMemberServiceTests
{
    private readonly Mock<ICrewMemberRepository> _mockRepo;
    private readonly CrewMemberService _sut;

    public CrewMemberServiceTests()
    {
        _mockRepo = new Mock<ICrewMemberRepository>();
        _sut = new CrewMemberService(_mockRepo.Object);
    }

    // Testler buraya
    [Fact]
    public async Task GetCrewByIdAsync_ShouldReturnCrewMember_WhenExists()
    {
        // Arrange
        var crewMember = new CrewMember
        {
            CrewId = 1,
            FirstName = "Ali",
            LastName = "Veli",
            Email = "ali@test.com",
            PhoneNumber = "+905551112233",
            Role = "Captain"
        };

        _mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(crewMember);

        // Act
        var result = await _sut.GetCrewByIdAsync(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Ali", result!.FirstName);
        Assert.Equal("Veli", result.LastName);
    }
    [Fact]
    public async Task GetCrewByIdAsync_ShouldReturnNull_WhenNotFound()
    {
        // Arrange
        _mockRepo.Setup(r => r.GetByIdAsync(99)).ReturnsAsync((CrewMember?)null);

        // Act
        var result = await _sut.GetCrewByIdAsync(99);

        // Assert
        Assert.Null(result);
    }
}