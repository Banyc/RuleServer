using System;
using BenchmarkDotNet.Running;

namespace RuleEngine.Benchmark
{
    class Program
    {
        static void Main(string[] args)
        {
            // var summary = BenchmarkRunner.Run(typeof(Program).Assembly);
            // var summary = BenchmarkRunner.Run<TestBenchmarkSystem>();
            var summary = BenchmarkRunner.Run<GeneralBenchmarks>();
            // Debug();
        }

        static void Debug()
        {
            GeneralBenchmarks benchmarks = new();
            benchmarks.InputCount = 10000;
            benchmarks.RuleCount = 10000;
            benchmarks.TestType = Models.TestType.ManySameSubtrees;

            benchmarks.GlobalSetup();
            benchmarks.DoMatch();
            benchmarks.Cleanup();

            Console.WriteLine($"Match count: {benchmarks.MatchCount}. Exception count: {benchmarks.ExceptionCount}");
        }
    }
}
