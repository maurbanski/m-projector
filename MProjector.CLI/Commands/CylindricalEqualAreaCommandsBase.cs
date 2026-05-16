using DotMake.CommandLine;
using Microsoft.Extensions.Logging;
using MProjector.Abstractions.Application;

namespace MProjector.CLI.Commands;

public abstract class CylindricalEqualAreaCommandsBase
{
    [CliArgument(Required = true, ValidationRules = CliValidationRules.ExistingFile)]
    public FileInfo Input { get; set; }
    
    [CliArgument(Required = true, ValidationRules = CliValidationRules.LegalPath & CliValidationRules.LegalFileName)]
    public FileInfo Output { get; set; }

    [CliOption(Required = false, Name = "lambda0", Description = "Central meridian (default = 0)")]
    public double Lambda0 { get; set; }
    
    public abstract double Phi0 { get; }
    
    public RootCliCommand RootCommand { get; set; }

    private readonly ILogger<CylindricalEqualAreaCommandsBase> _logger;
    private readonly ICylindricalEqualAreaService _cylindricalEqualAreaService;

    public CylindricalEqualAreaCommandsBase(ILogger<CylindricalEqualAreaCommandsBase> logger, ICylindricalEqualAreaService cylindricalEqualAreaService)
    {
        _logger = logger;
        _cylindricalEqualAreaService = cylindricalEqualAreaService;
    }

    public void Run(CliContext context)
    {
        RootCommand.SetLogging();

        if (Lambda0 > 180 || Lambda0 < -180)
        {
            _logger.LogError($"Supplied Lambda0 value must be between 180 and -180 degrees (provided: {Lambda0})");
            return;
        }

        try
        {
            _cylindricalEqualAreaService.Convert(Input.FullName, Output.FullName, Lambda0, Phi0);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
        }
    }
}