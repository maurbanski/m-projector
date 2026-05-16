using DotMake.CommandLine;
using Microsoft.Extensions.Logging;
using MProjector.Abstractions.Application;

namespace MProjector.CLI.Commands;

[CliCommand(
    Name = "behrmann", 
    Description = "Converts Equirectangular map to Behrmann projection (Cylindrical Equal Area projection with standard parallel at 30 degrees)")
]
public class BehrmannCommand : CylindricalEqualAreaCommandsBase
{
    public BehrmannCommand(ILogger<BehrmannCommand> logger,
        ICylindricalEqualAreaService cylindricalEqualAreaService) : base(logger, cylindricalEqualAreaService) {}

    public override double Phi0 => 30;
}