using System.Runtime.CompilerServices;

using Benchmark.Common;

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace Benchmark;

/// <summary>
/// Меряет propagation исключений через async boundaries: before await, after completed await, after suspended await и direct faulted forwarding.
/// Это проверка exception-path overhead, где стоимость самих исключений может доминировать над эффектом async-модели.
/// </summary>
[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
public class ExceptionPathBenchmarks
{
    private const int ExceptionIterations = 1_000;

    [Benchmark]
    public int Direct_ThrowLoop_Control()
    {
        var handled = 0;

        for (var i = 0; i < ExceptionIterations; i++)
        {
            try
            {
                handled += DirectThrow();
            }
            catch (InvalidOperationException)
            {
                handled++;
            }
        }

        return handled;
    }

    [Benchmark]
    public int Task_DirectFaultedForwardingLoop()
    {
        var handled = 0;

        for (var i = 0; i < ExceptionIterations; i++)
            handled += Consume(TaskDirectFaultedForwarding());

        return handled;
    }

    [Benchmark]
    public int Task_ThrowBeforeAwaitLoop()
    {
        var handled = 0;

        for (var i = 0; i < ExceptionIterations; i++)
            handled += Consume(TaskThrowBeforeAwait());

        return handled;
    }

    [Benchmark]
    public int Task_ThrowAfterCompletedAwaitLoop()
    {
        var handled = 0;

        for (var i = 0; i < ExceptionIterations; i++)
            handled += Consume(TaskThrowAfterCompletedAwait());

        return handled;
    }

    [Benchmark]
    public int Task_ThrowAfterSuspendedAwaitLoop()
    {
        var handled = 0;

        for (var i = 0; i < ExceptionIterations; i++)
            handled += Consume(TaskThrowAfterSuspendedAwait());

        return handled;
    }

    [Benchmark]
    public int ValueTask_DirectFaultedForwardingLoop()
    {
        var handled = 0;

        for (var i = 0; i < ExceptionIterations; i++)
            handled += Consume(ValueTaskDirectFaultedForwarding());

        return handled;
    }

    [Benchmark]
    public int ValueTask_ThrowBeforeAwaitLoop()
    {
        var handled = 0;

        for (var i = 0; i < ExceptionIterations; i++)
            handled += Consume(ValueTaskThrowBeforeAwait());

        return handled;
    }

    [Benchmark]
    public int ValueTask_ThrowAfterCompletedAwaitLoop()
    {
        var handled = 0;

        for (var i = 0; i < ExceptionIterations; i++)
            handled += Consume(ValueTaskThrowAfterCompletedAwait());

        return handled;
    }

    [Benchmark]
    public int ValueTask_ThrowAfterSuspendedAwaitLoop()
    {
        var handled = 0;

        for (var i = 0; i < ExceptionIterations; i++)
            handled += Consume(ValueTaskThrowAfterSuspendedAwait());

        return handled;
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
    private static int DirectThrow()
    {
        return ThrowFault();
    }

    private static Task<int> TaskDirectFaultedForwarding()
    {
        return Task.FromException<int>(CreateFault());
    }

    private static ValueTask<int> ValueTaskDirectFaultedForwarding()
    {
        return new ValueTask<int>(Task.FromException<int>(CreateFault()));
    }

    private static async Task<int> TaskThrowBeforeAwait()
    {
        var result = ThrowFault();
        await Task.CompletedTask;
        return result;
    }

    private static async Task<int> TaskThrowAfterCompletedAwait()
    {
        await AsyncSources.CompletedTask;
        return ThrowFault();
    }

    private static async Task<int> TaskThrowAfterSuspendedAwait()
    {
        await AlwaysIncomplete.Yield();
        return ThrowFault();
    }

    private static async ValueTask<int> ValueTaskThrowBeforeAwait()
    {
        var result = ThrowFault();
        await new ValueTask();
        return result;
    }

    private static async ValueTask<int> ValueTaskThrowAfterCompletedAwait()
    {
        await AsyncSources.CompletedValueTask;
        return ThrowFault();
    }

    private static async ValueTask<int> ValueTaskThrowAfterSuspendedAwait()
    {
        await AlwaysIncomplete.Yield();
        return ThrowFault();
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
