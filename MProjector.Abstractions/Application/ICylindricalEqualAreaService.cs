namespace MProjector.Abstractions.Application;

public interface ICylindricalEqualAreaService
{
    void Convert(string inputPath, string outputPath, double lambda0, double phi0);
}