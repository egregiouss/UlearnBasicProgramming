using System;

namespace Recognizer
{
    internal static class SobelFilterTask
    {
        public static double[,] SobelFilter(double[,] g, double[,] sx)
        {
            var width = g.GetLength(0);
            var height = g.GetLength(1);
            var result = new double[width, height];
            var sy = TransposeMatrix(sx);
            var sxWidth = sx.GetLength(0);
            
			sxWidth = (sxWidth - 1) / 2;
            for (int x = sxWidth; x < width - sxWidth; x++)
                for (int y = sxWidth; y < height - sxWidth; y++)
                    result[x, y] = GetСonvolution(g, sx, sy, x, y);
                
            return result;
        }
		
        public static double[,] TransposeMatrix(double[,] sx)
        {
            var width = sx.GetLength(0);
            var height = sx.GetLength(1);
            var sy = new double[height, width];
            for (int x = 0; x < width; x++)
                for (int y = 0; y < height; y++)
                    sy[x, y] = sx[y, x];
            return sy;
        }
		
        public static double GetСonvolution(double[,] g, double[,] sx, double[,] sy, int x, int y)
        {
            var width = sx.GetLength(0);
            var height = sx.GetLength(1);
            var gx = 0.0;
            var gy = 0.0;
            for (var i = 0; i < width; i++)
                for (var j = 0; j < height; j++)
                {       
                    gx += sx[i, j] * g[x + i - (width - 1) / 2, y + j - (height - 1) / 2];
                    gy += sy[i, j] * g[x + i - (width - 1) / 2, y + j - (height - 1) / 2];
                }
            return Math.Sqrt(gx * gx + gy * gy);
        }
    }
}
