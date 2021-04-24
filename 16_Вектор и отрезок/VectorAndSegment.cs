using System;

namespace GeometryTasks
{
    public class Vector
    {
        public double X;
        public double Y;

        public double GetLength()
        {
            return Geometry.GetLength(this);
        }

        public Vector Add(Vector vector2)
        {
            return Geometry.Add(this, vector2);
        }

        public bool Belongs(Segment segment)
        {
            return Geometry.IsVectorInSegment(this, segment);
        }
    }

    public class Segment
    {
        public Vector Begin;
        public Vector End;

        public bool Contains(Vector vector)
        {
            return Geometry.IsVectorInSegment(vector, this);
        }
    }

    public class Geometry
    {
        public static double GetLength(Segment segment)
        {
            return GetLengthBetweenTwoPoints(segment.End.X, segment.Begin.X, 
                             				 segment.End.Y, segment.Begin.Y);
        }
		
		private static double GetLengthBetweenTwoPoints(double endX, double beginX, 
														double endY, double beginY)
		{
			return Math.Sqrt((endX - beginX) * (endX - beginX) + 
                             (endY - beginY) * (endY - beginY));
		}
		
        public static double GetLength(Vector vector)
        {
            return GetLengthBetweenTwoPoints(vector.X, 0, vector.Y, 0);
        }
		
        public static bool IsVectorInSegment(Vector vector, Segment segment)
        {
            var sgm1 = new Segment() { Begin = segment.Begin, End = vector };
            var sgm2 = new Segment() { Begin = vector, End = segment.End };
            return GetLength(segment) == GetLength(sgm1) + GetLength(sgm2);
        }

        public static Vector Add(Vector vector1, Vector vector2)
        {
            return new Vector() { X = vector1.X + vector2.X, Y = vector1.Y + vector2.Y };
        }
    }
}
