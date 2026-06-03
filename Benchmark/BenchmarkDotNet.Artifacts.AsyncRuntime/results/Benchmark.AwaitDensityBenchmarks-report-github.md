```

BenchmarkDotNet v0.14.0, macOS 26.4.1 (25E253) [Darwin 25.4.0]
Apple M3 Pro, 1 CPU, 11 logical and 11 physical cores
.NET SDK 11.0.100-preview.4.26230.115
  [Host]   : .NET 11.0.0 (11.0.26.23115), Arm64 RyuJIT AdvSIMD
  ShortRun : .NET 11.0.0 (11.0.26.23115), Arm64 RyuJIT AdvSIMD

Job=ShortRun  IterationCount=3  LaunchCount=1  
WarmupCount=3  

```
| Method                        | AwaitCount | Mean       | Error      | StdDev     | Gen0   | Allocated |
|------------------------------ |----------- |-----------:|-----------:|-----------:|-------:|----------:|
| ValueTask_ManyCompletedAwaits | 1          |   4.014 ns |   7.625 ns |  0.4179 ns |      - |         - |
| ValueTask_ManyCompletedAwaits | 2          |   5.422 ns |   9.326 ns |  0.5112 ns |      - |         - |
| ValueTask_ManyCompletedAwaits | 4          |   8.431 ns |   4.803 ns |  0.2633 ns |      - |         - |
| Task_ManyCompletedAwaits      | 1          |   8.488 ns |   7.519 ns |  0.4122 ns | 0.0086 |      72 B |
| Task_ManyCompletedAwaits      | 2          |  12.568 ns |  40.722 ns |  2.2321 ns | 0.0086 |      72 B |
| Task_ManyCompletedAwaits      | 4          |  14.532 ns |  46.777 ns |  2.5640 ns | 0.0086 |      72 B |
| Task_ManyCompletedAwaits      | 8          |  18.246 ns |   1.458 ns |  0.0799 ns | 0.0086 |      72 B |
| ValueTask_ManyCompletedAwaits | 8          |  18.430 ns |   2.477 ns |  0.1358 ns |      - |         - |
| ValueTask_ManyCompletedAwaits | 16         |  30.626 ns |  39.312 ns |  2.1548 ns |      - |         - |
| Task_ManyCompletedAwaits      | 16         |  32.446 ns |   9.411 ns |  0.5158 ns | 0.0086 |      72 B |
| Task_ManyCompletedAwaits      | 32         |  67.278 ns |  15.970 ns |  0.8754 ns | 0.0086 |      72 B |
| ValueTask_ManyCompletedAwaits | 32         |  75.931 ns |  78.773 ns |  4.3178 ns |      - |         - |
| ValueTask_ManyCompletedAwaits | 64         | 137.595 ns |  14.286 ns |  0.7830 ns |      - |         - |
| Task_ManyCompletedAwaits      | 64         | 146.710 ns | 220.901 ns | 12.1083 ns | 0.0086 |      72 B |
