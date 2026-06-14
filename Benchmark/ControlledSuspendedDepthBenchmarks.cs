using Benchmark.Common;

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace Benchmark;

/// <summary>
/// Repeated controlled suspension without Task.Yield or ThreadPool scheduler noise.
/// </summary>
[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
public class ControlledSuspendedDepthBenchmarks
{
    private const int SuspendedIterations = 1_000;

    [Benchmark]
    public async Task<int> Task_ControlledSuspensionLoop()
    {
        var sum = 0;

        for (var i = 0; i < SuspendedIterations; i++)
            sum += await TaskSuspendOnce();

        return sum;
    }

    [Benchmark]
    public async ValueTask<int> ValueTask_ControlledSuspensionLoop()
    {
        var sum = 0;

        for (var i = 0; i < SuspendedIterations; i++)
            sum += await ValueTaskSuspendOnce();

        return sum;
    }

    private static async Task<int> TaskSuspendOnce()
    {
        await AlwaysIncomplete.Yield();
        return 1;
    }

    private static async ValueTask<int> ValueTaskSuspendOnce()
    {
        await AlwaysIncomplete.Yield();
        return 1;
    }
}
