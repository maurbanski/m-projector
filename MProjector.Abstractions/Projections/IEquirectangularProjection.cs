namespace MProjector.Abstractions.Projections;

public interface IEquirectangularProjection
{
    byte[] FromLambert(byte[] inputBytes);
    byte[] FromRobinson(byte[] inputBytes);
}