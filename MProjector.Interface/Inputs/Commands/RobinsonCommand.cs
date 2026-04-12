using System.CommandLine;

namespace MProjector.Interface.Inputs.Commands;

public static class RobinsonCommand
{
    public static IEnumerable<Argument> Arguments => new List<Argument>
        { CommonArguments.InputArgument, CommonArguments.OutputArgument };
    public static string Name => "robinson";
    
    public static Command Command {
        get
        {
            var command = new Command(Name, "Converts Equirectangular map to Robinson projection");
            foreach (var arg in Arguments) command.Add(arg);
            return command;
        } 
    }
}