using DotMake.CommandLine;
using Microsoft.Extensions.Logging;
using MProjector.Abstractions.Projections;

namespace MProjector.CLI.Commands;

[CliCommand(
    Name = "lambert", 
    Description = "Converts Equirectangular map to Lambert Cylindrical projection")
]
public class LambertCommand
{
    [CliArgument(Required = true, ValidationRules = CliValidationRules.ExistingFile)]
    public FileInfo Input { get; set; }
    
    [CliArgument(Required = true, ValidationRules = CliValidationRules.ExistingFile)]
    public FileInfo Output { get; set; }
    
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
        if (RootCommand.LogFile != null)
        {
            NLog.GlobalDiagnosticsContext.Set("logToFile", true);
            NLog.GlobalDiagnosticsContext.Set("logFile", RootCommand.LogFile);
        }
        
        _logger.LogInformation($"Reading file {Input.FullName}...");
        var inputBytes = File.ReadAllBytes(Input.FullName);
        
        _logger.LogInformation($"Converting map to Lambert projection...");
        var outputBytes = _projection.FromEquirectangular(inputBytes, RootCommand.Verbose);
        
        _logger.LogInformation($"Saving map to {Output.FullName}...");
        File.WriteAllBytes(Output.FullName, outputBytes);
        
        _logger.LogInformation($"File saved");
    }
}