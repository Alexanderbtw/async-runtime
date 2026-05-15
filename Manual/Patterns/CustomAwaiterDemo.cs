using System.Runtime.CompilerServices;

namespace Manual.Patterns;

// Custom Awaiter — shows that 'await' is pure duck-typing.
// The compiler only requires:
//   expr.GetAwaiter()  →  an object with
//     bool IsCompleted { get; }
//     void OnCompleted(Action continuation)   // or ICriticalNotifyCompletion
//     TResult GetResult()
// No interface is strictly required on the awaitable itself.
static class CustomAwaiterDemo
{
    public static async Task RunAsync()
    {
        Console.WriteLine("=== Custom Awaiter (duck-typing) ===");
        Console.WriteLine($"  Before await, thread: {Thread.CurrentThread.ManagedThreadId}");

        await new Delay100Awaitable(); // works — compiler finds GetAwaiter()

        Console.WriteLine($"  After await,  thread: {Thread.CurrentThread.ManagedThreadId}");
    }
}

// The awaitable — only needs GetAwaiter(); no interface required.
readonly struct Delay100Awaitable
{
    public Delay100Awaiter GetAwaiter() => new();
}

// The awaiter — must have IsCompleted, OnCompleted, GetResult.
// Implementing INotifyCompletion lets the state machine use AwaitUnsafeOnCompleted
// for better performance (avoids an extra allocation when capturing ExecutionContext).
sealed class Delay100Awaiter : INotifyCompletion
{
    private readonly Task _task = Task.Delay(100);

    public bool IsCompleted => _task.IsCompleted;

    // Called by the state machine when IsCompleted == false.
    // Must invoke 'continuation' exactly once when the result is ready.
    public void OnCompleted(Action continuation) =>
        _task.ContinueWith(_ => continuation(), TaskScheduler.Default);

    // Called by the state machine after IsCompleted or after OnCompleted fires.
    public void GetResult() => _task.GetAwaiter().GetResult();
}