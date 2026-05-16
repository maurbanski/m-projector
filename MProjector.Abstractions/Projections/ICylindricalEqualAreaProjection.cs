using MProjector.Domain.Maps;

namespace MProjector.Abstractions.Projections;

public interface ICylindricalEqualAreaProjection
{
    Map Convert(Map inputMap, double lambda0 = 0, double phi0 = 0);
}