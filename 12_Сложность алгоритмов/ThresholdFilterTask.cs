using System.Linq;

namespace Recognizer
{
    public static class ThresholdFilterTask
    {
        public static double[,] ThresholdFilter(double[,] original, double whitePixelsFraction)
        {
            var weight = original.GetLength(0);
            var height = original.GetLength(1);
            var minCountWitePixels = (int)(original.Length * whitePixelsFraction);
            if (whitePixelsFraction == 0 || minCountWitePixels == 0)
                return new double[weight, height];

            var pixels = original.Cast<double>().ToList();
            pixels.Sort();
            var t = pixels[pixels.Count - minCountWitePixels];
            return GetThresholdFilterResult(original, t);
        }

        public static double[,] GetThresholdFilterResult(double[,] original, double t)
        {
            var result = original;
            var weight = result.GetLength(0);
            var height = result.GetLength(1);
            for (var x = 0; x < weight; x++)
                for (var y = 0; y < height; y++)
                    if (result[x, y] >= t)
                        result[x, y] = 1.0;
                    else result[x, y] = 0.0;
            return result;
        }
    }
}
