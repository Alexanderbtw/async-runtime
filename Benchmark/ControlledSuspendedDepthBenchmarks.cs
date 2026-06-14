using Benchmark.Common;

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Order;

namespace Benchmark;

/// <summary>
/// Вычисления на каждом этапе, но при этом гарантированно приостановка на каждом этапе,
/// так что мы измеряем накладные расходы на приостановку и возобновление задач, а также передачу задач и await в условиях реальной приостановки.
/// </summary>
[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
public class ControlledSuspendedDepthBenchmarks
{
    [Params(1, 2, 4, 8, 16, 32, 64)]
    public int Depth { get; set; }

    [Benchmark]
    public Task<int> Task_ControlledSuspendedOnce()
    {
        return TaskControlledSuspendedOnce(Depth);
    }

    [Benchmark]
    public ValueTask<int> ValueTask_ControlledSuspendedOnce()
    {
        return ValueTaskControlledSuspendedOnce(Depth);
    }

    private static async Task<int> TaskLeafControlledSuspendedOnce()
    {
        await AlwaysIncomplete.Yield(); // TOdo точно ли это что-то дает и отличается от forwarding?
        return 1;
    }

    private static async ValueTask<int> ValueTaskLeafControlledSuspendedOnce()
    {
        await AlwaysIncomplete.Yield();
        return 1;
    }

    private static async Task<int> TaskControlledSuspendedOnce(int depth)
    {
        if (depth == 0)
            return await TaskLeafControlledSuspendedOnce();

        return 1 + await TaskControlledSuspendedOnce(depth - 1);
    }

    private static async ValueTask<int> ValueTaskControlledSuspendedOnce(int depth)
    {
        if (depth == 0)
            return await ValueTaskLeafControlledSuspendedOnce();

        return 1 + await ValueTaskControlledSuspendedOnce(depth - 1);
    }
}
