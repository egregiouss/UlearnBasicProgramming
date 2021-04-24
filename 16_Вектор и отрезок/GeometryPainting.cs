using System;
using System.Collections.Generic;
using System.Drawing;
using GeometryTasks;

namespace GeometryPainting
{
    public static class SegmentExtensions
    {
        public static Dictionary<Segment, Color> SegmentsColors = new Dictionary<Segment, Color>();
		
		    public static void SetColor(this Segment segment, Color color)
		        => SegmentsColors[segment] = color;

		    public static Color GetColor(this Segment segment)
			      => SegmentsColors.ContainsKey(segment) ? SegmentsColors[segment] : Color.Black;
    }
}
