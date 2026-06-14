using Benchmark.Common;

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace Benchmark;

/// <summary>
/// High-density forwarding patterns. Direct forwarding is a non-async baseline;
/// await-only forwarding exercises async wrappers on a completed hot path.
/// </summary>
[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
public class ForwardingPatternsBenchmarks
{
    private const int ForwardingIterations = 10_000;

    [Benchmark]
    public long Task_DirectForwardingLoop()
    {
        long sum = 0;

        for (var i = 0; i < ForwardingIterations; i++)
            sum += TaskDirectForwarding().GetAwaiter().GetResult();

        return sum;
    }

    [Benchmark]
    public async Task<long> Task_AwaitOnlyForwardingLoop()
    {
        long sum = 0;

        for (var i = 0; i < ForwardingIterations; i++)
            sum += await TaskAwaitOnly();

        return sum;
    }

    [Benchmark]
    public long ValueTask_DirectForwardingLoop()
    {
        long sum = 0;

        for (var i = 0; i < ForwardingIterations; i++)
            sum += ValueTaskDirectForwarding().GetAwaiter().GetResult();

        return sum;
    }

    [Benchmark]
    public async ValueTask<long> ValueTask_AwaitOnlyForwardingLoop()
    {
        long sum = 0;

        for (var i = 0; i < ForwardingIterations; i++)
            sum += await ValueTaskAwaitOnly();

        return sum;
    }

    private static Task<int> TaskDirectForwarding()
    {
        return AsyncSources.CompletedTask;
    }

    private static async Task<int> TaskAwaitOnly()
    {
        return await AsyncSources.CompletedTask;
    }

    private static ValueTask<int> ValueTaskDirectForwarding()
    {
        return AsyncSources.CompletedValueTask;
    }

    private static async ValueTask<int> ValueTaskAwaitOnly()
    {
        return await AsyncSources.CompletedValueTask;
    }
}
