using System.Net.NetworkInformation;
using FluentAssertions;
using FluentAssertions.Extensions;
using NetworkUtility.Ping;

namespace NetworkUtility.Tests.PingTests;

public class NetworkServiceTests
{
    private readonly NetworkService _pingService;
    public NetworkServiceTests()
    {
        _pingService = new NetworkService();
    } 
    
    [Fact]
    public void NetworkService_SendPing_ReturnString()
    {
        // Arrange 
        
        // Act
        var result = _pingService.SendPing();

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
        
        // Act
        var result = _pingService.PingTimeout(a, b);
        
        // Assert
        result.Should().Be(expected);
        result.Should().BeGreaterThan(3);
        result.Should().NotBeInRange(-1000, 0); // It should be positive value  
    }

    [Fact]
    public void NetworkService_LastPingData_ReturnDate()
    {
        // Arrange 

        // Act
        var result = _pingService.LastPingData();

        // Assert
        result.Should().BeAfter(1.January(2010));
        result.Should().BeBefore(1.January(2030));
    }

    [Fact]
    public void NetworkService_GetPingOptions_ReturnObject()
    {
        // Arrange
        var expected = new PingOptions()
        {
            DontFragment = true,
            Ttl = 1 
        };
        
        // Act
        var result = _pingService.GetPingOptions();

        // Assert
        result.Should().BeOfType<PingOptions>();
        result.Should().BeEquivalentTo(expected);
        result.Ttl.Should().Be(1);
    }
    
    [Fact]
    public void NetworkService_MostRecentPingOptions_ReturnObject()
    {
        // Arrange
        var expected = new PingOptions()
        {
            DontFragment = true,
            Ttl = 1 
        };
        
        // Act
        var result = _pingService.MostRecentPings();

        // Assert
        result.Should().BeOfType<List<PingOptions>>();
        result.Should().ContainEquivalentOf(expected);
        result.Should().Contain(x => x.DontFragment == true);
    }
}