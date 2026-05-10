namespace MProjector.Abstractions.Projections;

public interface ICylindricalEqualAreaProjection
{
    void ConvertFromEquirectangular(FileInfo input, FileInfo output, double lambda0 = 0, double phi0 = 0);
}