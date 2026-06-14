using System.Runtime.CompilerServices;

namespace Benchmark.Common;

public readonly struct AlwaysIncomplete
{
    public static AlwaysIncomplete Yield()
    {
        return default;
    }

    public Awaiter GetAwaiter()
    {
        return default;
    }

    public readonly struct Awaiter : ICriticalNotifyCompletion
    {
        public bool IsCompleted => false;

        public void GetResult()
        {
        }

        public void OnCompleted(Action continuation)
        {
            continuation();
        }

        public void UnsafeOnCompleted(Action continuation)
        {
            continuation();
        }
    }
}
