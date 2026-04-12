using System.CommandLine;
using MProjector.Interface.Inputs;

class Program
{
    static int Main(string[] args)
    {
        var rootCommand = new RootCommand();
        rootCommand.AddSubCommands();
        rootCommand.Description = "CLI app for converting map projections";
        rootCommand.SetAction(parseResult =>
        {
            Console.WriteLine("Hello World");
            return 0;
        });

        
        ParseResult parseResult = rootCommand.Parse(args);
        Console.WriteLine(parseResult.Errors.Count());
        return parseResult.Invoke();
    }
}


