using System.Runtime.CompilerServices;

namespace Benchmark.Common;

public static class AsyncSources
{
    public static int LargeRandomValue => Random.Shared.Next(10_000, 1_000_000);

    public static readonly Task<int> CompletedTask = Task.FromResult(LargeRandomValue);
    public static readonly ValueTask<int> CompletedValueTask = new(LargeRandomValue);

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static int ExpensiveOrSideEffect()
    {
        return ExpensiveOrSideEffect(0);
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static int ExpensiveOrSideEffect(int value)
    {
        return value + 42;
    }
}
