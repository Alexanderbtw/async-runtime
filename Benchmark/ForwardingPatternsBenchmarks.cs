using Benchmark.Common;

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Order;

namespace Benchmark;

/// <summary>
/// Простая цепочка прямой передачи задач без вычислений и без приостановки,
/// чтобы измерить накладные расходы на передачу задач и await без фактического переключения контекста или выполнения кода между ними.
/// </summary>
[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
public class ForwardingPatternsBenchmarks
{
    [Params(1, 2, 4, 8, 16, 32, 64)]
    public int Depth { get; set; }

    [Benchmark]
    public Task<int> Task_DirectForwarding()
    {
        return TaskDirectForwarding(Depth);
    }

    [Benchmark]
    public Task<int> Task_AwaitOnly()
    {
        return TaskAwaitOnly(Depth);
    }

    [Benchmark]
    public ValueTask<int> ValueTask_DirectForwarding()
    {
        return ValueTaskDirectForwarding(Depth);
    }

    [Benchmark]
    public ValueTask<int> ValueTask_AwaitOnly()
    {
        return ValueTaskAwaitOnly(Depth);
    }

    private static Task<int> TaskDirectForwarding(int depth)
    {
        if (depth == 0)
            return AsyncSources.CompletedTask;

        return TaskDirectForwarding(depth - 1);
    }

    private static async Task<int> TaskAwaitOnly(int depth)
    {
        if (depth == 0)
            return await AsyncSources.CompletedTask;

        return await TaskAwaitOnly(depth - 1);
    }

    private static ValueTask<int> ValueTaskDirectForwarding(int depth)
    {
        if (depth == 0)
            return AsyncSources.CompletedValueTask;

        return ValueTaskDirectForwarding(depth - 1);
    }

    private static async ValueTask<int> ValueTaskAwaitOnly(int depth)
    {
        if (depth == 0)
            return await AsyncSources.CompletedValueTask;

        return await ValueTaskAwaitOnly(depth - 1);
    }
}
