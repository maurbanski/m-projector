using MProjector.Logic.Coordinates;

namespace MProjector.Logic.Tests.CoordinateTests;

public class BitmapPointTests
{
    [Fact]
    public void Constructor_GivenCartesianCoordinates_ReturnsCorrectPoint()
    {
        var xMax = 1024;
        var yMax = 512;

        Dictionary<CartesianCoordinates, BitmapPoint> pairs = new Dictionary<CartesianCoordinates, BitmapPoint>
        {
            {
                new CartesianCoordinates(-512, 256),
                new BitmapPoint(0, 0)
            },
            {
                new CartesianCoordinates(0, 256),
                new BitmapPoint(512, 0)
            },
            {
                new CartesianCoordinates(512, 256),
                new BitmapPoint(1024, 0)
            },
            {
                new CartesianCoordinates(-512, 0),
                new BitmapPoint(0, 256)
            },
            {
                new CartesianCoordinates(0, 0),
                new BitmapPoint(512, 256)
            },
            {
                new CartesianCoordinates(512, 0),
                new BitmapPoint(1024, 256)
            },
            {
                new CartesianCoordinates(-512, -256),
                new BitmapPoint(0, 512)
            },
            {
                new CartesianCoordinates(0, -256),
                new BitmapPoint(512, 512)
            },
            {
                new CartesianCoordinates(512, -256),
                new BitmapPoint(1024, 512)
            },
        };

        foreach (var pair in pairs)
        {
            var actualBitmapPoint = new BitmapPoint(pair.Key, xMax, yMax);
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
            Action act = () => new BitmapPoint(point, xMax, yMax);
            Assert.Throws<ArgumentException>(act);
        }

    }
    
    [Fact]
    public void Constructor_GivenInvalidCoordinates_ThrowsException()
    {
        Action act1 = () => new BitmapPoint(-1, 0);
        Action act2 = () => new BitmapPoint(0, -1);
        
        Assert.Throws<ArgumentException>(act1);
        Assert.Throws<ArgumentException>(act2);

    }
}