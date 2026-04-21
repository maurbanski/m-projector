using MProjector.Abstractions.Graphics;
using MProjector.Abstractions.Projections;
using MProjector.Graphics;
using MProjector.Logic.Coordinates;

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

        var xBound = Bitmap.Width - 1;
        var yBound = Bitmap.Height - 1;
        
        for (int i = 0; i < Bitmap.Width; i++)
        {
            for (int j = 0; j < Bitmap.Height; j++)
            {
                var sourceBitmapPoint = new BitmapPoint(i, j);
                var cartesianCoordinates = new CartesianCoordinates(sourceBitmapPoint, xBound, yBound);
                var geodeticCoordinates = new GeodeticCoordinates(cartesianCoordinates, xBound, yBound);
                var lambertCoords = FindLambertCoords(geodeticCoordinates, rX, rY);
                Console.WriteLine(geodeticCoordinates.LambdaDeg + "|" + geodeticCoordinates.PhiDeg);
                Console.WriteLine(lambertCoords.X + "|" + lambertCoords.Y);
                var bitmapPoint = new BitmapPoint(lambertCoords, xBound, yBound);
                Bitmap.SetPixel(bitmapPoint.X, bitmapPoint.Y, Bitmap.GetPixel(i, j));
            }
        }

        ClearHorizontalBars();
        ClearVerticalBars();

        return Bitmap.ToBytes();
    }

    public CartesianCoordinates FindLambertCoords(GeodeticCoordinates geodeticCoordinates, double rX, double rY)
    {
        var x = rX*Math.Cos(_phi0) * (geodeticCoordinates.LambdaRad - _lambda0);
        var y = rY*Math.Sin(geodeticCoordinates.PhiRad)/Math.Cos(_phi0);

        return new CartesianCoordinates(x, y);
    }
}