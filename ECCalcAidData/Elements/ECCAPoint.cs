using System;

namespace ECCalcAidData.Elements
{
    [Serializable]
    public class ECCAPoint
    {
        public double X;
        public double Y;
        public double Z;

        public ECCAPoint()
        {
            this.X = 0;
            this.Y = 0;
            this.Z = 0;
        }

        public ECCAPoint(double x, double y, double z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }
    }
}