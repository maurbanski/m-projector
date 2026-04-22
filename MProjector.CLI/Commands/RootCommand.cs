using DotMake.CommandLine;

namespace MProjector.CLI.Commands;

[CliCommand(
    Description = "CLI app for converting map projections",
    Children = new []{ typeof(LambertCommand) }
)]
public class RootCliCommand()
{
    [CliOption(Description = "Show verbose output", Recursive = true)]
    public bool Verbose { get; set; } = false;

    [CliOption(Description = "Log to provided file", Recursive = true, Required = false, ValidationRules = CliValidationRules.LegalPath)]
    public string? LogFile { get; set; }
    
}