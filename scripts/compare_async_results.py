#!/usr/bin/env python3
from __future__ import annotations

import argparse
import re
from pathlib import Path

import matplotlib.pyplot as plt
import pandas as pd


TIME_UNITS_TO_NS = {
    "ns": 1.0,
    "us": 1_000.0,
    "μs": 1_000.0,
    "µs": 1_000.0,
    "ms": 1_000_000.0,
    "s": 1_000_000_000.0,
}

SIZE_UNITS_TO_BYTES = {
    "B": 1.0,
    "KB": 1024.0,
    "MB": 1024.0**2,
    "GB": 1024.0**3,
}

INFRA_COLUMNS = {
    "Method",
    "Job",
    "AnalyzeLaunchVariance",
    "EvaluateOverhead",
    "MaxAbsoluteError",
    "MaxRelativeError",
    "MinInvokeCount",
    "MinIterationTime",
    "OutlierMode",
    "Affinity",
    "EnvironmentVariables",
    "Jit",
    "LargeAddressAware",
    "Platform",
    "PowerPlanMode",
    "Runtime",
    "AllowVeryLargeObjects",
    "Concurrent",
    "CpuGroups",
    "Force",
    "HeapAffinitizeMask",
    "HeapCount",
    "NoAffinitize",
    "RetainVm",
    "Server",
    "Arguments",
    "BuildConfiguration",
    "Clock",
    "EngineFactory",
    "NuGetReferences",
    "Toolchain",
    "IsMutator",
    "InvocationCount",
    "IterationCount",
    "IterationTime",
    "LaunchCount",
    "MaxIterationCount",
    "MaxWarmupIterationCount",
    "MemoryRandomization",
    "MinIterationCount",
    "MinWarmupIterationCount",
    "RunStrategy",
    "UnrollFactor",
    "WarmupCount",
    "Mean",
    "Error",
    "StdDev",
    "Median",
    "Gen0",
    "Allocated",
}


def parse_measurement(value: object, units: dict[str, float]) -> float:
    text = str(value or "").replace(",", "").strip()
    if not text:
        return 0.0

    match = re.match(r"^([-+]?\d+(?:\.\d+)?)\s*([^\s]+)?$", text)
    if not match:
        raise ValueError(f"Cannot parse measurement: {value!r}")

    number = float(match.group(1))
    unit = match.group(2)
    if unit is None:
        return number

    if unit not in units:
        raise ValueError(f"Unsupported unit {unit!r} in {value!r}")

    return number * units[unit]


def benchmark_name(path: Path) -> str:
    name = path.name.removesuffix("-report.csv")
    return name.removeprefix("Benchmark.")


def param_columns(frame: pd.DataFrame) -> list[str]:
    columns: list[str] = []
    for column in frame.columns:
        if column in INFRA_COLUMNS:
            continue

        values = frame[column].fillna("").astype(str).str.strip()
        meaningful = values[~values.isin(["", "Default"])]
        if not meaningful.empty:
            columns.append(column)

    return columns


def load_benchmark_report(path: Path, variant: str) -> pd.DataFrame:
    frame = pd.read_csv(path)
    params = param_columns(frame)
    report = benchmark_name(path)

    normalized = pd.DataFrame(
        {
            "Benchmark": report,
            "Method": frame["Method"].astype(str),
            "Variant": variant,
            "MeanNs": frame["Mean"].map(lambda x: parse_measurement(x, TIME_UNITS_TO_NS)),
            "ErrorNs": frame["Error"].map(lambda x: parse_measurement(x, TIME_UNITS_TO_NS)),
            "StdDevNs": frame["StdDev"].map(lambda x: parse_measurement(x, TIME_UNITS_TO_NS)),
            "AllocatedBytes": frame["Allocated"].map(lambda x: parse_measurement(x, SIZE_UNITS_TO_BYTES)),
        }
    )

    for column in params:
        normalized[column] = frame[column].astype(str)

    normalized["Params"] = normalized.apply(lambda row: format_params(row, params), axis=1)
    normalized["Key"] = normalized.apply(
        lambda row: f"{row['Benchmark']}|{row['Method']}|{row['Params']}",
        axis=1,
    )
    return normalized


def format_params(row: pd.Series, params: list[str]) -> str:
    parts = []
    for column in params:
        value = str(row[column]).strip()
        if value and value != "Default":
            parts.append(f"{column}={value}")

    return ", ".join(parts)


def load_comparison(standard_dir: Path, runtime_dir: Path) -> pd.DataFrame:
    standard_reports = {path.name: path for path in standard_dir.glob("*-report.csv")}
    runtime_reports = {path.name: path for path in runtime_dir.glob("*-report.csv")}
    common_reports = sorted(standard_reports.keys() & runtime_reports.keys())

    if not common_reports:
        raise FileNotFoundError(
            f"No matching BenchmarkDotNet CSV reports in {standard_dir} and {runtime_dir}"
        )

    rows = []
    for report in common_reports:
        standard = load_benchmark_report(standard_reports[report], "classic")
        runtime = load_benchmark_report(runtime_reports[report], "runtime")
        merged = standard.merge(
            runtime,
            on=["Key", "Benchmark", "Method", "Params"],
            suffixes=("_Classic", "_Runtime"),
            how="inner",
        )

        if len(merged) != len(standard) or len(merged) != len(runtime):
            missing = sorted((set(standard["Key"]) ^ set(runtime["Key"])))
            raise ValueError(f"Unmatched rows in {report}: {missing}")

        rows.append(merged)

    comparison = pd.concat(rows, ignore_index=True)
    comparison["Speedup"] = comparison["MeanNs_Classic"] / comparison["MeanNs_Runtime"]
    comparison["MeanDeltaPercent"] = (
        (comparison["MeanNs_Runtime"] - comparison["MeanNs_Classic"])
        / comparison["MeanNs_Classic"]
        * 100.0
    )
    comparison["AllocationDeltaBytes"] = (
        comparison["AllocatedBytes_Runtime"] - comparison["AllocatedBytes_Classic"]
    )
    comparison["CombinedErrorNs"] = comparison["ErrorNs_Classic"] + comparison["ErrorNs_Runtime"]
    comparison["MeanDeltaNs"] = comparison["MeanNs_Runtime"] - comparison["MeanNs_Classic"]
    comparison["IsNoiseSensitive"] = comparison["MeanDeltaNs"].abs() <= comparison["CombinedErrorNs"]
    comparison["IsSubNanosecond"] = comparison[["MeanNs_Classic", "MeanNs_Runtime"]].min(axis=1) < 1.0
    comparison["Interpretation"] = comparison.apply(interpret_row, axis=1)

    return comparison.sort_values(["Benchmark", "Method", "Params"]).reset_index(drop=True)


def interpret_row(row: pd.Series) -> str:
    if row["IsSubNanosecond"]:
        return "micro-noise"
    if row["IsNoiseSensitive"]:
        return "parity/noise"
    if row["Speedup"] > 1.05:
        return "runtime faster"
    if row["Speedup"] < 0.95:
        return "runtime slower"
    return "parity"


def plot_speedup(comparison: pd.DataFrame, output: Path) -> None:
    plot_frame = select_speedup_rows(comparison)
    plot_frame["Label"] = plot_frame.apply(format_benchmark_label, axis=1)
    plot_frame = plot_frame.sort_values("Speedup")

    colors = plot_frame["Interpretation"].map(
        {
            "runtime faster": "#2f7d5b",
            "runtime slower": "#b84a4a",
            "parity": "#8a8f98",
            "parity/noise": "#b0a46f",
            "micro-noise": "#9aa3ad",
        }
    )

    _, ax = plt.subplots(figsize=(13.33, 7.5), dpi=180)
    ax.barh(plot_frame["Label"], plot_frame["Speedup"], color=colors)
    ax.axvline(1.0, color="#20242a", linewidth=1.1)
    ax.axvspan(0.95, 1.05, color="#d8d2b0", alpha=0.22, linewidth=0)
    ax.set_xlabel("Runtime async speedup vs classic async (classic mean / runtime mean)")
    ax.set_ylabel("")
    ax.set_title("BenchmarkDotNet highlights: classic async vs runtime async", loc="left", pad=14)
    ax.grid(axis="x", color="#d7dce2", linewidth=0.8, alpha=0.8)
    ax.set_axisbelow(True)
    ax.tick_params(axis="y", labelsize=8.5)
    ax.tick_params(axis="x", labelsize=9)

    max_speedup = max(plot_frame["Speedup"].max(), 1.08)
    ax.set_xlim(left=max(0.0, plot_frame["Speedup"].min() - 0.08), right=max_speedup + 0.14)

    for index, (_, row) in enumerate(plot_frame.iterrows()):
        ax.text(
            row["Speedup"] + 0.015,
            index,
            f"{row['Speedup']:.2f}x",
            va="center",
            ha="left",
            fontsize=7.5,
            color="#20242a",
        )

    plt.tight_layout()
    output.parent.mkdir(parents=True, exist_ok=True)
    plt.savefig(output, bbox_inches="tight")
    plt.close()


def select_speedup_rows(comparison: pd.DataFrame) -> pd.DataFrame:
    stable = comparison[comparison["Interpretation"] != "micro-noise"].copy()
    faster = stable[stable["Speedup"] > 1.05].nlargest(14, "Speedup")
    slower = stable[stable["Speedup"] < 0.95].nsmallest(6, "Speedup")

    selected = pd.concat([faster, slower], ignore_index=True)
    if selected.empty:
        selected = stable.iloc[
            (stable["Speedup"] - 1.0).abs().sort_values(ascending=False).index[:20]
        ]

    return selected.drop_duplicates("Key")


def format_benchmark_label(row: pd.Series) -> str:
    params = f" ({row['Params']})" if row["Params"] else ""
    return f"{short_benchmark(row['Benchmark'])}: {row['Method']}{params}"


def short_benchmark(name: str) -> str:
    return name.removesuffix("Benchmarks")


def plot_size(size_csv: Path, output: Path) -> None:
    frame = pd.read_csv(size_csv)
    required = {"Label", "FileSizeBytes"}
    missing = required - set(frame.columns)
    if missing:
        raise ValueError(f"Size CSV is missing columns: {', '.join(sorted(missing))}")

    frame = frame.copy()
    frame["SizeKiB"] = frame["FileSizeBytes"] / 1024.0

    order = ["classic", "runtime"]
    if set(order).issubset(set(frame["Label"])):
        frame["Order"] = frame["Label"].map({label: index for index, label in enumerate(order)})
        frame = frame.sort_values("Order")

    _, ax = plt.subplots(figsize=(13.33, 7.5), dpi=180)
    bars = ax.bar(frame["Label"], frame["SizeKiB"], color=["#526b8f", "#2f7d5b"][: len(frame)])
    ax.set_ylabel("DLL size, KiB")
    ax.set_title("Parktronik.Api.dll size", loc="left", pad=16, fontsize=18)
    ax.grid(axis="y", color="#d7dce2", linewidth=0.8, alpha=0.8)
    ax.set_axisbelow(True)
    ax.tick_params(axis="x", labelsize=12)
    ax.tick_params(axis="y", labelsize=10)

    max_size = frame["SizeKiB"].max()
    ax.set_ylim(0, max_size * 1.32)

    for bar, value in zip(bars, frame["SizeKiB"], strict=False):
        ax.text(
            bar.get_x() + bar.get_width() / 2,
            bar.get_height() + max_size * 0.018,
            f"{value:.1f} KiB",
            ha="center",
            va="bottom",
            fontsize=12,
            color="#20242a",
        )

    if set(order).issubset(set(frame["Label"])):
        classic = frame.loc[frame["Label"] == "classic", "FileSizeBytes"].iloc[0]
        runtime = frame.loc[frame["Label"] == "runtime", "FileSizeBytes"].iloc[0]
        delta_kib = (runtime - classic) / 1024.0
        delta_percent = (runtime - classic) / classic * 100.0
        ax.text(
            0.5,
            max_size * 1.18,
            f"{delta_kib:+.1f} KiB / {delta_percent:+.1f}%",
            ha="center",
            va="center",
            fontsize=24,
            fontweight="bold",
            color="#2f7d5b" if delta_percent < 0 else "#b84a4a",
        )

    footer_parts = ["Release publish", "framework-dependent", "no PDB", "main DLL only"]
    ax.text(
        0.0,
        -0.10,
        " | ".join(footer_parts),
        transform=ax.transAxes,
        ha="left",
        va="top",
        fontsize=9,
        color="#505862",
    )

    plt.tight_layout()
    output.parent.mkdir(parents=True, exist_ok=True)
    plt.savefig(output, bbox_inches="tight")
    plt.close()


def main() -> None:
    parser = argparse.ArgumentParser(
        description="Compare existing BenchmarkDotNet Standard/AsyncRuntime CSV reports and plot slide-ready charts."
    )
    parser.add_argument(
        "--standard-dir",
        type=Path,
        default=Path("Benchmark/BenchmarkDotNet.Artifacts.Standard/results"),
        help="Directory with classic async BenchmarkDotNet CSV reports.",
    )
    parser.add_argument(
        "--runtime-dir",
        type=Path,
        default=Path("Benchmark/BenchmarkDotNet.Artifacts.AsyncRuntime/results"),
        help="Directory with runtime async BenchmarkDotNet CSV reports.",
    )
    parser.add_argument(
        "--size-csv",
        type=Path,
        default=None,
        help="Optional SizeBench CSV with classic/runtime rows.",
    )
    parser.add_argument(
        "--out-dir",
        type=Path,
        default=Path("artifacts/async-comparison"),
        help="Directory for normalized CSV and PNG outputs.",
    )
    args = parser.parse_args()

    args.out_dir.mkdir(parents=True, exist_ok=True)

    comparison = load_comparison(args.standard_dir, args.runtime_dir)
    comparison_csv = args.out_dir / "benchmark-comparison.csv"
    comparison.to_csv(comparison_csv, index=False)
    plot_speedup(comparison, args.out_dir / "benchmark-speedup.png")

    print(f"Wrote {comparison_csv}")
    print(f"Wrote {args.out_dir / 'benchmark-speedup.png'}")

    if args.size_csv is not None:
        size_csv = args.size_csv
        if not size_csv.exists():
            raise FileNotFoundError(size_csv)

        normalized_size_csv = args.out_dir / "parktronik-size.csv"
        pd.read_csv(size_csv).to_csv(normalized_size_csv, index=False)
        plot_size(normalized_size_csv, args.out_dir / "parktronik-size.png")
        print(f"Wrote {normalized_size_csv}")
        print(f"Wrote {args.out_dir / 'parktronik-size.png'}")


if __name__ == "__main__":
    main()
