using DotMake.CommandLine;
using Microsoft.Extensions.Logging;
using MProjector.Abstractions.Projections;

namespace MProjector.CLI.Commands;

[CliCommand(
    Name = "gall-orthographic", 
    Description = "Converts Equirectangular map to Gall Orthographic projection (Cylindrical Equal Area projection with standard parallel at 45 degrees)")
]
public class GallOrthographicCommand : CylindricalEqualAreaCommandsBase
{
    public GallOrthographicCommand(ILogger<GallOrthographicCommand> logger,
        ICylindricalEqualAreaProjection cylindricalEqualAreaProjection) : base(logger, cylindricalEqualAreaProjection) {}

    public override double Phi0 => 45;
}