namespace MProjector.Logic.Coordinates;

public record BitmapPoint
{
    public int X { get; set; }
    public int Y { get; set; }

    public BitmapPoint(int x, int y)
    {
        if (x < 0) throw new ArgumentException("X must not be negative");
        if (y < 0) throw new ArgumentException("Y must not be negative");
        
        X = x;
        Y = y;
    }

    public BitmapPoint(CartesianCoordinates cartesianCoordinates, int xBound, int yBound)
    {
        if (Math.Abs(cartesianCoordinates.X) > xBound / 2) throw new ArgumentException($"X-coord out of bounds");
        if (Math.Abs(cartesianCoordinates.Y) > yBound / 2) throw new ArgumentException($"Y-coord out of bounds");
        
        X = Convert.ToInt32(cartesianCoordinates.X + xBound / 2);
        Y = Convert.ToInt32(yBound / 2 - cartesianCoordinates.Y);
    }
}