using DotMake.CommandLine;
using Microsoft.Extensions.Logging;
using MProjector.Abstractions.Application;

namespace MProjector.CLI.Commands;

[CliCommand(
    Name = "lambert", 
    Description = "Converts Equirectangular map to Lambert projection (Cylindrical Equal Area projection with standard parallel at 0 degrees)")
]
public class LambertCommand : CylindricalEqualAreaCommandsBase
{
    public LambertCommand(ILogger<LambertCommand> logger, 
        ICylindricalEqualAreaService cylindricalEqualAreaService) : base(logger, cylindricalEqualAreaService) {}

    public override double Phi0 => 0;
}