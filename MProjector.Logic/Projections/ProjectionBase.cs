using MProjector.Abstractions.Graphics;
using MProjector.Logic.ErrorHandling;

namespace MProjector.Logic.Projections;

public abstract class ProjectionBase
{
    public IBitmap Bitmap;

    public ProjectionBase(IBitmap bitmap)
    {
        Bitmap = bitmap;
    }
    
    public void ClearHorizontalBars()
    {
        if (Bitmap.Width == 0 || Bitmap.Height == 0) throw new DataNotLoadedException();
        
        for (int i = 1; i < Bitmap.Width - 1; i++)
        {
            for (int j = 1; j < Bitmap.Height - 1; j++)
            {
                var pixel = Bitmap.GetPixel(i, j);
                if (pixel.IsBlack)
                {
                    var above = Bitmap.GetPixel(i, j + 1);
                    var below = Bitmap.GetPixel(i, j - 1);
                    if (!above.IsBlack && !below.IsBlack)
                    {
                        pixel.R = (above.R + below.R) / 2;
                        pixel.G = (above.G + below.G) / 2;
                        pixel.B = (above.B + below.B) / 2;
                        Bitmap.SetPixel(i, j, pixel);
                    }
                }
            }
        }
    }

    public void ClearVerticalBars()
    {
        if (Bitmap.Width == 0 || Bitmap.Height == 0) throw new DataNotLoadedException();
        
        for (int i = 1; i < Bitmap.Width - 1; i++)
        {
            for (int j = 1; j < Bitmap.Height - 1; j++)
            {
                var pixel = Bitmap.GetPixel(i, j);
                if (pixel.IsBlack)
                {
                    var left = Bitmap.GetPixel(i - 1, j);
                    var right = Bitmap.GetPixel(i + 1, j);
                    if (!left.IsBlack && !right.IsBlack)
                    {
                        pixel.R = (left.R + right.R) / 2;
                        pixel.G = (left.G + right.G) / 2;
                        pixel.B = (left.B + right.B) / 2;
                        Bitmap.SetPixel(i, j, pixel);
                    }
                }
            }
        }
    }
}