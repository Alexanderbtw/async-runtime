```

BenchmarkDotNet v0.14.0, macOS 26.4.1 (25E253) [Darwin 25.4.0]
Apple M3 Pro, 1 CPU, 11 logical and 11 physical cores
.NET SDK 11.0.100-preview.4.26230.115
  [Host]     : .NET 11.0.0 (11.0.26.23115), Arm64 RyuJIT AdvSIMD
  DefaultJob : .NET 11.0.0 (11.0.26.23115), Arm64 RyuJIT AdvSIMD


```
| Method          | Mean         | StdDev     | Median       | Ratio  | RatioSD | Gen0   | Allocated | Alloc Ratio |
|---------------- |-------------:|-----------:|-------------:|-------:|--------:|-------:|----------:|------------:|
| Task_Sync       |     6.006 ns |  0.2528 ns |     6.021 ns |   1.00 |    0.06 | 0.0086 |      72 B |        1.00 |
| ValueTask_Sync  |     2.240 ns |  0.0466 ns |     2.217 ns |   0.37 |    0.02 |      - |         - |        0.00 |
| Pooling_Sync    |     2.352 ns |  0.0588 ns |     2.337 ns |   0.39 |    0.02 |      - |         - |        0.00 |
| Task_Yield      |   921.207 ns | 31.8357 ns |   922.607 ns | 153.66 |    8.31 | 0.0114 |      96 B |        1.33 |
| ValueTask_Yield | 1,050.570 ns | 73.5016 ns | 1,031.165 ns | 175.23 |   14.24 | 0.0114 |     104 B |        1.44 |
| Pooling_Yield   |   930.226 ns | 28.6528 ns |   944.059 ns | 155.16 |    8.03 |      - |         - |        0.00 |
