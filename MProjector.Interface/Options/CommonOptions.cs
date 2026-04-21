using System.CommandLine;

namespace MProjector.Interface.Options;

public static class CommonOptions
{
    public static Option<bool> VerboseOption => new("--verbose", "-v")
    {
        Description = "Show verbose output",
        Recursive = true
    };

    public static Option<string> LogFileOption => new("--logfile")
    {
        Description = "File to log to",
        Recursive = true
    };
}