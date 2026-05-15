// SynchronizationContext — shows how 'await' captures and restores execution context.
//
// When you 'await' something that isn't already complete, the compiler inserts code
// to capture SynchronizationContext.Current (or TaskScheduler.Current) and post the
// continuation back to it when the awaited task finishes.
// ConfigureAwait(false) opts out: continuation runs on whatever thread completes the task.

namespace Manual.Additional;

static class SyncContextDemo
{
    public static async Task RunAsync()
    {
        Console.WriteLine("=== SynchronizationContext (context capture & restore) ===");

        var original = SynchronizationContext.Current;
        var logging  = new LoggingSynchronizationContext();
        SynchronizationContext.SetSynchronizationContext(logging);

        Console.WriteLine($"  Before await, thread: {Thread.CurrentThread.ManagedThreadId}");

        // The awaiter sees SynchronizationContext.Current != null → posts continuation via Post()
        Console.WriteLine("  -- await Task.Yield() (captures context) --");
        await Task.Yield();
        Console.WriteLine($"  After Yield(),  thread: {Thread.CurrentThread.ManagedThreadId}");

        // Re-install: continuation ran on a ThreadPool thread that has no context set
        SynchronizationContext.SetSynchronizationContext(logging);

        // ConfigureAwait(false) ignores the captured context → posts directly to ThreadPool
        Console.WriteLine("  -- await Task.Delay(0).ConfigureAwait(false) --");
        await Task.Delay(0).ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);
        Console.WriteLine($"  After ConfigureAwait(false), thread: {Thread.CurrentThread.ManagedThreadId} (no Post)");

        SynchronizationContext.SetSynchronizationContext(original);
    }
}

sealed class LoggingSynchronizationContext : SynchronizationContext
{
    public override void Post(SendOrPostCallback d, object? state)
    {
        Console.WriteLine($"  [SyncContext.Post] on thread {Thread.CurrentThread.ManagedThreadId} → scheduling on ThreadPool");
        ThreadPool.QueueUserWorkItem(_ => d(state));
    }

    public override void Send(SendOrPostCallback d, object? state) => d(state);
}


