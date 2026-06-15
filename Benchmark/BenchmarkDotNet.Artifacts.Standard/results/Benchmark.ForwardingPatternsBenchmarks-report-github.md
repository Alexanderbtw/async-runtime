```

BenchmarkDotNet v0.14.0, macOS 26.4.1 (25E253) [Darwin 25.4.0]
Apple M3 Pro, 1 CPU, 11 logical and 11 physical cores
.NET SDK 11.0.100-preview.4.26230.115
  [Host]     : .NET 11.0.0 (11.0.26.23115), Arm64 RyuJIT AdvSIMD
  DefaultJob : .NET 11.0.0 (11.0.26.23115), Arm64 RyuJIT AdvSIMD


```
| Method                            | Mean      | Error     | StdDev    | Gen0    | Allocated |
|---------------------------------- |----------:|----------:|----------:|--------:|----------:|
| ValueTask_DirectForwardingLoop    |  2.601 μs | 0.0520 μs | 0.0779 μs |       - |         - |
| Task_DirectForwardingLoop         |  5.399 μs | 0.0520 μs | 0.0461 μs |       - |         - |
| ValueTask_AwaitOnlyForwardingLoop | 41.500 μs | 0.4160 μs | 0.3688 μs |       - |         - |
| Task_AwaitOnlyForwardingLoop      | 75.297 μs | 0.6839 μs | 0.5339 μs | 86.0596 |  720072 B |
