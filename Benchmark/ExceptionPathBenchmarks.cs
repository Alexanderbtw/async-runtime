using System.Runtime.CompilerServices;

using Benchmark.Common;

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace Benchmark;

/// <summary>
/// Faulted async-chain propagation through await boundaries.
/// </summary>
[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
public class ExceptionPathBenchmarks
{
    [Params(1, 2, 4, 8, 16, 32, 64)]
    public int Depth { get; set; }

    [Benchmark]
    public int Direct_ThrowChain_Control()
    {
        try
        {
            return DirectThrowChain(Depth);
        }
        catch (InvalidOperationException)
        {
            return 1;
        }
    }

    [Benchmark]
    public int Task_DirectFaultedForwardingChain()
    {
        return Consume(TaskDirectFaultedForwardingChain(Depth));
    }

    [Benchmark]
    public int Task_ThrowBeforeAwaitChain()
    {
        return Consume(TaskThrowBeforeAwaitChain(Depth));
    }

    [Benchmark]
    public int Task_ThrowAfterCompletedAwaitChain()
    {
        return Consume(TaskThrowAfterCompletedAwaitChain(Depth));
    }

    [Benchmark]
    public int Task_ThrowAfterSuspendedAwaitChain()
    {
        return Consume(TaskThrowAfterSuspendedAwaitChain(Depth));
    }

    [Benchmark]
    public int ValueTask_DirectFaultedForwardingChain()
    {
        return Consume(ValueTaskDirectFaultedForwardingChain(Depth));
    }

    [Benchmark]
    public int ValueTask_ThrowBeforeAwaitChain()
    {
        return Consume(ValueTaskThrowBeforeAwaitChain(Depth));
    }

    [Benchmark]
    public int ValueTask_ThrowAfterCompletedAwaitChain()
    {
        return Consume(ValueTaskThrowAfterCompletedAwaitChain(Depth));
    }

    [Benchmark]
    public int ValueTask_ThrowAfterSuspendedAwaitChain()
    {
        return Consume(ValueTaskThrowAfterSuspendedAwaitChain(Depth));
    }

    private static int Consume(Task<int> task)
    {
        try
        {
            return task.GetAwaiter().GetResult();
        }
        catch (InvalidOperationException)
        {
            return 1;
        }
    }

    private static int Consume(ValueTask<int> task)
    {
        try
        {
            return task.GetAwaiter().GetResult();
        }
        catch (InvalidOperationException)
        {
            return 1;
        }
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private static int DirectThrowChain(int depth)
    {
        if (depth == 0)
            return ThrowFault();

        return DirectThrowChain(depth - 1);
    }

    private static Task<int> TaskDirectFaultedForwardingChain(int depth)
    {
        if (depth == 0)
            return Task.FromException<int>(CreateFault());

        return TaskDirectFaultedForwardingChain(depth - 1);
    }

    private static ValueTask<int> ValueTaskDirectFaultedForwardingChain(int depth)
    {
        if (depth == 0)
            return new ValueTask<int>(Task.FromException<int>(CreateFault()));

        return ValueTaskDirectFaultedForwardingChain(depth - 1);
    }

    private static async Task<int> TaskThrowBeforeAwaitChain(int depth)
    {
        if (depth == 0)
            return ThrowFault();

        return await TaskThrowBeforeAwaitChain(depth - 1);
    }

    private static async Task<int> TaskThrowAfterCompletedAwaitChain(int depth)
    {
        if (depth == 0)
        {
            await AsyncSources.CompletedTask;
            return ThrowFault();
        }

        return await TaskThrowAfterCompletedAwaitChain(depth - 1);
    }

    private static async Task<int> TaskThrowAfterSuspendedAwaitChain(int depth)
    {
        if (depth == 0)
        {
            await AlwaysIncomplete.Yield();
            return ThrowFault();
        }

        return await TaskThrowAfterSuspendedAwaitChain(depth - 1);
    }

    private static async ValueTask<int> ValueTaskThrowBeforeAwaitChain(int depth)
    {
        if (depth == 0)
            return ThrowFault();

        return await ValueTaskThrowBeforeAwaitChain(depth - 1);
    }

    private static async ValueTask<int> ValueTaskThrowAfterCompletedAwaitChain(int depth)
    {
        if (depth == 0)
        {
            await AsyncSources.CompletedValueTask;
            return ThrowFault();
        }

        return await ValueTaskThrowAfterCompletedAwaitChain(depth - 1);
    }

    private static async ValueTask<int> ValueTaskThrowAfterSuspendedAwaitChain(int depth)
    {
        if (depth == 0)
        {
            await AlwaysIncomplete.Yield();
            return ThrowFault();
        }

        return await ValueTaskThrowAfterSuspendedAwaitChain(depth - 1);
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private static int ThrowFault()
    {
        throw CreateFault();
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private static InvalidOperationException CreateFault()
    {
        return new InvalidOperationException("Benchmark exception");
    }
}
