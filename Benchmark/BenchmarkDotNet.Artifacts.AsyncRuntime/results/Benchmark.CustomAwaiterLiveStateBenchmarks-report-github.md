```

BenchmarkDotNet v0.14.0, macOS 26.4.1 (25E253) [Darwin 25.4.0]
Apple M3 Pro, 1 CPU, 11 logical and 11 physical cores
.NET SDK 11.0.100-preview.4.26230.115
  [Host]   : .NET 11.0.0 (11.0.26.23115), Arm64 RyuJIT AdvSIMD
  ShortRun : .NET 11.0.0 (11.0.26.23115), Arm64 RyuJIT AdvSIMD

Job=ShortRun  IterationCount=3  LaunchCount=1  
WarmupCount=3  

```
| Method                                  | Mean     | Error     | StdDev   | Gen0   | Allocated |
|---------------------------------------- |---------:|----------:|---------:|-------:|----------:|
| ValueTask_CustomAwaiter_NoLiveState     | 35.89 ns |  19.21 ns | 1.053 ns | 0.0200 |     168 B |
| Task_CustomAwaiter_EightLiveValues      | 37.16 ns |  30.87 ns | 1.692 ns | 0.0229 |     192 B |
| ValueTask_CustomAwaiter_EightLiveValues | 42.04 ns |  31.47 ns | 1.725 ns | 0.0239 |     200 B |
| Task_CustomAwaiter_NoLiveState          | 43.44 ns | 103.17 ns | 5.655 ns | 0.0191 |     160 B |
