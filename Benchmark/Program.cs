using Benchmark;

using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Exporters.Csv;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;

var artifactsPath =
#if RUNTIME_ASYNC
    "BenchmarkDotNet.Artifacts.AsyncRuntime";
#else
    "BenchmarkDotNet.Artifacts.Standard";
#endif

ManualConfig config = DefaultConfig.Instance
    .HideColumns(columns: [StatisticColumn.Error])
    .AddExporter(MarkdownExporter.GitHub)
    .AddExporter(CsvExporter.Default)
    .WithArtifactsPath(artifactsPath);

BenchmarkSwitcher.FromTypes(
[
    typeof(TaskAllocationBenchmarks),
    typeof(TaskTimeoutBenchmarks)
]).Run(args, config);
