using Benchmark.Common;

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace Benchmark;

/// <summary>
/// Меряет влияние живого состояния через await с большим value-type payload.
/// Сравнивает dead-before-await, live-after-await и disjoint-state сценарии, чтобы оценить стоимость сохранения locals/state.
/// </summary>
[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
public class ControlledLiveStateBenchmarks
{
    private const int LiveStateIterations = 1_000;
    private const int PayloadStride = 256;

    [Benchmark]
    public async Task<long> Task_DeadBeforeAwait_ControlLoop()
    {
        long sum = 0;

        for (var i = 0; i < LiveStateIterations; i++)
            sum += await TaskDeadBeforeAwaitControl(Payload128.Create(i * PayloadStride));

        return sum;
    }

    [Benchmark]
    public async Task<long> Task_OneAwait_LiveStateLoop()
    {
        long sum = 0;

        for (var i = 0; i < LiveStateIterations; i++)
            sum += await TaskOneAwaitLiveState(Payload128.Create(i * PayloadStride));

        return sum;
    }

    [Benchmark]
    public async Task<long> Task_TwoAwaits_DisjointStateLoop()
    {
        long sum = 0;

        for (var i = 0; i < LiveStateIterations; i++)
            sum += await TaskTwoAwaitsDisjointState(Payload128.Create(i * PayloadStride));

        return sum;
    }

    [Benchmark]
    public async ValueTask<long> ValueTask_DeadBeforeAwait_ControlLoop()
    {
        long sum = 0;

        for (var i = 0; i < LiveStateIterations; i++)
            sum += await ValueTaskDeadBeforeAwaitControl(Payload128.Create(i * PayloadStride));

        return sum;
    }

    [Benchmark]
    public async ValueTask<long> ValueTask_OneAwait_LiveStateLoop()
    {
        long sum = 0;

        for (var i = 0; i < LiveStateIterations; i++)
            sum += await ValueTaskOneAwaitLiveState(Payload128.Create(i * PayloadStride));

        return sum;
    }

    [Benchmark]
    public async ValueTask<long> ValueTask_TwoAwaits_DisjointStateLoop()
    {
        long sum = 0;

        for (var i = 0; i < LiveStateIterations; i++)
            sum += await ValueTaskTwoAwaitsDisjointState(Payload128.Create(i * PayloadStride));

        return sum;
    }

    private static async Task<int> TaskDeadBeforeAwaitControl<TPayload>(TPayload dead)
        where TPayload : struct, ILiveStatePayload<TPayload>
    {
        var seed = dead.Sum();

        await AlwaysIncomplete.Yield();

        return seed + AsyncSources.ExpensiveOrSideEffect(0);
    }

    private static async Task<int> TaskOneAwaitLiveState<TPayload>(TPayload live)
        where TPayload : struct, ILiveStatePayload<TPayload>
    {
        await AlwaysIncomplete.Yield();

        return live.Sum();
    }

    private static async Task<int> TaskTwoAwaitsDisjointState<TPayload>(TPayload first)
        where TPayload : struct, ILiveStatePayload<TPayload>
    {
        await AlwaysIncomplete.Yield();

        var firstSum = first.Sum();
        var second = TPayload.Create(1_000);

        await AlwaysIncomplete.Yield();

        return firstSum + second.Sum();
    }

    private static async ValueTask<int> ValueTaskDeadBeforeAwaitControl<TPayload>(TPayload dead)
        where TPayload : struct, ILiveStatePayload<TPayload>
    {
        var seed = dead.Sum();

        await AlwaysIncomplete.Yield();

        return seed + AsyncSources.ExpensiveOrSideEffect(0);
    }

    private static async ValueTask<int> ValueTaskOneAwaitLiveState<TPayload>(TPayload live)
        where TPayload : struct, ILiveStatePayload<TPayload>
    {
        await AlwaysIncomplete.Yield();

        return live.Sum();
    }

    private static async ValueTask<int> ValueTaskTwoAwaitsDisjointState<TPayload>(TPayload first)
        where TPayload : struct, ILiveStatePayload<TPayload>
    {
        await AlwaysIncomplete.Yield();

        var firstSum = first.Sum();
        var second = TPayload.Create(1_000);

        await AlwaysIncomplete.Yield();

        return firstSum + second.Sum();
    }

    private static int Value(int index)
    {
        return AsyncSources.ExpensiveOrSideEffect(index);
    }

    private interface ILiveStatePayload<TSelf>
        where TSelf : struct, ILiveStatePayload<TSelf>
    {
        static abstract TSelf Create(int offset = 0);

        int Sum();
    }

    private readonly struct Payload4 : ILiveStatePayload<Payload4>
    {
        private readonly int _x1;
        private readonly int _x2;
        private readonly int _x3;
        private readonly int _x4;

        private Payload4(int x1, int x2, int x3, int x4)
        {
            _x1 = x1;
            _x2 = x2;
            _x3 = x3;
            _x4 = x4;
        }

        public static Payload4 Create(int offset = 0)
        {
            return new Payload4(
                Value(offset + 1),
                Value(offset + 2),
                Value(offset + 3),
                Value(offset + 4));
        }

        public int Sum()
        {
            return _x1 + _x2 + _x3 + _x4;
        }
    }

    private readonly struct Payload8 : ILiveStatePayload<Payload8>
    {
        private readonly Payload4 _left;
        private readonly Payload4 _right;

        private Payload8(Payload4 left, Payload4 right)
        {
            _left = left;
            _right = right;
        }

        public static Payload8 Create(int offset = 0)
        {
            return new Payload8(
                Payload4.Create(offset),
                Payload4.Create(offset + 4));
        }

        public int Sum()
        {
            return _left.Sum() + _right.Sum();
        }
    }

    private readonly struct Payload16 : ILiveStatePayload<Payload16>
    {
        private readonly Payload8 _left;
        private readonly Payload8 _right;

        private Payload16(Payload8 left, Payload8 right)
        {
            _left = left;
            _right = right;
        }

        public static Payload16 Create(int offset = 0)
        {
            return new Payload16(
                Payload8.Create(offset),
                Payload8.Create(offset + 8));
        }

        public int Sum()
        {
            return _left.Sum() + _right.Sum();
        }
    }

    private readonly struct Payload32 : ILiveStatePayload<Payload32>
    {
        private readonly Payload16 _left;
        private readonly Payload16 _right;

        private Payload32(Payload16 left, Payload16 right)
        {
            _left = left;
            _right = right;
        }

        public static Payload32 Create(int offset = 0)
        {
            return new Payload32(
                Payload16.Create(offset),
                Payload16.Create(offset + 16));
        }

        public int Sum()
        {
            return _left.Sum() + _right.Sum();
        }
    }

    private readonly struct Payload64 : ILiveStatePayload<Payload64>
    {
        private readonly Payload32 _left;
        private readonly Payload32 _right;

        private Payload64(Payload32 left, Payload32 right)
        {
            _left = left;
            _right = right;
        }

        public static Payload64 Create(int offset = 0)
        {
            return new Payload64(
                Payload32.Create(offset),
                Payload32.Create(offset + 32));
        }

        public int Sum()
        {
            return _left.Sum() + _right.Sum();
        }
    }

    private readonly struct Payload128 : ILiveStatePayload<Payload128>
    {
        private readonly Payload64 _left;
        private readonly Payload64 _right;

        private Payload128(Payload64 left, Payload64 right)
        {
            _left = left;
            _right = right;
        }

        public static Payload128 Create(int offset = 0)
        {
            return new Payload128(
                Payload64.Create(offset),
                Payload64.Create(offset + 64));
        }

        public int Sum()
        {
            return _left.Sum() + _right.Sum();
        }
    }
}
