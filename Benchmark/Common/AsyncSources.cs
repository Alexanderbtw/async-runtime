using System.Runtime.CompilerServices;

namespace Benchmark.Common;

public static class AsyncSources
{
    public static int LargeValue => Random.Shared.Next(10_000, 1_000_000);

    public static readonly Task<int> CompletedTask = Task.FromResult(LargeValue);
    public static readonly ValueTask<int> CompletedValueTask = new(LargeValue);

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static int ExpensiveOrSideEffect()
    {
        return 42;
    }
}