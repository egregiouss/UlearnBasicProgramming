using System;
using System.Drawing;

namespace Fractals
{
	internal static class DragonFractalTask
	{
		public static void DrawDragonFractal(Pixels pixels, int iterationsCount, int seed)
		{
			var x = 1.0;
			var y = 0.0;
			var pixel = new double[2];
			pixels.SetPixel(x, y);
			var random = new Random(seed);
			for (var i = 0; i < iterationsCount; i++)
			{
				if (random.Next(0, 3) < 2)
					pixel = Draw(x, y, 45);
				else
				{ 
					pixel = Draw(x, y, 135);
					pixel[0]++;
				}
				x = pixel[0];
				y = pixel[1];
				pixels.SetPixel(x, y);
			}
		}

		public static double[] Draw(double x, double y, double ang)
		{
			var newCoord = new double[2];
			newCoord[0] = DrawFractal(x,  y, Math.Cos(ang), Math.Sin(ang));
			newCoord[1] = DrawFractal(x, -y, Math.Sin(ang), Math.Cos(ang));
			return newCoord;
		}

		public static double DrawFractal(double x, double y, double mathAngX, double mathAngY)
		{
			return (x * mathAngX - y * mathAngY) / Math.Sqrt(2);
		}
	}
}
