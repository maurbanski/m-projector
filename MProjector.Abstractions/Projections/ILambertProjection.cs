namespace MProjector.Abstractions.Projections;

public interface ILambertProjection
{
    void ConvertFromEquirectangular(FileInfo input, FileInfo output, double lambda0 = 0, double phi0 = 0);
}