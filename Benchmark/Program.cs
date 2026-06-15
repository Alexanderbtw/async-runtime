using Benchmark;

using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;

var artifactsDirectory =
#if RUNTIME_ASYNC
    "BenchmarkDotNet.Artifacts.AsyncRuntime"
#else
    "BenchmarkDotNet.Artifacts.Standard"
#endif
    ;

var projectDirectory = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "../../.."));
var artifactsPath = Path.Combine(projectDirectory, artifactsDirectory);

IConfig config = DefaultConfig.Instance.WithArtifactsPath(artifactsPath);

BenchmarkSwitcher.FromTypes(
[
    typeof(AsyncBenchmark),
    typeof(CompletedChainDepthBenchmarks),
    typeof(ForwardingPatternsBenchmarks),
    typeof(ControlledSuspendedDepthBenchmarks),
    typeof(ControlledLiveStateBenchmarks),
    typeof(ExceptionPathBenchmarks),
    typeof(TaskAllocationBenchmarks)
]).Run(args, config);

