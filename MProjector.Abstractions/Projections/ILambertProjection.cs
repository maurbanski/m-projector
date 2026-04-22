namespace MProjector.Abstractions.Projections;

public interface ILambertProjection
{
    byte[] FromEquirectangular(byte[] inputBytes, bool verbose);
}