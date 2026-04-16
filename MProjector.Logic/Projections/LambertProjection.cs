using MProjector.Abstractions.Graphics;
using MProjector.Abstractions.Projections;
using MProjector.Logic.Points;

namespace MProjector.Logic.Projections;

public class LambertProjection : ProjectionBase, ILambertProjection
{
    private double _lambda0 => 0;
    private double _phi0 => 0;
    
    public LambertProjection(IBitmap bitmap) : base(bitmap) {}
    
    public byte[] FromEquirectangular(byte[] inputBytes)
    {
        Bitmap.Clear();
        Bitmap.LoadFromBytes(inputBytes);
        
        var rX = Bitmap.Width / 6.28318530719;
        var rY = (double)(Bitmap.Height / 2);
        
        for (int i = 0; i < Bitmap.Width; i++)
        {
            for (int j = 0; j < Bitmap.Height; j++)
            {
                var geodeticPoint = new GeodeticPoint(i, j, Bitmap.Width, Bitmap.Height);
                var lambertCoords = FindLambertCoords(geodeticPoint, rX, rY);
                var bitmapPoint = new BitmapPoint(lambertCoords, Bitmap.Width, Bitmap.Height);
                Bitmap.SetPixel(bitmapPoint.X, bitmapPoint.Y, Bitmap.GetPixel(i, j));
            }
        }

        ClearHorizontalBars();
        ClearVerticalBars();

        return Bitmap.ToBytes();
    }

    public CartesianPoint FindLambertCoords(GeodeticPoint geodeticPoint, double rX, double rY)
    {
        var x = rX*Math.Cos(_phi0) * (geodeticPoint.LambdaRad - _lambda0);
        var y = rY*Math.Sin(geodeticPoint.PhiRad)/Math.Cos(_phi0);

        return new CartesianPoint(x, y);
    }
}