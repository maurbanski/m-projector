namespace MProjector.Abstractions.V2.Logic;

public interface IMapPoint
{
    int X { get; }
    int Y { get; }
    
    int R { get; set; }
    int G { get; set; }
    int B { get; set; }
}