namespace MProjector.Abstractions.Graphics;

public interface IBitmap
{
    int Width { get; set; }
    int Height { get; set; }

    void Clear();
    IPixel GetPixel(int x, int y);
    void SetPixel(int x, int y, IPixel pixel);

    void LoadFromBytes(byte[] inputBytes);
    byte[] ToBytes();
}