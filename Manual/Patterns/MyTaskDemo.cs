using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;

namespace Manual.Patterns;

// A tiny task-like type for demo purposes.
// Shows the minimal pieces that make `async MyTask<T>` and `await MyTask<T>` work:
//   - AsyncMethodBuilderAttribute on the return type
//   - builder with Create/Start/Task/SetResult/SetException/Await* methods
//   - awaiter with IsCompleted/OnCompleted/GetResult
static class MyTaskDemo
{
    public static async Task RunAsync()
    {
        Console.WriteLine("=== Custom MyTask<T> (minimal task-like type) ===");
        Console.WriteLine($"  Main starts on thread {Thread.CurrentThread.ManagedThreadId}");

        var result = await Caller();
        Console.WriteLine($"  Caller result: {result}");

        try
        {
            await Fails();
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"  Failure flowed through MyTask: {ex.Message}");
        }
    }

    static async MyTask<int> Caller()
    {
        Console.WriteLine($"  Caller starts on thread {Thread.CurrentThread.ManagedThreadId}");

        var x = await Callee(20, 10);
        var y = await Callee(22, 10);

        Console.WriteLine($"  Caller resumes on thread {Thread.CurrentThread.ManagedThreadId}");
        return x + y;
    }

    static async MyTask<int> Fails()
    {
        await Task.Delay(10);
        throw new InvalidOperationException("boom");
    }

    static async Task<int> Callee(int value, int delayMs)
    {
        await Task.Delay(delayMs);
        Console.WriteLine($"  Callee({value}) completed on thread {Thread.CurrentThread.ManagedThreadId}");
        return value;
    }
}

[AsyncMethodBuilder(typeof(MyTaskMethodBuilder<>))]
readonly struct MyTask<T>
{
    private readonly MyTaskState<T> _state;

    internal MyTask(MyTaskState<T> state) => _state = state;

    public MyTaskAwaiter<T> GetAwaiter() => new(_state);
}

readonly struct MyTaskAwaiter<T> : ICriticalNotifyCompletion
{
    private readonly MyTaskState<T> _state;

    internal MyTaskAwaiter(MyTaskState<T> state) => _state = state;

    public bool IsCompleted => _state.IsCompleted;

    public T GetResult() => _state.GetResult();

    public void OnCompleted(Action continuation) => _state.OnCompleted(continuation);

    public void UnsafeOnCompleted(Action continuation) => _state.OnCompleted(continuation);
}

struct MyTaskMethodBuilder<T>
{
    private MyTaskState<T>? _state;

    private MyTaskState<T> State => _state ??= new MyTaskState<T>();

    public static MyTaskMethodBuilder<T> Create() => new();

    public MyTask<T> Task => new(State);

    public void Start<TStateMachine>(ref TStateMachine stateMachine)
        where TStateMachine : IAsyncStateMachine =>
        stateMachine.MoveNext();

    public void SetResult(T result) => State.SetResult(result);

    public void SetException(Exception exception) => State.SetException(exception);

    public void SetStateMachine(IAsyncStateMachine stateMachine)
    {
        // No-op for this demo. The real builder uses this hook when the compiler
        // boxes the state machine and links it to the underlying task machinery.
    }

    public void AwaitOnCompleted<TAwaiter, TStateMachine>(
        ref TAwaiter awaiter,
        ref TStateMachine stateMachine)
        where TAwaiter : INotifyCompletion
        where TStateMachine : IAsyncStateMachine
    {
        var box = stateMachine;
        awaiter.OnCompleted(box.MoveNext);
    }

    public void AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(
        ref TAwaiter awaiter,
        ref TStateMachine stateMachine)
        where TAwaiter : ICriticalNotifyCompletion
        where TStateMachine : IAsyncStateMachine
    {
        var box = stateMachine;
        awaiter.UnsafeOnCompleted(box.MoveNext);
    }
}

sealed class MyTaskState<T>
{
    private readonly object _gate = new();
    private List<Action>? _continuations;
    private ExceptionDispatchInfo? _exception;
    private T? _result;
    private bool _isCompleted;

    public bool IsCompleted
    {
        get
        {
            lock (_gate)
            {
                return _isCompleted;
            }
        }
    }

    public T GetResult()
    {
        lock (_gate)
        {
            if (!_isCompleted)
            {
                throw new InvalidOperationException("MyTask result is not ready yet.");
            }

            _exception?.Throw();
            return _result!;
        }
    }

    public void OnCompleted(Action continuation)
    {
        var runNow = false;

        lock (_gate)
        {
            if (_isCompleted)
            {
                runNow = true;
            }
            else
            {
                _continuations ??= [];
                _continuations.Add(continuation);
            }
        }

        if (runNow)
        {
            QueueContinuation(continuation);
        }
    }

    public void SetResult(T result)
    {
        List<Action>? continuations;

        lock (_gate)
        {
            if (_isCompleted)
            {
                throw new InvalidOperationException("MyTask is already completed.");
            }

            _result = result;
            _isCompleted = true;
            continuations = _continuations;
            _continuations = null;
        }

        QueueContinuations(continuations);
    }

    public void SetException(Exception exception)
    {
        List<Action>? continuations;

        lock (_gate)
        {
            if (_isCompleted)
            {
                throw new InvalidOperationException("MyTask is already completed.");
            }

            _exception = ExceptionDispatchInfo.Capture(exception);
            _isCompleted = true;
            continuations = _continuations;
            _continuations = null;
        }

        QueueContinuations(continuations);
    }

    private static void QueueContinuations(List<Action>? continuations)
    {
        if (continuations is null)
        {
            return;
        }

        foreach (var continuation in continuations)
        {
            QueueContinuation(continuation);
        }
    }

    private static void QueueContinuation(Action continuation) =>
        ThreadPool.QueueUserWorkItem(static state => ((Action)state!).Invoke(), continuation);
}
