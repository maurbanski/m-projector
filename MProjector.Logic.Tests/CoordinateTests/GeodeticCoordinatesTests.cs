using MProjector.Logic.Coordinates;

namespace MProjector.Logic.Tests.CoordinateTests;

public class GeodeticCoordinatesTests
{
    [Fact]
    public void Constructor_GivenInvalidCoordinates_ThrowsException()
    {
        Action act1 = () => new GeodeticCoordinates(181, 0);
        Action act2 = () => new GeodeticCoordinates(-181, 0);
        Action act3 = () => new GeodeticCoordinates(0, 91);
        Action act4 = () => new GeodeticCoordinates(0, -91);

        Assert.Throws<ArgumentException>(act1);
        Assert.Throws<ArgumentException>(act2);
        Assert.Throws<ArgumentException>(act3);
        Assert.Throws<ArgumentException>(act4);
    }

    [Fact]
    public void Contructor_GivenCartesianCoordinates_ReturnsCorrectCoordinates()
    {
        var xBound = 1024;
        var yBound = 512;
        
        Dictionary<CartesianCoordinates, GeodeticCoordinates> pairs = new Dictionary<CartesianCoordinates, GeodeticCoordinates>
        {
            {
                new CartesianCoordinates(-512, 256),
                new GeodeticCoordinates(-180, 90)
            },
            {
                new CartesianCoordinates(0, 256),
                new GeodeticCoordinates(0, 90)
            },
            {
                new CartesianCoordinates(512, 256),
                new GeodeticCoordinates(180, 90)
            },
            {
                new CartesianCoordinates(-512, 0),
                new GeodeticCoordinates(-180, 0)
            },
            {
                new CartesianCoordinates(0, 0),
                new GeodeticCoordinates(0, 0)
            },
            {
                new CartesianCoordinates(512, 0),
                new GeodeticCoordinates(180, 0)
            },
            {
                new CartesianCoordinates(-512, -256),
                new GeodeticCoordinates(-180, -90)
            },
            {
                new CartesianCoordinates(0, -256),
                new GeodeticCoordinates(0, -90)
            },
            {
                new CartesianCoordinates(512, -256),
                new GeodeticCoordinates(180, -90)
            },
        };

        foreach (var pair in pairs)
        {
            var actualGeodeticPoint = new GeodeticCoordinates(pair.Key, xBound, yBound);
            Assert.Equal(pair.Value, actualGeodeticPoint);
        }
    }

    [Fact]
    public void Constructor_GivenInvalidCartesianCoordinates_ThrowsException()
    {
        var xBound = 1024;
        var yBound = 512;

        var points = new List<CartesianCoordinates>
        {
            new CartesianCoordinates(-513, 0),
            new CartesianCoordinates(0, 257),
            new CartesianCoordinates(513, 0),
            new CartesianCoordinates(0, 257),
        };

        foreach (var point in points)
        {
            Action act = () => new GeodeticCoordinates(point, xBound, yBound);
            Assert.Throws<ArgumentException>(act);
        }
    }
}