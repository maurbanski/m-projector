namespace MProjector.Domain.Maps;

public record MapPoint
{
    public MapPoint(int r, int g, int b)
    {
        R = r;
        G = g;
        B = b;
    }

    public MapPoint()
    {
        R = 255;
        G = 255;
        B = 255;
    }

    private int _r;
    public int R
    {
        get => _r;
        set => _r = value >= 0 && value <= 255
            ? value
            : throw new ArgumentOutOfRangeException($"R must be between 0 and 255 (provided: {value})");
    }
    
    private int _g;
    public int G
    {
        get => _g;
        set => _g = value >= 0 && value <= 255
            ? value
            : throw new ArgumentOutOfRangeException($"G must be between 0 and 255 (provided: {value})");
    }
    
    private int _b;
    public int B
    {
        get => _b;
        set => _b = value >= 0 && value <= 255
            ? value
            : throw new ArgumentOutOfRangeException($"B must be between 0 and 255 (provided: {value})");
    }

    public bool IsBlack => _r == 0 && _g == 0 && _b == 0;
    public bool IsWhite => _r == 255 && _g == 255 && _b == 255;
}