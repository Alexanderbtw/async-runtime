```

BenchmarkDotNet v0.14.0, macOS 26.4.1 (25E253) [Darwin 25.4.0]
Apple M3 Pro, 1 CPU, 11 logical and 11 physical cores
.NET SDK 11.0.100-preview.4.26230.115
  [Host]   : .NET 11.0.0 (11.0.26.23115), Arm64 RyuJIT AdvSIMD
  ShortRun : .NET 11.0.0 (11.0.26.23115), Arm64 RyuJIT AdvSIMD

Job=ShortRun  IterationCount=3  LaunchCount=1  
WarmupCount=3  

```
| Method                     | Depth | Mean        | Error       | StdDev     | Median      | Gen0   | Allocated |
|--------------------------- |------ |------------:|------------:|-----------:|------------:|-------:|----------:|
| Task_DirectForwarding      | 1     |   0.0556 ns |   0.6981 ns |  0.0383 ns |   0.0396 ns |      - |         - |
| Task_DirectForwarding      | 2     |   0.1844 ns |   2.9422 ns |  0.1613 ns |   0.1821 ns |      - |         - |
| ValueTask_DirectForwarding | 1     |   1.3791 ns |   3.1668 ns |  0.1736 ns |   1.2853 ns |      - |         - |
| Task_DirectForwarding      | 4     |   1.5895 ns |   2.4620 ns |  0.1350 ns |   1.5190 ns |      - |         - |
| ValueTask_DirectForwarding | 2     |   1.6287 ns |   2.3042 ns |  0.1263 ns |   1.6815 ns |      - |         - |
| Task_DirectForwarding      | 8     |   2.5286 ns |   2.6021 ns |  0.1426 ns |   2.4741 ns |      - |         - |
| ValueTask_DirectForwarding | 4     |   3.1451 ns |  12.1702 ns |  0.6671 ns |   3.0077 ns |      - |         - |
| Task_DirectForwarding      | 16    |   5.1457 ns |   1.9441 ns |  0.1066 ns |   5.0842 ns |      - |         - |
| ValueTask_DirectForwarding | 8     |   5.4936 ns |   4.9384 ns |  0.2707 ns |   5.3383 ns |      - |         - |
| ValueTask_AwaitOnly        | 1     |   8.3197 ns |  12.4025 ns |  0.6798 ns |   8.1325 ns |      - |         - |
| ValueTask_AddAfterAwait    | 1     |   8.8162 ns |  11.7334 ns |  0.6431 ns |   8.7096 ns |      - |         - |
| Task_DirectForwarding      | 32    |  10.1984 ns |   8.6154 ns |  0.4722 ns |  10.3208 ns |      - |         - |
| ValueTask_AddAfterAwait    | 2     |  12.2135 ns |  11.0955 ns |  0.6082 ns |  11.9127 ns |      - |         - |
| ValueTask_DirectForwarding | 16    |  12.3936 ns |  11.7841 ns |  0.6459 ns |  12.2286 ns |      - |         - |
| ValueTask_AwaitOnly        | 2     |  14.0922 ns |  27.2071 ns |  1.4913 ns |  13.8703 ns |      - |         - |
| Task_AddAfterAwait         | 1     |  16.8920 ns |  10.3406 ns |  0.5668 ns |  16.8699 ns | 0.0172 |     144 B |
| Task_AwaitOnly             | 1     |  18.7425 ns |  33.5892 ns |  1.8411 ns |  18.3025 ns | 0.0172 |     144 B |
| ValueTask_AddAfterAwait    | 4     |  24.3964 ns |   7.9566 ns |  0.4361 ns |  24.3034 ns |      - |         - |
| ValueTask_AwaitOnly        | 4     |  24.7930 ns |  30.4298 ns |  1.6680 ns |  24.3033 ns |      - |         - |
| ValueTask_DirectForwarding | 32    |  24.9477 ns |  22.8146 ns |  1.2505 ns |  24.2924 ns |      - |         - |
| Task_AddAfterAwait         | 2     |  25.0669 ns |  17.1642 ns |  0.9408 ns |  25.3671 ns | 0.0258 |     216 B |
| Task_AwaitOnly             | 2     |  25.5622 ns |  64.6645 ns |  3.5445 ns |  24.8206 ns | 0.0258 |     216 B |
| Task_AddAfterAwait         | 4     |  38.5599 ns |  10.1472 ns |  0.5562 ns |  38.3753 ns | 0.0430 |     360 B |
| ValueTask_AddAfterAwait    | 8     |  47.0647 ns |   5.0737 ns |  0.2781 ns |  47.0625 ns |      - |         - |
| ValueTask_AwaitOnly        | 8     |  47.4407 ns |  30.5471 ns |  1.6744 ns |  47.3964 ns |      - |         - |
| Task_AwaitOnly             | 4     |  48.3012 ns | 130.7855 ns |  7.1688 ns |  45.3621 ns | 0.0430 |     360 B |
| Task_AddAfterAwait         | 8     |  75.2120 ns |  56.4077 ns |  3.0919 ns |  74.8828 ns | 0.0774 |     648 B |
| Task_AwaitOnly             | 8     |  86.2742 ns | 330.7794 ns | 18.1311 ns |  78.8041 ns | 0.0774 |     648 B |
| ValueTask_AwaitOnly        | 16    |  99.8464 ns |  35.4342 ns |  1.9423 ns |  99.7560 ns |      - |         - |
| ValueTask_AddAfterAwait    | 16    | 103.3486 ns |   5.8644 ns |  0.3214 ns | 103.3848 ns |      - |         - |
| Task_AddAfterAwait         | 16    | 138.3535 ns |  69.5992 ns |  3.8150 ns | 139.4636 ns | 0.1462 |    1224 B |
| Task_AwaitOnly             | 16    | 141.9339 ns |  87.9079 ns |  4.8185 ns | 139.7758 ns | 0.1462 |    1224 B |
| ValueTask_AwaitOnly        | 32    | 224.3764 ns | 144.0331 ns |  7.8949 ns | 222.0482 ns |      - |         - |
| ValueTask_AddAfterAwait    | 32    | 235.9558 ns | 492.4680 ns | 26.9938 ns | 230.1478 ns |      - |         - |
| Task_AwaitOnly             | 32    | 445.1166 ns | 157.7279 ns |  8.6456 ns | 446.4026 ns | 0.2837 |    2376 B |
| Task_AddAfterAwait         | 32    | 474.0721 ns | 216.7430 ns | 11.8804 ns | 470.0240 ns | 0.2837 |    2376 B |
