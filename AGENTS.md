# Agent Instructions

This repository compares classic C# async lowering with .NET runtime async.

## Core Rules

- Do not run BenchmarkDotNet unless the user explicitly asks for a benchmark run.
- Visualization must use existing CSV reports; it must not rerun benchmarks.
- Use a fresh `--out-dir` for generated comparison artifacts to avoid mixing old and new PNG files.
- Do not revert unrelated user changes or generated benchmark artifacts.
- Prefer `rg`/`rg --files` for repo exploration.

## Build Modes

- `Standard` is the classic async configuration.
- `AsyncRuntime` enables runtime async with `runtime-async=on`.
- `Benchmark/Benchmark.csproj` also sets `UseRuntimeAsync=false` for `Standard`.

After changing benchmark code, verify both configurations:

```bash
dotnet build Benchmark/Benchmark.csproj -c Standard
dotnet build Benchmark/Benchmark.csproj -c AsyncRuntime
```

## Benchmark Results

The active `Benchmark/Program.cs` suite is intentionally high-density and presentation-oriented. One benchmark operation may run thousands of awaits, controlled suspensions, live-state payload saves, or exception propagations so small async-model overheads become visible. Do not describe these numbers as single-operation latency.

BenchmarkDotNet results are expected under:

- `Benchmark/BenchmarkDotNet.Artifacts.Standard/results`
- `Benchmark/BenchmarkDotNet.Artifacts.AsyncRuntime/results`

To generate comparison charts from existing CSV reports:

```bash
.venv/bin/python scripts/compare_async_results.py \
  --standard-dir Benchmark/BenchmarkDotNet.Artifacts.Standard/results \
  --runtime-dir Benchmark/BenchmarkDotNet.Artifacts.AsyncRuntime/results \
  --out-dir artifacts/async-comparison-$(date +%Y%m%d) \
  --metrics Mean,Allocated \
  --include-derived
```

The important outputs are:

- `benchmark-comparison.csv`
- `manifest.csv`
- `benchmarks/<BenchmarkName>/<Metric>.png`

For audit charts, `--metrics Mean,Median,Error,StdDev,Allocated,Gen0,Gen1` remains supported.

## Interpreting Results

- Raw classic/runtime charts are primary. Speedup and delta charts are derived views.
- `Mean` is the main time metric; `Allocated` is the main memory metric.
- `Error` means BenchmarkDotNet statistical uncertainty.
- Tiny timing deltas, especially sub-nanosecond deltas, are weak evidence.
- `Task.Yield`, timers, timeout scenarios, scheduler dispatch, and ThreadPool behavior are noise-sensitive.
- Direct forwarding methods without `async` are baselines, not direct evidence about async model differences.
- Prefer explaining each benchmark by the async-model behavior it isolates: completed path, controlled suspension, live state, exception propagation, or allocation behavior.

## Documentation Expectations

- Keep README focused on the benchmark workflow.
- Keep this file focused on agent behavior and repo-specific constraints.
- If the visualizer CLI changes, update both `README.md` and this file in the same change.
