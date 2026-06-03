using System.Runtime.CompilerServices;
using BenchmarkDotNet.Attributes;

namespace Benchmark;

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
