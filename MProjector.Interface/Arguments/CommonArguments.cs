using System.CommandLine;

namespace MProjector.Interface.Arguments;

public static class CommonArguments
{
    public static Argument<FileInfo> InputArgument
    {
        get
        {
            var arg = new Argument<FileInfo>("input");
            arg.AcceptExistingOnly();
            return arg;
        }
    }
    
    public static Argument<FileInfo> OutputArgument
    {
        get
        {
            var arg = new Argument<FileInfo>("output");
            arg.AcceptExistingOnly();
            return arg;
        }
    }
}