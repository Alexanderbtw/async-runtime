// ValueTask vs Task — allocation comparison for sync-completing hot paths.
//
// Task<T> is a class: every call to Task.FromResult() allocates a new object (~56 bytes).
// ValueTask<T> is a struct: wrapping a plain value costs zero heap allocation.
// The difference matters for high-throughput code where the result is often cached/sync.
namespace Manual.Additional;

static class ValueTaskDemo
{
    private static readonly int _cached = 42;

    public static async Task RunAsync()
    {
        Console.WriteLine("=== ValueTask vs Task (allocation on sync-completing path) ===");

        const int iterations = 10_000;

        // Warmup — JIT and caches must be hot before we measure
        for (int i = 0; i < 200; i++)
        {
            _ = await GetWithTask(cached: true);
            _ = await GetWithValueTask(cached: true);
        }

        GC.Collect(2, GCCollectionMode.Forced, blocking: true);
        long before = GC.GetTotalAllocatedBytes(precise: false);
        for (int i = 0; i < iterations; i++) _ = await GetWithTask(cached: true);
        long taskBytes = GC.GetTotalAllocatedBytes(precise: false) - before;

        GC.Collect(2, GCCollectionMode.Forced, blocking: true);
        before = GC.GetTotalAllocatedBytes(precise: false);
        for (int i = 0; i < iterations; i++) _ = await GetWithValueTask(cached: true);
        long valueTaskBytes = GC.GetTotalAllocatedBytes(precise: false) - before;

        Console.WriteLine($"  Task<int>      {iterations} cached calls: {taskBytes,8} bytes  (~{taskBytes / iterations} B/call)");
        Console.WriteLine($"  ValueTask<int> {iterations} cached calls: {valueTaskBytes,8} bytes  (~{valueTaskBytes / iterations} B/call)");
        Console.WriteLine($"  Saved: {taskBytes - valueTaskBytes} bytes ({(double)(taskBytes - valueTaskBytes) / taskBytes:P0} less)");
    }

    // Always allocates a new Task<int> object on the heap — even when the value is ready.
    static Task<int> GetWithTask(bool cached) =>
        cached ? Task.FromResult(_cached) : SlowAsync();

    // Returns a struct wrapping the value — zero allocation when result is sync/cached.
    static ValueTask<int> GetWithValueTask(bool cached) =>
        cached ? new ValueTask<int>(_cached) : new ValueTask<int>(SlowAsync());

    static async Task<int> SlowAsync()
    {
        await Task.Delay(1);
        return _cached;
    }
}