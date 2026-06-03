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
| Task_NoLiveState          | 1.145 μs | 0.3610 μs | 0.0198 μs | 0.0114 |      96 B |
| Task_OneLiveValue         | 1.168 μs | 0.2424 μs | 0.0133 μs | 0.0114 |      96 B |
| Task_EightLiveValues      | 1.285 μs | 0.8361 μs | 0.0458 μs | 0.0153 |     128 B |
| ValueTask_NoLiveState     | 1.448 μs | 0.4738 μs | 0.0260 μs | 0.0114 |     104 B |
| ValueTask_EightLiveValues | 1.495 μs | 1.0427 μs | 0.0572 μs | 0.0153 |     136 B |
| ValueTask_OneLiveValue    | 1.579 μs | 1.0270 μs | 0.0563 μs | 0.0114 |     104 B |
