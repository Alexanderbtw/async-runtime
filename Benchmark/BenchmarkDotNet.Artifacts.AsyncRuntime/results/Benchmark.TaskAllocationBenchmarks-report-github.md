```

BenchmarkDotNet v0.14.0, macOS 26.4.1 (25E253) [Darwin 25.4.0]
Apple M3 Pro, 1 CPU, 11 logical and 11 physical cores
.NET SDK 11.0.100-preview.4.26230.115
  [Host]     : .NET 11.0.0 (11.0.26.23115), Arm64 RyuJIT AdvSIMD
  DefaultJob : .NET 11.0.0 (11.0.26.23115), Arm64 RyuJIT AdvSIMD


```
| Method          | Mean         | Error      | StdDev      | Median       | Ratio  | RatioSD | Gen0   | Allocated | Alloc Ratio |
|---------------- |-------------:|-----------:|------------:|-------------:|-------:|--------:|-------:|----------:|------------:|
| Task_Sync       |     5.529 ns |  0.1329 ns |   0.1178 ns |     5.501 ns |   1.00 |    0.03 | 0.0086 |      72 B |        1.00 |
| ValueTask_Sync  |     2.097 ns |  0.0278 ns |   0.0232 ns |     2.103 ns |   0.38 |    0.01 |      - |         - |        0.00 |
| Pooling_Sync    |     2.096 ns |  0.0151 ns |   0.0134 ns |     2.091 ns |   0.38 |    0.01 |      - |         - |        0.00 |
| Task_Yield      |   925.900 ns | 18.3137 ns |  43.1676 ns |   924.535 ns | 167.54 |    8.47 | 0.0114 |      96 B |        1.33 |
| ValueTask_Yield | 1,072.774 ns | 36.3297 ns | 107.1190 ns | 1,000.295 ns | 194.12 |   19.70 | 0.0114 |     104 B |        1.44 |
| Pooling_Yield   |   896.893 ns | 17.7827 ns |  34.2612 ns |   893.777 ns | 162.30 |    6.97 |      - |         - |        0.00 |
