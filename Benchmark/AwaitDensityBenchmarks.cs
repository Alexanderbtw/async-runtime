using System.Runtime.CompilerServices;

using Benchmark.Common;

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Order;

namespace Benchmark;

[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
public class AwaitDensityBenchmarks
{
    [Params(1, 2, 4, 8, 16, 32, 64)]
    public int AwaitCount { get; set; }

    [Benchmark]
    public Task<int> Task_ManyCompletedAwaits()
    {
        return TaskManyCompletedAwaits(AwaitCount);
    }

    [Benchmark]
    public ValueTask<int> ValueTask_ManyCompletedAwaits()
    {
        return ValueTaskManyCompletedAwaits(AwaitCount);
    }

    private static async Task<int> TaskManyCompletedAwaits(int count)
    {
        var sum = 0;

        for (var i = 0; i < count; i++)
            sum += await AsyncSources.CompletedTask;

        return sum;
    }

    private static async ValueTask<int> ValueTaskManyCompletedAwaits(int count)
    {
        var sum = 0;

        for (var i = 0; i < count; i++)
            sum += await AsyncSources.CompletedValueTask;

        return sum;
    }
}

public readonly struct AlwaysIncomplete
{
    public static AlwaysIncomplete Yield() => default;

    public Awaiter GetAwaiter() => default;

    public readonly struct Awaiter : ICriticalNotifyCompletion
    {
        public bool IsCompleted => false;

        public void GetResult()
        {
        }

        public void OnCompleted(Action continuation)
        {
            continuation();
        }

        public void UnsafeOnCompleted(Action continuation)
        {
            continuation();
        }
    }
}