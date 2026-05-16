using System.Drawing;
using MProjector.Domain.Maps;

namespace MProjector.Domain.Tests.MapsTests;

public class MapTests
{
    [Fact]
    public void Constructor_GivenWidthAndHeight_CreatesWhiteMapOfCorrectDimensions()
    {
        var width = 200;
        var height = 100;

        var map = new Map(width, height);
        
        Assert.Equal(map.Width, width);
        Assert.Equal(map.Height, height);

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                var point = map.GetPoint(i, j);
                var expectedPoint = new MapPoint(255, 255, 255);
                
                Assert.Equal(expectedPoint, point);
            }
        }
    }

    [Fact]
    public void Constructor_GivenIncorrectAspectWidthAndHeight_ThrowsException()
    {
        var width = 100;
        var height = 100;

        var act = () => new Map(width, height);
        Assert.Throws<ArgumentException>(act);
    }

    [Fact]
    public void Constructor_GivenPoints_ReturnsMapWithGivenPoints()
    {
        var width = 200;
        var height = 100;

        var points = new MapPoint[width, height];
        var rng = new Random();
        
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                points[i, j] = new MapPoint(rng.Next(0, 255), rng.Next(0, 255), rng.Next(0, 255));
            }
        }

        var map = new Map(points);
        
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Assert.Equal(points[i, j], map.GetPoint(i, j));
            }
        }
    }
    
    [Fact]
    public void Constructor_GivenPointsWithIncorrectDimensions_ThrowsException()
    {
        var width = 100;
        var height = 100;

        var points = new MapPoint[width, height];
        var rng = new Random();
        
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                points[i, j] = new MapPoint(rng.Next(0, 255), rng.Next(0, 255), rng.Next(0, 255));
            }
        }

        var act = () => new Map(points);
        Assert.Throws<ArgumentException>(act);
    }

    [Fact]
    public void SetPoint_GivenPoint_SetsCorrectPoint()
    {
        var width = 200;
        var height = 100;

        var points = new MapPoint[width, height];
        var rng = new Random();
        
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                points[i, j] = new MapPoint(rng.Next(0, 255), rng.Next(0, 255), rng.Next(0, 255));
            }
        }

        var map = new Map(points);
        var x = 50;
        var y = 50;
        var point = map.GetPoint(x, y);
        
        var modifiedPoint = new MapPoint(rng.Next(0, 255), rng.Next(0, 255), rng.Next(0, 255));
        while (modifiedPoint == point) modifiedPoint = new MapPoint(rng.Next(0, 255), rng.Next(0, 255), rng.Next(0, 255));
        
        map.SetPoint(x, y, modifiedPoint);
        Assert.Equal(modifiedPoint, map.GetPoint(x, y));
    }
}