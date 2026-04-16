using MProjector.Logic.Points;

namespace MProjector.Logic.Tests.PointTests;

public class BitmapPointTests
{
    [Fact]
    public void BitmapPoint_ShouldCreateCorrectPoint_GivenCartesianCoords()
    {
        var bitmapWidth = 1024;
        var bitmapHeight = 512;

        Dictionary<CartesianPoint, BitmapPoint> pairs = new Dictionary<CartesianPoint, BitmapPoint>
        {
            {
                new CartesianPoint(-512, -256),
                new BitmapPoint(0, 0)
            },
            {
                new CartesianPoint(0, -256),
                new BitmapPoint(512, 0)
            },
            {
                new CartesianPoint(512, -256),
                new BitmapPoint(1023, 0)
            },
            {
                new CartesianPoint(-512, 0),
                new BitmapPoint(0, 256)
            },
            {
                new CartesianPoint(0, 0),
                new BitmapPoint(512, 256)
            },
            {
                new CartesianPoint(512, 0),
                new BitmapPoint(1023, 256)
            },
            {
                new CartesianPoint(-512, 256),
                new BitmapPoint(0, 511)
            },
            {
                new CartesianPoint(0, 256),
                new BitmapPoint(512, 511)
            },
            {
                new CartesianPoint(512, 256),
                new BitmapPoint(1023, 511)
            },
        };

        foreach (var pair in pairs)
        {
            var actualBitmapPoint = new BitmapPoint(pair.Key, bitmapWidth, bitmapHeight);
            Assert.Equal(actualBitmapPoint, pair.Value);
        }
    }

    [Fact]
    public void BitmapPoint_ShouldThrowException_GivenOutOfBoundsCoords()
    {
        var bitmapWidth = 1024;
        var bitmapHeight = 512;

        var points = new List<CartesianPoint>
        {
            new CartesianPoint(-513, 0),
            new CartesianPoint(0, -257),
            new CartesianPoint(513, 0),
            new CartesianPoint(0, 257)
        };

        foreach (var point in points)
        {
            Action act = () => new BitmapPoint(point, bitmapWidth, bitmapHeight);
            Assert.Throws<ArgumentException>(act);
        }

    }
}