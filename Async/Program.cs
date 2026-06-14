using System.Diagnostics;

using Async;

#pragma warning disable CS8321 // Local function is declared but never used

await Alloc.Start();
return;

int r = await Outer();
Console.WriteLine(r);
return;

async Task<int> Outer()
{
    return await Caller();
}

async Task<int> Caller()
{
    int x = await Callee();
    int y = await Callee();

    return x + y;
}

async Task<int> Callee()
{
    Console.WriteLine(new StackTrace(true));
    await Task.Yield();
    return 21;
}