using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Sources;

/// <summary>
/// Пул объектов с асинхронным ожиданием. Если объект недоступен —
/// ожидающий получит его без выделения Task в куче.
/// </summary>
public sealed class AsyncObjectPool<T> where T : class
{
    private readonly ConcurrentQueue<T> _items = new();
    private readonly ConcurrentQueue<PoolAwaiter> _waiters = new();
    private readonly Func<T> _factory;

    public AsyncObjectPool(Func<T> factory, int initialCount = 4)
    {
        _factory = factory;
        for (int i = 0; i < initialCount; i++)
            _items.Enqueue(factory());
    }

    /// <summary>
    /// Получить объект из пула. Если пустой — ждём без аллокаций.
    /// </summary>
    public ValueTask<T> AcquireAsync(CancellationToken ct = default)
    {
        // Быстрый путь: объект уже есть — возвращаем синхронно, без аллокации
        if (_items.TryDequeue(out T? item))
            return ValueTask.FromResult(item);

        // Медленный путь: ставим в очередь ожидания
        var awaiter = PoolAwaiter.Rent(this, ct);
        _waiters.Enqueue(awaiter);

        // Проверяем race condition: вдруг объект появился пока мы ставились в очередь
        TryDispatchToWaiters();

        return new ValueTask<T>(awaiter, awaiter.Token);
    }

    /// <summary>
    /// Вернуть объект в пул.
    /// </summary>
    public void Release(T item)
    {
        _items.Enqueue(item);
        TryDispatchToWaiters();
    }

    private void TryDispatchToWaiters()
    {
        while (_waiters.TryPeek(out _) && _items.TryDequeue(out T? item))
        {
            if (_waiters.TryDequeue(out PoolAwaiter? waiter))
                waiter.SetResult(item);
            else
                _items.Enqueue(item); // вернули обратно, если не получилось
        }
    }

    private sealed class PoolAwaiter : IValueTaskSource<T>
    {
        // Статический пул самих awaiter-объектов, чтобы не создавать их каждый раз
        private static readonly ConcurrentQueue<PoolAwaiter> s_pool = new();

        private ManualResetValueTaskSourceCore<T> _core; // struct внутри класса
        private AsyncObjectPool<T>? _owner;
        private CancellationTokenRegistration _ctr;

        // Токен версии — защита от повторного использования
        public short Token => _core.Version;

        public static PoolAwaiter Rent(AsyncObjectPool<T> owner, CancellationToken ct)
        {
            if (!s_pool.TryDequeue(out PoolAwaiter? awaiter))
                awaiter = new PoolAwaiter();

            awaiter._owner = owner;
            awaiter._core.Reset();

            if (ct.CanBeCanceled)
                awaiter._ctr = ct.Register(
                    static state => ((PoolAwaiter)state!).SetCanceled(),
                    awaiter);

            return awaiter;
        }

        public void SetResult(T result)
        {
            _ctr.Dispose();
            _core.SetResult(result);
        }

        private void SetCanceled()
        {
            _core.SetException(new OperationCanceledException());
        }

        T IValueTaskSource<T>.GetResult(short token)
        {
            try
            {
                return _core.GetResult(token);
            }
            finally
            {
                // После получения результата возвращаем awaiter в пул
                _ctr = default;
                _owner = null;
                s_pool.Enqueue(this);
            }
        }

        ValueTaskSourceStatus IValueTaskSource<T>.GetStatus(short token)
            => _core.GetStatus(token);

        void IValueTaskSource<T>.OnCompleted(
            Action<object?> continuation,
            object? state,
            short token,
            ValueTaskSourceOnCompletedFlags flags)
            => _core.OnCompleted(continuation, state, token, flags);
    }
}