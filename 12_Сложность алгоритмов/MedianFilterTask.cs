using System;
using System.Collections.Generic;
namespace Recognizer
{
    internal static class MedianFilterTask
	{
		public static double[,] MedianFilter(double[,] original)
		{
            var width = original.GetLength(0);
            var height = original.GetLength(1);
            var medianArray = new double[width, height];
            for (var x = 0; x < width; x++)
                for (var y = 0; y < height; y++)
                    medianArray[x, y] = MedianBlock(original, x, y);
            return medianArray;
        }

        public static double MedianBlock(double[,] original, int x, int y)
        {
            var list = new List<double>();
            var width  = original.GetLength(0);
            var height = original.GetLength(1);
            var countRows    = 3;
            var countColumns = 3;
            if (x < 1 || x + 1 == width ) countRows--;
            if (y < 1 || y + 1 == height) countColumns--;
			if (x >= 1) x--;
            if (y >= 1) y--;
            for (var i = 0; i < countRows && i < width; i++)
                for (var j = 0; j < countColumns && j < height; j++)
                    list.Add(original[x + i, y + j]);
            list.Sort();
			return list.Count < 2 ? list[0] : GetMedianOfArray(list);
        }
  
        public static double GetMedianOfArray(List<double> list)
        {
            if (list.Count % 2 == 0)
                return (list[(int)(list.Count / 2) - 1] + list[(int)(list.Count / 2)]) / 2;
            return list[(int)(list.Count / 2)];  
        }
    }
}
