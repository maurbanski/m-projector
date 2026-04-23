using DotMake.CommandLine;
using Microsoft.Extensions.Logging;
using MProjector.Abstractions.Projections;
using NLog;

namespace MProjector.CLI.Commands;

[CliCommand(
    Name = "lambert", 
    Description = "Converts Equirectangular map to Lambert Cylindrical projection")
]
public class LambertCommand
{
    [CliArgument(Required = true, ValidationRules = CliValidationRules.ExistingFile)]
    public FileInfo Input { get; set; }
    
    [CliArgument(Required = true, ValidationRules = CliValidationRules.LegalPath & CliValidationRules.LegalFileName)]
    public FileInfo Output { get; set; }

    [CliOption(Required = false, Name = "lambda0", Description = "Central meridian (default = 0)")]
    public double Lambda0 { get; set; }

    [CliOption(Required = false, Name = "phi0", Description = "Standard parallel (default = 0 = equator)")]
    public double Phi0 { get; set; }
    
    public RootCliCommand RootCommand { get; set; }

    private readonly ILogger<LambertCommand> _logger;
    private readonly ILambertProjection _projection;

    public LambertCommand(ILogger<LambertCommand> logger, ILambertProjection lambertProjection)
    {
        _logger = logger;
        _projection = lambertProjection;
    }
    
    public void Run(CliContext context)
    {
        RootCommand.SetLogging();

        if (Lambda0 > 180 || Lambda0 < -180)
        {
            _logger.LogError($"Supplied Lambda0 value must be between 180 and -180 degrees (provided: {Lambda0})");
            return;
        }

        if (Phi0 > 90 || Phi0 < -90)
        {
            _logger.LogError($"Supplied Phi0 value must be between 90 and -90 degrees (provided: {Phi0})");
            return;
        }

        try
        {
            _projection.ConvertFromEquirectangular(Input, Output, Lambda0, Phi0);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
        }
    }
}