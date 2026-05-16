using MProjector.Abstractions.Graphics;
using MProjector.Domain.Maps;
using SkiaSharp;

namespace MProjector.Graphics;

public class GraphicalMap : IGraphicalMap
{
    public int Width { get; set; }
    public int Height { get; set; }
    
    private SKBitmap _bitmap;
    
    public GraphicalMap(SKBitmap bitmap)
    {
        _bitmap = bitmap;
        Width = bitmap.Width;
        Height = bitmap.Height;
    }

    public Map ToMap()
    {
        MapPoint[,] points = new MapPoint[Width, Height];
        for (var i = 0; i < Width; i++)
        {
            for (var j = 0; j < Height; j++)
            {
                var color = _bitmap.GetPixel(i, j);
                var point = new MapPoint(
                    color.Red,
                    color.Green,
                    color.Blue);

                points[i, j] = point;
            }
        }

        return new Map(points);
    }

    public void Save(string outputPath)
    {
        using (var stream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
        {
            var image = SKImage.FromBitmap(_bitmap);
            var encodedImage = image.Encode(SKEncodedImageFormat.Png, 100);
            encodedImage.SaveTo(stream);
        }
    }
}