using DotMake.CommandLine;
using MProjector.CLI.Verbosity;
using NLog;

namespace MProjector.CLI.Commands;

[CliCommand(
    Description = "CLI app for converting map projections",
    Children = new []{ typeof(LambertCommand) }
)]
public class RootCliCommand()
{
    [CliOption(Description = "Set verbosity", Recursive = true)]
    public VerbosityLevel Verbosity { get; set; } = VerbosityLevel.Normal;

    [CliOption(Description = "Log to provided file", Recursive = true, Required = false, ValidationRules = CliValidationRules.LegalPath)]
    public string? LogFile { get; set; }
    
    public void SetLogging()
    {
        LogLevel logLevel = Verbosity switch
        {
            VerbosityLevel.Quiet => LogLevel.Off,
            VerbosityLevel.Normal => LogLevel.Info,
            VerbosityLevel.Diagnostic => LogLevel.Debug
        };
        
        var consoleRule = LogManager.Configuration.LoggingRules.Where(x => x.RuleName == "console").First();
        consoleRule.EnableLoggingForLevels(logLevel, LogLevel.Off);
        LogManager.ReconfigExistingLoggers();
        
        if (LogFile != null)
        {
            GlobalDiagnosticsContext.Set("logToFile", true);
            GlobalDiagnosticsContext.Set("logFile", LogFile);
        }
    }
    
}