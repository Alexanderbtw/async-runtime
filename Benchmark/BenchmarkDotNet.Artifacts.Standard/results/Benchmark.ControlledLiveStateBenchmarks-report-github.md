```

BenchmarkDotNet v0.14.0, macOS 26.4.1 (25E253) [Darwin 25.4.0]
Apple M3 Pro, 1 CPU, 11 logical and 11 physical cores
.NET SDK 11.0.100-preview.4.26230.115
  [Host]     : .NET 11.0.0 (11.0.26.23115), Arm64 RyuJIT AdvSIMD
  DefaultJob : .NET 11.0.0 (11.0.26.23115), Arm64 RyuJIT AdvSIMD


```
| Method                                | Mean     | Error    | StdDev   | Gen0     | Gen1   | Allocated  |
|-------------------------------------- |---------:|---------:|---------:|---------:|-------:|-----------:|
| ValueTask_OneAwait_LiveStateLoop      | 319.2 μs |  1.20 μs |  1.06 μs |  81.0547 |      - |  664.06 KB |
| Task_DeadBeforeAwait_ControlLoop      | 324.8 μs |  6.42 μs |  7.40 μs |  80.0781 |      - |  656.32 KB |
| ValueTask_DeadBeforeAwait_ControlLoop | 325.7 μs |  6.44 μs | 11.44 μs |  81.0547 |      - |  664.06 KB |
| Task_OneAwait_LiveStateLoop           | 350.4 μs |  6.93 μs | 16.34 μs |  80.0781 |      - |  656.32 KB |
| ValueTask_TwoAwaits_DisjointStateLoop | 797.2 μs |  6.91 μs |  6.13 μs | 141.6016 | 0.9766 | 1164.06 KB |
| Task_TwoAwaits_DisjointStateLoop      | 854.2 μs | 15.28 μs | 21.42 μs | 140.6250 | 0.9766 | 1156.32 KB |
