using Microsoft.Extensions.Logging;
using MProjector.Abstractions.Projections;
using MProjector.Domain.Maps;
using MProjector.Logic.Coordinates;

namespace MProjector.Logic.Projections;

public class CylindricalEqualAreaProjection : ProjectionBase, ICylindricalEqualAreaProjection
{
    private readonly ILogger<CylindricalEqualAreaProjection> _logger;
    
    public CylindricalEqualAreaProjection(ILogger<CylindricalEqualAreaProjection> logger)
    {
        _logger = logger;
    }

    public Map Convert(Map inputMap, double lambda0 = 0, double phi0 = 0)
    {
        var outputMap = new Map(inputMap.Width, inputMap.Height);
        
        _logger.LogInformation($"Calculating rX, rY");
        var rX = inputMap.Width / 6.28318530719;
        var rY = (double)inputMap.Height / 2;
        _logger.LogInformation($"Calculated rX = {rX}, rY = {rY}");
        
        var lambda0Rad = double.DegreesToRadians(lambda0);
        var phi0Rad = double.DegreesToRadians(phi0);
        
        _logger.LogInformation($"Iterating over bitmap");
        for (int i = 0; i < inputMap.Width; i++)
        {
            for (int j = 0; j < inputMap.Height; j++)
            {
                _logger.LogDebug($"Point ({i}, {j})");
                var inputMapCoordinates = new MapCoordinates(i, j);
                
                _logger.LogDebug($"Calculating cartesian coordinates");
                var cartesianCoordinates = new CartesianCoordinates(inputMapCoordinates, inputMap.Width, inputMap.Height);
                _logger.LogDebug($"Calculated x = {cartesianCoordinates.X}, y = {cartesianCoordinates.Y}");
                
                _logger.LogDebug($"Calculating geodetic coordinates");
                var geodeticCoordinates = new GeodeticCoordinates(cartesianCoordinates, inputMap.Width, inputMap.Height);
                _logger.LogDebug($"Calculated lambdaDeg = {geodeticCoordinates.LambdaDeg}, phiDeg = {geodeticCoordinates.PhiDeg}");
                
                _logger.LogDebug($"Calculating cylindrical coordinates (cartesian)");
                var cylindricalCoords = FindCylindricalCoords(geodeticCoordinates, rX, rY, lambda0Rad, phi0Rad);
                _logger.LogDebug($"Calculated x = {cylindricalCoords.X}, y = {cylindricalCoords.Y}");
                
                _logger.LogDebug($"Converting to map coordinates");
                var outputMapCoordinates = new MapCoordinates(cylindricalCoords, inputMap.Width, inputMap.Height);
                _logger.LogDebug($"Setting converted map point ({outputMapCoordinates.X}, {outputMapCoordinates.Y})");
                
                outputMap.SetPoint(outputMapCoordinates.X, outputMapCoordinates.Y, inputMap.GetPoint(i, j));
            }
        }
        
        _logger.LogInformation($"Removing horizontal bars");
        outputMap = ClearHorizontalDistorion(outputMap);
        
        _logger.LogInformation($"Removing vertical bars");
        outputMap = ClearVerticalDistortion(outputMap);

        return outputMap;
    }
    
    public CartesianCoordinates FindCylindricalCoords(GeodeticCoordinates geodeticCoordinates, double rX, double rY, double lambda0Rad, double phi0Rad)
    {
        _logger.LogDebug($"{phi0Rad}");
        _logger.LogDebug($"{Math.Cos(phi0Rad)}");
        var x = rX * Math.Cos(phi0Rad) * CircularShiftLambda(geodeticCoordinates.LambdaRad, lambda0Rad);
        var y = rY * Math.Sin(geodeticCoordinates.PhiRad)/Math.Cos(phi0Rad);
        
        return new CartesianCoordinates(x, y);
    }
}