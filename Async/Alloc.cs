using Benchmark.Common;

namespace Async;

internal class Alloc
{
    public static async Task Start()
    {
        var res = await TaskChain(8);
        Console.WriteLine(res);
    }

    private static async Task<int> TaskChain(int depth)
    {
        if (depth <= 0)
        {
            await AlwaysIncomplete.Yield();
            return 1;
        }

        return 1 + await TaskChain(depth - 1).ConfigureAwait(false);
    }
}