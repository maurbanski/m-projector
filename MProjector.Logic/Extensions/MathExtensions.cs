namespace MProjector.Logic.Extensions;

public static class MathExtensions
{
    public static double Mod(double a, double n) => ((a % n) + n) % n;
    public static int Mod(int a, int n) => ((a % n) + n) % n;
    public static decimal Mod(decimal a, decimal n) => ((a % n) + n) % n;
}