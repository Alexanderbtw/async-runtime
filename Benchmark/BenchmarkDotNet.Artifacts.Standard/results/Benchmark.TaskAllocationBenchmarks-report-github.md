```

BenchmarkDotNet v0.14.0, macOS 26.4.1 (25E253) [Darwin 25.4.0]
Apple M3 Pro, 1 CPU, 11 logical and 11 physical cores
.NET SDK 11.0.100-preview.4.26230.115
  [Host]     : .NET 11.0.0 (11.0.26.23115), Arm64 RyuJIT AdvSIMD
  DefaultJob : .NET 11.0.0 (11.0.26.23115), Arm64 RyuJIT AdvSIMD


```
| Method          | Mean       | Error      | StdDev     | Ratio  | RatioSD | Gen0   | Allocated | Alloc Ratio |
|---------------- |-----------:|-----------:|-----------:|-------:|--------:|-------:|----------:|------------:|
| Task_Sync       |   5.746 ns |  0.1425 ns |  0.3779 ns |   1.00 |    0.09 | 0.0086 |      72 B |        1.00 |
| ValueTask_Sync  |   2.149 ns |  0.0324 ns |  0.0287 ns |   0.38 |    0.02 |      - |         - |        0.00 |
| Pooling_Sync    |   2.046 ns |  0.0568 ns |  0.0475 ns |   0.36 |    0.02 |      - |         - |        0.00 |
| Task_Yield      | 898.025 ns | 13.8447 ns | 12.2729 ns | 156.93 |   10.14 | 0.0114 |      96 B |        1.33 |
| ValueTask_Yield | 819.855 ns |  7.1063 ns |  6.6472 ns | 143.27 |    9.13 | 0.0124 |     104 B |        1.44 |
| Pooling_Yield   | 831.380 ns |  6.9607 ns |  5.8125 ns | 145.28 |    9.24 |      - |         - |        0.00 |
