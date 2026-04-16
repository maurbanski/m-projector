namespace MProjector.Abstractions.Graphics;

public interface IPixel
{
    int R { get; set; }
    int B { get; set; }
    int G { get; set; }
    bool IsBlack { get; set; }
    bool IsWhite { get; set; }
}