# async-runtime benchmark workflow

This repository is used to compare classic C# async lowering with .NET runtime async.
The main workflow is:

1. build/run the same BenchmarkDotNet suite twice;
2. collect classic and runtime-async CSV reports;
3. generate normalized CSV files and PNG charts for slide selection.

## Prerequisites

- .NET 11 preview SDK.
- Python 3 with the packages from `requirements.txt`.

Recommended Python setup:

```bash
python3 -m venv .venv
.venv/bin/pip install -r requirements.txt
```

## Run Benchmarks

The active benchmark suite is presentation-oriented: each operation runs many completed awaits, controlled suspensions, live-state saves, or exception propagations. This intentionally amplifies per-await overhead so classic/runtime async differences are easier to see on slides.

Run classic async:

```bash
dotnet run -c Standard --project Benchmark/Benchmark.csproj
```

Run runtime async:

```bash
dotnet run -c AsyncRuntime --project Benchmark/Benchmark.csproj
```

The benchmark project writes CSV reports to:

- `Benchmark/BenchmarkDotNet.Artifacts.Standard/results`
- `Benchmark/BenchmarkDotNet.Artifacts.AsyncRuntime/results`

`Standard` is the classic async build. `AsyncRuntime` enables `runtime-async=on`.

## Generate Comparison Artifacts

Do not rerun BenchmarkDotNet just to visualize existing results. Generate charts from the CSV reports:

```bash
.venv/bin/python scripts/compare_async_results.py \
  --standard-dir Benchmark/BenchmarkDotNet.Artifacts.Standard/results \
  --runtime-dir Benchmark/BenchmarkDotNet.Artifacts.AsyncRuntime/results \
  --out-dir artifacts/async-comparison-$(date +%Y%m%d) \
  --metrics Mean,Median,Error,StdDev,Allocated,Gen0 \
  --include-derived
```

Outputs:

- `benchmark-comparison.csv` - normalized classic/runtime comparison table.
- `manifest.csv` - list of generated PNG files and significance hints.
- `benchmarks/<BenchmarkName>/<Metric>.png` - raw metric charts.
- `benchmarks/<BenchmarkName>/<Metric>-derived.png` - speedup/delta charts when `--include-derived` is used.

Prefer a fresh `--out-dir` for each benchmark run so old PNG files are not mixed with new results.

## Reading Results

- Treat current benchmark numbers as high-density hot-path results, not as the cost of a single business operation.
- Prefer raw classic/runtime charts over speedup-only charts.
- `Mean` is the main time metric for most benchmark families.
- `Allocated` is the main metric for allocation-sensitive scenarios.
- `Error` is BenchmarkDotNet statistical uncertainty, not an exception/error-path metric.
- Treat differences smaller than the reported uncertainty as weak evidence.
- `Task.Yield`, timers, timeouts, scheduler dispatch, and ThreadPool continuation behavior are noise-sensitive.
- Allocation deltas are usually easier to defend than tiny sub-nanosecond timing deltas.

## Optional Size Workflow

`SizeBench` can compare published assembly size and metadata for classic/runtime outputs. It is useful for size slides, but it is separate from the BenchmarkDotNet timing workflow.

Example shape:

```bash
dotnet run -c Release --project SizeBench/SizeBench.csproj -- \
  classic=path/to/classic/Parktronik.Api.dll \
  runtime=path/to/runtime/Parktronik.Api.dll \
  --csv artifacts/async-comparison/parktronik-size.csv
```

If a size CSV is available, pass it to the visualizer:

```bash
.venv/bin/python scripts/compare_async_results.py \
  --standard-dir Benchmark/BenchmarkDotNet.Artifacts.Standard/results \
  --runtime-dir Benchmark/BenchmarkDotNet.Artifacts.AsyncRuntime/results \
  --out-dir artifacts/async-comparison-$(date +%Y%m%d) \
  --metrics Mean,Median,Error,StdDev,Allocated,Gen0 \
  --include-derived \
  --size-csv artifacts/async-comparison/parktronik-size.csv
```
