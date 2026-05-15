// TaskCompletionSource — the canonical bridge from callback-based APIs to TAP.
// Useful when you can't make an existing API async (e.g. legacy sockets, P/Invoke callbacks).
namespace Manual.Patterns;

static class TcsDemo
{
    public static async Task RunAsync()
    {
        Console.WriteLine("=== TaskCompletionSource (callback → TAP bridge) ===");

        // Success path
        int result = await WrapAsTask(throwError: false);
        Console.WriteLine($"  Success: {result}");

        // Error path — exception flows through the Task as usual
        try
        {
            await WrapAsTask(throwError: true);
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"  Exception: {ex.Message}");
        }

        // Cancellation path
        using var cts = new CancellationTokenSource();
        cts.Cancel();
        try
        {
            await WrapAsCancellable(cts.Token);
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine($"  Cancelled as expected");
        }
    }

    // Simulates a legacy API that delivers its result via callback instead of return value.
    static void LegacyApi(bool throwError, Action<int, Exception?> callback)
    {
        ThreadPool.QueueUserWorkItem(_ =>
        {
            Thread.Sleep(20);
            if (throwError) callback(0, new InvalidOperationException("Legacy API failed"));
            else            callback(42, null);
        });
    }

    // Bridge: wrap the callback into a Task<int> using TaskCompletionSource<int>.
    static Task<int> WrapAsTask(bool throwError)
    {
        var tcs = new TaskCompletionSource<int>();
        LegacyApi(throwError, (value, ex) =>
        {
            if (ex is not null) tcs.SetException(ex);
            else                tcs.SetResult(value);
        });
        return tcs.Task;
    }

    // Also demonstrates SetCanceled for CancellationToken integration.
    static Task WrapAsCancellable(CancellationToken ct)
    {
        var tcs = new TaskCompletionSource();
        if (ct.IsCancellationRequested)
        {
            tcs.SetCanceled(ct);
            return tcs.Task;
        }
        ct.Register(() => tcs.TrySetCanceled(ct));
        // (real work would start here)
        return tcs.Task;
    }
}