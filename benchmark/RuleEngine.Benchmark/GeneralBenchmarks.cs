using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using BenchmarkDotNet.Attributes;
using RuleEngine.Benchmark.Models;
using RuleEngine.Models.RuleEngine;

namespace RuleEngine.Benchmark
{
    [ShortRunJob]
    // [MediumRunJob]
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
    public class GeneralBenchmarks
    {
        [Params(10, 100, 1000, 5000, 7500, 10000, 20000)]
        public int RuleCount { get; set; }
        // [Params(10, 100, 1000, 10000)]
        [Params(10000)]
        public int InputCount { get; set; }
        [Params(TestType.AllRulesAreTheSame, TestType.NoRuleIsTheSame, TestType.ManySameSubtrees, TestType.FewSameSubtrees)]
        public TestType TestType { get; set; }
        private TestCase testCase;

        public int MatchCount { get; set; }
        public int ExceptionCount { get; set; }

        [GlobalSetup]
        public void GlobalSetup()
        {
            this.MatchCount = 0;
            this.ExceptionCount = 0;

            TestCase testCase = new()
            {
                InputCount = this.InputCount,
                RuleCount = this.RuleCount,
                Type = this.TestType,
            };
            RuleEngineSettings settings = new()
            {
                RuleGroups = new(),
            };
            List<RuleSettings> rules = new();
            int i;
            for (i = 0; i < this.RuleCount; i++)
            {
                switch (this.TestType)
                {
                    case TestType.AllRulesAreTheSame:
                        rules.Add(new()
                        {
                            ConditionExpression = "sensorId == \"1\"",
                            Description = "test",
                            RuleName = $"rule #{i}",
                        });
                        break;
                    case TestType.NoRuleIsTheSame:
                        rules.Add(new()
                        {
                            ConditionExpression = $"sensorId == \"{i}\"",
                            Description = "test",
                            RuleName = $"rule #{i}",
                        });
                        break;
                    case TestType.ManySameSubtrees:
                        rules.Add(new()
                        {
                            ConditionExpression = $"sensorId == \"1\" && (number < 3 && number < (number + 2))",
                            Description = "test",
                            RuleName = $"rule #{i}",
                        });
                        break;
                    case TestType.FewSameSubtrees:
                        rules.Add(new()
                        {
                            ConditionExpression = $"sensorId == \"1\" && (number < (3 + {i}) && number < (number + (2 + {i})))",
                            Description = "test",
                            RuleName = $"rule #{i}",
                        });
                        break;
                    default:
                        break;
                }
            }
            RuleGroup group = new()
            {
                GroupName = "default",
                IndexedParameters = new() {
                    "sensorId"
                },
                MaxIndexCacheSize = 1024,
                RuleSet = rules,
            };
            settings.RuleGroups.Add(group);

            testCase.Engine = new(settings);

            testCase.Input = new()
            {
                { "sensorId", "1" },
                { "number", 1 },
            };

            this.testCase = testCase;
        }

        [Benchmark]
        public void DoMatch()
        {
            int i;
            for (i = 0; i < this.testCase.InputCount; i++)
            {
                this.testCase.Engine.Match("default", this.testCase.Input, (_, _) => this.MatchCount++, (_, _) => this.ExceptionCount++);
            }
        }

        [GlobalCleanup]
        public void Cleanup()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
    }
}
