using MProjector.Logic.Extensions;

namespace MProjector.Logic.Tests.ExtensionsTests;

public class MathExtensionsTests
{
    [Fact]
    public void ModInt_GivenPositiveOverflow_CirclesBackToBeginning() => Assert.Equal(5, MathExtensions.Mod(25, 20));
    
    [Fact]
    public void ModInt_GivenNegativeOverflow_CirclesBackToEnd() => Assert.Equal(14, MathExtensions.Mod(-6, 20));

    [Fact]
    public void ModDouble_GivenPositiveOverflow_CirclesBackToBeginning() => Assert.Equal(5.5, MathExtensions.Mod(25.5, 20));

    [Fact]
    public void ModDouble_GivenNegativeOverflow_CirclesBackToEnd() => Assert.Equal(13.5, MathExtensions.Mod(-6.5, 20));
}