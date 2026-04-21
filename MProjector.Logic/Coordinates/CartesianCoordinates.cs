namespace MProjector.Logic.Coordinates
{
    public record CartesianCoordinates
    {
        public double X { get; set; }
        public double Y { get; set; }

        public CartesianCoordinates(double x, double y)
        {
            X = x;
            Y = y;
        }

        public CartesianCoordinates(BitmapPoint bitmapPoint, double xBound, double yBound)
        {
            if (Math.Abs(bitmapPoint.X) > xBound) throw new ArgumentException("X-coord out of bounds");
            if (Math.Abs(bitmapPoint.Y) > yBound) throw new ArgumentException("Y-coord out of bounds");

            X = bitmapPoint.X - xBound / 2;
            Y = yBound / 2 - bitmapPoint.Y;
        }

        public CartesianCoordinates(GeodeticCoordinates geodeticCoordinates, double xBound, double yBound)
        {
            X = geodeticCoordinates.LambdaDeg / (2 * 180 / xBound);
            Y = geodeticCoordinates.PhiDeg / (2 * 90 / yBound);
        }
    }
}
