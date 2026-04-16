using System.CommandLine;
using MProjector.Interface.Arguments;
using MProjector.Abstractions.Projections;

namespace MProjector.Interface.Commands;

public class EquirectangularCommand
{
    public static string Name => "equirectangular";
    
    public readonly IEnumerable<Argument> Arguments;
    public readonly Command Command;

    private readonly IEquirectangularProjection _projection;
    
    public EquirectangularCommand(IEquirectangularProjection projection)
    {
        _projection = projection;
        
        var inputProjectionArgument = new Argument<string>("input-projection");
        var inputProjections = new[] { EquirectangularCommand.Name };
        inputProjectionArgument.AcceptOnlyFromAmong(inputProjections);
        Arguments = new List<Argument>
        { 
            inputProjectionArgument,
            CommonArguments.InputArgument, 
            CommonArguments.OutputArgument
        };
        
        Command = new Command(Name, "Converts map of specified projection to Equirectangular");
        foreach (var arg in Arguments) Command.Add(arg);
    }

    public void HandleCommand(FileSystemInfo input, FileSystemInfo output, string inputProjection)
    {
        var inputBytes = File.ReadAllBytes(input.FullName);
        byte[] outputBytes;

        switch (inputProjection)
        {
            case "lambert":
                outputBytes = _projection.FromLambert(inputBytes);
                break;
            case "robinson":
                outputBytes = _projection.FromRobinson(inputBytes);
                break;
            default:
                throw new ArgumentException();
        }
        
        File.WriteAllBytes(output.FullName, outputBytes);
    }
}