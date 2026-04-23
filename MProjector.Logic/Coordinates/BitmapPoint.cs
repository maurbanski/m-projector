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

    public BitmapPoint(CartesianCoordinates cartesianCoordinates, int width, int height)
    {
        if (Math.Abs(cartesianCoordinates.X) > width / 2) throw new ArgumentException($"X-coord out of bounds");
        if (Math.Abs(cartesianCoordinates.Y) > height / 2) throw new ArgumentException($"Y-coord out of bounds");
        
        X = Math.Min(Convert.ToInt32(cartesianCoordinates.X + width / 2), width - 1);
        Y = Math.Min(Convert.ToInt32(height / 2 - cartesianCoordinates.Y), height - 1);
    }
}