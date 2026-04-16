using System.CommandLine;
using MProjector.Abstractions.Projections;
using MProjector.Interface.Arguments;

namespace MProjector.Interface.Commands;

public class RobinsonCommand
{
    public static string Name => "robinson";
    
    public readonly IEnumerable<Argument> Arguments;
    public readonly Command Command;

    private readonly IRobinsonProjection _projection;

    public RobinsonCommand(IRobinsonProjection projection)
    {
        _projection = projection;
        
        Arguments = new List<Argument>
        { 
            CommonArguments.InputArgument, 
            CommonArguments.OutputArgument
        };
        
        Command = new Command(Name, "Converts Equirectangular map to Robinson projection");
        foreach (var arg in Arguments) Command.Add(arg);
    }
    
    public void HandleCommand(FileSystemInfo input, FileSystemInfo output)
    {
        var inputBytes = File.ReadAllBytes(input.FullName);
        byte[] outputBytes = _projection.FromEquirectangular(inputBytes);
        File.WriteAllBytes(output.FullName, outputBytes);
    }
}