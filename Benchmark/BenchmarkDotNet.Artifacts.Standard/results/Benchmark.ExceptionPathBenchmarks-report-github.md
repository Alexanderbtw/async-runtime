```

BenchmarkDotNet v0.14.0, macOS 26.4.1 (25E253) [Darwin 25.4.0]
Apple M3 Pro, 1 CPU, 11 logical and 11 physical cores
.NET SDK 11.0.100-preview.4.26230.115
  [Host]     : .NET 11.0.0 (11.0.26.23115), Arm64 RyuJIT AdvSIMD
  DefaultJob : .NET 11.0.0 (11.0.26.23115), Arm64 RyuJIT AdvSIMD


```
| Method                                 | Mean       | Error    | StdDev   | Gen0     | Allocated  |
|--------------------------------------- |-----------:|---------:|---------:|---------:|-----------:|
| Direct_ThrowLoop_Control               |   946.2 μs | 18.44 μs | 39.69 μs |  57.6172 |  476.56 KB |
| Task_DirectFaultedForwardingLoop       | 1,067.1 μs | 21.26 μs | 25.31 μs |  95.7031 |  789.06 KB |
| ValueTask_DirectFaultedForwardingLoop  | 1,072.2 μs | 14.88 μs | 12.43 μs |  95.7031 |  789.06 KB |
| Task_ThrowBeforeAwaitLoop              | 1,825.3 μs | 10.29 μs |  9.12 μs | 144.5313 | 1203.13 KB |
| ValueTask_ThrowBeforeAwaitLoop         | 1,892.6 μs | 25.40 μs | 21.21 μs | 144.5313 | 1203.13 KB |
| ValueTask_ThrowAfterSuspendedAwaitLoop | 1,900.5 μs | 21.95 μs | 20.53 μs | 158.2031 | 1296.88 KB |
| ValueTask_ThrowAfterCompletedAwaitLoop | 1,915.0 μs | 36.67 μs | 74.91 μs | 144.5313 | 1203.13 KB |
| Task_ThrowAfterSuspendedAwaitLoop      | 1,923.7 μs | 36.67 μs | 48.95 μs | 156.2500 | 1289.06 KB |
| Task_ThrowAfterCompletedAwaitLoop      | 1,926.3 μs | 38.12 μs | 43.89 μs | 146.4844 | 1203.13 KB |
