using System.Linq;

namespace Names
{
    internal static class HistogramTask
    {
        public static HistogramData GetBirthsPerDayHistogram(NameData[] names, string name)
        {
            var days = Enumerable.Range(1, 31)
                                 .Select(x => x.ToString())
                                 .ToArray();
            return new HistogramData("Рождаемость людей с именем " + name, days, GetBirthsPerDay(names, name));
        }

        public static double[] GetBirthsPerDay(NameData[] names, string name)
        {
            var birthsCounts = new double[31];
            foreach (var n in names)
            {
                if (n.Name == name && n.BirthDate.Day != 1)
                    birthsCounts[n.BirthDate.Day - 1]++;
            }
            return birthsCounts;
        }
    }
}