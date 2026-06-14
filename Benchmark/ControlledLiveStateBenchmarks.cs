using Benchmark.Common;

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace Benchmark;

/// <summary>
/// Forced suspend/resume with controlled live and dead value-type payloads.
/// </summary>
[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
public class ControlledLiveStateBenchmarks
{
    [Params(0, 1, 4, 8, 16, 32, 64, 128)]
    public int PayloadValues { get; set; }

    [Benchmark]
    public Task<int> Task_DeadBeforeAwait_Control()
    {
        return PayloadValues switch
        {
            0 => TaskDeadBeforeAwaitControl(EmptyPayload.Create()),
            1 => TaskDeadBeforeAwaitControl(Payload1.Create()),
            4 => TaskDeadBeforeAwaitControl(Payload4.Create()),
            8 => TaskDeadBeforeAwaitControl(Payload8.Create()),
            16 => TaskDeadBeforeAwaitControl(Payload16.Create()),
            32 => TaskDeadBeforeAwaitControl(Payload32.Create()),
            64 => TaskDeadBeforeAwaitControl(Payload64.Create()),
            128 => TaskDeadBeforeAwaitControl(Payload128.Create()),
            _ => throw new ArgumentOutOfRangeException(nameof(PayloadValues), PayloadValues, null)
        };
    }

    [Benchmark]
    public Task<int> Task_OneAwait_LiveState()
    {
        return PayloadValues switch
        {
            0 => TaskOneAwaitLiveState(EmptyPayload.Create()),
            1 => TaskOneAwaitLiveState(Payload1.Create()),
            4 => TaskOneAwaitLiveState(Payload4.Create()),
            8 => TaskOneAwaitLiveState(Payload8.Create()),
            16 => TaskOneAwaitLiveState(Payload16.Create()),
            32 => TaskOneAwaitLiveState(Payload32.Create()),
            64 => TaskOneAwaitLiveState(Payload64.Create()),
            128 => TaskOneAwaitLiveState(Payload128.Create()),
            _ => throw new ArgumentOutOfRangeException(nameof(PayloadValues), PayloadValues, null)
        };
    }

    [Benchmark]
    public Task<int> Task_TwoAwaits_DisjointState()
    {
        return PayloadValues switch
        {
            0 => TaskTwoAwaitsDisjointState(EmptyPayload.Create()),
            1 => TaskTwoAwaitsDisjointState(Payload1.Create()),
            4 => TaskTwoAwaitsDisjointState(Payload4.Create()),
            8 => TaskTwoAwaitsDisjointState(Payload8.Create()),
            16 => TaskTwoAwaitsDisjointState(Payload16.Create()),
            32 => TaskTwoAwaitsDisjointState(Payload32.Create()),
            64 => TaskTwoAwaitsDisjointState(Payload64.Create()),
            128 => TaskTwoAwaitsDisjointState(Payload128.Create()),
            _ => throw new ArgumentOutOfRangeException(nameof(PayloadValues), PayloadValues, null)
        };
    }

    [Benchmark]
    public ValueTask<int> ValueTask_DeadBeforeAwait_Control()
    {
        return PayloadValues switch
        {
            0 => ValueTaskDeadBeforeAwaitControl(EmptyPayload.Create()),
            1 => ValueTaskDeadBeforeAwaitControl(Payload1.Create()),
            4 => ValueTaskDeadBeforeAwaitControl(Payload4.Create()),
            8 => ValueTaskDeadBeforeAwaitControl(Payload8.Create()),
            16 => ValueTaskDeadBeforeAwaitControl(Payload16.Create()),
            32 => ValueTaskDeadBeforeAwaitControl(Payload32.Create()),
            64 => ValueTaskDeadBeforeAwaitControl(Payload64.Create()),
            128 => ValueTaskDeadBeforeAwaitControl(Payload128.Create()),
            _ => throw new ArgumentOutOfRangeException(nameof(PayloadValues), PayloadValues, null)
        };
    }

    [Benchmark]
    public ValueTask<int> ValueTask_OneAwait_LiveState()
    {
        return PayloadValues switch
        {
            0 => ValueTaskOneAwaitLiveState(EmptyPayload.Create()),
            1 => ValueTaskOneAwaitLiveState(Payload1.Create()),
            4 => ValueTaskOneAwaitLiveState(Payload4.Create()),
            8 => ValueTaskOneAwaitLiveState(Payload8.Create()),
            16 => ValueTaskOneAwaitLiveState(Payload16.Create()),
            32 => ValueTaskOneAwaitLiveState(Payload32.Create()),
            64 => ValueTaskOneAwaitLiveState(Payload64.Create()),
            128 => ValueTaskOneAwaitLiveState(Payload128.Create()),
            _ => throw new ArgumentOutOfRangeException(nameof(PayloadValues), PayloadValues, null)
        };
    }

    [Benchmark]
    public ValueTask<int> ValueTask_TwoAwaits_DisjointState()
    {
        return PayloadValues switch
        {
            0 => ValueTaskTwoAwaitsDisjointState(EmptyPayload.Create()),
            1 => ValueTaskTwoAwaitsDisjointState(Payload1.Create()),
            4 => ValueTaskTwoAwaitsDisjointState(Payload4.Create()),
            8 => ValueTaskTwoAwaitsDisjointState(Payload8.Create()),
            16 => ValueTaskTwoAwaitsDisjointState(Payload16.Create()),
            32 => ValueTaskTwoAwaitsDisjointState(Payload32.Create()),
            64 => ValueTaskTwoAwaitsDisjointState(Payload64.Create()),
            128 => ValueTaskTwoAwaitsDisjointState(Payload128.Create()),
            _ => throw new ArgumentOutOfRangeException(nameof(PayloadValues), PayloadValues, null)
        };
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

    private readonly struct EmptyPayload : ILiveStatePayload<EmptyPayload>
    {
        public static EmptyPayload Create(int offset = 0)
        {
            return new EmptyPayload();
        }

        public int Sum()
        {
            return AsyncSources.ExpensiveOrSideEffect(0);
        }
    }

    private readonly struct Payload1 : ILiveStatePayload<Payload1>
    {
        private readonly int _x1;

        private Payload1(int x1)
        {
            _x1 = x1;
        }

        public static Payload1 Create(int offset = 0)
        {
            return new Payload1(Value(offset + 1));
        }

        public int Sum()
        {
            return _x1;
        }
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
