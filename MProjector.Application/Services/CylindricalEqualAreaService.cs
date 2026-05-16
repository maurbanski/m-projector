using Microsoft.Extensions.Logging;
using MProjector.Abstractions.Application;
using MProjector.Abstractions.Graphics;
using MProjector.Abstractions.Projections;

namespace MProjector.Application.Services;

public class CylindricalEqualAreaService : ICylindricalEqualAreaService
{
    private readonly ILogger<CylindricalEqualAreaService> _logger;
    private readonly ICylindricalEqualAreaProjection _projection;
    private readonly IGraphicalMapFactory _graphicalMapFactory;
    
    public CylindricalEqualAreaService(ILogger<CylindricalEqualAreaService> logger, ICylindricalEqualAreaProjection projection, IGraphicalMapFactory graphicalMapFactory)
    {
        _logger = logger;
        _projection = projection;
        _graphicalMapFactory = graphicalMapFactory;
    }
    
    public void Convert(string inputPath, string outputPath, double lambda0, double phi0)
    {
        _logger.LogInformation($"Loading graphics from file {inputPath}");
        var graphicalMap = _graphicalMapFactory.CreateFromFile(inputPath);
        
        _logger.LogInformation($"Converting graphical map to logical representation");
        var logicalMap = graphicalMap.ToMap();
        
        _logger.LogInformation($"Converting to cylindrical equal area projection");
        var convertedLogicalMap = _projection.Convert(logicalMap, lambda0, phi0);
        
        _logger.LogInformation($"Converting new cylindrical equal area map back to graphical representation");
        var convertedGraphicalMap = _graphicalMapFactory.CreateFromMap(convertedLogicalMap);
        
        _logger.LogInformation($"Saving output graphics to {outputPath}");
        convertedGraphicalMap.Save(outputPath);

    }
}