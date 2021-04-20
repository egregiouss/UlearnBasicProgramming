using System;
namespace DistanceTask
{
    public static class DistanceTask
    {
        public static double GetDistanceToSegment(double ax, double ay, double bx, double by, double x, double y)
        {
            double ac = GetDistance( x, y,  ax, ay);
            double bc = GetDistance( x, y,  bx, by);
            double ab = GetDistance(ax, ay, bx, by);

            double scalACAB = GetScalarProduct(x, y, ax, ay, bx, by); 
            double scalBCAB = GetScalarProduct(x, y, bx, by, ax, ay);

            if (ab == 0.0) return ac;
            if (scalACAB >= 0 && scalBCAB >= 0)
                return GetDistanceThroughSquare(ab, ac, bc);
            if (scalACAB < 0 || scalBCAB < 0)
                return Math.Min(ac, bc);
            return 0;
        }

        public static double GetDistance(double x, double y, double bx, double by)
        {
            return Math.Sqrt((x - bx) * (x - bx) + (y - by) * (y - by));
        }

        public static double GetScalarProduct(double x, double y, double ax, double ay, double bx, double by)
        {
            return (x - ax) * (bx - ax) + (y - ay) * (by - ay);
        }

        public static double GetDistanceThroughSquare(double ab, double ac, double bc)
        {
            double p = (ac + bc + ab) / 2.0;
            double s = Math.Sqrt(Math.Abs(p * (p - ac) * (p - bc) * (p - ab)));
            return (2.0 * s) / ab;
        }
    }
}
