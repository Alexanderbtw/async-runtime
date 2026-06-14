using Benchmark.Common;

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace Benchmark;

/// <summary>
/// High-density completed async path. One benchmark operation performs many
/// completed awaits so small per-await overhead becomes visible.
/// </summary>
[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
public class CompletedChainDepthBenchmarks
{
    private const int CompletedIterations = 10_000;

    [Benchmark]
    public async Task<int> Task_ListFromResultNestedLoop()
    {
        var data = new List<int>(CompletedIterations);

        for (var i = 0; i < CompletedIterations; i++)
            data.Add(await GetTaskFromResultAsync(i));

        return data[^1];
    }

    [Benchmark]
    public async ValueTask<int> ValueTask_ListFromResultNestedLoop()
    {
        var data = new List<int>(CompletedIterations);

        for (var i = 0; i < CompletedIterations; i++)
            data.Add(await GetValueTaskFromResultAsync(i));

        return data[^1];
    }

    [Benchmark]
    public async Task<long> Task_CachedCompletedLoop()
    {
        long sum = 0;

        for (var i = 0; i < CompletedIterations; i++)
            sum += await AsyncSources.CompletedTask;

        return sum;
    }

    [Benchmark]
    public async ValueTask<long> ValueTask_CachedCompletedLoop()
    {
        long sum = 0;

        for (var i = 0; i < CompletedIterations; i++)
            sum += await AsyncSources.CompletedValueTask;

        return sum;
    }

    private static async Task<int> GetTaskFromResultAsync(int value)
    {
        return await Task.FromResult(AsyncSources.ExpensiveOrSideEffect(value));
    }

    private static async ValueTask<int> GetValueTaskFromResultAsync(int value)
    {
        return await new ValueTask<int>(AsyncSources.ExpensiveOrSideEffect(value));
    }
}
