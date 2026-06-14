```

BenchmarkDotNet v0.14.0, macOS 26.4.1 (25E253) [Darwin 25.4.0]
Apple M3 Pro, 1 CPU, 11 logical and 11 physical cores
.NET SDK 11.0.100-preview.4.26230.115
  [Host]     : .NET 11.0.0 (11.0.26.23115), Arm64 RyuJIT AdvSIMD
  DefaultJob : .NET 11.0.0 (11.0.26.23115), Arm64 RyuJIT AdvSIMD
  Job-JFQWGN : .NET 11.0.0 (11.0.26.23115), Arm64 RyuJIT AdvSIMD


```
| Method              | Job        | InvocationCount | UnrollFactor | Mean              | StdDev          | Ratio | RatioSD | Gen0   | Allocated | Alloc Ratio |
|-------------------- |----------- |---------------- |------------- |------------------:|----------------:|------:|--------:|-------:|----------:|------------:|
| WaitAsync_Completed | DefaultJob | Default         | 16           |         0.7197 ns |       0.0144 ns | 0.006 |    0.00 |      - |         - |        0.00 |
| WhenAny_Completed   | DefaultJob | Default         | 16           |       111.2923 ns |       3.0055 ns | 1.001 |    0.04 | 0.0631 |     528 B |        1.00 |
|                     |            |                 |              |                   |                 |       |         |        |           |             |
| WaitAsync_Timeout   | Job-JFQWGN | 1               | 1            | 5,692,855.1333 ns |  21,811.9625 ns |     ? |       ? |      - |         - |           ? |
| WhenAny_Timeout     | Job-JFQWGN | 1               | 1            | 5,641,893.3750 ns | 105,968.0773 ns |     ? |       ? |      - |         - |           ? |
