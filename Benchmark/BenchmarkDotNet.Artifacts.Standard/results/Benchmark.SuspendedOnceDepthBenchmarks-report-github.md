```

BenchmarkDotNet v0.14.0, macOS 26.4.1 (25E253) [Darwin 25.4.0]
Apple M3 Pro, 1 CPU, 11 logical and 11 physical cores
.NET SDK 11.0.100-preview.4.26230.115
  [Host]   : .NET 11.0.0 (11.0.26.23115), Arm64 RyuJIT AdvSIMD
  ShortRun : .NET 11.0.0 (11.0.26.23115), Arm64 RyuJIT AdvSIMD

Job=ShortRun  IterationCount=3  LaunchCount=1  
WarmupCount=3  

```
| Method                  | Depth | Mean     | Error     | StdDev    | Gen0   | Allocated |
|------------------------ |------ |---------:|----------:|----------:|-------:|----------:|
| Task_SuspendedOnce      | 1     | 1.303 μs | 0.3844 μs | 0.0211 μs | 0.0324 |     278 B |
| Task_SuspendedOnce      | 2     | 1.454 μs | 0.1654 μs | 0.0091 μs | 0.0420 |     366 B |
| ValueTask_SuspendedOnce | 1     | 1.595 μs | 1.5495 μs | 0.0849 μs | 0.0362 |     307 B |
| ValueTask_SuspendedOnce | 2     | 1.610 μs | 0.5723 μs | 0.0314 μs | 0.0496 |     416 B |
| Task_SuspendedOnce      | 4     | 1.691 μs | 2.5102 μs | 0.1376 μs | 0.0629 |     537 B |
| ValueTask_SuspendedOnce | 4     | 1.890 μs | 0.9014 μs | 0.0494 μs | 0.0725 |     607 B |
| Task_SuspendedOnce      | 8     | 2.013 μs | 0.3300 μs | 0.0181 μs | 0.1030 |     865 B |
| ValueTask_SuspendedOnce | 8     | 2.301 μs | 0.9251 μs | 0.0507 μs | 0.1106 |     949 B |
| Task_SuspendedOnce      | 16    | 2.674 μs | 0.9270 μs | 0.0508 μs | 0.1831 |    1532 B |
| ValueTask_SuspendedOnce | 16    | 3.015 μs | 0.5255 μs | 0.0288 μs | 0.1945 |    1648 B |
