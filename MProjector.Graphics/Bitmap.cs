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

    public Bitmap(SKBitmap bitmap)
    {
        Width = bitmap.Width;
        Height = bitmap.Height;

        _bitmap = bitmap;
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

    public void LoadFromBytes(byte[] inputBytes)
    {
        var bitmap = SKBitmap.FromImage(SKImage.FromEncodedData(inputBytes));
        Height = bitmap.Height;
        Width = bitmap.Width;
        _bitmap = bitmap;
    }

    public byte[] ToBytes()
    {
        return SKImage.FromBitmap(_bitmap).Encode(SKEncodedImageFormat.Png, 100).ToArray();
    }
}