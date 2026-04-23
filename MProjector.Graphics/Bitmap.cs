using MProjector.Abstractions.Graphics;
using SkiaSharp;

namespace MProjector.Graphics;

public class Bitmap : IBitmap
{
    public int Width { get; set; }
    public int Height { get; set; }
    private SKBitmap _bitmap;

    public Bitmap()
    {
        Height = 0;
        Width = 0;
        _bitmap = new SKBitmap();
    }

    public void Clear()
    {
        Height = 0;
        Width = 0;
        _bitmap = new SKBitmap();
    }

    public IPixel GetPixel(int x, int y)
    {
        var colour = _bitmap.GetPixel(x, y);
        return new Pixel(colour);
    }

    public void SetPixel(int x, int y, IPixel pixel)
    {
        _bitmap.SetPixel(x, y, ((Pixel)pixel).ToSKColor());
    }

    public void InitialiseEmpty(int width, int height)
    {
        Width = width;
        Height = height;

        _bitmap = new SKBitmap(width, height);
    }

    public void FromFile(string inputPath)
    {
        var image = SKImage.FromEncodedData(inputPath);
        _bitmap = SKBitmap.FromImage(image);
        Height = _bitmap.Height;
        Width = _bitmap.Width;
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