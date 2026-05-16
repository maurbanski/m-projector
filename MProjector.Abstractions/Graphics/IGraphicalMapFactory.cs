using MProjector.Domain.Maps;

namespace MProjector.Abstractions.Graphics;

public interface IGraphicalMapFactory
{
    IGraphicalMap CreateFromFile(string filePath);
    IGraphicalMap CreateFromMap(Map map);
}