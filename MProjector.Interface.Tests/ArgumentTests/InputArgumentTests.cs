using System.CommandLine;
using MProjector.Interface.Arguments;
using MProjector.Interface.Tests.CommandLine;
using NSubstitute;

namespace MProjector.Interface.Tests.ArgumentTests;

public class InputArgumentTests
{
    private readonly Argument<FileInfo> _sut;
    
    public InputArgumentTests()
    {
        _sut = Substitute.For<Argument<FileInfo>>();
    }
    
    [Fact]
    public void InputArgument_ShouldThrowError_IfInvalidFileSupplied()
    {
        Action act = () => _sut.Parse()
    }
}