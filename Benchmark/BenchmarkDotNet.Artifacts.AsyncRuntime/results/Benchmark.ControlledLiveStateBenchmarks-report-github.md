```

BenchmarkDotNet v0.14.0, macOS 26.4.1 (25E253) [Darwin 25.4.0]
Apple M3 Pro, 1 CPU, 11 logical and 11 physical cores
.NET SDK 11.0.100-preview.4.26230.115
  [Host]     : .NET 11.0.0 (11.0.26.23115), Arm64 RyuJIT AdvSIMD
  DefaultJob : .NET 11.0.0 (11.0.26.23115), Arm64 RyuJIT AdvSIMD


```
| Method                                | Mean     | Error    | StdDev   | Gen0     | Gen1   | Allocated  |
|-------------------------------------- |---------:|---------:|---------:|---------:|-------:|-----------:|
| ValueTask_OneAwait_LiveStateLoop      | 324.1 μs |  2.74 μs |  2.14 μs |  81.0547 |      - |  664.06 KB |
| Task_DeadBeforeAwait_ControlLoop      | 329.6 μs |  6.17 μs | 10.97 μs |  80.0781 |      - |  656.32 KB |
| Task_OneAwait_LiveStateLoop           | 339.3 μs |  2.81 μs |  2.63 μs |  80.0781 |      - |  656.32 KB |
| ValueTask_DeadBeforeAwait_ControlLoop | 359.2 μs |  6.63 μs |  6.51 μs |  81.0547 |      - |  664.06 KB |
| ValueTask_TwoAwaits_DisjointStateLoop | 795.1 μs | 12.17 μs | 10.79 μs | 141.6016 | 0.9766 | 1164.06 KB |
| Task_TwoAwaits_DisjointStateLoop      | 887.9 μs | 16.16 μs | 30.36 μs | 140.6250 | 0.9766 | 1156.32 KB |
