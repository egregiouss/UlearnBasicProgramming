namespace Recognizer
{
    public static class GrayscaleTask
    {
        public static double[,] ToGrayscale(Pixel[,] original)
	{
            var weight = original.GetLength(0);
            var height = original.GetLength(1);
            var grayscale = new double[weight, height];
            for (var x = 0; x < weight; x++)
                for (var y = 0; y < height; y++)
                    grayscale[x, y] = (0.299 * original[x, y].R +
                                       0.587 * original[x, y].G + 
                                       0.114 * original[x, y].B) / 255;
            return grayscale;
	}
    }
}
