using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using NUnit.Framework;

namespace StructBenchmarking
{
    public class Benchmark : IBenchmark
    {
        public double MeasureDurationInMs(ITask task, int repetitionCount)
        {
            GC.Collect();                   
            GC.WaitForPendingFinalizers();
            var watch = new Stopwatch();
            task.Run();
            watch.Start();
            for (var i = 0; i < repetitionCount; i++)
            {
                task.Run();
            }
            watch.Stop();
            return (double)watch.ElapsedMilliseconds / repetitionCount;
        }
    }

    public class StrBuilder : ITask
    {
        public char Letter {get; set;}
        public int RepetitionCount { get; set; }
        public StrBuilder(char letter, int repetitionCount)
        {
            Letter = letter;
            RepetitionCount = repetitionCount;
        }

        public string RunStrBuilderAppend() 
        {
            var builder = new StringBuilder();
            for (int i = 0; i < RepetitionCount; i++)
            {
                builder.Append(Letter);
            }
            return builder.ToString();
        }

        public void RunTestStrBuilderAppend()
        {
            var builder = new StringBuilder();
            for (int i = 0; i < 10; i++)
            {
                builder.Append(Letter);
            }
            builder.Clear();
        }
		
        public void Run() 
        {
            RunTestStrBuilderAppend();
            RunStrBuilderAppend(); 
        }
    }

    public class StringConstructor : ITask
    {
        public char Letter { get; set; }
        public int RepetitionCount { get; set; }
        public StringConstructor(char letter, int repetitionCount)
        {
            Letter = letter;
            RepetitionCount = repetitionCount;
        }

        public string RunStringConstructor()
        {
            return new string(Letter, RepetitionCount);
        }

        public void RunTestConstructor()
        {
             new string('a', 10);
        }
		
        public void Run() 
        {
            RunTestConstructor();
            RunStringConstructor(); 
        }
    }

    [TestFixture]
    public class RealBenchmarkUsageSample
    {
        [Test]
        public void StringConstructorFasterThanStringBuilder()
        {
            var benchmark = new Benchmark();
            var strConstructor = new StringConstructor('a', 100000);
            var strBuilder = new StrBuilder('a', 100000);
            var builderTime = benchmark.MeasureDurationInMs(strBuilder, 20);
            var constructorTime = benchmark.MeasureDurationInMs(strConstructor, 20);
            Assert.Less(constructorTime, builderTime);
        }
    }
}
