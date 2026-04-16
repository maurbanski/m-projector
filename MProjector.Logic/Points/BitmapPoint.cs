namespace MProjector.Logic.Points;

public record BitmapPoint
{
    public int X { get; set; }
    public int Y { get; set; }

    public BitmapPoint(int x, int y)
    {
        X = x;
        Y = y;
    }

    public BitmapPoint(CartesianPoint cartesianPoint, int bitmapWidth, int bitmapHeight)
    {
        if (Math.Abs(cartesianPoint.X) > bitmapWidth / 2) throw new ArgumentException($"X-coord out of bounds");
        if (Math.Abs(cartesianPoint.Y) > bitmapHeight / 2) throw new ArgumentException($"Y-coord out of bounds");
        
        X = Math.Min(Convert.ToInt32(cartesianPoint.X + bitmapWidth/2), bitmapWidth - 1);
        Y = Math.Min(Convert.ToInt32(cartesianPoint.Y + bitmapHeight/2), bitmapHeight - 1);
    }
}