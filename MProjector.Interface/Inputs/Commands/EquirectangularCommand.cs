using System.CommandLine;

namespace MProjector.Interface.Inputs.Commands;

public static class EquirectangularCommand
{
    private static Argument<string> InputProjectionArgument
    {
        get
        {
            var arg = new Argument<string>("input-projection");
            arg.AcceptOnlyFromAmong(CommandsHelper.GetAvailableCommands().Where(x => x != EquirectangularCommand.Name).ToArray());
            return arg;
        }
    }
    
    public static IEnumerable<Argument> Arguments => new List<Argument>
        { 
            InputProjectionArgument,
            CommonArguments.InputArgument, 
            CommonArguments.OutputArgument
        };
    
    public static string Name => "equirectangular";
    
    public static Command Command {
        get
        {
            var command = new Command(Name, "Converts map of specified projection to Equirectangular");
            foreach (var arg in Arguments) command.Add(arg);
            return command;
        } 
    }
}