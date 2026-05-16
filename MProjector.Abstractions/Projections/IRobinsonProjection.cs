using MProjector.Domain.Maps;

namespace MProjector.Abstractions.Projections;

public interface IRobinsonProjection
{
    Map Convert(Map inputMap);
}