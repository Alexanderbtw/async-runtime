```

BenchmarkDotNet v0.14.0, macOS 26.4.1 (25E253) [Darwin 25.4.0]
Apple M3 Pro, 1 CPU, 11 logical and 11 physical cores
.NET SDK 11.0.100-preview.4.26230.115
  [Host]   : .NET 11.0.0 (11.0.26.23115), Arm64 RyuJIT AdvSIMD
  ShortRun : .NET 11.0.0 (11.0.26.23115), Arm64 RyuJIT AdvSIMD

Job=ShortRun  IterationCount=3  LaunchCount=1  
WarmupCount=3  

```
| Method                  | Depth | Mean     | Error      | StdDev    | Gen0   | Allocated |
|------------------------ |------ |---------:|-----------:|----------:|-------:|----------:|
| Task_SuspendedOnce      | 2     | 1.514 μs |  1.7575 μs | 0.0963 μs | 0.0420 |     365 B |
| ValueTask_SuspendedOnce | 1     | 1.538 μs |  0.6539 μs | 0.0358 μs | 0.0362 |     312 B |
| Task_SuspendedOnce      | 1     | 1.694 μs |  6.3275 μs | 0.3468 μs | 0.0324 |     276 B |
| ValueTask_SuspendedOnce | 2     | 1.710 μs |  0.4556 μs | 0.0250 μs | 0.0496 |     417 B |
| Task_SuspendedOnce      | 4     | 1.794 μs |  1.4359 μs | 0.0787 μs | 0.0629 |     542 B |
| ValueTask_SuspendedOnce | 4     | 1.830 μs |  0.4309 μs | 0.0236 μs | 0.0687 |     595 B |
| Task_SuspendedOnce      | 8     | 2.311 μs |  3.9659 μs | 0.2174 μs | 0.0992 |     849 B |
| Task_SuspendedOnce      | 16    | 2.942 μs |  2.8599 μs | 0.1568 μs | 0.1869 |    1571 B |
| ValueTask_SuspendedOnce | 16    | 3.222 μs |  1.2569 μs | 0.0689 μs | 0.1984 |    1667 B |
| ValueTask_SuspendedOnce | 8     | 3.274 μs | 20.6804 μs | 1.1336 μs | 0.1221 |    1025 B |
