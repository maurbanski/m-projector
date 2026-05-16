using MProjector.Domain.Maps;
using MProjector.Logic.Extensions;

namespace MProjector.Logic.Projections;

public abstract class ProjectionBase
{
    public Map ClearHorizontalDistorion(Map map)
    {
        for (int i = 1; i < map.Width - 1; i++)
        {
            for (int j = 1; j < map.Height - 1; j++)
            {
                var point = map.GetPoint(i, j);
                if (point.IsWhite)
                {
                    var above = map.GetPoint(i, j + 1);
                    var below = map.GetPoint(i, j - 1);
                    if (!above.IsWhite && !below.IsWhite)
                    {
                        point.R = (above.R + below.R) / 2;
                        point.G = (above.G + below.G) / 2;
                        point.B = (above.B + below.B) / 2;
                    }
                }
            }
        }

        return map;
    }

    public Map ClearVerticalDistortion(Map map)
    {
        for (int i = 1; i < map.Width - 1; i++)
        {
            for (int j = 1; j < map.Height - 1; j++)
            {
                var point = map.GetPoint(i, j);
                if (point.IsWhite)
                {
                    var left = map.GetPoint(i - 1, j);
                    var right = map.GetPoint(i + 1, j);
                    if (!left.IsWhite && !right.IsWhite)
                    {
                        point.R = (left.R + right.R) / 2;
                        point.G = (left.G + right.G) / 2;
                        point.B = (left.B + right.B) / 2;
                    }
                }
            }
        }

        return map;
    }

    public double CircularShiftLambda(double lambdaRad, double lambda0Rad) => 
        MathExtensions.Mod(lambdaRad + Math.PI - lambda0Rad, Math.PI * 2) - Math.PI;
}