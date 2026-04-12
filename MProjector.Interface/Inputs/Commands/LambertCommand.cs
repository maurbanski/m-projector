using System.CommandLine;

namespace MProjector.Interface.Inputs.Commands;

public static class LambertCommand
{
    public static IEnumerable<Argument> Arguments => new List<Argument>
        { CommonArguments.InputArgument, CommonArguments.OutputArgument };
    public static string Name => "lambert";
    
    public static Command Command {
        get
        {
            var command = new Command(Name, "Converts Equirectangular map to Lambert Cylindrical projection");
            foreach (var arg in Arguments) command.Add(arg);
            return command;
        } 
    }
}