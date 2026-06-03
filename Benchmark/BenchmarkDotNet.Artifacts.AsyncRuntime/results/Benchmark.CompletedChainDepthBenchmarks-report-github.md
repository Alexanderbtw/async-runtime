```

BenchmarkDotNet v0.14.0, macOS 26.4.1 (25E253) [Darwin 25.4.0]
Apple M3 Pro, 1 CPU, 11 logical and 11 physical cores
.NET SDK 11.0.100-preview.4.26230.115
  [Host]   : .NET 11.0.0 (11.0.26.23115), Arm64 RyuJIT AdvSIMD
  ShortRun : .NET 11.0.0 (11.0.26.23115), Arm64 RyuJIT AdvSIMD

Job=ShortRun  IterationCount=3  LaunchCount=1  
WarmupCount=3  

```
| Method              | Depth | Mean       | Error      | StdDev     | Gen0   | Allocated |
|-------------------- |------ |-----------:|-----------:|-----------:|-------:|----------:|
| ValueTask_Completed | 1     |   9.646 ns |  17.525 ns |  0.9606 ns |      - |         - |
| Task_Completed      | 1     |  16.654 ns |   3.052 ns |  0.1673 ns | 0.0172 |     144 B |
| ValueTask_Completed | 4     |  23.455 ns |  22.897 ns |  1.2551 ns |      - |         - |
| ValueTask_Completed | 2     |  27.158 ns | 124.416 ns |  6.8197 ns |      - |         - |
| Task_Completed      | 2     |  34.080 ns |  82.579 ns |  4.5264 ns | 0.0258 |     216 B |
| ValueTask_Completed | 8     |  47.254 ns |  14.947 ns |  0.8193 ns |      - |         - |
| Task_Completed      | 4     |  52.526 ns | 185.379 ns | 10.1612 ns | 0.0430 |     360 B |
| Task_Completed      | 8     |  77.343 ns |  32.250 ns |  1.7677 ns | 0.0774 |     648 B |
| ValueTask_Completed | 16    |  99.422 ns |  36.226 ns |  1.9857 ns |      - |         - |
| Task_Completed      | 16    | 170.975 ns | 386.225 ns | 21.1703 ns | 0.1462 |    1224 B |
| ValueTask_Completed | 32    | 202.374 ns |  76.189 ns |  4.1762 ns |      - |         - |
| Task_Completed      | 32    | 431.235 ns | 643.038 ns | 35.2471 ns | 0.2837 |    2376 B |
