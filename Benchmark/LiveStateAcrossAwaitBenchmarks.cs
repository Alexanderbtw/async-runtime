using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System.Runtime.CompilerServices;

using Benchmark.Common;

using BenchmarkDotNet.Order;

[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
public class LiveStateAcrossAwaitBenchmarks
{
    [Benchmark]
    public Task<int> Task_NoLiveState()
    {
        return TaskNoLiveState();
    }

    [Benchmark]
    public Task<int> Task_OneLiveValue()
    {
        return TaskOneLiveValue();
    }

    [Benchmark]
    public Task<int> Task_EightLiveValues()
    {
        return TaskEightLiveValues();
    }

    [Benchmark]
    public ValueTask<int> ValueTask_NoLiveState()
    {
        return ValueTaskNoLiveState();
    }

    [Benchmark]
    public ValueTask<int> ValueTask_OneLiveValue()
    {
        return ValueTaskOneLiveValue();
    }

    [Benchmark]
    public ValueTask<int> ValueTask_EightLiveValues()
    {
        return ValueTaskEightLiveValues();
    }

    private static async Task<int> TaskNoLiveState()
    {
        await Task.Yield();

        var x = AsyncSources.ExpensiveOrSideEffect();
        return x + 1;
    }

    private static async Task<int> TaskOneLiveValue()
    {
        var x = AsyncSources.ExpensiveOrSideEffect();

        await Task.Yield();

        return x + 1;
    }

    private static async Task<int> TaskEightLiveValues()
    {
        var x1 = AsyncSources.ExpensiveOrSideEffect();
        var x2 = AsyncSources.ExpensiveOrSideEffect();
        var x3 = AsyncSources.ExpensiveOrSideEffect();
        var x4 = AsyncSources.ExpensiveOrSideEffect();
        var x5 = AsyncSources.ExpensiveOrSideEffect();
        var x6 = AsyncSources.ExpensiveOrSideEffect();
        var x7 = AsyncSources.ExpensiveOrSideEffect();
        var x8 = AsyncSources.ExpensiveOrSideEffect();

        await Task.Yield();

        return x1 + x2 + x3 + x4 + x5 + x6 + x7 + x8;
    }

    private static async ValueTask<int> ValueTaskNoLiveState()
    {
        await Task.Yield();

        var x = AsyncSources.ExpensiveOrSideEffect();
        return x + 1;
    }

    private static async ValueTask<int> ValueTaskOneLiveValue()
    {
        var x = AsyncSources.ExpensiveOrSideEffect();

        await Task.Yield();

        return x + 1;
    }

    private static async ValueTask<int> ValueTaskEightLiveValues()
    {
        var x1 = AsyncSources.ExpensiveOrSideEffect();
        var x2 = AsyncSources.ExpensiveOrSideEffect();
        var x3 = AsyncSources.ExpensiveOrSideEffect();
        var x4 = AsyncSources.ExpensiveOrSideEffect();
        var x5 = AsyncSources.ExpensiveOrSideEffect();
        var x6 = AsyncSources.ExpensiveOrSideEffect();
        var x7 = AsyncSources.ExpensiveOrSideEffect();
        var x8 = AsyncSources.ExpensiveOrSideEffect();

        await Task.Yield();

        return x1 + x2 + x3 + x4 + x5 + x6 + x7 + x8;
    }
}