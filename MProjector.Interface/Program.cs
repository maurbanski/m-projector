using System.CommandLine;
using MProjector.Interface.Commands;

class Program
{
    static int Main(string[] args)
    {
        var rootCommand = new RootCommand();
        rootCommand.Description = "CLI app for converting map projections";
        rootCommand.AddSubcommands();
        return rootCommand.Parse(args).Invoke();
    }
}


