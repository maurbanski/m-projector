namespace MProjector.Logic.Points
{
    public record GeodeticPoint
    {
        private double _lambda;
        private double _phi;

        public double LambdaDeg { get { return _lambda; } }
        public double PhiDeg { get { return _phi; } }

        public double LambdaRad { get { return _lambda * (Math.PI / 180); } }
        public double PhiRad { get { return _phi * (Math.PI / 180); } }

        public GeodeticPoint(double lambda, double phi)
        {
            _lambda = lambda;
            _phi = phi;
        }

        public GeodeticPoint(double x, double y, double mapWidth, double mapHeight)
        {
            _lambda = (x - mapWidth / 2) * (2 * 180.00 / mapWidth);
            _phi = (y - mapHeight / 2) * (2 * 90.00 / mapHeight);
        }
    }
}
