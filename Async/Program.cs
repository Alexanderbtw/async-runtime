using System.Diagnostics;

#pragma warning disable CS8321 // Local function is declared but never used

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