оо```

BenchmarkDotNet v0.14.0, macOS 26.4.1 (25E253) [Darwin 25.4.0]
Apple M3 Pro, 1 CPU, 11 logical and 11 physical cores
.NET SDK 11.0.100-preview.4.26230.115
  [Host]     : .NET 11.0.0 (11.0.26.23115), Arm64 RyuJIT AdvSIMD
  DefaultJob : .NET 11.0.0 (11.0.26.23115), Arm64 RyuJIT AdvSIMD


```
| Method          | Mean         | StdDev     | Ratio  | RatioSD | Gen0   | Allocated | Alloc Ratio |
|---------------- |-------------:|-----------:|-------:|--------:|-------:|----------:|------------:|
| Task_Sync       |     5.841 ns |  0.0202 ns |   1.00 |    0.00 | 0.0086 |      72 B |        1.00 |
| ValueTask_Sync  |     2.228 ns |  0.0295 ns |   0.38 |    0.01 |      - |         - |        0.00 |
| Pooling_Sync    |     2.204 ns |  0.0283 ns |   0.38 |    0.00 |      - |         - |        0.00 |
| Task_Yield      |   981.153 ns | 43.1272 ns | 167.99 |    7.35 | 0.0114 |      96 B |        1.33 |
| ValueTask_Yield | 1,070.439 ns | 42.7025 ns | 183.27 |    7.27 | 0.0114 |     104 B |        1.44 |
| Pooling_Yield   |   938.601 ns | 16.2351 ns | 160.70 |    2.74 |      - |         - |        0.00 |
