```

BenchmarkDotNet v0.14.0, macOS 26.4.1 (25E253) [Darwin 25.4.0]
Apple M3 Pro, 1 CPU, 11 logical and 11 physical cores
.NET SDK 11.0.100-preview.4.26230.115
  [Host]   : .NET 11.0.0 (11.0.26.23115), Arm64 RyuJIT AdvSIMD
  ShortRun : .NET 11.0.0 (11.0.26.23115), Arm64 RyuJIT AdvSIMD

Job=ShortRun  IterationCount=3  LaunchCount=1  
WarmupCount=3  

```
| Method                    | Mean     | Error     | StdDev    | Gen0   | Allocated |
|-------------------------- |---------:|----------:|----------:|-------:|----------:|
| Task_NoLiveState          | 1.275 μs | 0.7057 μs | 0.0387 μs | 0.0114 |      96 B |
| Task_OneLiveValue         | 1.299 μs | 0.7885 μs | 0.0432 μs | 0.0114 |      96 B |
| ValueTask_NoLiveState     | 1.486 μs | 1.5968 μs | 0.0875 μs | 0.0114 |     104 B |
| ValueTask_OneLiveValue    | 1.503 μs | 0.8597 μs | 0.0471 μs | 0.0114 |     104 B |
| Task_EightLiveValues      | 1.507 μs | 3.5011 μs | 0.1919 μs | 0.0153 |     128 B |
| ValueTask_EightLiveValues | 1.514 μs | 1.5584 μs | 0.0854 μs | 0.0153 |     136 B |
