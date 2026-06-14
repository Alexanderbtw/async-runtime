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
    .AddJob(Job.ShortRun)
    .HideColumns(columns: [StatisticColumn.Error])
    .AddExporter(MarkdownExporter.GitHub)
    .AddExporter(CsvExporter.Default)
    .WithArtifactsPath(artifactsPath);

BenchmarkSwitcher.FromTypes(
[
    typeof(CompletedChainDepthBenchmarks),
    typeof(ForwardingPatternsBenchmarks),
    typeof(ControlledSuspendedDepthBenchmarks),
    typeof(ControlledLiveStateBenchmarks),
    typeof(ExceptionPathBenchmarks)
]).Run(args, config);
