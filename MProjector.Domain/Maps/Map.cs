namespace MProjector.Domain.Maps;

public class Map
{
    public readonly int Width;
    public readonly int Height;

    private readonly MapPoint[,] _points;
    
    public Map(int width, int height)
    {
        if (width != 2 * height) throw new ArgumentException($"Maps must have an aspect ratio of 2:1 (provided width: {width}, height: {height})");
        
        Width = width;
        Height = height;

        _points = new MapPoint[width, height];
    }

    public Map(MapPoint[,] points)
    {
        var width = points.GetLength(0);
        var height = points.GetLength(1);
        
        if (width != 2 * height) throw new ArgumentException($"Maps must have an aspect ratio of 2:1 (provided width: {width}, height: {height})");

        Width = width;
        Height = height;
        
        _points = points;
    }

    public MapPoint GetPoint(int x, int y) => _points[x,y];

    public void SetPoint(int x, int y, MapPoint point) => _points[x,y] = point;
}