using System;
using BenchmarkDotNet.Attributes;

namespace RuleEngine.Benchmark
{
    [ShortRunJob]
    [MediumRunJob]
    [KeepBenchmarkFiles]

    [AsciiDocExporter]
    [CsvExporter]
    [CsvMeasurementsExporter]
    [HtmlExporter]
    [PlainExporter]
    [RPlotExporter]
    [JsonExporterAttribute.Brief]
    [JsonExporterAttribute.BriefCompressed]
    [JsonExporterAttribute.Full]
    [JsonExporterAttribute.FullCompressed]
    [MarkdownExporterAttribute.Default]
    [MarkdownExporterAttribute.GitHub]
    [MarkdownExporterAttribute.StackOverflow]
    [MarkdownExporterAttribute.Atlassian]
    [XmlExporterAttribute.Brief]
    [XmlExporterAttribute.BriefCompressed]
    [XmlExporterAttribute.Full]
    [XmlExporterAttribute.FullCompressed]
    public class TestBenchmarkSystem
    {
        [Params(7500, 10000)]
        public int N { get; set; }
        [Params(10, 100)]
        public int M { get; set; }

        [GlobalSetup]
        public void GlobalSetup()
        {
            Console.WriteLine($"[global setup] N = {N}; M = {M}");
        }

        [Benchmark]
        public void Benchmark()
        {
            // Console.WriteLine($"[benchmark] N = {N}; M = {M}");
        }

        [GlobalCleanup]
        public void GlobalCleanup()
        {
            Console.WriteLine($"[global cleanup] N = {N}; M = {M}");
        }
    }
}
