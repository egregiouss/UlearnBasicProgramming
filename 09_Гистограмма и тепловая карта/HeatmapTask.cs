using System.Linq;

namespace Names
{
    internal static class HeatmapTask
    {
        public static HeatmapData GetBirthsPerDateHeatmap(NameData[] names)
        {
            var days = Enumerable.Range(2, 30)
                                   .Select(x => x.ToString())
                                   .ToArray();
            var months = Enumerable.Range(1, 12)
                                   .Select(x => x.ToString())
                                   .ToArray();
            return new HeatmapData("Карта интенсивностей", GetBirthsPerDate(names), days, months);
        }

        public static double[,] GetBirthsPerDate(NameData[] names)
        {
            var birthsCounts = new double[30, 12];
            foreach (var n in names)
            {
                if (n.BirthDate.Day != 1)
                    birthsCounts[n.BirthDate.Day - 2, n.BirthDate.Month - 1]++;
            }
            return birthsCounts;
        }
    }
}