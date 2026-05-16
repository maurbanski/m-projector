using DotMake.CommandLine;
using Microsoft.Extensions.Logging;
using MProjector.Abstractions.Application;

namespace MProjector.CLI.Commands;

[CliCommand(
    Name = "gall-orthographic", 
    Description = "Converts Equirectangular map to Gall Orthographic projection (Cylindrical Equal Area projection with standard parallel at 45 degrees)")
]
public class GallOrthographicCommand : CylindricalEqualAreaCommandsBase
{
    public GallOrthographicCommand(ILogger<GallOrthographicCommand> logger,
        ICylindricalEqualAreaService cylindricalEqualAreaService) : base(logger, cylindricalEqualAreaService) {}

    public override double Phi0 => 45;
}