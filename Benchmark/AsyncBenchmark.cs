using System.Runtime.CompilerServices;

using Benchmark.Common;

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Toolchains.InProcess.Emit;

namespace Benchmark;

/// <summary>
/// Best-case completed async hot path: 10 000 раз вызывает async wrapper над уже завершенным Task.FromResult или ValueTask.
/// Показывает экономию runtime async на overhead async machinery и, для Task, на аллокациях wrapper-задач.
/// </summary>
[MemoryDiagnoser]
[Config(typeof(InProcessConfig))]
public class AsyncBenchmark
{
    [Benchmark]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public async Task<int> Task_CompletedWrapperLoop()
    {
        var data = new List<int>(capacity: 10_000);
        for (var i = 0; i < 10_000; i++)
        {
            data.Add(await GetFakeAsync());
        }

        return data[9_999];
    }

    [Benchmark]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public async ValueTask<int> ValueTask_CompletedWrapperLoop()
    {
        var data = new List<int>(capacity: 10_000);
        for (var i = 0; i < 10_000; i++)
        {
            data.Add(await GetFakeValueTaskAsync());
        }

        return data[9_999];
    }

    private static async Task<int> GetFakeAsync()
    {
        return await Task.FromResult(AsyncSources.LargeRandomValue);
    }

    private static async ValueTask<int> GetFakeValueTaskAsync()
    {
        return await new ValueTask<int>(AsyncSources.LargeRandomValue);
    }

    public class InProcessConfig : ManualConfig
    {
        public InProcessConfig()
        {
            AddJob(Job.Default.WithToolchain(InProcessEmitToolchain.Instance));
        }
    }
}
