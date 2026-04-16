using System.CommandLine;
using System.Reflection.Metadata;
using MProjector.Abstractions.Projections;
using MProjector.Interface.Arguments;

namespace MProjector.Interface.Commands;

public class LambertCommand
{
    public static string Name => "lambert";
    
    public readonly IEnumerable<Argument> Arguments;
    public readonly Command Command;

    private readonly ILambertProjection _projection;

    public LambertCommand(ILambertProjection projection)
    {
        _projection = projection;
        
        Arguments = new List<Argument>
        { 
            CommonArguments.InputArgument, 
            CommonArguments.OutputArgument
        };
        
        Command = new Command(Name, "Converts Equirectangular map to Lambert Cylindrical projection");
        foreach (var arg in Arguments) Command.Add(arg);
        Command.SetAction(HandleCommand);
    }
    
    public void HandleCommand(ParseResult parseResult)
    {
        var input = parseResult.GetValue<FileInfo>(CommonArguments.InputArgument.Name);
        var output = parseResult.GetValue<FileInfo>(CommonArguments.OutputArgument.Name);
        
        var inputBytes = File.ReadAllBytes(input.FullName);
        byte[] outputBytes = _projection.FromEquirectangular(inputBytes);
        File.WriteAllBytes(output.FullName, outputBytes);
    }
}