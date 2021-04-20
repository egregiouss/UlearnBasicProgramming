using System;
namespace Rectangles
{
    public static class RectanglesTask
    {
        public static bool AreIntersected(Rectangle r1, Rectangle r2)
        {
            return (r1.Left <= r2.Right)  && 
                   (r2.Left <= r1.Right)  && 
                   (r1.Top  <= r2.Bottom) && 
                   (r2.Top  <= r1.Bottom);
        }

        public static int IntersectionSquare(Rectangle r1, Rectangle r2)
        {
            if (!AreIntersected(r1, r2)) return 0;

            int aIntersection = SegmentsIntersection(r1.Left, r1.Right, r2.Left, r2.Right);
            int bIntersection = SegmentsIntersection(r1.Top, r1.Bottom, r2.Top, r2.Bottom);
            return aIntersection * bIntersection;
        }

        public static int SegmentsIntersection(int r1Left, int r1Right, int r2Left, int r2Right)
        {
            int left = Math.Max(r1Left, r2Left);
            int right = Math.Min(r1Right, r2Right);
            return Math.Max(right - left, 0);
        }

        public static int IndexOfInnerRectangle(Rectangle r1, Rectangle r2)
        { 
            return AreIntersected(r1, r2) ?
                ((r1.Left   >= r2.Left  && 
                  r1.Top    >= r2.Top   && 
                  r1.Right  <= r2.Right && 
                  r1.Bottom <= r2.Bottom) ? 0 : (r2.Left   >= r1.Left  && 
                                                 r2.Top    >= r1.Top   && 
                                                 r2.Right  <= r1.Right && 
                                                 r2.Bottom <= r1.Bottom) ? 1 : -1) : -1;
        }
    }
}
