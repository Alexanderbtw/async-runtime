```

BenchmarkDotNet v0.14.0, macOS 26.4.1 (25E253) [Darwin 25.4.0]
Apple M3 Pro, 1 CPU, 11 logical and 11 physical cores
.NET SDK 11.0.100-preview.4.26230.115
  [Host]     : .NET 11.0.0 (11.0.26.23115), Arm64 RyuJIT AdvSIMD
  DefaultJob : .NET 11.0.0 (11.0.26.23115), Arm64 RyuJIT AdvSIMD


```
| Method                            | Mean      | Error     | StdDev    | Median    | Gen0    | Allocated |
|---------------------------------- |----------:|----------:|----------:|----------:|--------:|----------:|
| ValueTask_DirectForwardingLoop    |  2.724 μs | 0.0637 μs | 0.1880 μs |  2.668 μs |       - |         - |
| Task_DirectForwardingLoop         |  5.766 μs | 0.1087 μs | 0.1163 μs |  5.734 μs |       - |         - |
| ValueTask_AwaitOnlyForwardingLoop | 44.858 μs | 0.8931 μs | 2.2075 μs | 44.076 μs |       - |         - |
| Task_AwaitOnlyForwardingLoop      | 80.679 μs | 1.6060 μs | 2.8547 μs | 80.498 μs | 86.0596 |  720072 B |
