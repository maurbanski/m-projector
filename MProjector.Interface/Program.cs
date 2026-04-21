using System.CommandLine;
using MProjector.Interface.Commands;
using MProjector.Interface.Options;
using NLog;
using NLog.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

class Program
{
    static int Main(string[] args)
    {
        
        
        
        ServiceCollection services = new();
        services.AddLogging()
        
        var logger = LogManager.Setup().LoadConfigurationFromSection(builder.Configuration).GetCurrentClassLogger();
        builder.Logging.ClearProviders();
        builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Debug);
        builder.Logging.AddNLog();
        
        var rootCommand = new RootCommand();
        rootCommand.Description = "CLI app for converting map projections";
        rootCommand.Options.Add(CommonOptions.LogFileOption);
        rootCommand.Options.Add(CommonOptions.VerboseOption);
        rootCommand.AddSubcommands();
        return rootCommand.Parse(args).Invoke();
    }
}


