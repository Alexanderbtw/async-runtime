using System.ComponentModel;

namespace Manual.Patterns;

// EBA / EAP — Event-Based Asynchronous Pattern (.NET 2.0 / BackgroundWorker-style)
// Contract: call XxxAsync() to start; subscribe XxxCompleted to receive the result.
static class EbaDemo
{
    public static async Task RunAsync()
    {
        Console.WriteLine("=== EBA (Event-Based Asynchronous Pattern) ===");

        var worker = new AsyncWorker();
        var tcs = new TaskCompletionSource();

        worker.WorkCompleted += (_, e) =>
        {
            Console.WriteLine($"  WorkCompleted on thread {Thread.CurrentThread.ManagedThreadId}, result: {e.Result}");
            tcs.SetResult();
        };

        Console.WriteLine($"  RunAsync() called on thread {Thread.CurrentThread.ManagedThreadId}");
        worker.RunAsync();
        await tcs.Task;
    }
}

sealed class WorkCompletedEventArgs(int result, Exception? error, bool cancelled, object? state)
    : AsyncCompletedEventArgs(error, cancelled, state)
{
    public int Result { get; } = result;
}

sealed class AsyncWorker
{
    // EAP rule: capture the caller's SynchronizationContext at construction time
    // so that the Completed event fires on the original thread (e.g. UI thread).
    private readonly SynchronizationContext? _ctx = SynchronizationContext.Current;

    public event EventHandler<WorkCompletedEventArgs>? WorkCompleted;

    public void RunAsync()
    {
        ThreadPool.QueueUserWorkItem(_ =>
        {
            Console.WriteLine($"  Working on thread {Thread.CurrentThread.ManagedThreadId}...");
            Thread.Sleep(30);

            var args = new WorkCompletedEventArgs(99, null, false, null);

            // Marshal event back to the captured context (UI thread, etc.).
            // In a console app SynchronizationContext is null, so we fire directly.
            if (_ctx is not null)
                _ctx.Post(_ => WorkCompleted?.Invoke(this, args), null);
            else
                WorkCompleted?.Invoke(this, args);
        });
    }
}