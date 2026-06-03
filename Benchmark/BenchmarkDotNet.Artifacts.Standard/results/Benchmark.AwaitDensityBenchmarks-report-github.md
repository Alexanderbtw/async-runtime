```

BenchmarkDotNet v0.14.0, macOS 26.4.1 (25E253) [Darwin 25.4.0]
Apple M3 Pro, 1 CPU, 11 logical and 11 physical cores
.NET SDK 11.0.100-preview.4.26230.115
  [Host]   : .NET 11.0.0 (11.0.26.23115), Arm64 RyuJIT AdvSIMD
  ShortRun : .NET 11.0.0 (11.0.26.23115), Arm64 RyuJIT AdvSIMD

Job=ShortRun  IterationCount=3  LaunchCount=1  
WarmupCount=3  

```
| Method                        | AwaitCount | Mean       | Error     | StdDev    | Gen0   | Allocated |
|------------------------------ |----------- |-----------:|----------:|----------:|-------:|----------:|
| ValueTask_ManyCompletedAwaits | 1          |   3.825 ns |  2.180 ns | 0.1195 ns |      - |         - |
| ValueTask_ManyCompletedAwaits | 2          |   5.108 ns |  1.890 ns | 0.1036 ns |      - |         - |
| ValueTask_ManyCompletedAwaits | 4          |   7.626 ns |  3.676 ns | 0.2015 ns |      - |         - |
| Task_ManyCompletedAwaits      | 1          |   7.931 ns |  7.649 ns | 0.4192 ns | 0.0086 |      72 B |
| Task_ManyCompletedAwaits      | 2          |   8.928 ns |  4.782 ns | 0.2621 ns | 0.0086 |      72 B |
| Task_ManyCompletedAwaits      | 4          |  11.698 ns |  4.561 ns | 0.2500 ns | 0.0086 |      72 B |
| Task_ManyCompletedAwaits      | 8          |  17.483 ns | 11.569 ns | 0.6341 ns | 0.0086 |      72 B |
| ValueTask_ManyCompletedAwaits | 8          |  19.472 ns |  6.401 ns | 0.3509 ns |      - |         - |
| ValueTask_ManyCompletedAwaits | 16         |  28.526 ns | 16.142 ns | 0.8848 ns |      - |         - |
| Task_ManyCompletedAwaits      | 16         |  33.200 ns |  4.611 ns | 0.2528 ns | 0.0086 |      72 B |
| ValueTask_ManyCompletedAwaits | 32         |  62.621 ns | 21.798 ns | 1.1948 ns |      - |         - |
| Task_ManyCompletedAwaits      | 32         |  65.317 ns | 36.415 ns | 1.9960 ns | 0.0086 |      72 B |
| ValueTask_ManyCompletedAwaits | 64         | 135.624 ns | 70.094 ns | 3.8421 ns |      - |         - |
| Task_ManyCompletedAwaits      | 64         | 139.295 ns | 95.291 ns | 5.2232 ns | 0.0086 |      72 B |
