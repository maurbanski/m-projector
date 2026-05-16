using MProjector.Domain.Maps;

namespace MProjector.Domain.Tests.MapsTests;

public class MapPointTests
{
    [Fact]
    public void IsBlack_GivenBlackPoint_ReturnsTrue()
    {
        var point = new MapPoint(0, 0, 0);
        
        Assert.True(point.IsBlack);
    }
    
    [Fact]
    public void IsBlack_GivenNonBlackPoint_ReturnsFalse()
    {
        var point = new MapPoint(1, 0, 0);

        Assert.False(point.IsBlack);
    }
    
    [Fact]
    public void IsWhite_GivenWhitePoint_ReturnsTrue()
    {
        var point = new MapPoint(255, 255, 255);
        
        Assert.True(point.IsWhite);
    }
    
    [Fact]
    public void IsWhite_GivenNonWhitePoint_ReturnsFalse()
    {
        var point = new MapPoint(255, 255, 254);

        Assert.False(point.IsWhite);
    }
}