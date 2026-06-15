```

BenchmarkDotNet v0.14.0, macOS 26.4.1 (25E253) [Darwin 25.4.0]
Apple M3 Pro, 1 CPU, 11 logical and 11 physical cores
.NET SDK 11.0.100-preview.4.26230.115
  [Host]     : .NET 11.0.0 (11.0.26.23115), Arm64 RyuJIT AdvSIMD
  DefaultJob : .NET 11.0.0 (11.0.26.23115), Arm64 RyuJIT AdvSIMD


```
| Method                             | Mean      | Error    | StdDev   | Gen0     | Gen1    | Allocated |
|----------------------------------- |----------:|---------:|---------:|---------:|--------:|----------:|
| ValueTask_CachedCompletedLoop      |  13.52 μs | 0.690 μs | 2.001 μs |        - |       - |         - |
| Task_CachedCompletedLoop           |  18.42 μs | 0.439 μs | 1.279 μs |        - |       - |      72 B |
| ValueTask_ListFromResultNestedLoop |  57.97 μs | 1.145 μs | 1.913 μs |   4.7607 |  0.5493 |   40056 B |
| Task_ListFromResultNestedLoop      | 126.69 μs | 2.332 μs | 2.181 μs | 176.7578 | 25.1465 | 1480128 B |
