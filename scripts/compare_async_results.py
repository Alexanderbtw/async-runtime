#!/usr/bin/env python3
from __future__ import annotations

import argparse
import math
import re
from pathlib import Path

import matplotlib.pyplot as plt
import pandas as pd


CLASSIC_COLOR = "#526b8f"
RUNTIME_COLOR = "#2f7d5b"
DELTA_COLOR = "#7a6f45"
SLOWER_COLOR = "#b84a4a"
GRID_COLOR = "#d7dce2"
TEXT_COLOR = "#20242a"

DEFAULT_METRICS = ["Mean", "Median", "Error", "StdDev", "Allocated", "Gen0"]

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

METRIC_COLUMNS = {
    "Mean",
    "Median",
    "Error",
    "StdDev",
    "Gen0",
    "Allocated",
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
    "Ratio",
    "RatioSD",
    "Alloc Ratio",
    *METRIC_COLUMNS,
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


def short_benchmark(name: str) -> str:
    return name.removesuffix("Benchmarks")


def slug(value: str) -> str:
    cleaned = re.sub(r"[^A-Za-z0-9]+", "-", value).strip("-").lower()
    return cleaned or "item"


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
    data: dict[str, object] = {
        "Benchmark": report,
        "Method": frame["Method"].astype(str),
        "Variant": variant,
    }

    for metric in METRIC_COLUMNS:
        if metric not in frame.columns:
            continue

        if metric in {"Mean", "Median", "Error", "StdDev"}:
            data[f"{metric}Ns"] = frame[metric].map(lambda x: parse_measurement(x, TIME_UNITS_TO_NS))
        elif metric == "Allocated":
            data["AllocatedBytes"] = frame[metric].map(lambda x: parse_measurement(x, SIZE_UNITS_TO_BYTES))
        elif metric == "Gen0":
            data["Gen0"] = pd.to_numeric(frame[metric], errors="coerce").fillna(0.0)

    normalized = pd.DataFrame(data)

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
            missing = sorted(set(standard["Key"]) ^ set(runtime["Key"]))
            raise ValueError(f"Unmatched rows in {report}: {missing}")

        rows.append(merged)

    comparison = pd.concat(rows, ignore_index=True)
    add_derived_columns(comparison)
    return comparison.sort_values(["Benchmark", "Method", "Params"]).reset_index(drop=True)


def add_derived_columns(comparison: pd.DataFrame) -> None:
    if {"MeanNs_Classic", "MeanNs_Runtime"}.issubset(comparison.columns):
        comparison["Speedup"] = comparison["MeanNs_Classic"] / comparison["MeanNs_Runtime"]
        comparison["MeanDeltaPercent"] = (
            (comparison["MeanNs_Runtime"] - comparison["MeanNs_Classic"])
            / comparison["MeanNs_Classic"]
            * 100.0
        )
        comparison["CombinedErrorNs"] = comparison.get("ErrorNs_Classic", 0.0) + comparison.get(
            "ErrorNs_Runtime", 0.0
        )
        comparison["MeanDeltaNs"] = comparison["MeanNs_Runtime"] - comparison["MeanNs_Classic"]
        comparison["IsNoiseSensitive"] = (
            comparison["MeanDeltaNs"].abs() <= comparison["CombinedErrorNs"]
        )
        comparison["IsSubNanosecond"] = comparison[["MeanNs_Classic", "MeanNs_Runtime"]].min(axis=1) < 1.0
        comparison["Interpretation"] = comparison.apply(interpret_row, axis=1)

    if {"AllocatedBytes_Classic", "AllocatedBytes_Runtime"}.issubset(comparison.columns):
        comparison["AllocationDeltaBytes"] = (
            comparison["AllocatedBytes_Runtime"] - comparison["AllocatedBytes_Classic"]
        )


def interpret_row(row: pd.Series) -> str:
    if bool(row.get("IsSubNanosecond", False)):
        return "micro-noise"
    if bool(row.get("IsNoiseSensitive", False)):
        return "parity/noise"
    if row["Speedup"] > 1.05:
        return "runtime faster"
    if row["Speedup"] < 0.95:
        return "runtime slower"
    return "parity"


def parse_metrics(metrics_text: str) -> list[str]:
    metrics = [part.strip() for part in metrics_text.split(",") if part.strip()]
    unknown = sorted(set(metrics) - set(DEFAULT_METRICS))
    if unknown:
        raise ValueError(f"Unsupported metrics: {', '.join(unknown)}")

    return metrics


def metric_value_column(metric: str) -> str:
    return {
        "Mean": "MeanNs",
        "Median": "MedianNs",
        "Error": "ErrorNs",
        "StdDev": "StdDevNs",
        "Allocated": "AllocatedBytes",
        "Gen0": "Gen0",
    }[metric]


def metric_kind(metric: str) -> str:
    if metric in {"Mean", "Median", "Error", "StdDev"}:
        return "time"
    if metric == "Allocated":
        return "memory"
    return "raw"


def choose_display_scale(values: pd.Series, kind: str) -> tuple[float, str]:
    positive = values[values > 0]
    max_value = float(positive.max()) if not positive.empty else 0.0

    if kind == "time":
        if max_value >= 1_000_000.0:
            return 1_000_000.0, "ms"
        if max_value >= 1_000.0:
            return 1_000.0, "us"
        return 1.0, "ns"

    if kind == "memory":
        if max_value >= 1024.0:
            return 1024.0, "KiB"
        return 1.0, "B"

    return 1.0, ""


def available_metric(comparison: pd.DataFrame, metric: str) -> bool:
    base = metric_value_column(metric)
    return {f"{base}_Classic", f"{base}_Runtime"}.issubset(comparison.columns)


def numeric_param_columns(frame: pd.DataFrame) -> list[str]:
    columns = []
    metric_bases = {metric_value_column(metric) for metric in DEFAULT_METRICS}
    candidates = sorted(
        {
            column.removesuffix("_Classic").removesuffix("_Runtime")
            for column in frame.columns
            if column.endswith("_Classic") or column.endswith("_Runtime")
        }
    )

    for column in candidates:
        if (
            column in INFRA_COLUMNS
            or column in METRIC_COLUMNS
            or column in metric_bases
            or column
            in {
                "Key",
                "Benchmark",
                "Method",
                "Variant",
                "Params",
                "Speedup",
                "MeanDeltaPercent",
                "MeanDeltaNs",
                "CombinedErrorNs",
                "AllocationDeltaBytes",
                "IsNoiseSensitive",
                "IsSubNanosecond",
                "Interpretation",
            }
        ):
            continue
        classic = f"{column}_Classic"
        runtime = f"{column}_Runtime"
        source = classic if classic in frame.columns else runtime
        values = pd.to_numeric(frame[source], errors="coerce")
        if values.notna().all() and values.nunique() > 1:
            columns.append(column)

    return columns


def prepare_metric_frame(frame: pd.DataFrame, metric: str) -> pd.DataFrame:
    base = metric_value_column(metric)
    result = frame.copy()
    result["ClassicValue"] = result[f"{base}_Classic"]
    result["RuntimeValue"] = result[f"{base}_Runtime"]
    result["Delta"] = result["RuntimeValue"] - result["ClassicValue"]
    result["AbsDelta"] = result["Delta"].abs()
    result["PercentDelta"] = result.apply(percent_delta, axis=1)
    return result


def percent_delta(row: pd.Series) -> float:
    classic = float(row["ClassicValue"])
    if classic == 0.0:
        return math.nan
    return float(row["Delta"]) / classic * 100.0


def is_flat(frame: pd.DataFrame) -> bool:
    values = pd.concat([frame["ClassicValue"], frame["RuntimeValue"]], ignore_index=True)
    if values.empty:
        return True

    spread = float(values.max() - values.min())
    max_abs = float(values.abs().max())
    return spread <= max(1e-12, max_abs * 0.01)


def has_regression(frame: pd.DataFrame, metric: str) -> bool:
    if metric in {"Mean", "Median", "Error", "StdDev", "Allocated", "Gen0"}:
        return bool((frame["RuntimeValue"] > frame["ClassicValue"]).any())

    return False


def plot_metric_for_benchmark(
    benchmark: str,
    frame: pd.DataFrame,
    metric: str,
    out_dir: Path,
) -> tuple[Path, dict[str, object]]:
    metric_frame = prepare_metric_frame(frame, metric)
    params = numeric_param_columns(frame)
    output = out_dir / "benchmarks" / benchmark / f"{metric}.png"
    output.parent.mkdir(parents=True, exist_ok=True)

    if params:
        chart_type = "line"
        plot_metric_lines(benchmark, metric_frame, metric, params[0], output)
    else:
        chart_type = "grouped-bars"
        plot_metric_bars(benchmark, metric_frame, metric, output)

    manifest = metric_manifest_row(benchmark, metric, output, chart_type, metric_frame)
    return output, manifest


def plot_metric_lines(
    benchmark: str,
    frame: pd.DataFrame,
    metric: str,
    param: str,
    output: Path,
) -> None:
    kind = metric_kind(metric)
    scale, unit = choose_display_scale(
        pd.concat([frame["ClassicValue"], frame["RuntimeValue"]], ignore_index=True),
        kind,
    )
    methods = sorted(frame["Method"].unique())
    columns = min(3, len(methods))
    rows = math.ceil(len(methods) / columns)
    fig, axes = plt.subplots(
        rows,
        columns,
        figsize=(13.33, max(4.8, rows * 3.0)),
        dpi=180,
        squeeze=False,
    )

    for index, method in enumerate(methods):
        ax = axes[index // columns][index % columns]
        method_frame = frame[frame["Method"] == method].copy()
        x_source = f"{param}_Classic"
        method_frame["X"] = pd.to_numeric(method_frame[x_source], errors="coerce")
        method_frame = method_frame.sort_values("X")

        ax.plot(
            method_frame["X"],
            method_frame["ClassicValue"] / scale,
            marker="o",
            color=CLASSIC_COLOR,
            linewidth=2,
            label="classic",
        )
        ax.plot(
            method_frame["X"],
            method_frame["RuntimeValue"] / scale,
            marker="o",
            color=RUNTIME_COLOR,
            linewidth=2,
            label="runtime",
        )
        ax.set_title(method, loc="left", fontsize=10, pad=6)
        ax.set_xlabel(param)
        ax.set_ylabel(metric_axis_label(metric, unit))
        ax.grid(color=GRID_COLOR, linewidth=0.8, alpha=0.8)
        ax.set_axisbelow(True)
        ax.legend(loc="best", fontsize=8, frameon=False)

    for index in range(len(methods), rows * columns):
        axes[index // columns][index % columns].axis("off")

    fig.suptitle(f"{short_benchmark(benchmark)}: {metric}", x=0.02, ha="left", fontsize=16)
    fig.tight_layout(rect=(0, 0, 1, 0.96))
    fig.savefig(output, bbox_inches="tight")
    plt.close(fig)


def plot_metric_bars(benchmark: str, frame: pd.DataFrame, metric: str, output: Path) -> None:
    kind = metric_kind(metric)
    scale, unit = choose_display_scale(
        pd.concat([frame["ClassicValue"], frame["RuntimeValue"]], ignore_index=True),
        kind,
    )
    plot_frame = frame.sort_values("Method").reset_index(drop=True)
    positions = list(range(len(plot_frame)))
    width = 0.36
    height = max(5.6, 0.45 * len(plot_frame) + 3.0)

    _, ax = plt.subplots(figsize=(13.33, height), dpi=180)
    ax.bar(
        [pos - width / 2 for pos in positions],
        plot_frame["ClassicValue"] / scale,
        width=width,
        color=CLASSIC_COLOR,
        label="classic",
    )
    ax.bar(
        [pos + width / 2 for pos in positions],
        plot_frame["RuntimeValue"] / scale,
        width=width,
        color=RUNTIME_COLOR,
        label="runtime",
    )
    ax.set_xticks(positions)
    ax.set_xticklabels(plot_frame["Method"], rotation=30, ha="right")
    ax.set_ylabel(metric_axis_label(metric, unit))
    ax.set_title(f"{short_benchmark(benchmark)}: {metric}", loc="left", pad=14)
    ax.grid(axis="y", color=GRID_COLOR, linewidth=0.8, alpha=0.8)
    ax.set_axisbelow(True)
    ax.legend(loc="best", frameon=False)
    plt.tight_layout()
    output.parent.mkdir(parents=True, exist_ok=True)
    plt.savefig(output, bbox_inches="tight")
    plt.close()


def metric_axis_label(metric: str, unit: str) -> str:
    suffix = f", {unit}" if unit else ""
    if metric in {"Mean", "Median", "Error", "StdDev"}:
        return f"{metric} time per operation{suffix}; lower is better"
    if metric == "Allocated":
        return f"Allocated per operation{suffix}; lower is better"
    return f"{metric}{suffix}; lower is better"


def metric_manifest_row(
    benchmark: str,
    metric: str,
    output: Path,
    chart_type: str,
    frame: pd.DataFrame,
) -> dict[str, object]:
    percent_delta = frame["PercentDelta"].abs().dropna()
    row: dict[str, object] = {
        "benchmark": benchmark,
        "metric": metric,
        "artifact_type": "raw",
        "chart_type": chart_type,
        "path": str(output),
        "rows": len(frame),
        "is_flat": is_flat(frame),
        "has_delta": bool((frame["AbsDelta"] > 0).any()),
        "has_regression": has_regression(frame, metric),
        "max_abs_delta": float(frame["AbsDelta"].max()) if len(frame) else 0.0,
        "max_percent_delta": float(percent_delta.max()) if not percent_delta.empty else math.nan,
        "allocation_changed": bool(
            "AllocationDeltaBytes" in frame.columns and (frame["AllocationDeltaBytes"] != 0).any()
        ),
        "noise_sensitive": bool(
            "IsNoiseSensitive" in frame.columns and frame["IsNoiseSensitive"].fillna(False).any()
        ),
        "min_speedup": float(frame["Speedup"].min()) if "Speedup" in frame.columns else math.nan,
        "max_speedup": float(frame["Speedup"].max()) if "Speedup" in frame.columns else math.nan,
    }
    return row


def plot_derived_for_benchmark(
    benchmark: str,
    frame: pd.DataFrame,
    out_dir: Path,
) -> list[dict[str, object]]:
    rows: list[dict[str, object]] = []
    if {"Speedup", "MeanDeltaPercent"}.issubset(frame.columns):
        output = out_dir / "benchmarks" / benchmark / "Speedup-derived.png"
        output.parent.mkdir(parents=True, exist_ok=True)
        params = numeric_param_columns(frame)
        if params:
            chart_type = "line"
            plot_derived_lines(benchmark, frame, "Speedup", params[0], output)
        else:
            chart_type = "grouped-bars"
            plot_derived_bars(benchmark, frame, "Speedup", output)
        rows.append(derived_manifest_row(benchmark, "Speedup", output, chart_type, frame))

    if "AllocationDeltaBytes" in frame.columns:
        output = out_dir / "benchmarks" / benchmark / "AllocationDelta-derived.png"
        output.parent.mkdir(parents=True, exist_ok=True)
        params = numeric_param_columns(frame)
        if params:
            chart_type = "line"
            plot_derived_lines(benchmark, frame, "AllocationDeltaBytes", params[0], output)
        else:
            chart_type = "grouped-bars"
            plot_derived_bars(benchmark, frame, "AllocationDeltaBytes", output)
        rows.append(derived_manifest_row(benchmark, "AllocationDeltaBytes", output, chart_type, frame))

    return rows


def plot_derived_lines(
    benchmark: str,
    frame: pd.DataFrame,
    metric: str,
    param: str,
    output: Path,
) -> None:
    methods = sorted(frame["Method"].unique())
    columns = min(3, len(methods))
    rows = math.ceil(len(methods) / columns)
    fig, axes = plt.subplots(
        rows,
        columns,
        figsize=(13.33, max(4.8, rows * 3.0)),
        dpi=180,
        squeeze=False,
    )

    for index, method in enumerate(methods):
        ax = axes[index // columns][index % columns]
        method_frame = frame[frame["Method"] == method].copy()
        method_frame["X"] = pd.to_numeric(method_frame[f"{param}_Classic"], errors="coerce")
        method_frame = method_frame.sort_values("X")
        y = method_frame[metric]
        ax.plot(method_frame["X"], y, marker="o", color=DELTA_COLOR, linewidth=2)
        if metric == "Speedup":
            ax.axhline(1.0, color=TEXT_COLOR, linewidth=1)
            ax.axhspan(0.95, 1.05, color="#d8d2b0", alpha=0.22, linewidth=0)
        else:
            ax.axhline(0.0, color=TEXT_COLOR, linewidth=1)
        ax.set_title(method, loc="left", fontsize=10, pad=6)
        ax.set_xlabel(param)
        ax.set_ylabel(derived_axis_label(metric))
        ax.grid(color=GRID_COLOR, linewidth=0.8, alpha=0.8)
        ax.set_axisbelow(True)

    for index in range(len(methods), rows * columns):
        axes[index // columns][index % columns].axis("off")

    fig.suptitle(f"{short_benchmark(benchmark)}: {metric}", x=0.02, ha="left", fontsize=16)
    fig.tight_layout(rect=(0, 0, 1, 0.96))
    fig.savefig(output, bbox_inches="tight")
    plt.close(fig)


def plot_derived_bars(benchmark: str, frame: pd.DataFrame, metric: str, output: Path) -> None:
    plot_frame = frame.sort_values("Method").reset_index(drop=True)
    positions = list(range(len(plot_frame)))
    colors = [RUNTIME_COLOR if value >= 1.0 else SLOWER_COLOR for value in plot_frame[metric]]
    if metric != "Speedup":
        colors = [SLOWER_COLOR if value > 0 else RUNTIME_COLOR for value in plot_frame[metric]]

    _, ax = plt.subplots(figsize=(13.33, max(5.6, 0.45 * len(plot_frame) + 3.0)), dpi=180)
    ax.bar(positions, plot_frame[metric], color=colors)
    ax.set_xticks(positions)
    ax.set_xticklabels(plot_frame["Method"], rotation=30, ha="right")
    if metric == "Speedup":
        ax.axhline(1.0, color=TEXT_COLOR, linewidth=1)
        ax.axhspan(0.95, 1.05, color="#d8d2b0", alpha=0.22, linewidth=0)
    else:
        ax.axhline(0.0, color=TEXT_COLOR, linewidth=1)
    ax.set_ylabel(derived_axis_label(metric))
    ax.set_title(f"{short_benchmark(benchmark)}: {metric}", loc="left", pad=14)
    ax.grid(axis="y", color=GRID_COLOR, linewidth=0.8, alpha=0.8)
    ax.set_axisbelow(True)
    plt.tight_layout()
    output.parent.mkdir(parents=True, exist_ok=True)
    plt.savefig(output, bbox_inches="tight")
    plt.close()


def derived_axis_label(metric: str) -> str:
    if metric == "Speedup":
        return "Classic / runtime mean; >1 means runtime faster"
    if metric == "AllocationDeltaBytes":
        return "Runtime - classic allocated bytes"
    return metric


def derived_manifest_row(
    benchmark: str,
    metric: str,
    output: Path,
    chart_type: str,
    frame: pd.DataFrame,
) -> dict[str, object]:
    values = frame[metric].dropna()
    row = {
        "benchmark": benchmark,
        "metric": metric,
        "artifact_type": "derived",
        "chart_type": chart_type,
        "path": str(output),
        "rows": len(frame),
        "is_flat": bool(values.max() - values.min() <= max(1e-12, values.abs().max() * 0.01))
        if not values.empty
        else True,
        "has_delta": bool((values != 0).any()) if metric != "Speedup" else bool((values != 1).any()),
        "has_regression": bool((values < 0.95).any()) if metric == "Speedup" else bool((values > 0).any()),
        "max_abs_delta": float(values.abs().max()) if not values.empty else 0.0,
        "max_percent_delta": math.nan,
        "allocation_changed": bool(
            "AllocationDeltaBytes" in frame.columns and (frame["AllocationDeltaBytes"] != 0).any()
        ),
        "noise_sensitive": bool(
            "IsNoiseSensitive" in frame.columns and frame["IsNoiseSensitive"].fillna(False).any()
        ),
        "min_speedup": float(frame["Speedup"].min()) if "Speedup" in frame.columns else math.nan,
        "max_speedup": float(frame["Speedup"].max()) if "Speedup" in frame.columns else math.nan,
    }
    return row


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
    bars = ax.bar(frame["Label"], frame["SizeKiB"], color=[CLASSIC_COLOR, RUNTIME_COLOR][: len(frame)])
    ax.set_ylabel("DLL size, KiB")
    ax.set_title("Parktronik.Api.dll size", loc="left", pad=16, fontsize=18)
    ax.grid(axis="y", color=GRID_COLOR, linewidth=0.8, alpha=0.8)
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
            color=TEXT_COLOR,
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
            color=RUNTIME_COLOR if delta_percent < 0 else SLOWER_COLOR,
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


def generate_benchmark_artifacts(
    comparison: pd.DataFrame,
    metrics: list[str],
    include_derived: bool,
    out_dir: Path,
) -> pd.DataFrame:
    manifest_rows: list[dict[str, object]] = []
    for benchmark, frame in comparison.groupby("Benchmark", sort=True):
        for metric in metrics:
            if not available_metric(frame, metric):
                continue

            _, row = plot_metric_for_benchmark(benchmark, frame, metric, out_dir)
            manifest_rows.append(row)

        if include_derived:
            manifest_rows.extend(plot_derived_for_benchmark(benchmark, frame, out_dir))

    return pd.DataFrame(manifest_rows)


def main() -> None:
    parser = argparse.ArgumentParser(
        description="Generate visualization artifacts from existing BenchmarkDotNet Standard/AsyncRuntime CSV reports."
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
        "--out-dir",
        type=Path,
        default=Path("artifacts/async-comparison"),
        help="Directory for normalized CSV and PNG outputs.",
    )
    parser.add_argument(
        "--metrics",
        default=",".join(DEFAULT_METRICS),
        help="Comma-separated metrics to plot. Supported: Mean,Median,Error,StdDev,Allocated,Gen0.",
    )
    parser.add_argument(
        "--include-derived",
        action="store_true",
        help="Also generate derived Speedup and allocation-delta charts.",
    )
    parser.add_argument(
        "--size-csv",
        type=Path,
        default=None,
        help="Optional SizeBench CSV with classic/runtime rows.",
    )
    args = parser.parse_args()

    args.out_dir.mkdir(parents=True, exist_ok=True)
    metrics = parse_metrics(args.metrics)

    comparison = load_comparison(args.standard_dir, args.runtime_dir)
    comparison_csv = args.out_dir / "benchmark-comparison.csv"
    comparison.to_csv(comparison_csv, index=False)
    print(f"Wrote {comparison_csv}")

    manifest = generate_benchmark_artifacts(
        comparison,
        metrics=metrics,
        include_derived=args.include_derived,
        out_dir=args.out_dir,
    )
    manifest_csv = args.out_dir / "manifest.csv"
    manifest.to_csv(manifest_csv, index=False)
    print(f"Wrote {manifest_csv}")

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
