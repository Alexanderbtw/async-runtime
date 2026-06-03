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
| ValueTask_CustomAwaiter_NoLiveState     | 33.50 ns | 15.766 ns | 0.864 ns | 0.0200 |     168 B |
| Task_CustomAwaiter_EightLiveValues      | 37.48 ns |  3.165 ns | 0.173 ns | 0.0229 |     192 B |
| Task_CustomAwaiter_NoLiveState          | 38.32 ns | 48.577 ns | 2.663 ns | 0.0191 |     160 B |
| ValueTask_CustomAwaiter_EightLiveValues | 43.65 ns | 31.371 ns | 1.720 ns | 0.0239 |     200 B |
