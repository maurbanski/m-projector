using MProjector.Abstractions.Graphics;
using MProjector.Logic.Projections;
using NSubstitute;

namespace MProjector.Logic.Tests.ProjectionTests;

public class ProjectionBaseTests
{
    private readonly ProjectionBase _sut;
    
    public ProjectionBaseTests()
    {
        var bitmapMock = Substitute.For<IBitmap>();
        _sut = Substitute.For<ProjectionBase>(bitmapMock, bitmapMock);
    }
    
    [Fact]
    public void CircularShiftLambda_GivenPositiveLambda0_ShiftsCoordinatesCorrectly()
    {
        var lambda0 = 160;
        var inputOutputPairs = new Dictionary<double, double>
        {
            { 50, -110 },
            { 110, -50 },
            { -30, 170 },
            { -70, 130 }
        };

        foreach (var pair in inputOutputPairs)
        {
            Assert.Equal(double.DegreesToRadians(pair.Value),
                _sut.CircularShiftLambda(double.DegreesToRadians(pair.Key), double.DegreesToRadians(lambda0)),
                0.001);
        }
    }
    
    [Fact]
    public void CircularShiftLambda_GivenNegativeLambda0_ShiftsCoordinatesCorrectly()
    {
        var lambda0 = -160;
        var inputOutputPairs = new Dictionary<double, double>
        {
            { -170, -10 },
            { -90, 70 },
            { 20, -180 },
            { 90, -110 }
        };

        foreach (var pair in inputOutputPairs)
        {
            Assert.Equal(double.DegreesToRadians(pair.Value),
                _sut.CircularShiftLambda(double.DegreesToRadians(pair.Key), double.DegreesToRadians(lambda0)),
                0.001);
        }
    }
}