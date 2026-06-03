using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Loader;
using System.Text;

var options = ParseArgs(args);

if (options is null)
{
    PrintUsage();
    return;
}

var rows = options.Targets.Select(AnalyzeTarget).ToArray();

foreach (var row in rows)
{
    Console.WriteLine($"Label: {row.Label}");
    Console.WriteLine($"Assembly: {row.Assembly}");
    Console.WriteLine($"File size: {row.FileSizeBytes.ToString("N0", CultureInfo.InvariantCulture)} bytes");
    Console.WriteLine($"Types: {row.Types.ToString("N0", CultureInfo.InvariantCulture)}");
    Console.WriteLine($"Methods: {row.Methods.ToString("N0", CultureInfo.InvariantCulture)}");
    Console.WriteLine($"IAsyncStateMachine types: {row.AsyncStateMachineTypes.ToString("N0", CultureInfo.InvariantCulture)}");
    Console.WriteLine($"AsyncStateMachineAttribute methods: {row.AsyncStateMachineAttributeMethods.ToString("N0", CultureInfo.InvariantCulture)}");
    Console.WriteLine($"MethodImpl.Async methods: {row.MethodImplAsyncMethods.ToString("N0", CultureInfo.InvariantCulture)}");
    Console.WriteLine();
}

if (options.CsvPath is not null)
{
    var csvPath = Path.GetFullPath(options.CsvPath);
    var csvDirectory = Path.GetDirectoryName(csvPath);
    if (!string.IsNullOrEmpty(csvDirectory))
        Directory.CreateDirectory(csvDirectory);

    File.WriteAllText(csvPath, ToCsv(rows));
    Console.WriteLine($"CSV: {csvPath}");
}

static Options? ParseArgs(string[] args)
{
    if (args.Length == 0 || args.Any(a => a is "-h" or "--help"))
        return null;

    var targets = new List<Target>();
    string? csvPath = null;

    for (var i = 0; i < args.Length; i++)
    {
        var arg = args[i];
        if (arg == "--csv")
        {
            if (i + 1 >= args.Length)
                throw new ArgumentException("--csv requires an output path.");

            csvPath = args[++i];
            continue;
        }

        var separator = arg.IndexOf('=');
        if (separator > 0)
        {
            var label = arg[..separator];
            var path = arg[(separator + 1)..];
            targets.Add(new Target(label, Path.GetFullPath(path)));
        }
        else
        {
            var path = Path.GetFullPath(arg);
            targets.Add(new Target(Path.GetFileNameWithoutExtension(path), path));
        }
    }

    if (targets.Count == 0)
        return null;

    return new Options(targets, csvPath);
}

static SizeRow AnalyzeTarget(Target target)
{
    if (!File.Exists(target.AssemblyPath))
        throw new FileNotFoundException("Target assembly was not found.", target.AssemblyPath);

    var loadContext = new TargetAssemblyLoadContext(target.AssemblyPath);

    try
    {
        var assembly = loadContext.LoadFromAssemblyPath(target.AssemblyPath);
        var types = GetLoadableTypes(assembly);
        var methods = types
            .SelectMany(GetDeclaredMethods)
            .ToArray();

        var asyncStateMachineTypes = types.Count(IsAsyncStateMachineType);
        var asyncStateMachineAttributeMethods = methods.Count(HasAsyncStateMachineAttribute);
        var methodImplAsyncMethods = methods.Count(m =>
            m.GetMethodImplementationFlags().HasFlag(MethodImplAttributes.Async));
        var fileSize = new FileInfo(target.AssemblyPath).Length;

        return new SizeRow(
            target.Label,
            Path.GetFileName(target.AssemblyPath),
            target.AssemblyPath,
            fileSize,
            types.Length,
            methods.Length,
            asyncStateMachineTypes,
            asyncStateMachineAttributeMethods,
            methodImplAsyncMethods);
    }
    finally
    {
        loadContext.Unload();
    }
}

static Type[] GetLoadableTypes(Assembly assembly)
{
    try
    {
        return assembly.GetTypes();
    }
    catch (ReflectionTypeLoadException ex)
    {
        return ex.Types.OfType<Type>().ToArray();
    }
}

static MethodInfo[] GetDeclaredMethods(Type type)
{
    try
    {
        return type.GetMethods(
            BindingFlags.Public |
            BindingFlags.NonPublic |
            BindingFlags.Static |
            BindingFlags.Instance |
            BindingFlags.DeclaredOnly);
    }
    catch
    {
        return [];
    }
}

static bool IsAsyncStateMachineType(Type type)
{
    return type.GetInterfaces()
        .Any(i => i.FullName == typeof(IAsyncStateMachine).FullName);
}

static bool HasAsyncStateMachineAttribute(MethodInfo method)
{
    return method.CustomAttributes.Any(a =>
        a.AttributeType.FullName == typeof(AsyncStateMachineAttribute).FullName);
}

static string ToCsv(IEnumerable<SizeRow> rows)
{
    var builder = new StringBuilder();
    builder.AppendLine(
        "Label,Assembly,AssemblyPath,FileSizeBytes,Types,Methods,AsyncStateMachineTypes,AsyncStateMachineAttributeMethods,MethodImplAsyncMethods");

    foreach (var row in rows)
    {
        builder
            .Append(Csv(row.Label)).Append(',')
            .Append(Csv(row.Assembly)).Append(',')
            .Append(Csv(row.AssemblyPath)).Append(',')
            .Append(row.FileSizeBytes.ToString(CultureInfo.InvariantCulture)).Append(',')
            .Append(row.Types.ToString(CultureInfo.InvariantCulture)).Append(',')
            .Append(row.Methods.ToString(CultureInfo.InvariantCulture)).Append(',')
            .Append(row.AsyncStateMachineTypes.ToString(CultureInfo.InvariantCulture)).Append(',')
            .Append(row.AsyncStateMachineAttributeMethods.ToString(CultureInfo.InvariantCulture)).Append(',')
            .Append(row.MethodImplAsyncMethods.ToString(CultureInfo.InvariantCulture))
            .AppendLine();
    }

    return builder.ToString();
}

static string Csv(string value)
{
    if (!value.Contains(',') && !value.Contains('"') && !value.Contains('\n') && !value.Contains('\r'))
        return value;

    return $"\"{value.Replace("\"", "\"\"")}\"";
}

static void PrintUsage()
{
    Console.WriteLine("Usage:");
    Console.WriteLine("  dotnet run --project SizeBench/SizeBench.csproj -c Standard -- [--csv <path>] <path-to-dll>");
    Console.WriteLine("  dotnet run --project SizeBench/SizeBench.csproj -c Standard -- [--csv <path>] classic=<path-to-dll> runtime=<path-to-dll>");
}

internal sealed record Options(IReadOnlyList<Target> Targets, string? CsvPath);

internal sealed record Target(string Label, string AssemblyPath);

internal sealed record SizeRow(
    string Label,
    string Assembly,
    string AssemblyPath,
    long FileSizeBytes,
    int Types,
    int Methods,
    int AsyncStateMachineTypes,
    int AsyncStateMachineAttributeMethods,
    int MethodImplAsyncMethods);

internal sealed class TargetAssemblyLoadContext(string mainAssemblyPath) : AssemblyLoadContext(isCollectible: true)
{
    private readonly AssemblyDependencyResolver _resolver = new(mainAssemblyPath);

    protected override Assembly? Load(AssemblyName assemblyName)
    {
        var assemblyPath = _resolver.ResolveAssemblyToPath(assemblyName);
        return assemblyPath is null ? null : LoadFromAssemblyPath(assemblyPath);
    }
}
