```

BenchmarkDotNet v0.14.0, macOS 26.4.1 (25E253) [Darwin 25.4.0]
Apple M3 Pro, 1 CPU, 11 logical and 11 physical cores
.NET SDK 11.0.100-preview.4.26230.115
  [Host]   : .NET 11.0.0 (11.0.26.23115), Arm64 RyuJIT AdvSIMD
  ShortRun : .NET 11.0.0 (11.0.26.23115), Arm64 RyuJIT AdvSIMD

Job=ShortRun  IterationCount=3  LaunchCount=1  
WarmupCount=3  

```
| Method              | Depth | Mean       | Error      | StdDev    | Gen0   | Allocated |
|-------------------- |------ |-----------:|-----------:|----------:|-------:|----------:|
| ValueTask_Completed | 1     |   8.015 ns |   2.837 ns | 0.1555 ns |      - |         - |
| ValueTask_Completed | 2     |  12.974 ns |   4.119 ns | 0.2258 ns |      - |         - |
| Task_Completed      | 1     |  22.627 ns |  10.538 ns | 0.5776 ns | 0.0172 |     144 B |
| Task_Completed      | 2     |  24.229 ns |   5.785 ns | 0.3171 ns | 0.0258 |     216 B |
| ValueTask_Completed | 4     |  24.731 ns |  14.650 ns | 0.8030 ns |      - |         - |
| Task_Completed      | 4     |  39.470 ns |  27.605 ns | 1.5131 ns | 0.0430 |     360 B |
| ValueTask_Completed | 8     |  47.303 ns |  11.331 ns | 0.6211 ns |      - |         - |
| Task_Completed      | 8     |  76.223 ns |  44.047 ns | 2.4144 ns | 0.0774 |     648 B |
| ValueTask_Completed | 16    |  95.750 ns |  52.019 ns | 2.8513 ns |      - |         - |
| Task_Completed      | 16    | 136.853 ns |  65.376 ns | 3.5835 ns | 0.1462 |    1224 B |
| ValueTask_Completed | 32    | 209.887 ns | 177.001 ns | 9.7020 ns |      - |         - |
| Task_Completed      | 32    | 447.431 ns |  54.092 ns | 2.9650 ns | 0.2837 |    2376 B |
