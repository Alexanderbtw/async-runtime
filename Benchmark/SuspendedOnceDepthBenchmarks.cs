using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Order;

namespace Benchmark;

[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
public class SuspendedOnceDepthBenchmarks
{
    [Params(1, 2, 4, 8, 16)]
    public int Depth { get; set; }

    [Benchmark]
    public Task<int> Task_SuspendedOnce()
    {
        return TaskSuspendedOnce(Depth);
    }

    [Benchmark]
    public ValueTask<int> ValueTask_SuspendedOnce()
    {
        return ValueTaskSuspendedOnce(Depth);
    }

    private static async Task<int> TaskLeafSuspendedOnce()
    {
        await Task.Yield();
        return 1;
    }

    private static async ValueTask<int> ValueTaskLeafSuspendedOnce()
    {
        await Task.Yield();
        return 1;
    }

    private static async Task<int> TaskSuspendedOnce(int depth)
    {
        if (depth == 0)
            return await TaskLeafSuspendedOnce();

        return 1 + await TaskSuspendedOnce(depth - 1);
    }

    private static async ValueTask<int> ValueTaskSuspendedOnce(int depth)
    {
        if (depth == 0)
            return await ValueTaskLeafSuspendedOnce();

        return 1 + await ValueTaskSuspendedOnce(depth - 1);
    }
}