using MProjector.Domain.Maps;

namespace MProjector.Abstractions.Graphics;

public interface IGraphicalMap
{
    int Width { get; set; }
    int Height { get; set; }
    
    Map ToMap();
    void Save(string outputPath);
}