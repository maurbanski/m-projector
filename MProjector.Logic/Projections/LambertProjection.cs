using Microsoft.Extensions.Logging;
using MProjector.Abstractions.Graphics;
using MProjector.Abstractions.Projections;
using MProjector.Logic.Coordinates;

namespace MProjector.Logic.Projections;

public class LambertProjection : ProjectionBase, ILambertProjection
{
    private readonly ILogger<LambertProjection> _logger;
    
    public LambertProjection(IBitmap inputBitmap, IBitmap outputBitmap, ILogger<LambertProjection> logger) : base(inputBitmap, outputBitmap)
    {
        _logger = logger;
    }
    
    public void ConvertFromEquirectangular(FileInfo input, FileInfo output, double lambda0 = 0, double phi0 = 0)
    {
        _logger.LogInformation("Loading input bitmap from file");
        InputBitmap.FromFile(input.FullName);

        _logger.LogInformation("Initialising empty output bitmap");
        OutputBitmap.InitialiseEmpty(InputBitmap.Width, InputBitmap.Height);
        
        _logger.LogInformation($"Calculating rX, rY based on map dimensions ({InputBitmap.Width}, {InputBitmap.Height})");
        var rX = InputBitmap.Width / 6.28318530719;
        var rY = (double)(InputBitmap.Height / 2);
        _logger.LogInformation($"Calculated rX = {rX}, rY = {rY}");
        
        var lambda0Rad = double.DegreesToRadians(lambda0);
        var phi0Rad = double.DegreesToRadians(phi0);
        
        _logger.LogInformation($"Iterating over bitmap");
        for (int i = 0; i < InputBitmap.Width; i++)
        {
            for (int j = 0; j < InputBitmap.Height; j++)
            {
                _logger.LogDebug($"Point ({i}, {j})");
                var sourceBitmapPoint = new BitmapPoint(i, j);
                
                _logger.LogDebug($"Calculating cartesian coordinates");
                var cartesianCoordinates = new CartesianCoordinates(sourceBitmapPoint, InputBitmap.Width, InputBitmap.Height);
                _logger.LogDebug($"Calculated x = {cartesianCoordinates.X}, y = {cartesianCoordinates.Y}");
                
                _logger.LogDebug($"Calculating geodetic coordinates");
                var geodeticCoordinates = new GeodeticCoordinates(cartesianCoordinates, InputBitmap.Width, InputBitmap.Height);
                _logger.LogDebug($"Calculated lambdaDeg = {geodeticCoordinates.LambdaDeg}, phiDeg = {geodeticCoordinates.PhiDeg}");
                
                _logger.LogDebug($"Calculating lambert coordinates (cartesian)");
                var lambertCoords = FindLambertCoords(geodeticCoordinates, rX, rY, lambda0Rad, phi0Rad);
                _logger.LogDebug($"Calculated x = {lambertCoords.X}, y = {lambertCoords.Y}");
                
                _logger.LogDebug($"Converting to bitmap point");
                var bitmapPoint = new BitmapPoint(lambertCoords, InputBitmap.Width, InputBitmap.Height);
                _logger.LogDebug($"Setting converted bitmap point ({bitmapPoint.X}, {bitmapPoint.Y})");
                
                OutputBitmap.SetPixel(bitmapPoint.X, bitmapPoint.Y, InputBitmap.GetPixel(i, j));
            }
        }
        
        _logger.LogInformation($"Removing horizontal bars");
        ClearHorizontalBars();
        
        _logger.LogInformation($"Removing vertical bars");
        ClearVerticalBars();
        
        _logger.LogInformation($"Saving output bitmap to file");
        OutputBitmap.Save(output.FullName);
    }
    
    public CartesianCoordinates FindLambertCoords(GeodeticCoordinates geodeticCoordinates, double rX, double rY, double lambda0Rad, double phi0Rad)
    {
        _logger.LogDebug($"{CircularShiftLambda(geodeticCoordinates.LambdaRad, lambda0Rad)}");
        
        var x = rX * Math.Cos(phi0Rad) * CircularShiftLambda(geodeticCoordinates.LambdaRad, lambda0Rad);
        var y = rY * Math.Sin(geodeticCoordinates.PhiRad)/Math.Cos(phi0Rad);
        
        return new CartesianCoordinates(x, y);
    }
}