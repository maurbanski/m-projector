using MProjector.Abstractions.Graphics;
using SkiaSharp;

namespace MProjector.Graphics;

public class Pixel : IPixel
{
    public int R { get; set; }
    public int B { get; set; }
    public int G { get; set; }
    
    public bool IsBlack { get; set; }
    public bool IsWhite { get; set; }

    public Pixel(SKColor skColor)
    {
        R = skColor.Red;
        G = skColor.Green;
        B = skColor.Blue;
        IsBlack = (R == 0 && G == 0 && B == 0);
        IsWhite = (R == 255 && G == 255 && B == 255);
    }

    public SKColor ToSKColor()
    {
        var red = Convert.ToByte(R);
        var green = Convert.ToByte(G);
        var blue = Convert.ToByte(B);
        return new SKColor(red, green, blue);
    }
}