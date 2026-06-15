```

BenchmarkDotNet v0.14.0, macOS 26.4.1 (25E253) [Darwin 25.4.0]
Apple M3 Pro, 1 CPU, 11 logical and 11 physical cores
.NET SDK 11.0.100-preview.4.26230.115
  [Host]     : .NET 11.0.0 (11.0.26.23115), Arm64 RyuJIT AdvSIMD
  DefaultJob : .NET 11.0.0 (11.0.26.23115), Arm64 RyuJIT AdvSIMD


```
| Method                             | Mean      | Error    | StdDev   | Median    | Gen0     | Gen1    | Allocated |
|----------------------------------- |----------:|---------:|---------:|----------:|---------:|--------:|----------:|
| ValueTask_CachedCompletedLoop      |  13.86 μs | 1.113 μs | 3.281 μs |  13.59 μs |        - |       - |         - |
| Task_CachedCompletedLoop           |  17.38 μs | 0.675 μs | 1.991 μs |  17.90 μs |        - |       - |      72 B |
| ValueTask_ListFromResultNestedLoop |  56.15 μs | 0.598 μs | 0.467 μs |  56.11 μs |   4.7607 |  0.5493 |   40056 B |
| Task_ListFromResultNestedLoop      | 129.32 μs | 2.422 μs | 4.892 μs | 126.71 μs | 176.7578 | 25.1465 | 1480128 B |
