namespace MProjector.Logic.Coordinates
{
    public record GeodeticCoordinates
    {
        private double _lambda;
        private double _phi;

        public double LambdaDeg { get { return _lambda; } }
        public double PhiDeg { get { return _phi; } }

        public double LambdaRad { get { return _lambda * (Math.PI / 180); } }
        public double PhiRad { get { return _phi * (Math.PI / 180); } }

        public GeodeticCoordinates(double lambda, double phi)
        {
            if (lambda > 180 || lambda < -180) throw new ArgumentException("Lambda out of bounds");
            if (phi > 90 || phi < -90) throw new ArgumentException("Phi out of bounds");
            
            _lambda = lambda;
            _phi = phi;
        }

        public GeodeticCoordinates(CartesianCoordinates cartesianCoordinates, double width, double height)
        {
            _lambda = cartesianCoordinates.X * (2 * 180.00 / width);
            _phi = cartesianCoordinates.Y * (2 * 90.00 / height);
            
            if (Math.Abs(_lambda) > 180) throw new ArgumentException("X-coord out of bounds");
            if (Math.Abs(_phi) > 90) throw new ArgumentException("Y-coord out of bounds");
        }
    }
}
