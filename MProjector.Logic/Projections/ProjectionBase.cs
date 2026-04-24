using MProjector.Abstractions.Graphics;
using MProjector.Logic.ErrorHandling;
using MProjector.Logic.Extensions;

namespace MProjector.Logic.Projections;

public abstract class ProjectionBase
{
    public IBitmap InputBitmap;
    public IBitmap OutputBitmap;

    public ProjectionBase(IBitmap inputBitmap, IBitmap outputBitmap)
    {
        InputBitmap = inputBitmap;
        OutputBitmap = outputBitmap;
    }
    
    public void ClearHorizontalBars()
    {
        if (OutputBitmap.Width == 0 || OutputBitmap.Height == 0) throw new DataNotLoadedException();
        
        for (int i = 1; i < OutputBitmap.Width - 1; i++)
        {
            for (int j = 1; j < OutputBitmap.Height - 1; j++)
            {
                var pixel = OutputBitmap.GetPixel(i, j);
                if (pixel.IsBlack)
                {
                    var above = OutputBitmap.GetPixel(i, j + 1);
                    var below = OutputBitmap.GetPixel(i, j - 1);
                    if (!above.IsBlack && !below.IsBlack)
                    {
                        pixel.R = (above.R + below.R) / 2;
                        pixel.G = (above.G + below.G) / 2;
                        pixel.B = (above.B + below.B) / 2;
                        OutputBitmap.SetPixel(i, j, pixel);
                    }
                }
            }
        }
    }

    public void ClearVerticalBars()
    {
        if (OutputBitmap.Width == 0 || OutputBitmap.Height == 0) throw new DataNotLoadedException();
        
        for (int i = 1; i < OutputBitmap.Width - 1; i++)
        {
            for (int j = 1; j < OutputBitmap.Height - 1; j++)
            {
                var pixel = OutputBitmap.GetPixel(i, j);
                if (pixel.IsBlack)
                {
                    var left = OutputBitmap.GetPixel(i - 1, j);
                    var right = OutputBitmap.GetPixel(i + 1, j);
                    if (!left.IsBlack && !right.IsBlack)
                    {
                        pixel.R = (left.R + right.R) / 2;
                        pixel.G = (left.G + right.G) / 2;
                        pixel.B = (left.B + right.B) / 2;
                        OutputBitmap.SetPixel(i, j, pixel);
                    }
                }
            }
        }
    }

    public double CircularShiftLambda(double lambdaRad, double lambda0Rad) => 
        MathExtensions.Mod(lambdaRad + Math.PI - lambda0Rad, Math.PI * 2) - Math.PI;
}