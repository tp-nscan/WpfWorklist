namespace MathUtils.Geometry
{
    public struct Point3D : IPoint3D
    {
        public Point3D(double x, double y, double z)
        {
            _x = x;
            _y = y;
            _z = z;
        }

        private double _x;
        public double X
        {
            get { return _x; }
            set { _x = value; }
        }

        private double _y;  
        public double Y
        {
            get { return _y; }
            set { _y = value; }
        }

        private double _z;
        public double Z
        {
            get { return _z; }
            set { _z = value; }
        }
    }
}
