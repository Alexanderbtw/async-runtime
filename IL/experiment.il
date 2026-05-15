// IL code: Experiment
.class private auto ansi '<Module>'
{
} // end of class <Module>

.class private auto ansi beforefieldinit Program
	extends [System.Runtime]System.Object
{
	.custom instance void [System.Runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
		01 00 00 00
	)
	// Methods
	.method private hidebysig static 
		class [System.Runtime]System.Threading.Tasks.Task '<Main>$' (
			string[] args
		) cil managed flags(2000) 
	{
		// Method begins at RVA 0x2050
		// Header size: 12
		// Code size: 62 (0x3e)
		.maxstack 3
		.locals init (
			[0] valuetype [System.Runtime]System.Runtime.CompilerServices.DefaultInterpolatedStringHandler
		)

		IL_0000: ldloca.s 0
		IL_0002: ldc.i4.s 9
		IL_0004: ldc.i4.1
		IL_0005: call instance void [System.Runtime]System.Runtime.CompilerServices.DefaultInterpolatedStringHandler::.ctor(int32, int32)
		IL_000a: ldloca.s 0
		IL_000c: ldstr "Runtime: "
		IL_0011: call instance void [System.Runtime]System.Runtime.CompilerServices.DefaultInterpolatedStringHandler::AppendLiteral(string)
		IL_0016: ldloca.s 0
		IL_0018: call class [System.Runtime]System.Version [System.Runtime]System.Environment::get_Version()
		IL_001d: call instance void [System.Runtime]System.Runtime.CompilerServices.DefaultInterpolatedStringHandler::AppendFormatted<class [System.Runtime]System.Version>(!!0)
		IL_0022: ldloca.s 0
		IL_0024: call instance string [System.Runtime]System.Runtime.CompilerServices.DefaultInterpolatedStringHandler::ToStringAndClear()
		IL_0029: call void [System.Console]System.Console::WriteLine(string)
		IL_002e: call void [System.Console]System.Console::WriteLine()
		IL_0033: call class [System.Runtime]System.Threading.Tasks.Task Experiment::RunAsync()
		IL_0038: call void [System.Runtime]System.Runtime.CompilerServices.AsyncHelpers::Await(class [System.Runtime]System.Threading.Tasks.Task)
		IL_003d: ret
	} // end of method Program::'<Main>$'

	.method public hidebysig specialname rtspecialname 
		instance void .ctor () cil managed 
	{
		// Method begins at RVA 0x209a
		// Header size: 1
		// Code size: 7 (0x7)
		.maxstack 8

		IL_0000: ldarg.0
		IL_0001: call instance void [System.Runtime]System.Object::.ctor()
		IL_0006: ret
	} // end of method Program::.ctor

	.method private hidebysig specialname static 
		void '<Main>' (
			string[] args
		) cil managed 
	{
		// Method begins at RVA 0x20a2
		// Header size: 1
		// Code size: 12 (0xc)
		.maxstack 8
		.entrypoint

		IL_0000: ldarg.0
		IL_0001: call class [System.Runtime]System.Threading.Tasks.Task Program::'<Main>$'(string[])
		IL_0006: call void [System.Runtime]System.Runtime.CompilerServices.AsyncHelpers::HandleAsyncEntryPoint(class [System.Runtime]System.Threading.Tasks.Task)
		IL_000b: ret
	} // end of method Program::'<Main>'

} // end of class Program

.class private auto ansi abstract sealed beforefieldinit Experiment
	extends [System.Runtime]System.Object
{
	.custom instance void [System.Runtime]System.Runtime.CompilerServices.NullableContextAttribute::.ctor(uint8) = (
		01 00 01 00 00
	)
	.custom instance void [System.Runtime]System.Runtime.CompilerServices.NullableAttribute::.ctor(uint8) = (
		01 00 00 00 00
	)
	// Methods
	.method public hidebysig static 
		class [System.Runtime]System.Threading.Tasks.Task RunAsync () cil managed flags(2000) 
	{
		// Method begins at RVA 0x20b0
		// Header size: 12
		// Code size: 90 (0x5a)
		.maxstack 1

		IL_0000: ldstr "=== Sync-completing async ==="
		IL_0005: call void [System.Console]System.Console::WriteLine(string)
		IL_000a: call class [System.Runtime]System.Threading.Tasks.Task`1<int32> Experiment::SyncCompletingAsync()
		IL_000f: call !!0 [System.Runtime]System.Runtime.CompilerServices.AsyncHelpers::Await<int32>(class [System.Runtime]System.Threading.Tasks.Task`1<!!0>)
		IL_0014: call void [System.Console]System.Console::WriteLine(int32)
		IL_0019: call void [System.Console]System.Console::WriteLine()
		IL_001e: ldstr "=== Real suspension ==="
		IL_0023: call void [System.Console]System.Console::WriteLine(string)
		IL_0028: call class [System.Runtime]System.Threading.Tasks.Task`1<int32> Experiment::RealSuspensionAsync()
		IL_002d: call !!0 [System.Runtime]System.Runtime.CompilerServices.AsyncHelpers::Await<int32>(class [System.Runtime]System.Threading.Tasks.Task`1<!!0>)
		IL_0032: call void [System.Console]System.Console::WriteLine(int32)
		IL_0037: call void [System.Console]System.Console::WriteLine()
		IL_003c: ldstr "=== Stack trace ==="
		IL_0041: call void [System.Console]System.Console::WriteLine(string)
		.try
		{
			IL_0046: call class [System.Runtime]System.Threading.Tasks.Task Experiment::AAsync()
			IL_004b: call void [System.Runtime]System.Runtime.CompilerServices.AsyncHelpers::Await(class [System.Runtime]System.Threading.Tasks.Task)
			IL_0050: leave.s IL_0059
		} // end .try
		catch [System.Runtime]System.Exception
		{
			IL_0052: call void [System.Console]System.Console::WriteLine(object)
			IL_0057: leave.s IL_0059
		} // end handler

		IL_0059: ret
	} // end of method Experiment::RunAsync

	.method private hidebysig static 
		class [System.Runtime]System.Threading.Tasks.Task`1<int32> SyncCompletingAsync () cil managed flags(2000) 
	{
		// Method begins at RVA 0x2128
		// Header size: 1
		// Code size: 3 (0x3)
		.maxstack 8

		IL_0000: ldc.i4.s 42
		IL_0002: ret
	} // end of method Experiment::SyncCompletingAsync

	.method private hidebysig static 
		class [System.Runtime]System.Threading.Tasks.Task`1<int32> RealSuspensionAsync () cil managed flags(2000) 
	{
		// Method begins at RVA 0x212c
		// Header size: 12
		// Code size: 39 (0x27)
		.maxstack 1
		.locals init (
			[0] valuetype [System.Runtime]System.Runtime.CompilerServices.YieldAwaitable/YieldAwaiter,
			[1] valuetype [System.Runtime]System.Runtime.CompilerServices.YieldAwaitable
		)

		IL_0000: call valuetype [System.Runtime]System.Runtime.CompilerServices.YieldAwaitable [System.Runtime]System.Threading.Tasks.Task::Yield()
		IL_0005: stloc.1
		IL_0006: ldloca.s 1
		IL_0008: call instance valuetype [System.Runtime]System.Runtime.CompilerServices.YieldAwaitable/YieldAwaiter [System.Runtime]System.Runtime.CompilerServices.YieldAwaitable::GetAwaiter()
		IL_000d: stloc.0
		IL_000e: ldloca.s 0
		IL_0010: call instance bool [System.Runtime]System.Runtime.CompilerServices.YieldAwaitable/YieldAwaiter::get_IsCompleted()
		IL_0015: brtrue.s IL_001d

		IL_0017: ldloc.0
		IL_0018: call void [System.Runtime]System.Runtime.CompilerServices.AsyncHelpers::UnsafeAwaitAwaiter<valuetype [System.Runtime]System.Runtime.CompilerServices.YieldAwaitable/YieldAwaiter>(!!0)

		IL_001d: ldloca.s 0
		IL_001f: call instance void [System.Runtime]System.Runtime.CompilerServices.YieldAwaitable/YieldAwaiter::GetResult()
		IL_0024: ldc.i4.s 42
		IL_0026: ret
	} // end of method Experiment::RealSuspensionAsync

	.method private hidebysig static 
		class [System.Runtime]System.Threading.Tasks.Task AAsync () cil managed flags(2000) 
	{
		// Method begins at RVA 0x215f
		// Header size: 1
		// Code size: 11 (0xb)
		.maxstack 8

		IL_0000: call class [System.Runtime]System.Threading.Tasks.Task Experiment::BAsync()
		IL_0005: call void [System.Runtime]System.Runtime.CompilerServices.AsyncHelpers::Await(class [System.Runtime]System.Threading.Tasks.Task)
		IL_000a: ret
	} // end of method Experiment::AAsync

	.method private hidebysig static 
		class [System.Runtime]System.Threading.Tasks.Task BAsync () cil managed flags(2000) 
	{
		// Method begins at RVA 0x216b
		// Header size: 1
		// Code size: 11 (0xb)
		.maxstack 8

		IL_0000: call class [System.Runtime]System.Threading.Tasks.Task Experiment::CAsync()
		IL_0005: call void [System.Runtime]System.Runtime.CompilerServices.AsyncHelpers::Await(class [System.Runtime]System.Threading.Tasks.Task)
		IL_000a: ret
	} // end of method Experiment::BAsync

	.method private hidebysig static 
		class [System.Runtime]System.Threading.Tasks.Task CAsync () cil managed flags(2000) 
	{
		// Method begins at RVA 0x2177
		// Header size: 1
		// Code size: 23 (0x17)
		.maxstack 8

		IL_0000: ldc.i4.s 10
		IL_0002: call class [System.Runtime]System.Threading.Tasks.Task [System.Runtime]System.Threading.Tasks.Task::Delay(int32)
		IL_0007: call void [System.Runtime]System.Runtime.CompilerServices.AsyncHelpers::Await(class [System.Runtime]System.Threading.Tasks.Task)
		IL_000c: ldstr "Boom from async method"
		IL_0011: newobj instance void [System.Runtime]System.InvalidOperationException::.ctor(string)
		IL_0016: throw
	} // end of method Experiment::CAsync

} // end of class Experiment

