namespace MProjector.Interface.Tests.CommandLine;

public class CommandLineInterface : ICommandLineInterface
{
    public string[] GetCommandLineArgs()
    {
        return Environment.GetCommandLineArgs();
    }
}