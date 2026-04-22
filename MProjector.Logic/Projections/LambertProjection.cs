using Microsoft.Extensions.Logging;
using MProjector.Abstractions.Graphics;
using MProjector.Abstractions.Projections;
using MProjector.Logic.Coordinates;

namespace MProjector.Logic.Projections;

public class LambertProjection : ProjectionBase, ILambertProjection
{
    private readonly ILogger<LambertProjection> _logger;
    private double _lambda0 => 0;
    private double _phi0 => 0;

    public LambertProjection(IBitmap bitmap, ILogger<LambertProjection> logger) : base(bitmap)
    {
        _logger = logger;
    }
    
    public byte[] FromEquirectangular(byte[] inputBytes, bool verbose)
    {
        Bitmap.Clear();
        if (verbose) _logger.LogInformation("Loading bitmap from bytes");
        Bitmap.LoadFromBytes(inputBytes);
        
        if (verbose) _logger.LogInformation($"Calculating rX, rY based on map dimensions ({Bitmap.Width}, {Bitmap.Height})");
        var rX = Bitmap.Width / 6.28318530719;
        var rY = (double)(Bitmap.Height / 2);
        if (verbose) _logger.LogInformation($"Calculated rX = {rX}, rY = {rY}");
        
        if (verbose) _logger.LogInformation($"Setting x and y absolute boundaries for cartesian coordinates");
        var xBound = Bitmap.Width - 1;
        var yBound = Bitmap.Height - 1;
        if (verbose) _logger.LogInformation($"Set xBound = {xBound}, yBound = {yBound}");

        if (verbose) _logger.LogInformation($"Iterating over bitmap");
        for (int i = 0; i < Bitmap.Width; i++)
        {
            for (int j = 0; j < Bitmap.Height; j++)
            {
                if (verbose) _logger.LogInformation($"Point ({i}, {j})");

                var sourceBitmapPoint = new BitmapPoint(i, j);
                
                if (verbose) _logger.LogInformation($"Calculating cartesian coordinates");
                var cartesianCoordinates = new CartesianCoordinates(sourceBitmapPoint, xBound, yBound);
                if (verbose) _logger.LogInformation($"Calculated x = {cartesianCoordinates.X}, y = {cartesianCoordinates.Y}");

                if (verbose) _logger.LogInformation($"Calculating geodetic coordinates");
                var geodeticCoordinates = new GeodeticCoordinates(cartesianCoordinates, xBound, yBound);
                if (verbose) _logger.LogInformation($"Calculated lambdaDeg = {geodeticCoordinates.LambdaDeg}, phiDeg = {geodeticCoordinates.PhiDeg}");

                if (verbose) _logger.LogInformation($"Calculating lambert coordinates (cartesian)");
                var lambertCoords = FindLambertCoords(geodeticCoordinates, rX, rY);
                if (verbose) _logger.LogInformation($"Calculated x = {lambertCoords.X}, y = {lambertCoords.Y}");

                if (verbose) _logger.LogInformation($"Converting to bitmap point");
                var bitmapPoint = new BitmapPoint(lambertCoords, xBound, yBound);
                if (verbose) _logger.LogInformation($"Setting converted bitmap point ({bitmapPoint.X}, {bitmapPoint.Y})");

                Bitmap.SetPixel(bitmapPoint.X, bitmapPoint.Y, Bitmap.GetPixel(i, j));
            }
        }

        if (verbose) _logger.LogInformation($"Removing horizontal bars");
        ClearHorizontalBars();
        
        if (verbose) _logger.LogInformation($"Removing vertical bars");
        ClearVerticalBars();

        if (verbose) _logger.LogInformation($"Saving bitmap to bytes");
        return Bitmap.ToBytes();
    }

    public CartesianCoordinates FindLambertCoords(GeodeticCoordinates geodeticCoordinates, double rX, double rY)
    {
        var x = rX*Math.Cos(_phi0) * (geodeticCoordinates.LambdaRad - _lambda0);
        var y = rY*Math.Sin(geodeticCoordinates.PhiRad)/Math.Cos(_phi0);

        return new CartesianCoordinates(x, y);
    }
}