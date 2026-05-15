using BenchmarkDotNet.Attributes;

namespace Benchmark;

[MemoryDiagnoser]
[HideColumns("Error", "StdDev", "RatioSD")]
public class TaskTimeoutBenchmarks
{
    private readonly static Task CompletedTask = Task.CompletedTask;
    private readonly static TimeSpan LongTimeout = TimeSpan.FromSeconds(30);
    private readonly static TimeSpan ShortTimeout = TimeSpan.FromMilliseconds(5);

    private TaskCompletionSource _neverTcs = new();

    [IterationSetup(
        Targets =
        [
            nameof(WhenAny_Timeout),
            nameof(WaitAsync_Timeout)
        ])]
    public void SetupNeverTask() => _neverTcs = new TaskCompletionSource();

    [Benchmark]
    public Task WaitAsync_Completed()
    {
        Task t = CompletedTask;
        return t.WaitAsync(LongTimeout);
    }

    [Benchmark]
    public async Task WaitAsync_Timeout()
    {
        Task t = _neverTcs.Task;
        try
        {
            await t.WaitAsync(ShortTimeout);
        }
        catch (TimeoutException)
        {
            /* expected */
        }
    }

    [Benchmark(Baseline = true)]
    public async Task WhenAny_Completed()
    {
        Task t = CompletedTask;
        using var cts = new CancellationTokenSource();
        if (await Task.WhenAny(Task.Delay(LongTimeout, cts.Token), t) != t)
            throw new TimeoutException();
        cts.Cancel();
        await t;
    }

    [Benchmark]
    public async Task WhenAny_Timeout()
    {
        Task t = _neverTcs.Task;
        using var cts = new CancellationTokenSource();
        try
        {
            if (await Task.WhenAny(Task.Delay(ShortTimeout, cts.Token), t) != t)
                throw new TimeoutException();
            cts.Cancel();
            await t;
        }
        catch (TimeoutException)
        {
            /* expected */
        }
    }
}
