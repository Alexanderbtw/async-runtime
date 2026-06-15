```

BenchmarkDotNet v0.14.0, macOS 26.4.1 (25E253) [Darwin 25.4.0]
Apple M3 Pro, 1 CPU, 11 logical and 11 physical cores
.NET SDK 11.0.100-preview.4.26230.115
  [Host]     : .NET 11.0.0 (11.0.26.23115), Arm64 RyuJIT AdvSIMD
  DefaultJob : .NET 11.0.0 (11.0.26.23115), Arm64 RyuJIT AdvSIMD


```
| Method                                 | Mean       | Error    | StdDev    | Median     | Gen0     | Allocated  |
|--------------------------------------- |-----------:|---------:|----------:|-----------:|---------:|-----------:|
| Direct_ThrowLoop_Control               |   934.3 μs | 18.68 μs |  40.21 μs |   923.1 μs |  57.6172 |  476.56 KB |
| Task_DirectFaultedForwardingLoop       | 1,060.9 μs |  7.87 μs |   6.97 μs | 1,061.4 μs |  95.7031 |  789.06 KB |
| ValueTask_DirectFaultedForwardingLoop  | 1,161.3 μs | 23.21 μs |  44.16 μs | 1,174.0 μs |  95.7031 |  789.06 KB |
| Task_ThrowAfterCompletedAwaitLoop      | 1,870.9 μs | 35.77 μs |  47.75 μs | 1,885.2 μs | 146.4844 | 1203.13 KB |
| Task_ThrowAfterSuspendedAwaitLoop      | 1,883.7 μs | 31.11 μs |  25.98 μs | 1,878.1 μs | 156.2500 | 1289.06 KB |
| ValueTask_ThrowBeforeAwaitLoop         | 1,911.4 μs | 34.11 μs |  64.07 μs | 1,880.4 μs | 146.4844 | 1203.13 KB |
| Task_ThrowBeforeAwaitLoop              | 1,934.9 μs | 38.68 μs |  95.62 μs | 1,931.9 μs | 146.4844 | 1203.13 KB |
| ValueTask_ThrowAfterCompletedAwaitLoop | 1,954.6 μs | 38.93 μs |  96.94 μs | 1,938.6 μs | 144.5313 | 1203.13 KB |
| ValueTask_ThrowAfterSuspendedAwaitLoop | 1,966.0 μs | 39.29 μs | 115.24 μs | 1,904.6 μs | 158.2031 | 1296.88 KB |
