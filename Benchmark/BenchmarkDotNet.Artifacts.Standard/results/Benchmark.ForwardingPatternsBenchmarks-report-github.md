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
| Task_DirectForwarding      | 2     |   0.1249 ns |   0.7646 ns |  0.0419 ns |   0.1125 ns |      - |         - |
| Task_DirectForwarding      | 1     |   0.2685 ns |   1.0235 ns |  0.0561 ns |   0.2710 ns |      - |         - |
| ValueTask_DirectForwarding | 1     |   1.3401 ns |   1.1409 ns |  0.0625 ns |   1.3363 ns |      - |         - |
| Task_DirectForwarding      | 4     |   1.6224 ns |   9.4073 ns |  0.5156 ns |   1.3848 ns |      - |         - |
| ValueTask_DirectForwarding | 2     |   2.2573 ns |  19.4580 ns |  1.0666 ns |   1.7720 ns |      - |         - |
| Task_DirectForwarding      | 8     |   3.3311 ns |   3.2310 ns |  0.1771 ns |   3.2619 ns |      - |         - |
| ValueTask_DirectForwarding | 4     |   3.7959 ns |   6.5912 ns |  0.3613 ns |   3.8054 ns |      - |         - |
| Task_DirectForwarding      | 16    |   5.1614 ns |   1.0262 ns |  0.0563 ns |   5.1606 ns |      - |         - |
| ValueTask_DirectForwarding | 8     |   5.2518 ns |   0.7986 ns |  0.0438 ns |   5.2745 ns |      - |         - |
| ValueTask_AddAfterAwait    | 1     |   8.1255 ns |   6.9089 ns |  0.3787 ns |   7.9580 ns |      - |         - |
| ValueTask_AwaitOnly        | 1     |   8.2747 ns |   3.8696 ns |  0.2121 ns |   8.2287 ns |      - |         - |
| Task_DirectForwarding      | 32    |  10.0344 ns |   2.5867 ns |  0.1418 ns |  10.0040 ns |      - |         - |
| ValueTask_DirectForwarding | 16    |  11.6630 ns |   5.8384 ns |  0.3200 ns |  11.5438 ns |      - |         - |
| ValueTask_AwaitOnly        | 2     |  14.3210 ns |  23.4008 ns |  1.2827 ns |  13.7864 ns |      - |         - |
| Task_AddAfterAwait         | 1     |  15.3855 ns |   6.2467 ns |  0.3424 ns |  15.4560 ns | 0.0172 |     144 B |
| Task_AwaitOnly             | 1     |  16.4942 ns |  21.6168 ns |  1.1849 ns |  16.6577 ns | 0.0172 |     144 B |
| ValueTask_AddAfterAwait    | 2     |  18.3924 ns |  65.6264 ns |  3.5972 ns |  17.5885 ns |      - |         - |
| ValueTask_DirectForwarding | 32    |  23.5892 ns |   1.1780 ns |  0.0646 ns |  23.6231 ns |      - |         - |
| Task_AddAfterAwait         | 2     |  24.2266 ns |  14.3865 ns |  0.7886 ns |  24.0288 ns | 0.0258 |     216 B |
| Task_AwaitOnly             | 2     |  24.7284 ns |   6.0235 ns |  0.3302 ns |  24.7210 ns | 0.0258 |     216 B |
| ValueTask_AddAfterAwait    | 4     |  26.3738 ns |  18.8175 ns |  1.0315 ns |  26.8467 ns |      - |         - |
| ValueTask_AwaitOnly        | 4     |  34.0742 ns |  31.7076 ns |  1.7380 ns |  33.7890 ns |      - |         - |
| Task_AddAfterAwait         | 4     |  50.3199 ns |  36.4514 ns |  1.9980 ns |  50.7567 ns | 0.0430 |     360 B |
| ValueTask_AwaitOnly        | 8     |  51.0723 ns | 115.1245 ns |  6.3104 ns |  49.3890 ns |      - |         - |
| Task_AwaitOnly             | 4     |  51.9752 ns |   8.5907 ns |  0.4709 ns |  52.1936 ns | 0.0430 |     360 B |
| ValueTask_AddAfterAwait    | 8     |  75.9698 ns | 651.8535 ns | 35.7303 ns |  58.5288 ns |      - |         - |
| Task_AwaitOnly             | 8     |  85.4375 ns |  89.6739 ns |  4.9153 ns |  86.6694 ns | 0.0774 |     648 B |
| ValueTask_AwaitOnly        | 16    |  95.3139 ns |  29.3395 ns |  1.6082 ns |  94.8987 ns |      - |         - |
| ValueTask_AddAfterAwait    | 16    |  97.0098 ns |  10.0939 ns |  0.5533 ns |  97.2298 ns |      - |         - |
| Task_AddAfterAwait         | 8     | 106.0603 ns | 731.8390 ns | 40.1146 ns |  89.2989 ns | 0.0774 |     648 B |
| Task_AddAfterAwait         | 16    | 138.3490 ns | 122.6223 ns |  6.7213 ns | 135.3749 ns | 0.1462 |    1224 B |
| Task_AwaitOnly             | 16    | 149.1846 ns | 119.5717 ns |  6.5541 ns | 150.9221 ns | 0.1462 |    1224 B |
| ValueTask_AwaitOnly        | 32    | 194.0604 ns |  21.8954 ns |  1.2002 ns | 193.9308 ns |      - |         - |
| ValueTask_AddAfterAwait    | 32    | 199.4780 ns |  57.3858 ns |  3.1455 ns | 198.5802 ns |      - |         - |
| Task_AddAfterAwait         | 32    | 409.7154 ns | 281.5764 ns | 15.4342 ns | 412.1860 ns | 0.2837 |    2376 B |
| Task_AwaitOnly             | 32    | 429.5033 ns | 103.5254 ns |  5.6746 ns | 428.5451 ns | 0.2837 |    2376 B |
