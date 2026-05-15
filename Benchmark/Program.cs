using Benchmark;

using BenchmarkDotNet.Running;

BenchmarkRunner.Run<TaskAllocationBenchmarks>(args: args);
