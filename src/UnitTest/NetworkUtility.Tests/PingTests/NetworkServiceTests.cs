using FluentAssertions;
using NetworkUtility.Ping;

namespace NetworkUtility.Tests.PingTests;

public class NetworkServiceTests
{
    [Fact]
    public void NetworkService_SendPing_ReturnString()
    {
        // Arrange 
        var pingService = new NetworkService();
        
        // Act
        var result = pingService.SendPing();

        // Assert
        result.Should().NotBeNullOrWhiteSpace();
        result.Should().Be("Success : Ping Sent");
        result.Should().Contain("Success", Exactly.Once());
    }

    [Theory]
    [InlineData(2,2,4)]
    public void NetworkService_PingTimeout_ReturnInteger(int a, int b, int expected)
    {
        // Arrange 
        var pingService = new NetworkService();
        
        // Act
        var result = pingService.PingTimeout(a, b);
        
        // Assert
        result.Should().Be(expected);
        result.Should().BeGreaterThan(3);
        result.Should().NotBeInRange(-1000, 0); // It should be positive value  
    }
}