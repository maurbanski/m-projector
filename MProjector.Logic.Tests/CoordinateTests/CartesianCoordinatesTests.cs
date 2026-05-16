using MProjector.Logic.Coordinates;

namespace MProjector.Logic.Tests.CoordinateTests;

public class CartesianCoordinatesTests
{
    [Fact]
    public void Constructor_GivenGeodeticCoordinates_ReturnsCorrectCoordinates()
    {
        var xBound = 1024;
        var yBound = 512;
        
        Dictionary<GeodeticCoordinates, CartesianCoordinates> pairs = new Dictionary<GeodeticCoordinates, CartesianCoordinates>
        {
            {
                new GeodeticCoordinates(-180, 90),
                new CartesianCoordinates(-512, 256)
            },
            {
                new GeodeticCoordinates(0, 90),
                new CartesianCoordinates(0, 256)
            },
            {
                new GeodeticCoordinates(180, 90),
                new CartesianCoordinates(512, 256)
            },
            {
                new GeodeticCoordinates(-180, 0),
                new CartesianCoordinates(-512, 0)
            },
            {
                new GeodeticCoordinates(0, 0),
                new CartesianCoordinates(0, 0)
            },
            {
                new GeodeticCoordinates(180, 0),
                new CartesianCoordinates(512, 0)
            },
            {
                new GeodeticCoordinates(-180, -90),
                new CartesianCoordinates(-512, -256)
            },
            {
                new GeodeticCoordinates(0, -90),
                new CartesianCoordinates(0, -256)
            },
            {
                new GeodeticCoordinates(180, -90),
                new CartesianCoordinates(512, -256)
            },
            {
                new GeodeticCoordinates(-90, 45),
                new CartesianCoordinates(-256, 128)
            },
            {
                new GeodeticCoordinates(90, 45),
                new CartesianCoordinates(256, 128)
            },
            {
                new GeodeticCoordinates(-90, -45),
                new CartesianCoordinates(-256, -128)
            },
            {
                new GeodeticCoordinates(90, -45),
                new CartesianCoordinates(256, -128)
            },
        };

        foreach (var pair in pairs)
        {
            var actualCartesianPoint = new CartesianCoordinates(pair.Key, xBound, yBound);
            Assert.Equal(pair.Value, actualCartesianPoint);
        }
    }

    [Fact]
    public void Constructor_GivenMapCoordinates_ReturnsCorrectCoordinates()
    {
        var xBound = 1024;
        var yBound = 512;
        
        Dictionary<MapCoordinates, CartesianCoordinates> pairs = new Dictionary<MapCoordinates, CartesianCoordinates>
        {
            {
                new MapCoordinates(0, 0),
                new CartesianCoordinates(-512, 256)
            },
            {
                new MapCoordinates(512, 0),
                new CartesianCoordinates(0, 256)
            },
            {
                new MapCoordinates(1024, 0),
                new CartesianCoordinates(512, 256)
            },
            {
                new MapCoordinates(0, 256),
                new CartesianCoordinates(-512, 0)
            },
            {
                new MapCoordinates(512, 256),
                new CartesianCoordinates(0, 0)
            },
            {
                new MapCoordinates(1024, 256),
                new CartesianCoordinates(512, 0)
            },
            {
                new MapCoordinates(0, 512),
                new CartesianCoordinates(-512, -256)
            },
            {
                new MapCoordinates(512, 512),
                new CartesianCoordinates(0, -256)
            },
            {
                new MapCoordinates(1024, 512),
                new CartesianCoordinates(512, -256)
            },
        };

        foreach (var pair in pairs)
        {
            var actualCartesianPoint = new CartesianCoordinates(pair.Key, xBound, yBound);
            Assert.Equal(pair.Value, actualCartesianPoint);
        }
    }

    [Fact]
    public void Constructor_GivenInvalidMapCoordinates_ThrowsException()
    {
        var mapWidth = 1024;
        var mapHeight = 512;

        var points = new List<MapCoordinates>
        {
            new MapCoordinates(1025, 0),
            new MapCoordinates(0, 513)
        };
        
        foreach (var point in points)
        {
            Action act = () => new CartesianCoordinates(point, mapWidth, mapHeight);
            Assert.Throws<ArgumentException>(act);
        }
    }
}