```

BenchmarkDotNet v0.14.0, macOS 26.4.1 (25E253) [Darwin 25.4.0]
Apple M3 Pro, 1 CPU, 11 logical and 11 physical cores
.NET SDK 11.0.100-preview.4.26230.115
  [Host] : .NET 11.0.0 (11.0.26.23115), Arm64 RyuJIT AdvSIMD

Toolchain=InProcessEmitToolchain  

```
| Method            | Mean      | Error    | StdDev   | Gen0     | Gen1    | Allocated  |
|------------------ |----------:|---------:|---------:|---------:|--------:|-----------:|
| NewAsync          | 165.57 μs | 2.682 μs | 2.094 μs | 176.7578 | 25.1465 | 1445.44 KB |
| NewValueTaskAsync |  66.65 μs | 0.664 μs | 0.588 μs |   4.7607 |  0.4883 |   39.12 KB |
