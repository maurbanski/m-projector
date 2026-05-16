using MProjector.Abstractions.Graphics;
using MProjector.Domain.Maps;
using SkiaSharp;

namespace MProjector.Graphics;

public class GraphicalMapFactory : IGraphicalMapFactory
{
    public IGraphicalMap CreateFromFile(string filePath)
    {
        var image = SKImage.FromEncodedData(filePath);
        return new GraphicalMap(SKBitmap.FromImage(image));
    }

    public IGraphicalMap CreateFromMap(Map map)
    {
        var bitmap = new SKBitmap(map.Width, map.Height);
        
        for (var i = 0; i < map.Width; i++)
        {
            for (var j = 0; j < map.Height; j++)
            {
                var point = map.GetPoint(i, j);
                SKColor color = new SKColor(
                    Convert.ToByte(point.R),
                    Convert.ToByte(point.G),
                    Convert.ToByte(point.B)
                );
                
                bitmap.SetPixel(i, j, color);
            }
        }

        return new GraphicalMap(bitmap);
    }
}