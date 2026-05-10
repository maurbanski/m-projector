namespace MProjector.Logic.V2.Coordinates;

public class CartesianCoordinates
{
    public double X { get; set; }
    public double Y { get; set; }

    public CartesianCoordinates(double x, double y)
    {
        X = x;
        Y = y;
    }

    public CartesianCoordinates(MapCoordinates mapCoordinates, double width, double height)
    {
        if (Math.Abs(mapCoordinates.X) > width) throw new ArgumentException("X-coord out of bounds");
        if (Math.Abs(mapCoordinates.Y) > height) throw new ArgumentException("Y-coord out of bounds");

        X = mapCoordinates.X - width / 2;
        Y = height / 2 - mapCoordinates.Y;
    }

    public CartesianCoordinates(GeodeticCoordinates geodeticCoordinates, double width, double height)
    {
        X = geodeticCoordinates.LambdaDeg / (2 * 180 / width);
        Y = geodeticCoordinates.PhiDeg / (2 * 90 / height);
    }
}