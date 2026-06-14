```

BenchmarkDotNet v0.14.0, macOS 26.4.1 (25E253) [Darwin 25.4.0]
Apple M3 Pro, 1 CPU, 11 logical and 11 physical cores
.NET SDK 11.0.100-preview.4.26230.115
  [Host]     : .NET 11.0.0 (11.0.26.23115), Arm64 RyuJIT AdvSIMD
  DefaultJob : .NET 11.0.0 (11.0.26.23115), Arm64 RyuJIT AdvSIMD
  Job-VSBRET : .NET 11.0.0 (11.0.26.23115), Arm64 RyuJIT AdvSIMD


```
| Method              | Job        | InvocationCount | UnrollFactor | Mean              | StdDev          | Median            | Ratio | RatioSD | Gen0   | Allocated | Alloc Ratio |
|-------------------- |----------- |---------------- |------------- |------------------:|----------------:|------------------:|------:|--------:|-------:|----------:|------------:|
| WaitAsync_Completed | DefaultJob | Default         | 16           |         0.7356 ns |       0.0082 ns |         0.7329 ns | 0.007 |    0.00 |      - |         - |        0.00 |
| WhenAny_Completed   | DefaultJob | Default         | 16           |       104.6075 ns |       1.8364 ns |       105.1049 ns | 1.000 |    0.02 | 0.0631 |     528 B |        1.00 |
|                     |            |                 |              |                   |                 |                   |       |         |        |           |             |
| WaitAsync_Timeout   | Job-VSBRET | 1               | 1            | 5,581,733.4688 ns | 254,651.6330 ns | 5,688,375.5000 ns |     ? |       ? |      - |         - |           ? |
| WhenAny_Timeout     | Job-VSBRET | 1               | 1            | 5,600,786.3243 ns | 187,921.6634 ns | 5,680,417.0000 ns |     ? |       ? |      - |         - |           ? |
