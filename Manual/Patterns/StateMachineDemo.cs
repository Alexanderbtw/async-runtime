using System.Runtime.CompilerServices;

namespace Manual.Patterns;

// Manual reproduction of what the C# compiler generates for:
//
//   async Task<int> ExampleAsync()
//   {
//       await Task.Delay(10);
//       return 42;
//   }
//
// The compiler lowers this into a struct implementing IAsyncStateMachine and a
// non-async wrapper method that creates, starts, and returns the task.
static class StateMachineDemo
{
    public static async Task RunAsync()
    {
        Console.WriteLine("=== Manual Async State Machine ===");
        Console.WriteLine("  Equivalent: async Task<int> ExampleAsync() { await Task.Delay(10); return 42; }");

        // ---- This block is exactly what the compiler emits for ExampleAsync() ----
        var sm = new ExampleStateMachine();
        sm.state   = -1;                                     // <>1__state
        sm.builder = AsyncTaskMethodBuilder<int>.Create();   // <>t__builder
        sm.builder.Start(ref sm);   // calls MoveNext() synchronously; may suspend
        Task<int> task = sm.builder.Task;
        // -------------------------------------------------------------------------

        int result = await task;
        Console.WriteLine($"  Result: {result}");
    }
}

// The generated nested type — renamed to valid C# identifiers with comments showing originals.
struct ExampleStateMachine : IAsyncStateMachine
{
    public int state;                             // <>1__state   (-1 = start/running, 0 = after first await, -2 = done)
    public AsyncTaskMethodBuilder<int> builder;   // <>t__builder
    private TaskAwaiter awaiter;                  // <>u__1

    public void MoveNext()
    {
        int result = 0;
        try
        {
            if (state == 0)
            {
                // Resumed after first await point — retrieve and discard the stored awaiter
                var a = awaiter;
                awaiter = default;
                state = -1;
                a.GetResult(); // rethrow if the task faulted
            }
            else
            {
                // state == -1: first entry — begin Task.Delay(10)
                var a = Task.Delay(10).GetAwaiter();
                if (!a.IsCompleted)
                {
                    state = 0;
                    awaiter = a;
                    // Schedules MoveNext() for when the awaiter completes.
                    // Also boxes this struct onto the heap so it survives past this stack frame.
                    builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
                    return; // suspend — MoveNext will be called again by the runtime
                }
                // Sync-completed path (rare for Task.Delay, but required for correctness)
                a.GetResult();
            }
            result = 42;
        }
        catch (Exception ex)
        {
            state = -2;
            builder.SetException(ex);
            return;
        }
        state = -2;
        builder.SetResult(result);
    }

    // Called by AwaitUnsafeOnCompleted when it boxes the struct;
    // links the heap copy's builder to the same underlying Task.
    public void SetStateMachine(IAsyncStateMachine stateMachine) =>
        builder.SetStateMachine(stateMachine);
}