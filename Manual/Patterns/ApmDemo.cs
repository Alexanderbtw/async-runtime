using System.Runtime.ExceptionServices;

namespace Manual.Patterns;

// APM — Asynchronous Programming Model (.NET 1.0 / 2.0)
// Contract: BeginXxx returns IAsyncResult; EndXxx blocks until done and returns the value.
static class ApmDemo
{
    public static Task RunAsync()
    {
        Console.WriteLine("=== APM (Asynchronous Programming Model) ===");

        // Pattern 1 — caller blocks on WaitHandle
        Console.WriteLine($"  [WaitHandle] caller thread: {Thread.CurrentThread.ManagedThreadId}");
        var ar = BeginDoWork(null, null);
        ar.AsyncWaitHandle.WaitOne();
        Console.WriteLine($"  [WaitHandle] result: {EndDoWork(ar)}");

        // Pattern 2 — fire-and-forget with callback
        Console.WriteLine($"  [Callback] registering...");
        var tcs = new TaskCompletionSource();
        BeginDoWork(iar =>
        {
            Console.WriteLine($"  [Callback] thread: {Thread.CurrentThread.ManagedThreadId}, result: {EndDoWork(iar)}");
            tcs.SetResult();
        }, null);

        return tcs.Task;
    }

    static IAsyncResult BeginDoWork(AsyncCallback? callback, object? state)
    {
        var ar = new WorkAsyncResult(state);
        ThreadPool.QueueUserWorkItem(_ =>
        {
            Thread.Sleep(20); // simulate async I/O
            ar.Complete(42);
            callback?.Invoke(ar);
        });
        return ar;
    }

    static int EndDoWork(IAsyncResult asyncResult) =>
        ((WorkAsyncResult)asyncResult).GetResult();
}

sealed class WorkAsyncResult : IAsyncResult
{
    private readonly object? _state;
    private readonly ManualResetEventSlim _gate = new(false);
    private int _result;
    private Exception? _exception;

    public WorkAsyncResult(object? state) => _state = state;

    public object? AsyncState => _state;
    public WaitHandle AsyncWaitHandle => _gate.WaitHandle;
    public bool CompletedSynchronously => false;
    public bool IsCompleted => _gate.IsSet;

    internal void Complete(int result) { _result = result; _gate.Set(); }
    internal void Fail(Exception ex)   { _exception = ex;   _gate.Set(); }

    internal int GetResult()
    {
        _gate.Wait();
        if (_exception is not null)
            ExceptionDispatchInfo.Capture(_exception).Throw();
        return _result;
    }
}