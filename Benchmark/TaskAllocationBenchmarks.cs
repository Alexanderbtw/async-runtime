using System.Runtime.CompilerServices;
using BenchmarkDotNet.Attributes;

namespace Benchmark;

// https://github.com/dotnet/roslyn/issues/84061
/// <summary>
/// Диагностические микробенчмарки аллокаций для Task, ValueTask и pooling builder на sync-completed и Task.Yield paths.
/// Task.Yield включает scheduler/continuation behavior, поэтому этот класс не является основным proof для runtime async.
/// </summary>
[MemoryDiagnoser]
public class TaskAllocationBenchmarks
{
    [Benchmark(Baseline = true)]
    public async Task<int> Task_Sync()
    {
        return 42;
    }

    [Benchmark]
    public async ValueTask<int> ValueTask_Sync()
    {
        return 42;
    }

    [Benchmark]
    [AsyncMethodBuilder(typeof(PoolingAsyncValueTaskMethodBuilder<>))]
    public async ValueTask<int> Pooling_Sync()
    {
        return 42;
    }

    [Benchmark]
    public async Task<int> Task_Yield()
    {
        await Task.Yield();
        return 42;
    }

    [Benchmark]
    public async ValueTask<int> ValueTask_Yield()
    {
        await Task.Yield();
        return 42;
    }

    [Benchmark]
    [AsyncMethodBuilder(typeof(PoolingAsyncValueTaskMethodBuilder<>))]
    public async ValueTask<int> Pooling_Yield()
    {
        await Task.Yield();
        return 42;
    }
}
