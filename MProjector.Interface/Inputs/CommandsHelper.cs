using System.CommandLine;
using MProjector.Interface.Inputs.Commands;

namespace MProjector.Interface.Inputs;

public static class CommandsHelper
{
    public static IEnumerable<string> GetAvailableCommands()
    {
        return new List<string>
        {
            LambertCommand.Name,
            RobinsonCommand.Name,
            EquirectangularCommand.Name
        };
    }
    
    public static RootCommand AddSubCommands(this RootCommand rootCommand)
    {
        rootCommand.Add(LambertCommand.Command);
        rootCommand.Add(RobinsonCommand.Command);
        rootCommand.Add(EquirectangularCommand.Command);

        return rootCommand;
    }
}