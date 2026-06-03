using Benchmark.Common;

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace Benchmark;

[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
public class CustomAwaiterLiveStateBenchmarks
{
    [Benchmark]
    public Task<int> Task_CustomAwaiter_NoLiveState()
    {
        return TaskCustomAwaiterNoLiveState();
    }

    [Benchmark]
    public Task<int> Task_CustomAwaiter_EightLiveValues()
    {
        return TaskCustomAwaiterEightLiveValues();
    }

    [Benchmark]
    public ValueTask<int> ValueTask_CustomAwaiter_NoLiveState()
    {
        return ValueTaskCustomAwaiterNoLiveState();
    }

    [Benchmark]
    public ValueTask<int> ValueTask_CustomAwaiter_EightLiveValues()
    {
        return ValueTaskCustomAwaiterEightLiveValues();
    }

    private static async Task<int> TaskCustomAwaiterNoLiveState()
    {
        await AlwaysIncomplete.Yield();

        var x = AsyncSources.ExpensiveOrSideEffect();
        return x + 1;
    }

    private static async Task<int> TaskCustomAwaiterEightLiveValues()
    {
        var x1 = AsyncSources.ExpensiveOrSideEffect();
        var x2 = AsyncSources.ExpensiveOrSideEffect();
        var x3 = AsyncSources.ExpensiveOrSideEffect();
        var x4 = AsyncSources.ExpensiveOrSideEffect();
        var x5 = AsyncSources.ExpensiveOrSideEffect();
        var x6 = AsyncSources.ExpensiveOrSideEffect();
        var x7 = AsyncSources.ExpensiveOrSideEffect();
        var x8 = AsyncSources.ExpensiveOrSideEffect();

        await AlwaysIncomplete.Yield();

        return x1 + x2 + x3 + x4 + x5 + x6 + x7 + x8;
    }

    private static async ValueTask<int> ValueTaskCustomAwaiterNoLiveState()
    {
        await AlwaysIncomplete.Yield();

        var x = AsyncSources.ExpensiveOrSideEffect();
        return x + 1;
    }

    private static async ValueTask<int> ValueTaskCustomAwaiterEightLiveValues()
    {
        var x1 = AsyncSources.ExpensiveOrSideEffect();
        var x2 = AsyncSources.ExpensiveOrSideEffect();
        var x3 = AsyncSources.ExpensiveOrSideEffect();
        var x4 = AsyncSources.ExpensiveOrSideEffect();
        var x5 = AsyncSources.ExpensiveOrSideEffect();
        var x6 = AsyncSources.ExpensiveOrSideEffect();
        var x7 = AsyncSources.ExpensiveOrSideEffect();
        var x8 = AsyncSources.ExpensiveOrSideEffect();

        await AlwaysIncomplete.Yield();

        return x1 + x2 + x3 + x4 + x5 + x6 + x7 + x8;
    }
}
