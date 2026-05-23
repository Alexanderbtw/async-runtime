#pragma warning disable CS8321 // Local function is declared but never used

await Outer();
return;

async Task<int> Outer()
{
    return await Caller();
}

async Task<int> Caller()
{
    var x = await Callee();
    var y = await Callee();

    return x + y;
}

async Task<int> Callee()
{
    switch (Random.Shared.Next(1))
    {
        case 0:
            Console.WriteLine("Callee: async success");
            await Task.Yield();
            return 21;

        case 1:
            Console.WriteLine("Callee: sync success");
            return 21;

        default:
            Console.WriteLine("Callee: error");
            throw new InvalidOperationException("Callee failed.");
    }
}
