using Benchmark.Common;

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Order;

namespace Benchmark;

[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
public class CompletedChainDepthBenchmarks
{
    [Params(1, 2, 4, 8, 16, 32)]
    public int Depth { get; set; }

    [Benchmark]
    public Task<int> Task_Completed()
    {
        return TaskCompleted(Depth);
    }

    [Benchmark]
    public ValueTask<int> ValueTask_Completed()
    {
        return ValueTaskCompleted(Depth);
    }

    private static async Task<int> TaskCompleted(int depth)
    {
        if (depth == 0)
            return await AsyncSources.CompletedTask;

        return 1 + await TaskCompleted(depth - 1);
    }

    private static async ValueTask<int> ValueTaskCompleted(int depth)
    {
        if (depth == 0)
            return await AsyncSources.CompletedValueTask;

        return 1 + await ValueTaskCompleted(depth - 1);
    }
}