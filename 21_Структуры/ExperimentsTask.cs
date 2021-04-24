using System.Collections.Generic;
using System.Linq;

namespace StructBenchmarking
{
    public class Experiments
    {
        public static ChartData BuildChartDataForArrayCreation(
            IBenchmark benchmark, int repetitionsCount)
        {
            return new ChartData
            {
                Title = "Create array",
                ClassPoints = Constants.FieldCounts.SelectMany(x => 
				 BuildChartData(benchmark, x, new ClassArrayCreationTask(x), repetitionsCount))
                                                   .ToList(),
                StructPoints = Constants.FieldCounts.SelectMany(x => 
				 BuildChartData(benchmark, x, new StructArrayCreationTask(x), repetitionsCount))
                                                   .ToList(),
            };
        }

        public static ChartData BuildChartDataForMethodCall(
            IBenchmark benchmark, int repetitionsCount)
        {
            return new ChartData
            {
                Title = "Call method with argument",
                ClassPoints = Constants.FieldCounts.SelectMany(x => 
				 BuildChartData(benchmark, x, new MethodCallWithClassArgumentTask(x), repetitionsCount))
                                                   .ToList(),
                StructPoints = Constants.FieldCounts.SelectMany(x => 
				 BuildChartData(benchmark, x, new MethodCallWithStructArgumentTask(x), repetitionsCount))
                                                   .ToList(),
            };
        }

        public static List<ExperimentResult> BuildChartData(
            IBenchmark benchmark, int fieldCount, 
            ITask obj, int repetitionsCount)
        {
        	var resultTimes = new List<ExperimentResult>();

            resultTimes.Add(new ExperimentResult
                    (fieldCount, new Benchmark().MeasureDurationInMs
                    (obj, repetitionsCount)));
            
        	return resultTimes;
        }
    }
}
