namespace MProjector.Abstractions.Projections;

public interface IRobinsonProjection
{
    byte[] FromEquirectangular(byte[] inputBytes);
}