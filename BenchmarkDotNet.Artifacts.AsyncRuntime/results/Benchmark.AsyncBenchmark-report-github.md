```

BenchmarkDotNet v0.14.0, macOS 26.4.1 (25E253) [Darwin 25.4.0]
Apple M3 Pro, 1 CPU, 11 logical and 11 physical cores
.NET SDK 11.0.100-preview.4.26230.115
  [Host] : .NET 11.0.0 (11.0.26.23115), Arm64 RyuJIT AdvSIMD

Toolchain=InProcessEmitToolchain  

```
| Method   | Mean     | Error   | StdDev  | Gen0    | Gen1    | Allocated |
|--------- |---------:|--------:|--------:|--------:|--------:|----------:|
| NewAsync | 107.2 μs | 2.07 μs | 2.39 μs | 90.8203 | 12.9395 | 742.31 KB |
