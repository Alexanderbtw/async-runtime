using Manual.Additional;
using Manual.Patterns;

Console.WriteLine($"Runtime: {Environment.Version}");
Console.WriteLine();

await ApmDemo.RunAsync();
Console.WriteLine();

await EbaDemo.RunAsync();
Console.WriteLine();

await StateMachineDemo.RunAsync();
Console.WriteLine();

await CustomAwaiterDemo.RunAsync();
Console.WriteLine();

await MyTaskDemo.RunAsync();
Console.WriteLine();

await TcsDemo.RunAsync();
Console.WriteLine();

await ValueTaskDemo.RunAsync();
Console.WriteLine();

await SyncContextDemo.RunAsync();
