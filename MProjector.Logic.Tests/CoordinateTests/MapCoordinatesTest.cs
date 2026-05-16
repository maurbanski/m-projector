using MProjector.Logic.Coordinates;

namespace MProjector.Logic.Tests.CoordinateTests;

public class MapCoordinatesTest
{
    [Fact]
    public void Constructor_GivenCartesianCoordinates_ReturnsCorrectMapCoordinates()
    {
        var xMax = 1024;
        var yMax = 512;

        Dictionary<CartesianCoordinates, MapCoordinates> pairs = new Dictionary<CartesianCoordinates, MapCoordinates>
        {
            {
                new CartesianCoordinates(-512, 256),
                new MapCoordinates(0, 0)
            },
            {
                new CartesianCoordinates(0, 256),
                new MapCoordinates(512, 0)
            },
            {
                new CartesianCoordinates(512, 256),
                new MapCoordinates(1023, 0)
            },
            {
                new CartesianCoordinates(-512, 0),
                new MapCoordinates(0, 256)
            },
            {
                new CartesianCoordinates(0, 0),
                new MapCoordinates(512, 256)
            },
            {
                new CartesianCoordinates(512, 0),
                new MapCoordinates(1023, 256)
            },
            {
                new CartesianCoordinates(-512, -256),
                new MapCoordinates(0, 511)
            },
            {
                new CartesianCoordinates(0, -256),
                new MapCoordinates(512, 511)
            },
            {
                new CartesianCoordinates(512, -256),
                new MapCoordinates(1023, 511)
            },
        };

        foreach (var pair in pairs)
        {
            var actualBitmapPoint = new MapCoordinates(pair.Key, xMax, yMax);
            Assert.Equal(pair.Value, actualBitmapPoint);
        }
    }

    [Fact]
    public void Constructor_GivenInvalidCartesianCoordinates_ThrowsException()
    {
        var xMax = 1024;
        var yMax = 512;

        var points = new List<CartesianCoordinates>
        {
            new CartesianCoordinates(-513, 0),
            new CartesianCoordinates(0, -257),
            new CartesianCoordinates(513, 0),
            new CartesianCoordinates(0, 257)
        };

        foreach (var point in points)
        {
            Action act = () => new MapCoordinates(point, xMax, yMax);
            Assert.Throws<ArgumentException>(act);
        }

    }
    
    [Fact]
    public void Constructor_GivenInvalidCoordinates_ThrowsException()
    {
        Action act1 = () => new MapCoordinates(-1, 0);
        Action act2 = () => new MapCoordinates(0, -1);
        
        Assert.Throws<ArgumentException>(act1);
        Assert.Throws<ArgumentException>(act2);

    }
}