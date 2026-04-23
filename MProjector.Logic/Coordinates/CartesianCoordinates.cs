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

        public CartesianCoordinates(BitmapPoint bitmapPoint, double width, double height)
        {
            if (Math.Abs(bitmapPoint.X) > width) throw new ArgumentException("X-coord out of bounds");
            if (Math.Abs(bitmapPoint.Y) > height) throw new ArgumentException("Y-coord out of bounds");

            X = bitmapPoint.X - width / 2;
            Y = height / 2 - bitmapPoint.Y;
        }

        public CartesianCoordinates(GeodeticCoordinates geodeticCoordinates, double width, double height)
        {
            X = geodeticCoordinates.LambdaDeg / (2 * 180 / width);
            Y = geodeticCoordinates.PhiDeg / (2 * 90 / height);
        }
    }
}
