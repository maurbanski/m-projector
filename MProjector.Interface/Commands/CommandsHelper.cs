using System.CommandLine;
using MProjector.Graphics;
using MProjector.Logic.Projections;

namespace MProjector.Interface.Commands;

public static class CommandsHelper
{
    public static RootCommand AddSubcommands(this RootCommand rootCommand)
    {
        var lambertCommand = new LambertCommand(new LambertProjection(new Bitmap()));
        var robinsonCommand = new RobinsonCommand(new RobinsonProjection());
        var equirectangularCommand = new EquirectangularCommand(new EquirectangularProjection());
        
        rootCommand.Add(lambertCommand.Command);
        rootCommand.Add(robinsonCommand.Command);
        rootCommand.Add(equirectangularCommand.Command);

        return rootCommand;
    }
}