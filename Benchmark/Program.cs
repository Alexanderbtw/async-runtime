using Benchmark;

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
    .AddExporter(MarkdownExporter.GitHub)
    .AddExporter(CsvExporter.Default)
    .WithArtifactsPath(artifactsPath);

BenchmarkSwitcher.FromTypes(
[
    typeof(CompletedChainDepthBenchmarks),
    typeof(AwaitDensityBenchmarks),
    typeof(SuspendedOnceDepthBenchmarks),
    typeof(ForwardingPatternsBenchmarks),
    typeof(LiveStateAcrossAwaitBenchmarks),
    typeof(CustomAwaiterLiveStateBenchmarks)
]).Run(args, config);
