namespace MProjector.Logic.Points
{
    public record CartesianPoint
    {
        public double X { get; set; }
        public double Y { get; set; }

        public CartesianPoint(double x, double y)
        {
            X = x;
            Y = y;
        }

        public CartesianPoint(double lambda, double phi, double mapWidth, double mapHeight)
        {
            X = lambda / (2 * 180 / mapWidth) + mapWidth / 2;
            Y = phi / (2 * 90 / mapHeight) + mapHeight / 2;
        }
    }
}
