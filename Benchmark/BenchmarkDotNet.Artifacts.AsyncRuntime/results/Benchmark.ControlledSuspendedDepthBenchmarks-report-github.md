```

BenchmarkDotNet v0.14.0, macOS 26.4.1 (25E253) [Darwin 25.4.0]
Apple M3 Pro, 1 CPU, 11 logical and 11 physical cores
.NET SDK 11.0.100-preview.4.26230.115
  [Host]     : .NET 11.0.0 (11.0.26.23115), Arm64 RyuJIT AdvSIMD
  DefaultJob : .NET 11.0.0 (11.0.26.23115), Arm64 RyuJIT AdvSIMD


```
| Method                             | Mean     | Error    | StdDev   | Gen0    | Allocated |
|----------------------------------- |---------:|---------:|---------:|--------:|----------:|
| Task_ControlledSuspensionLoop      | 28.89 μs | 0.570 μs | 0.887 μs | 19.1345 | 156.32 KB |
| ValueTask_ControlledSuspensionLoop | 29.63 μs | 0.143 μs | 0.112 μs | 20.0806 | 164.06 KB |
