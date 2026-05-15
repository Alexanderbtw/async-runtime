// IL code: Standart
.class private auto ansi '<Module>'
{
} // end of class <Module>

.class private auto ansi beforefieldinit Program
	extends [System.Runtime]System.Object
{
	.custom instance void [System.Runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
		01 00 00 00
	)
	// Nested Types
	.class nested private auto ansi sealed beforefieldinit '<<Main>$>d__0'
		extends [System.Runtime]System.ValueType
		implements [System.Runtime]System.Runtime.CompilerServices.IAsyncStateMachine
	{
		.custom instance void [System.Runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
			01 00 00 00
		)
		// Fields
		.field public int32 '<>1__state'
		.field public valuetype [System.Runtime]System.Runtime.CompilerServices.AsyncTaskMethodBuilder '<>t__builder'
		.field private valuetype [System.Runtime]System.Runtime.CompilerServices.TaskAwaiter '<>u__1'

		// Methods
		.method private final hidebysig newslot virtual 
			instance void MoveNext () cil managed 
		{
			.override method instance void [System.Runtime]System.Runtime.CompilerServices.IAsyncStateMachine::MoveNext()
			// Method begins at RVA 0x2208
			// Header size: 12
			// Code size: 192 (0xc0)
			.maxstack 3
			.locals init (
				[0] int32,
				[1] valuetype [System.Runtime]System.Runtime.CompilerServices.DefaultInterpolatedStringHandler,
				[2] valuetype [System.Runtime]System.Runtime.CompilerServices.TaskAwaiter,
				[3] class [System.Runtime]System.Exception
			)

			IL_0000: ldarg.0
			IL_0001: ldfld int32 Program/'<<Main>$>d__0'::'<>1__state'
			IL_0006: stloc.0
			.try
			{
				IL_0007: ldloc.0
				IL_0008: brfalse.s IL_0070

				IL_000a: ldc.i4.s 9
				IL_000c: ldc.i4.1
				IL_000d: newobj instance void [System.Runtime]System.Runtime.CompilerServices.DefaultInterpolatedStringHandler::.ctor(int32, int32)
				IL_0012: stloc.1
				IL_0013: ldloca.s 1
				IL_0015: ldstr "Runtime: "
				IL_001a: call instance void [System.Runtime]System.Runtime.CompilerServices.DefaultInterpolatedStringHandler::AppendLiteral(string)
				IL_001f: ldloca.s 1
				IL_0021: call class [System.Runtime]System.Version [System.Runtime]System.Environment::get_Version()
				IL_0026: call instance void [System.Runtime]System.Runtime.CompilerServices.DefaultInterpolatedStringHandler::AppendFormatted<class [System.Runtime]System.Version>(!!0)
				IL_002b: ldloca.s 1
				IL_002d: call instance string [System.Runtime]System.Runtime.CompilerServices.DefaultInterpolatedStringHandler::ToStringAndClear()
				IL_0032: call void [System.Console]System.Console::WriteLine(string)
				IL_0037: call void [System.Console]System.Console::WriteLine()
				IL_003c: call class [System.Runtime]System.Threading.Tasks.Task Experiment::RunAsync()
				IL_0041: callvirt instance valuetype [System.Runtime]System.Runtime.CompilerServices.TaskAwaiter [System.Runtime]System.Threading.Tasks.Task::GetAwaiter()
				IL_0046: stloc.2
				IL_0047: ldloca.s 2
				IL_0049: call instance bool [System.Runtime]System.Runtime.CompilerServices.TaskAwaiter::get_IsCompleted()
				IL_004e: brtrue.s IL_008c

				IL_0050: ldarg.0
				IL_0051: ldc.i4.0
				IL_0052: dup
				IL_0053: stloc.0
				IL_0054: stfld int32 Program/'<<Main>$>d__0'::'<>1__state'
				IL_0059: ldarg.0
				IL_005a: ldloc.2
				IL_005b: stfld valuetype [System.Runtime]System.Runtime.CompilerServices.TaskAwaiter Program/'<<Main>$>d__0'::'<>u__1'
				IL_0060: ldarg.0
				IL_0061: ldflda valuetype [System.Runtime]System.Runtime.CompilerServices.AsyncTaskMethodBuilder Program/'<<Main>$>d__0'::'<>t__builder'
				IL_0066: ldloca.s 2
				IL_0068: ldarg.0
				IL_0069: call instance void [System.Runtime]System.Runtime.CompilerServices.AsyncTaskMethodBuilder::AwaitUnsafeOnCompleted<valuetype [System.Runtime]System.Runtime.CompilerServices.TaskAwaiter, valuetype Program/'<<Main>$>d__0'>(!!0&, !!1&)
				IL_006e: leave.s IL_00bf

				IL_0070: ldarg.0
				IL_0071: ldfld valuetype [System.Runtime]System.Runtime.CompilerServices.TaskAwaiter Program/'<<Main>$>d__0'::'<>u__1'
				IL_0076: stloc.2
				IL_0077: ldarg.0
				IL_0078: ldflda valuetype [System.Runtime]System.Runtime.CompilerServices.TaskAwaiter Program/'<<Main>$>d__0'::'<>u__1'
				IL_007d: initobj [System.Runtime]System.Runtime.CompilerServices.TaskAwaiter
				IL_0083: ldarg.0
				IL_0084: ldc.i4.m1
				IL_0085: dup
				IL_0086: stloc.0
				IL_0087: stfld int32 Program/'<<Main>$>d__0'::'<>1__state'

				IL_008c: ldloca.s 2
				IL_008e: call instance void [System.Runtime]System.Runtime.CompilerServices.TaskAwaiter::GetResult()
				IL_0093: leave.s IL_00ac
			} // end .try
			catch [System.Runtime]System.Exception
			{
				IL_0095: stloc.3
				IL_0096: ldarg.0
				IL_0097: ldc.i4.s -2
				IL_0099: stfld int32 Program/'<<Main>$>d__0'::'<>1__state'
				IL_009e: ldarg.0
				IL_009f: ldflda valuetype [System.Runtime]System.Runtime.CompilerServices.AsyncTaskMethodBuilder Program/'<<Main>$>d__0'::'<>t__builder'
				IL_00a4: ldloc.3
				IL_00a5: call instance void [System.Runtime]System.Runtime.CompilerServices.AsyncTaskMethodBuilder::SetException(class [System.Runtime]System.Exception)
				IL_00aa: leave.s IL_00bf
			} // end handler

			IL_00ac: ldarg.0
			IL_00ad: ldc.i4.s -2
			IL_00af: stfld int32 Program/'<<Main>$>d__0'::'<>1__state'
			IL_00b4: ldarg.0
			IL_00b5: ldflda valuetype [System.Runtime]System.Runtime.CompilerServices.AsyncTaskMethodBuilder Program/'<<Main>$>d__0'::'<>t__builder'
			IL_00ba: call instance void [System.Runtime]System.Runtime.CompilerServices.AsyncTaskMethodBuilder::SetResult()

			IL_00bf: ret
		} // end of method '<<Main>$>d__0'::MoveNext

		.method private final hidebysig newslot virtual 
			instance void SetStateMachine (
				class [System.Runtime]System.Runtime.CompilerServices.IAsyncStateMachine stateMachine
			) cil managed 
		{
			.custom instance void [System.Runtime]System.Diagnostics.DebuggerHiddenAttribute::.ctor() = (
				01 00 00 00
			)
			.override method instance void [System.Runtime]System.Runtime.CompilerServices.IAsyncStateMachine::SetStateMachine(class [System.Runtime]System.Runtime.CompilerServices.IAsyncStateMachine)
			.param [1]
				.custom instance void [System.Runtime]System.Runtime.CompilerServices.NullableAttribute::.ctor(uint8) = (
					01 00 01 00 00
				)
			// Method begins at RVA 0x22e4
			// Header size: 1
			// Code size: 13 (0xd)
			.maxstack 8

			IL_0000: ldarg.0
			IL_0001: ldflda valuetype [System.Runtime]System.Runtime.CompilerServices.AsyncTaskMethodBuilder Program/'<<Main>$>d__0'::'<>t__builder'
			IL_0006: ldarg.1
			IL_0007: call instance void [System.Runtime]System.Runtime.CompilerServices.AsyncTaskMethodBuilder::SetStateMachine(class [System.Runtime]System.Runtime.CompilerServices.IAsyncStateMachine)
			IL_000c: ret
		} // end of method '<<Main>$>d__0'::SetStateMachine

	} // end of class <<Main>$>d__0


	// Methods
	.method private hidebysig static 
		class [System.Runtime]System.Threading.Tasks.Task '<Main>$' (
			string[] args
		) cil managed 
	{
		.custom instance void [System.Runtime]System.Runtime.CompilerServices.AsyncStateMachineAttribute::.ctor(class [System.Runtime]System.Type) = (
			01 00 15 50 72 6f 67 72 61 6d 2b 3c 3c 4d 61 69
			6e 3e 24 3e 64 5f 5f 30 00 00
		)
		// Method begins at RVA 0x2050
		// Header size: 12
		// Code size: 47 (0x2f)
		.maxstack 2
		.locals init (
			[0] valuetype Program/'<<Main>$>d__0'
		)

		IL_0000: ldloca.s 0
		IL_0002: call valuetype [System.Runtime]System.Runtime.CompilerServices.AsyncTaskMethodBuilder [System.Runtime]System.Runtime.CompilerServices.AsyncTaskMethodBuilder::Create()
		IL_0007: stfld valuetype [System.Runtime]System.Runtime.CompilerServices.AsyncTaskMethodBuilder Program/'<<Main>$>d__0'::'<>t__builder'
		IL_000c: ldloca.s 0
		IL_000e: ldc.i4.m1
		IL_000f: stfld int32 Program/'<<Main>$>d__0'::'<>1__state'
		IL_0014: ldloca.s 0
		IL_0016: ldflda valuetype [System.Runtime]System.Runtime.CompilerServices.AsyncTaskMethodBuilder Program/'<<Main>$>d__0'::'<>t__builder'
		IL_001b: ldloca.s 0
		IL_001d: call instance void [System.Runtime]System.Runtime.CompilerServices.AsyncTaskMethodBuilder::Start<valuetype Program/'<<Main>$>d__0'>(!!0&)
		IL_0022: ldloca.s 0
		IL_0024: ldflda valuetype [System.Runtime]System.Runtime.CompilerServices.AsyncTaskMethodBuilder Program/'<<Main>$>d__0'::'<>t__builder'
		IL_0029: call instance class [System.Runtime]System.Threading.Tasks.Task [System.Runtime]System.Runtime.CompilerServices.AsyncTaskMethodBuilder::get_Task()
		IL_002e: ret
	} // end of method Program::'<Main>$'

	.method public hidebysig specialname rtspecialname 
		instance void .ctor () cil managed 
	{
		// Method begins at RVA 0x208b
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
		// Method begins at RVA 0x2093
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
	// Nested Types
	.class nested private auto ansi sealed beforefieldinit '<AAsync>d__3'
		extends [System.Runtime]System.ValueType
		implements [System.Runtime]System.Runtime.CompilerServices.IAsyncStateMachine
	{
		.custom instance void [System.Runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
			01 00 00 00
		)
		.interfaceimpl type [System.Runtime]System.Runtime.CompilerServices.IAsyncStateMachine
			.custom instance void [System.Runtime]System.Runtime.CompilerServices.NullableAttribute::.ctor(uint8) = (
				01 00 00 00 00
			)

		// Fields
		.field public int32 '<>1__state'
		.field public valuetype [System.Runtime]System.Runtime.CompilerServices.AsyncTaskMethodBuilder '<>t__builder'
		.field private valuetype [System.Runtime]System.Runtime.CompilerServices.TaskAwaiter '<>u__1'

		// Methods
		.method private final hidebysig newslot virtual 
			instance void MoveNext () cil managed 
		{
			.override method instance void [System.Runtime]System.Runtime.CompilerServices.IAsyncStateMachine::MoveNext()
			// Method begins at RVA 0x22f4
			// Header size: 12
			// Code size: 142 (0x8e)
			.maxstack 3
			.locals init (
				[0] int32,
				[1] valuetype [System.Runtime]System.Runtime.CompilerServices.TaskAwaiter,
				[2] class [System.Runtime]System.Exception
			)

			IL_0000: ldarg.0
			IL_0001: ldfld int32 Experiment/'<AAsync>d__3'::'<>1__state'
			IL_0006: stloc.0
			.try
			{
				IL_0007: ldloc.0
				IL_0008: brfalse.s IL_003e

				IL_000a: call class [System.Runtime]System.Threading.Tasks.Task Experiment::BAsync()
				IL_000f: callvirt instance valuetype [System.Runtime]System.Runtime.CompilerServices.TaskAwaiter [System.Runtime]System.Threading.Tasks.Task::GetAwaiter()
				IL_0014: stloc.1
				IL_0015: ldloca.s 1
				IL_0017: call instance bool [System.Runtime]System.Runtime.CompilerServices.TaskAwaiter::get_IsCompleted()
				IL_001c: brtrue.s IL_005a

				IL_001e: ldarg.0
				IL_001f: ldc.i4.0
				IL_0020: dup
				IL_0021: stloc.0
				IL_0022: stfld int32 Experiment/'<AAsync>d__3'::'<>1__state'
				IL_0027: ldarg.0
				IL_0028: ldloc.1
				IL_0029: stfld valuetype [System.Runtime]System.Runtime.CompilerServices.TaskAwaiter Experiment/'<AAsync>d__3'::'<>u__1'
				IL_002e: ldarg.0
				IL_002f: ldflda valuetype [System.Runtime]System.Runtime.CompilerServices.AsyncTaskMethodBuilder Experiment/'<AAsync>d__3'::'<>t__builder'
				IL_0034: ldloca.s 1
				IL_0036: ldarg.0
				IL_0037: call instance void [System.Runtime]System.Runtime.CompilerServices.AsyncTaskMethodBuilder::AwaitUnsafeOnCompleted<valuetype [System.Runtime]System.Runtime.CompilerServices.TaskAwaiter, valuetype Experiment/'<AAsync>d__3'>(!!0&, !!1&)
				IL_003c: leave.s IL_008d

				IL_003e: ldarg.0
				IL_003f: ldfld valuetype [System.Runtime]System.Runtime.CompilerServices.TaskAwaiter Experiment/'<AAsync>d__3'::'<>u__1'
				IL_0044: stloc.1
				IL_0045: ldarg.0
				IL_0046: ldflda valuetype [System.Runtime]System.Runtime.CompilerServices.TaskAwaiter Experiment/'<AAsync>d__3'::'<>u__1'
				IL_004b: initobj [System.Runtime]System.Runtime.CompilerServices.TaskAwaiter
				IL_0051: ldarg.0
				IL_0052: ldc.i4.m1
				IL_0053: dup
				IL_0054: stloc.0
				IL_0055: stfld int32 Experiment/'<AAsync>d__3'::'<>1__state'

				IL_005a: ldloca.s 1
				IL_005c: call instance void [System.Runtime]System.Runtime.CompilerServices.TaskAwaiter::GetResult()
				IL_0061: leave.s IL_007a
			} // end .try
			catch [System.Runtime]System.Exception
			{
				IL_0063: stloc.2
				IL_0064: ldarg.0
				IL_0065: ldc.i4.s -2
				IL_0067: stfld int32 Experiment/'<AAsync>d__3'::'<>1__state'
				IL_006c: ldarg.0
				IL_006d: ldflda valuetype [System.Runtime]System.Runtime.CompilerServices.AsyncTaskMethodBuilder Experiment/'<AAsync>d__3'::'<>t__builder'
				IL_0072: ldloc.2
				IL_0073: call instance void [System.Runtime]System.Runtime.CompilerServices.AsyncTaskMethodBuilder::SetException(class [System.Runtime]System.Exception)
				IL_0078: leave.s IL_008d
			} // end handler

			IL_007a: ldarg.0
			IL_007b: ldc.i4.s -2
			IL_007d: stfld int32 Experiment/'<AAsync>d__3'::'<>1__state'
			IL_0082: ldarg.0
			IL_0083: ldflda valuetype [System.Runtime]System.Runtime.CompilerServices.AsyncTaskMethodBuilder Experiment/'<AAsync>d__3'::'<>t__builder'
			IL_0088: call instance void [System.Runtime]System.Runtime.CompilerServices.AsyncTaskMethodBuilder::SetResult()

			IL_008d: ret
		} // end of method '<AAsync>d__3'::MoveNext

		.method private final hidebysig newslot virtual 
			instance void SetStateMachine (
				class [System.Runtime]System.Runtime.CompilerServices.IAsyncStateMachine stateMachine
			) cil managed 
		{
			.custom instance void [System.Runtime]System.Diagnostics.DebuggerHiddenAttribute::.ctor() = (
				01 00 00 00
			)
			.override method instance void [System.Runtime]System.Runtime.CompilerServices.IAsyncStateMachine::SetStateMachine(class [System.Runtime]System.Runtime.CompilerServices.IAsyncStateMachine)
			// Method begins at RVA 0x23a0
			// Header size: 1
			// Code size: 13 (0xd)
			.maxstack 8

			IL_0000: ldarg.0
			IL_0001: ldflda valuetype [System.Runtime]System.Runtime.CompilerServices.AsyncTaskMethodBuilder Experiment/'<AAsync>d__3'::'<>t__builder'
			IL_0006: ldarg.1
			IL_0007: call instance void [System.Runtime]System.Runtime.CompilerServices.AsyncTaskMethodBuilder::SetStateMachine(class [System.Runtime]System.Runtime.CompilerServices.IAsyncStateMachine)
			IL_000c: ret
		} // end of method '<AAsync>d__3'::SetStateMachine

	} // end of class <AAsync>d__3

	.class nested private auto ansi sealed beforefieldinit '<BAsync>d__4'
		extends [System.Runtime]System.ValueType
		implements [System.Runtime]System.Runtime.CompilerServices.IAsyncStateMachine
	{
		.custom instance void [System.Runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
			01 00 00 00
		)
		.interfaceimpl type [System.Runtime]System.Runtime.CompilerServices.IAsyncStateMachine
			.custom instance void [System.Runtime]System.Runtime.CompilerServices.NullableAttribute::.ctor(uint8) = (
				01 00 00 00 00
			)

		// Fields
		.field public int32 '<>1__state'
		.field public valuetype [System.Runtime]System.Runtime.CompilerServices.AsyncTaskMethodBuilder '<>t__builder'
		.field private valuetype [System.Runtime]System.Runtime.CompilerServices.TaskAwaiter '<>u__1'

		// Methods
		.method private final hidebysig newslot virtual 
			instance void MoveNext () cil managed 
		{
			.override method instance void [System.Runtime]System.Runtime.CompilerServices.IAsyncStateMachine::MoveNext()
			// Method begins at RVA 0x23b0
			// Header size: 12
			// Code size: 142 (0x8e)
			.maxstack 3
			.locals init (
				[0] int32,
				[1] valuetype [System.Runtime]System.Runtime.CompilerServices.TaskAwaiter,
				[2] class [System.Runtime]System.Exception
			)

			IL_0000: ldarg.0
			IL_0001: ldfld int32 Experiment/'<BAsync>d__4'::'<>1__state'
			IL_0006: stloc.0
			.try
			{
				IL_0007: ldloc.0
				IL_0008: brfalse.s IL_003e

				IL_000a: call class [System.Runtime]System.Threading.Tasks.Task Experiment::CAsync()
				IL_000f: callvirt instance valuetype [System.Runtime]System.Runtime.CompilerServices.TaskAwaiter [System.Runtime]System.Threading.Tasks.Task::GetAwaiter()
				IL_0014: stloc.1
				IL_0015: ldloca.s 1
				IL_0017: call instance bool [System.Runtime]System.Runtime.CompilerServices.TaskAwaiter::get_IsCompleted()
				IL_001c: brtrue.s IL_005a

				IL_001e: ldarg.0
				IL_001f: ldc.i4.0
				IL_0020: dup
				IL_0021: stloc.0
				IL_0022: stfld int32 Experiment/'<BAsync>d__4'::'<>1__state'
				IL_0027: ldarg.0
				IL_0028: ldloc.1
				IL_0029: stfld valuetype [System.Runtime]System.Runtime.CompilerServices.TaskAwaiter Experiment/'<BAsync>d__4'::'<>u__1'
				IL_002e: ldarg.0
				IL_002f: ldflda valuetype [System.Runtime]System.Runtime.CompilerServices.AsyncTaskMethodBuilder Experiment/'<BAsync>d__4'::'<>t__builder'
				IL_0034: ldloca.s 1
				IL_0036: ldarg.0
				IL_0037: call instance void [System.Runtime]System.Runtime.CompilerServices.AsyncTaskMethodBuilder::AwaitUnsafeOnCompleted<valuetype [System.Runtime]System.Runtime.CompilerServices.TaskAwaiter, valuetype Experiment/'<BAsync>d__4'>(!!0&, !!1&)
				IL_003c: leave.s IL_008d

				IL_003e: ldarg.0
				IL_003f: ldfld valuetype [System.Runtime]System.Runtime.CompilerServices.TaskAwaiter Experiment/'<BAsync>d__4'::'<>u__1'
				IL_0044: stloc.1
				IL_0045: ldarg.0
				IL_0046: ldflda valuetype [System.Runtime]System.Runtime.CompilerServices.TaskAwaiter Experiment/'<BAsync>d__4'::'<>u__1'
				IL_004b: initobj [System.Runtime]System.Runtime.CompilerServices.TaskAwaiter
				IL_0051: ldarg.0
				IL_0052: ldc.i4.m1
				IL_0053: dup
				IL_0054: stloc.0
				IL_0055: stfld int32 Experiment/'<BAsync>d__4'::'<>1__state'

				IL_005a: ldloca.s 1
				IL_005c: call instance void [System.Runtime]System.Runtime.CompilerServices.TaskAwaiter::GetResult()
				IL_0061: leave.s IL_007a
			} // end .try
			catch [System.Runtime]System.Exception
			{
				IL_0063: stloc.2
				IL_0064: ldarg.0
				IL_0065: ldc.i4.s -2
				IL_0067: stfld int32 Experiment/'<BAsync>d__4'::'<>1__state'
				IL_006c: ldarg.0
				IL_006d: ldflda valuetype [System.Runtime]System.Runtime.CompilerServices.AsyncTaskMethodBuilder Experiment/'<BAsync>d__4'::'<>t__builder'
				IL_0072: ldloc.2
				IL_0073: call instance void [System.Runtime]System.Runtime.CompilerServices.AsyncTaskMethodBuilder::SetException(class [System.Runtime]System.Exception)
				IL_0078: leave.s IL_008d
			} // end handler

			IL_007a: ldarg.0
			IL_007b: ldc.i4.s -2
			IL_007d: stfld int32 Experiment/'<BAsync>d__4'::'<>1__state'
			IL_0082: ldarg.0
			IL_0083: ldflda valuetype [System.Runtime]System.Runtime.CompilerServices.AsyncTaskMethodBuilder Experiment/'<BAsync>d__4'::'<>t__builder'
			IL_0088: call instance void [System.Runtime]System.Runtime.CompilerServices.AsyncTaskMethodBuilder::SetResult()

			IL_008d: ret
		} // end of method '<BAsync>d__4'::MoveNext

		.method private final hidebysig newslot virtual 
			instance void SetStateMachine (
				class [System.Runtime]System.Runtime.CompilerServices.IAsyncStateMachine stateMachine
			) cil managed 
		{
			.custom instance void [System.Runtime]System.Diagnostics.DebuggerHiddenAttribute::.ctor() = (
				01 00 00 00
			)
			.override method instance void [System.Runtime]System.Runtime.CompilerServices.IAsyncStateMachine::SetStateMachine(class [System.Runtime]System.Runtime.CompilerServices.IAsyncStateMachine)
			// Method begins at RVA 0x245c
			// Header size: 1
			// Code size: 13 (0xd)
			.maxstack 8

			IL_0000: ldarg.0
			IL_0001: ldflda valuetype [System.Runtime]System.Runtime.CompilerServices.AsyncTaskMethodBuilder Experiment/'<BAsync>d__4'::'<>t__builder'
			IL_0006: ldarg.1
			IL_0007: call instance void [System.Runtime]System.Runtime.CompilerServices.AsyncTaskMethodBuilder::SetStateMachine(class [System.Runtime]System.Runtime.CompilerServices.IAsyncStateMachine)
			IL_000c: ret
		} // end of method '<BAsync>d__4'::SetStateMachine

	} // end of class <BAsync>d__4

	.class nested private auto ansi sealed beforefieldinit '<CAsync>d__5'
		extends [System.Runtime]System.ValueType
		implements [System.Runtime]System.Runtime.CompilerServices.IAsyncStateMachine
	{
		.custom instance void [System.Runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
			01 00 00 00
		)
		.interfaceimpl type [System.Runtime]System.Runtime.CompilerServices.IAsyncStateMachine
			.custom instance void [System.Runtime]System.Runtime.CompilerServices.NullableAttribute::.ctor(uint8) = (
				01 00 00 00 00
			)

		// Fields
		.field public int32 '<>1__state'
		.field public valuetype [System.Runtime]System.Runtime.CompilerServices.AsyncTaskMethodBuilder '<>t__builder'
		.field private valuetype [System.Runtime]System.Runtime.CompilerServices.TaskAwaiter '<>u__1'

		// Methods
		.method private final hidebysig newslot virtual 
			instance void MoveNext () cil managed 
		{
			.override method instance void [System.Runtime]System.Runtime.CompilerServices.IAsyncStateMachine::MoveNext()
			// Method begins at RVA 0x246c
			// Header size: 12
			// Code size: 134 (0x86)
			.maxstack 3
			.locals init (
				[0] int32,
				[1] valuetype [System.Runtime]System.Runtime.CompilerServices.TaskAwaiter,
				[2] class [System.Runtime]System.Exception
			)

			IL_0000: ldarg.0
			IL_0001: ldfld int32 Experiment/'<CAsync>d__5'::'<>1__state'
			IL_0006: stloc.0
			.try
			{
				IL_0007: ldloc.0
				IL_0008: brfalse.s IL_0040

				IL_000a: ldc.i4.s 10
				IL_000c: call class [System.Runtime]System.Threading.Tasks.Task [System.Runtime]System.Threading.Tasks.Task::Delay(int32)
				IL_0011: callvirt instance valuetype [System.Runtime]System.Runtime.CompilerServices.TaskAwaiter [System.Runtime]System.Threading.Tasks.Task::GetAwaiter()
				IL_0016: stloc.1
				IL_0017: ldloca.s 1
				IL_0019: call instance bool [System.Runtime]System.Runtime.CompilerServices.TaskAwaiter::get_IsCompleted()
				IL_001e: brtrue.s IL_005c

				IL_0020: ldarg.0
				IL_0021: ldc.i4.0
				IL_0022: dup
				IL_0023: stloc.0
				IL_0024: stfld int32 Experiment/'<CAsync>d__5'::'<>1__state'
				IL_0029: ldarg.0
				IL_002a: ldloc.1
				IL_002b: stfld valuetype [System.Runtime]System.Runtime.CompilerServices.TaskAwaiter Experiment/'<CAsync>d__5'::'<>u__1'
				IL_0030: ldarg.0
				IL_0031: ldflda valuetype [System.Runtime]System.Runtime.CompilerServices.AsyncTaskMethodBuilder Experiment/'<CAsync>d__5'::'<>t__builder'
				IL_0036: ldloca.s 1
				IL_0038: ldarg.0
				IL_0039: call instance void [System.Runtime]System.Runtime.CompilerServices.AsyncTaskMethodBuilder::AwaitUnsafeOnCompleted<valuetype [System.Runtime]System.Runtime.CompilerServices.TaskAwaiter, valuetype Experiment/'<CAsync>d__5'>(!!0&, !!1&)
				IL_003e: leave.s IL_0085

				IL_0040: ldarg.0
				IL_0041: ldfld valuetype [System.Runtime]System.Runtime.CompilerServices.TaskAwaiter Experiment/'<CAsync>d__5'::'<>u__1'
				IL_0046: stloc.1
				IL_0047: ldarg.0
				IL_0048: ldflda valuetype [System.Runtime]System.Runtime.CompilerServices.TaskAwaiter Experiment/'<CAsync>d__5'::'<>u__1'
				IL_004d: initobj [System.Runtime]System.Runtime.CompilerServices.TaskAwaiter
				IL_0053: ldarg.0
				IL_0054: ldc.i4.m1
				IL_0055: dup
				IL_0056: stloc.0
				IL_0057: stfld int32 Experiment/'<CAsync>d__5'::'<>1__state'

				IL_005c: ldloca.s 1
				IL_005e: call instance void [System.Runtime]System.Runtime.CompilerServices.TaskAwaiter::GetResult()
				IL_0063: ldstr "Boom from async method"
				IL_0068: newobj instance void [System.Runtime]System.InvalidOperationException::.ctor(string)
				IL_006d: throw
			} // end .try
			catch [System.Runtime]System.Exception
			{
				IL_006e: stloc.2
				IL_006f: ldarg.0
				IL_0070: ldc.i4.s -2
				IL_0072: stfld int32 Experiment/'<CAsync>d__5'::'<>1__state'
				IL_0077: ldarg.0
				IL_0078: ldflda valuetype [System.Runtime]System.Runtime.CompilerServices.AsyncTaskMethodBuilder Experiment/'<CAsync>d__5'::'<>t__builder'
				IL_007d: ldloc.2
				IL_007e: call instance void [System.Runtime]System.Runtime.CompilerServices.AsyncTaskMethodBuilder::SetException(class [System.Runtime]System.Exception)
				IL_0083: leave.s IL_0085
			} // end handler

			IL_0085: ret
		} // end of method '<CAsync>d__5'::MoveNext

		.method private final hidebysig newslot virtual 
			instance void SetStateMachine (
				class [System.Runtime]System.Runtime.CompilerServices.IAsyncStateMachine stateMachine
			) cil managed 
		{
			.custom instance void [System.Runtime]System.Diagnostics.DebuggerHiddenAttribute::.ctor() = (
				01 00 00 00
			)
			.override method instance void [System.Runtime]System.Runtime.CompilerServices.IAsyncStateMachine::SetStateMachine(class [System.Runtime]System.Runtime.CompilerServices.IAsyncStateMachine)
			// Method begins at RVA 0x2510
			// Header size: 1
			// Code size: 13 (0xd)
			.maxstack 8

			IL_0000: ldarg.0
			IL_0001: ldflda valuetype [System.Runtime]System.Runtime.CompilerServices.AsyncTaskMethodBuilder Experiment/'<CAsync>d__5'::'<>t__builder'
			IL_0006: ldarg.1
			IL_0007: call instance void [System.Runtime]System.Runtime.CompilerServices.AsyncTaskMethodBuilder::SetStateMachine(class [System.Runtime]System.Runtime.CompilerServices.IAsyncStateMachine)
			IL_000c: ret
		} // end of method '<CAsync>d__5'::SetStateMachine

	} // end of class <CAsync>d__5

	.class nested private auto ansi sealed beforefieldinit '<RealSuspensionAsync>d__2'
		extends [System.Runtime]System.ValueType
		implements [System.Runtime]System.Runtime.CompilerServices.IAsyncStateMachine
	{
		.custom instance void [System.Runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
			01 00 00 00
		)
		.interfaceimpl type [System.Runtime]System.Runtime.CompilerServices.IAsyncStateMachine
			.custom instance void [System.Runtime]System.Runtime.CompilerServices.NullableAttribute::.ctor(uint8) = (
				01 00 00 00 00
			)

		// Fields
		.field public int32 '<>1__state'
		.field public valuetype [System.Runtime]System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<int32> '<>t__builder'
		.custom instance void [System.Runtime]System.Runtime.CompilerServices.NullableAttribute::.ctor(uint8) = (
			01 00 00 00 00
		)
		.field private valuetype [System.Runtime]System.Runtime.CompilerServices.YieldAwaitable/YieldAwaiter '<>u__1'

		// Methods
		.method private final hidebysig newslot virtual 
			instance void MoveNext () cil managed 
		{
			.override method instance void [System.Runtime]System.Runtime.CompilerServices.IAsyncStateMachine::MoveNext()
			// Method begins at RVA 0x2520
			// Header size: 12
			// Code size: 151 (0x97)
			.maxstack 3
			.locals init (
				[0] int32,
				[1] int32,
				[2] valuetype [System.Runtime]System.Runtime.CompilerServices.YieldAwaitable/YieldAwaiter,
				[3] valuetype [System.Runtime]System.Runtime.CompilerServices.YieldAwaitable,
				[4] class [System.Runtime]System.Exception
			)

			IL_0000: ldarg.0
			IL_0001: ldfld int32 Experiment/'<RealSuspensionAsync>d__2'::'<>1__state'
			IL_0006: stloc.0
			.try
			{
				IL_0007: ldloc.0
				IL_0008: brfalse.s IL_0041

				IL_000a: call valuetype [System.Runtime]System.Runtime.CompilerServices.YieldAwaitable [System.Runtime]System.Threading.Tasks.Task::Yield()
				IL_000f: stloc.3
				IL_0010: ldloca.s 3
				IL_0012: call instance valuetype [System.Runtime]System.Runtime.CompilerServices.YieldAwaitable/YieldAwaiter [System.Runtime]System.Runtime.CompilerServices.YieldAwaitable::GetAwaiter()
				IL_0017: stloc.2
				IL_0018: ldloca.s 2
				IL_001a: call instance bool [System.Runtime]System.Runtime.CompilerServices.YieldAwaitable/YieldAwaiter::get_IsCompleted()
				IL_001f: brtrue.s IL_005d

				IL_0021: ldarg.0
				IL_0022: ldc.i4.0
				IL_0023: dup
				IL_0024: stloc.0
				IL_0025: stfld int32 Experiment/'<RealSuspensionAsync>d__2'::'<>1__state'
				IL_002a: ldarg.0
				IL_002b: ldloc.2
				IL_002c: stfld valuetype [System.Runtime]System.Runtime.CompilerServices.YieldAwaitable/YieldAwaiter Experiment/'<RealSuspensionAsync>d__2'::'<>u__1'
				IL_0031: ldarg.0
				IL_0032: ldflda valuetype [System.Runtime]System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<int32> Experiment/'<RealSuspensionAsync>d__2'::'<>t__builder'
				IL_0037: ldloca.s 2
				IL_0039: ldarg.0
				IL_003a: call instance void valuetype [System.Runtime]System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<int32>::AwaitUnsafeOnCompleted<valuetype [System.Runtime]System.Runtime.CompilerServices.YieldAwaitable/YieldAwaiter, valuetype Experiment/'<RealSuspensionAsync>d__2'>(!!0&, !!1&)
				IL_003f: leave.s IL_0096

				IL_0041: ldarg.0
				IL_0042: ldfld valuetype [System.Runtime]System.Runtime.CompilerServices.YieldAwaitable/YieldAwaiter Experiment/'<RealSuspensionAsync>d__2'::'<>u__1'
				IL_0047: stloc.2
				IL_0048: ldarg.0
				IL_0049: ldflda valuetype [System.Runtime]System.Runtime.CompilerServices.YieldAwaitable/YieldAwaiter Experiment/'<RealSuspensionAsync>d__2'::'<>u__1'
				IL_004e: initobj [System.Runtime]System.Runtime.CompilerServices.YieldAwaitable/YieldAwaiter
				IL_0054: ldarg.0
				IL_0055: ldc.i4.m1
				IL_0056: dup
				IL_0057: stloc.0
				IL_0058: stfld int32 Experiment/'<RealSuspensionAsync>d__2'::'<>1__state'

				IL_005d: ldloca.s 2
				IL_005f: call instance void [System.Runtime]System.Runtime.CompilerServices.YieldAwaitable/YieldAwaiter::GetResult()
				IL_0064: ldc.i4.s 42
				IL_0066: stloc.1
				IL_0067: leave.s IL_0082
			} // end .try
			catch [System.Runtime]System.Exception
			{
				IL_0069: stloc.s 4
				IL_006b: ldarg.0
				IL_006c: ldc.i4.s -2
				IL_006e: stfld int32 Experiment/'<RealSuspensionAsync>d__2'::'<>1__state'
				IL_0073: ldarg.0
				IL_0074: ldflda valuetype [System.Runtime]System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<int32> Experiment/'<RealSuspensionAsync>d__2'::'<>t__builder'
				IL_0079: ldloc.s 4
				IL_007b: call instance void valuetype [System.Runtime]System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<int32>::SetException(class [System.Runtime]System.Exception)
				IL_0080: leave.s IL_0096
			} // end handler

			IL_0082: ldarg.0
			IL_0083: ldc.i4.s -2
			IL_0085: stfld int32 Experiment/'<RealSuspensionAsync>d__2'::'<>1__state'
			IL_008a: ldarg.0
			IL_008b: ldflda valuetype [System.Runtime]System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<int32> Experiment/'<RealSuspensionAsync>d__2'::'<>t__builder'
			IL_0090: ldloc.1
			IL_0091: call instance void valuetype [System.Runtime]System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<int32>::SetResult(!0)

			IL_0096: ret
		} // end of method '<RealSuspensionAsync>d__2'::MoveNext

		.method private final hidebysig newslot virtual 
			instance void SetStateMachine (
				class [System.Runtime]System.Runtime.CompilerServices.IAsyncStateMachine stateMachine
			) cil managed 
		{
			.custom instance void [System.Runtime]System.Diagnostics.DebuggerHiddenAttribute::.ctor() = (
				01 00 00 00
			)
			.override method instance void [System.Runtime]System.Runtime.CompilerServices.IAsyncStateMachine::SetStateMachine(class [System.Runtime]System.Runtime.CompilerServices.IAsyncStateMachine)
			// Method begins at RVA 0x25d4
			// Header size: 1
			// Code size: 13 (0xd)
			.maxstack 8

			IL_0000: ldarg.0
			IL_0001: ldflda valuetype [System.Runtime]System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<int32> Experiment/'<RealSuspensionAsync>d__2'::'<>t__builder'
			IL_0006: ldarg.1
			IL_0007: call instance void valuetype [System.Runtime]System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<int32>::SetStateMachine(class [System.Runtime]System.Runtime.CompilerServices.IAsyncStateMachine)
			IL_000c: ret
		} // end of method '<RealSuspensionAsync>d__2'::SetStateMachine

	} // end of class <RealSuspensionAsync>d__2

	.class nested private auto ansi sealed beforefieldinit '<RunAsync>d__0'
		extends [System.Runtime]System.ValueType
		implements [System.Runtime]System.Runtime.CompilerServices.IAsyncStateMachine
	{
		.custom instance void [System.Runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
			01 00 00 00
		)
		.interfaceimpl type [System.Runtime]System.Runtime.CompilerServices.IAsyncStateMachine
			.custom instance void [System.Runtime]System.Runtime.CompilerServices.NullableAttribute::.ctor(uint8) = (
				01 00 00 00 00
			)

		// Fields
		.field public int32 '<>1__state'
		.field public valuetype [System.Runtime]System.Runtime.CompilerServices.AsyncTaskMethodBuilder '<>t__builder'
		.field private valuetype [System.Runtime]System.Runtime.CompilerServices.TaskAwaiter`1<int32> '<>u__1'
		.custom instance void [System.Runtime]System.Runtime.CompilerServices.NullableAttribute::.ctor(uint8) = (
			01 00 00 00 00
		)
		.field private valuetype [System.Runtime]System.Runtime.CompilerServices.TaskAwaiter '<>u__2'

		// Methods
		.method private final hidebysig newslot virtual 
			instance void MoveNext () cil managed 
		{
			.override method instance void [System.Runtime]System.Runtime.CompilerServices.IAsyncStateMachine::MoveNext()
			// Method begins at RVA 0x25e4
			// Header size: 12
			// Code size: 401 (0x191)
			.maxstack 3
			.locals init (
				[0] int32,
				[1] valuetype [System.Runtime]System.Runtime.CompilerServices.TaskAwaiter`1<int32>,
				[2] valuetype [System.Runtime]System.Runtime.CompilerServices.TaskAwaiter,
				[3] class [System.Runtime]System.Exception
			)

			IL_0000: ldarg.0
			IL_0001: ldfld int32 Experiment/'<RunAsync>d__0'::'<>1__state'
			IL_0006: stloc.0
			.try
			{
				IL_0007: ldloc.0
				IL_0008: switch (IL_005a, IL_00c8, IL_00ff)

				IL_0019: ldstr "=== Sync-completing async ==="
				IL_001e: call void [System.Console]System.Console::WriteLine(string)
				IL_0023: call class [System.Runtime]System.Threading.Tasks.Task`1<int32> Experiment::SyncCompletingAsync()
				IL_0028: callvirt instance valuetype [System.Runtime]System.Runtime.CompilerServices.TaskAwaiter`1<!0> class [System.Runtime]System.Threading.Tasks.Task`1<int32>::GetAwaiter()
				IL_002d: stloc.1
				IL_002e: ldloca.s 1
				IL_0030: call instance bool valuetype [System.Runtime]System.Runtime.CompilerServices.TaskAwaiter`1<int32>::get_IsCompleted()
				IL_0035: brtrue.s IL_0076

				IL_0037: ldarg.0
				IL_0038: ldc.i4.0
				IL_0039: dup
				IL_003a: stloc.0
				IL_003b: stfld int32 Experiment/'<RunAsync>d__0'::'<>1__state'
				IL_0040: ldarg.0
				IL_0041: ldloc.1
				IL_0042: stfld valuetype [System.Runtime]System.Runtime.CompilerServices.TaskAwaiter`1<int32> Experiment/'<RunAsync>d__0'::'<>u__1'
				IL_0047: ldarg.0
				IL_0048: ldflda valuetype [System.Runtime]System.Runtime.CompilerServices.AsyncTaskMethodBuilder Experiment/'<RunAsync>d__0'::'<>t__builder'
				IL_004d: ldloca.s 1
				IL_004f: ldarg.0
				IL_0050: call instance void [System.Runtime]System.Runtime.CompilerServices.AsyncTaskMethodBuilder::AwaitUnsafeOnCompleted<valuetype [System.Runtime]System.Runtime.CompilerServices.TaskAwaiter`1<int32>, valuetype Experiment/'<RunAsync>d__0'>(!!0&, !!1&)
				IL_0055: leave IL_0190

				IL_005a: ldarg.0
				IL_005b: ldfld valuetype [System.Runtime]System.Runtime.CompilerServices.TaskAwaiter`1<int32> Experiment/'<RunAsync>d__0'::'<>u__1'
				IL_0060: stloc.1
				IL_0061: ldarg.0
				IL_0062: ldflda valuetype [System.Runtime]System.Runtime.CompilerServices.TaskAwaiter`1<int32> Experiment/'<RunAsync>d__0'::'<>u__1'
				IL_0067: initobj valuetype [System.Runtime]System.Runtime.CompilerServices.TaskAwaiter`1<int32>
				IL_006d: ldarg.0
				IL_006e: ldc.i4.m1
				IL_006f: dup
				IL_0070: stloc.0
				IL_0071: stfld int32 Experiment/'<RunAsync>d__0'::'<>1__state'

				IL_0076: ldloca.s 1
				IL_0078: call instance !0 valuetype [System.Runtime]System.Runtime.CompilerServices.TaskAwaiter`1<int32>::GetResult()
				IL_007d: call void [System.Console]System.Console::WriteLine(int32)
				IL_0082: call void [System.Console]System.Console::WriteLine()
				IL_0087: ldstr "=== Real suspension ==="
				IL_008c: call void [System.Console]System.Console::WriteLine(string)
				IL_0091: call class [System.Runtime]System.Threading.Tasks.Task`1<int32> Experiment::RealSuspensionAsync()
				IL_0096: callvirt instance valuetype [System.Runtime]System.Runtime.CompilerServices.TaskAwaiter`1<!0> class [System.Runtime]System.Threading.Tasks.Task`1<int32>::GetAwaiter()
				IL_009b: stloc.1
				IL_009c: ldloca.s 1
				IL_009e: call instance bool valuetype [System.Runtime]System.Runtime.CompilerServices.TaskAwaiter`1<int32>::get_IsCompleted()
				IL_00a3: brtrue.s IL_00e4

				IL_00a5: ldarg.0
				IL_00a6: ldc.i4.1
				IL_00a7: dup
				IL_00a8: stloc.0
				IL_00a9: stfld int32 Experiment/'<RunAsync>d__0'::'<>1__state'
				IL_00ae: ldarg.0
				IL_00af: ldloc.1
				IL_00b0: stfld valuetype [System.Runtime]System.Runtime.CompilerServices.TaskAwaiter`1<int32> Experiment/'<RunAsync>d__0'::'<>u__1'
				IL_00b5: ldarg.0
				IL_00b6: ldflda valuetype [System.Runtime]System.Runtime.CompilerServices.AsyncTaskMethodBuilder Experiment/'<RunAsync>d__0'::'<>t__builder'
				IL_00bb: ldloca.s 1
				IL_00bd: ldarg.0
				IL_00be: call instance void [System.Runtime]System.Runtime.CompilerServices.AsyncTaskMethodBuilder::AwaitUnsafeOnCompleted<valuetype [System.Runtime]System.Runtime.CompilerServices.TaskAwaiter`1<int32>, valuetype Experiment/'<RunAsync>d__0'>(!!0&, !!1&)
				IL_00c3: leave IL_0190

				IL_00c8: ldarg.0
				IL_00c9: ldfld valuetype [System.Runtime]System.Runtime.CompilerServices.TaskAwaiter`1<int32> Experiment/'<RunAsync>d__0'::'<>u__1'
				IL_00ce: stloc.1
				IL_00cf: ldarg.0
				IL_00d0: ldflda valuetype [System.Runtime]System.Runtime.CompilerServices.TaskAwaiter`1<int32> Experiment/'<RunAsync>d__0'::'<>u__1'
				IL_00d5: initobj valuetype [System.Runtime]System.Runtime.CompilerServices.TaskAwaiter`1<int32>
				IL_00db: ldarg.0
				IL_00dc: ldc.i4.m1
				IL_00dd: dup
				IL_00de: stloc.0
				IL_00df: stfld int32 Experiment/'<RunAsync>d__0'::'<>1__state'

				IL_00e4: ldloca.s 1
				IL_00e6: call instance !0 valuetype [System.Runtime]System.Runtime.CompilerServices.TaskAwaiter`1<int32>::GetResult()
				IL_00eb: call void [System.Console]System.Console::WriteLine(int32)
				IL_00f0: call void [System.Console]System.Console::WriteLine()
				IL_00f5: ldstr "=== Stack trace ==="
				IL_00fa: call void [System.Console]System.Console::WriteLine(string)

				IL_00ff: nop
				.try
				{
					IL_0100: ldloc.0
					IL_0101: ldc.i4.2
					IL_0102: beq.s IL_0138

					IL_0104: call class [System.Runtime]System.Threading.Tasks.Task Experiment::AAsync()
					IL_0109: callvirt instance valuetype [System.Runtime]System.Runtime.CompilerServices.TaskAwaiter [System.Runtime]System.Threading.Tasks.Task::GetAwaiter()
					IL_010e: stloc.2
					IL_010f: ldloca.s 2
					IL_0111: call instance bool [System.Runtime]System.Runtime.CompilerServices.TaskAwaiter::get_IsCompleted()
					IL_0116: brtrue.s IL_0154

					IL_0118: ldarg.0
					IL_0119: ldc.i4.2
					IL_011a: dup
					IL_011b: stloc.0
					IL_011c: stfld int32 Experiment/'<RunAsync>d__0'::'<>1__state'
					IL_0121: ldarg.0
					IL_0122: ldloc.2
					IL_0123: stfld valuetype [System.Runtime]System.Runtime.CompilerServices.TaskAwaiter Experiment/'<RunAsync>d__0'::'<>u__2'
					IL_0128: ldarg.0
					IL_0129: ldflda valuetype [System.Runtime]System.Runtime.CompilerServices.AsyncTaskMethodBuilder Experiment/'<RunAsync>d__0'::'<>t__builder'
					IL_012e: ldloca.s 2
					IL_0130: ldarg.0
					IL_0131: call instance void [System.Runtime]System.Runtime.CompilerServices.AsyncTaskMethodBuilder::AwaitUnsafeOnCompleted<valuetype [System.Runtime]System.Runtime.CompilerServices.TaskAwaiter, valuetype Experiment/'<RunAsync>d__0'>(!!0&, !!1&)
					IL_0136: leave.s IL_0190

					IL_0138: ldarg.0
					IL_0139: ldfld valuetype [System.Runtime]System.Runtime.CompilerServices.TaskAwaiter Experiment/'<RunAsync>d__0'::'<>u__2'
					IL_013e: stloc.2
					IL_013f: ldarg.0
					IL_0140: ldflda valuetype [System.Runtime]System.Runtime.CompilerServices.TaskAwaiter Experiment/'<RunAsync>d__0'::'<>u__2'
					IL_0145: initobj [System.Runtime]System.Runtime.CompilerServices.TaskAwaiter
					IL_014b: ldarg.0
					IL_014c: ldc.i4.m1
					IL_014d: dup
					IL_014e: stloc.0
					IL_014f: stfld int32 Experiment/'<RunAsync>d__0'::'<>1__state'

					IL_0154: ldloca.s 2
					IL_0156: call instance void [System.Runtime]System.Runtime.CompilerServices.TaskAwaiter::GetResult()
					IL_015b: leave.s IL_0164
				} // end .try
				catch [System.Runtime]System.Exception
				{
					IL_015d: call void [System.Console]System.Console::WriteLine(object)
					IL_0162: leave.s IL_0164
				} // end handler

				IL_0164: leave.s IL_017d
			} // end .try
			catch [System.Runtime]System.Exception
			{
				IL_0166: stloc.3
				IL_0167: ldarg.0
				IL_0168: ldc.i4.s -2
				IL_016a: stfld int32 Experiment/'<RunAsync>d__0'::'<>1__state'
				IL_016f: ldarg.0
				IL_0170: ldflda valuetype [System.Runtime]System.Runtime.CompilerServices.AsyncTaskMethodBuilder Experiment/'<RunAsync>d__0'::'<>t__builder'
				IL_0175: ldloc.3
				IL_0176: call instance void [System.Runtime]System.Runtime.CompilerServices.AsyncTaskMethodBuilder::SetException(class [System.Runtime]System.Exception)
				IL_017b: leave.s IL_0190
			} // end handler

			IL_017d: ldarg.0
			IL_017e: ldc.i4.s -2
			IL_0180: stfld int32 Experiment/'<RunAsync>d__0'::'<>1__state'
			IL_0185: ldarg.0
			IL_0186: ldflda valuetype [System.Runtime]System.Runtime.CompilerServices.AsyncTaskMethodBuilder Experiment/'<RunAsync>d__0'::'<>t__builder'
			IL_018b: call instance void [System.Runtime]System.Runtime.CompilerServices.AsyncTaskMethodBuilder::SetResult()

			IL_0190: ret
		} // end of method '<RunAsync>d__0'::MoveNext

		.method private final hidebysig newslot virtual 
			instance void SetStateMachine (
				class [System.Runtime]System.Runtime.CompilerServices.IAsyncStateMachine stateMachine
			) cil managed 
		{
			.custom instance void [System.Runtime]System.Diagnostics.DebuggerHiddenAttribute::.ctor() = (
				01 00 00 00
			)
			.override method instance void [System.Runtime]System.Runtime.CompilerServices.IAsyncStateMachine::SetStateMachine(class [System.Runtime]System.Runtime.CompilerServices.IAsyncStateMachine)
			// Method begins at RVA 0x27b8
			// Header size: 1
			// Code size: 13 (0xd)
			.maxstack 8

			IL_0000: ldarg.0
			IL_0001: ldflda valuetype [System.Runtime]System.Runtime.CompilerServices.AsyncTaskMethodBuilder Experiment/'<RunAsync>d__0'::'<>t__builder'
			IL_0006: ldarg.1
			IL_0007: call instance void [System.Runtime]System.Runtime.CompilerServices.AsyncTaskMethodBuilder::SetStateMachine(class [System.Runtime]System.Runtime.CompilerServices.IAsyncStateMachine)
			IL_000c: ret
		} // end of method '<RunAsync>d__0'::SetStateMachine

	} // end of class <RunAsync>d__0

	.class nested private auto ansi sealed beforefieldinit '<SyncCompletingAsync>d__1'
		extends [System.Runtime]System.ValueType
		implements [System.Runtime]System.Runtime.CompilerServices.IAsyncStateMachine
	{
		.custom instance void [System.Runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
			01 00 00 00
		)
		.interfaceimpl type [System.Runtime]System.Runtime.CompilerServices.IAsyncStateMachine
			.custom instance void [System.Runtime]System.Runtime.CompilerServices.NullableAttribute::.ctor(uint8) = (
				01 00 00 00 00
			)

		// Fields
		.field public int32 '<>1__state'
		.field public valuetype [System.Runtime]System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<int32> '<>t__builder'
		.custom instance void [System.Runtime]System.Runtime.CompilerServices.NullableAttribute::.ctor(uint8) = (
			01 00 00 00 00
		)

		// Methods
		.method private final hidebysig newslot virtual 
			instance void MoveNext () cil managed 
		{
			.override method instance void [System.Runtime]System.Runtime.CompilerServices.IAsyncStateMachine::MoveNext()
			// Method begins at RVA 0x27c8
			// Header size: 12
			// Code size: 49 (0x31)
			.maxstack 2
			.locals init (
				[0] int32,
				[1] class [System.Runtime]System.Exception
			)

			.try
			{
				IL_0000: ldc.i4.s 42
				IL_0002: stloc.0
				IL_0003: leave.s IL_001c
			} // end .try
			catch [System.Runtime]System.Exception
			{
				IL_0005: stloc.1
				IL_0006: ldarg.0
				IL_0007: ldc.i4.s -2
				IL_0009: stfld int32 Experiment/'<SyncCompletingAsync>d__1'::'<>1__state'
				IL_000e: ldarg.0
				IL_000f: ldflda valuetype [System.Runtime]System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<int32> Experiment/'<SyncCompletingAsync>d__1'::'<>t__builder'
				IL_0014: ldloc.1
				IL_0015: call instance void valuetype [System.Runtime]System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<int32>::SetException(class [System.Runtime]System.Exception)
				IL_001a: leave.s IL_0030
			} // end handler

			IL_001c: ldarg.0
			IL_001d: ldc.i4.s -2
			IL_001f: stfld int32 Experiment/'<SyncCompletingAsync>d__1'::'<>1__state'
			IL_0024: ldarg.0
			IL_0025: ldflda valuetype [System.Runtime]System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<int32> Experiment/'<SyncCompletingAsync>d__1'::'<>t__builder'
			IL_002a: ldloc.0
			IL_002b: call instance void valuetype [System.Runtime]System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<int32>::SetResult(!0)

			IL_0030: ret
		} // end of method '<SyncCompletingAsync>d__1'::MoveNext

		.method private final hidebysig newslot virtual 
			instance void SetStateMachine (
				class [System.Runtime]System.Runtime.CompilerServices.IAsyncStateMachine stateMachine
			) cil managed 
		{
			.custom instance void [System.Runtime]System.Diagnostics.DebuggerHiddenAttribute::.ctor() = (
				01 00 00 00
			)
			.override method instance void [System.Runtime]System.Runtime.CompilerServices.IAsyncStateMachine::SetStateMachine(class [System.Runtime]System.Runtime.CompilerServices.IAsyncStateMachine)
			// Method begins at RVA 0x2818
			// Header size: 1
			// Code size: 13 (0xd)
			.maxstack 8

			IL_0000: ldarg.0
			IL_0001: ldflda valuetype [System.Runtime]System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<int32> Experiment/'<SyncCompletingAsync>d__1'::'<>t__builder'
			IL_0006: ldarg.1
			IL_0007: call instance void valuetype [System.Runtime]System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<int32>::SetStateMachine(class [System.Runtime]System.Runtime.CompilerServices.IAsyncStateMachine)
			IL_000c: ret
		} // end of method '<SyncCompletingAsync>d__1'::SetStateMachine

	} // end of class <SyncCompletingAsync>d__1


	// Methods
	.method public hidebysig static 
		class [System.Runtime]System.Threading.Tasks.Task RunAsync () cil managed 
	{
		.custom instance void [System.Runtime]System.Runtime.CompilerServices.AsyncStateMachineAttribute::.ctor(class [System.Runtime]System.Type) = (
			01 00 19 45 78 70 65 72 69 6d 65 6e 74 2b 3c 52
			75 6e 41 73 79 6e 63 3e 64 5f 5f 30 00 00
		)
		// Method begins at RVA 0x20a0
		// Header size: 12
		// Code size: 47 (0x2f)
		.maxstack 2
		.locals init (
			[0] valuetype Experiment/'<RunAsync>d__0'
		)

		IL_0000: ldloca.s 0
		IL_0002: call valuetype [System.Runtime]System.Runtime.CompilerServices.AsyncTaskMethodBuilder [System.Runtime]System.Runtime.CompilerServices.AsyncTaskMethodBuilder::Create()
		IL_0007: stfld valuetype [System.Runtime]System.Runtime.CompilerServices.AsyncTaskMethodBuilder Experiment/'<RunAsync>d__0'::'<>t__builder'
		IL_000c: ldloca.s 0
		IL_000e: ldc.i4.m1
		IL_000f: stfld int32 Experiment/'<RunAsync>d__0'::'<>1__state'
		IL_0014: ldloca.s 0
		IL_0016: ldflda valuetype [System.Runtime]System.Runtime.CompilerServices.AsyncTaskMethodBuilder Experiment/'<RunAsync>d__0'::'<>t__builder'
		IL_001b: ldloca.s 0
		IL_001d: call instance void [System.Runtime]System.Runtime.CompilerServices.AsyncTaskMethodBuilder::Start<valuetype Experiment/'<RunAsync>d__0'>(!!0&)
		IL_0022: ldloca.s 0
		IL_0024: ldflda valuetype [System.Runtime]System.Runtime.CompilerServices.AsyncTaskMethodBuilder Experiment/'<RunAsync>d__0'::'<>t__builder'
		IL_0029: call instance class [System.Runtime]System.Threading.Tasks.Task [System.Runtime]System.Runtime.CompilerServices.AsyncTaskMethodBuilder::get_Task()
		IL_002e: ret
	} // end of method Experiment::RunAsync

	.method private hidebysig static 
		class [System.Runtime]System.Threading.Tasks.Task`1<int32> SyncCompletingAsync () cil managed 
	{
		.custom instance void [System.Runtime]System.Runtime.CompilerServices.AsyncStateMachineAttribute::.ctor(class [System.Runtime]System.Type) = (
			01 00 24 45 78 70 65 72 69 6d 65 6e 74 2b 3c 53
			79 6e 63 43 6f 6d 70 6c 65 74 69 6e 67 41 73 79
			6e 63 3e 64 5f 5f 31 00 00
		)
		// Method begins at RVA 0x20dc
		// Header size: 12
		// Code size: 47 (0x2f)
		.maxstack 2
		.locals init (
			[0] valuetype Experiment/'<SyncCompletingAsync>d__1'
		)

		IL_0000: ldloca.s 0
		IL_0002: call valuetype [System.Runtime]System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<!0> valuetype [System.Runtime]System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<int32>::Create()
		IL_0007: stfld valuetype [System.Runtime]System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<int32> Experiment/'<SyncCompletingAsync>d__1'::'<>t__builder'
		IL_000c: ldloca.s 0
		IL_000e: ldc.i4.m1
		IL_000f: stfld int32 Experiment/'<SyncCompletingAsync>d__1'::'<>1__state'
		IL_0014: ldloca.s 0
		IL_0016: ldflda valuetype [System.Runtime]System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<int32> Experiment/'<SyncCompletingAsync>d__1'::'<>t__builder'
		IL_001b: ldloca.s 0
		IL_001d: call instance void valuetype [System.Runtime]System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<int32>::Start<valuetype Experiment/'<SyncCompletingAsync>d__1'>(!!0&)
		IL_0022: ldloca.s 0
		IL_0024: ldflda valuetype [System.Runtime]System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<int32> Experiment/'<SyncCompletingAsync>d__1'::'<>t__builder'
		IL_0029: call instance class [System.Runtime]System.Threading.Tasks.Task`1<!0> valuetype [System.Runtime]System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<int32>::get_Task()
		IL_002e: ret
	} // end of method Experiment::SyncCompletingAsync

	.method private hidebysig static 
		class [System.Runtime]System.Threading.Tasks.Task`1<int32> RealSuspensionAsync () cil managed 
	{
		.custom instance void [System.Runtime]System.Runtime.CompilerServices.AsyncStateMachineAttribute::.ctor(class [System.Runtime]System.Type) = (
			01 00 24 45 78 70 65 72 69 6d 65 6e 74 2b 3c 52
			65 61 6c 53 75 73 70 65 6e 73 69 6f 6e 41 73 79
			6e 63 3e 64 5f 5f 32 00 00
		)
		// Method begins at RVA 0x2118
		// Header size: 12
		// Code size: 47 (0x2f)
		.maxstack 2
		.locals init (
			[0] valuetype Experiment/'<RealSuspensionAsync>d__2'
		)

		IL_0000: ldloca.s 0
		IL_0002: call valuetype [System.Runtime]System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<!0> valuetype [System.Runtime]System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<int32>::Create()
		IL_0007: stfld valuetype [System.Runtime]System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<int32> Experiment/'<RealSuspensionAsync>d__2'::'<>t__builder'
		IL_000c: ldloca.s 0
		IL_000e: ldc.i4.m1
		IL_000f: stfld int32 Experiment/'<RealSuspensionAsync>d__2'::'<>1__state'
		IL_0014: ldloca.s 0
		IL_0016: ldflda valuetype [System.Runtime]System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<int32> Experiment/'<RealSuspensionAsync>d__2'::'<>t__builder'
		IL_001b: ldloca.s 0
		IL_001d: call instance void valuetype [System.Runtime]System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<int32>::Start<valuetype Experiment/'<RealSuspensionAsync>d__2'>(!!0&)
		IL_0022: ldloca.s 0
		IL_0024: ldflda valuetype [System.Runtime]System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<int32> Experiment/'<RealSuspensionAsync>d__2'::'<>t__builder'
		IL_0029: call instance class [System.Runtime]System.Threading.Tasks.Task`1<!0> valuetype [System.Runtime]System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<int32>::get_Task()
		IL_002e: ret
	} // end of method Experiment::RealSuspensionAsync

	.method private hidebysig static 
		class [System.Runtime]System.Threading.Tasks.Task AAsync () cil managed 
	{
		.custom instance void [System.Runtime]System.Runtime.CompilerServices.AsyncStateMachineAttribute::.ctor(class [System.Runtime]System.Type) = (
			01 00 17 45 78 70 65 72 69 6d 65 6e 74 2b 3c 41
			41 73 79 6e 63 3e 64 5f 5f 33 00 00
		)
		// Method begins at RVA 0x2154
		// Header size: 12
		// Code size: 47 (0x2f)
		.maxstack 2
		.locals init (
			[0] valuetype Experiment/'<AAsync>d__3'
		)

		IL_0000: ldloca.s 0
		IL_0002: call valuetype [System.Runtime]System.Runtime.CompilerServices.AsyncTaskMethodBuilder [System.Runtime]System.Runtime.CompilerServices.AsyncTaskMethodBuilder::Create()
		IL_0007: stfld valuetype [System.Runtime]System.Runtime.CompilerServices.AsyncTaskMethodBuilder Experiment/'<AAsync>d__3'::'<>t__builder'
		IL_000c: ldloca.s 0
		IL_000e: ldc.i4.m1
		IL_000f: stfld int32 Experiment/'<AAsync>d__3'::'<>1__state'
		IL_0014: ldloca.s 0
		IL_0016: ldflda valuetype [System.Runtime]System.Runtime.CompilerServices.AsyncTaskMethodBuilder Experiment/'<AAsync>d__3'::'<>t__builder'
		IL_001b: ldloca.s 0
		IL_001d: call instance void [System.Runtime]System.Runtime.CompilerServices.AsyncTaskMethodBuilder::Start<valuetype Experiment/'<AAsync>d__3'>(!!0&)
		IL_0022: ldloca.s 0
		IL_0024: ldflda valuetype [System.Runtime]System.Runtime.CompilerServices.AsyncTaskMethodBuilder Experiment/'<AAsync>d__3'::'<>t__builder'
		IL_0029: call instance class [System.Runtime]System.Threading.Tasks.Task [System.Runtime]System.Runtime.CompilerServices.AsyncTaskMethodBuilder::get_Task()
		IL_002e: ret
	} // end of method Experiment::AAsync

	.method private hidebysig static 
		class [System.Runtime]System.Threading.Tasks.Task BAsync () cil managed 
	{
		.custom instance void [System.Runtime]System.Runtime.CompilerServices.AsyncStateMachineAttribute::.ctor(class [System.Runtime]System.Type) = (
			01 00 17 45 78 70 65 72 69 6d 65 6e 74 2b 3c 42
			41 73 79 6e 63 3e 64 5f 5f 34 00 00
		)
		// Method begins at RVA 0x2190
		// Header size: 12
		// Code size: 47 (0x2f)
		.maxstack 2
		.locals init (
			[0] valuetype Experiment/'<BAsync>d__4'
		)

		IL_0000: ldloca.s 0
		IL_0002: call valuetype [System.Runtime]System.Runtime.CompilerServices.AsyncTaskMethodBuilder [System.Runtime]System.Runtime.CompilerServices.AsyncTaskMethodBuilder::Create()
		IL_0007: stfld valuetype [System.Runtime]System.Runtime.CompilerServices.AsyncTaskMethodBuilder Experiment/'<BAsync>d__4'::'<>t__builder'
		IL_000c: ldloca.s 0
		IL_000e: ldc.i4.m1
		IL_000f: stfld int32 Experiment/'<BAsync>d__4'::'<>1__state'
		IL_0014: ldloca.s 0
		IL_0016: ldflda valuetype [System.Runtime]System.Runtime.CompilerServices.AsyncTaskMethodBuilder Experiment/'<BAsync>d__4'::'<>t__builder'
		IL_001b: ldloca.s 0
		IL_001d: call instance void [System.Runtime]System.Runtime.CompilerServices.AsyncTaskMethodBuilder::Start<valuetype Experiment/'<BAsync>d__4'>(!!0&)
		IL_0022: ldloca.s 0
		IL_0024: ldflda valuetype [System.Runtime]System.Runtime.CompilerServices.AsyncTaskMethodBuilder Experiment/'<BAsync>d__4'::'<>t__builder'
		IL_0029: call instance class [System.Runtime]System.Threading.Tasks.Task [System.Runtime]System.Runtime.CompilerServices.AsyncTaskMethodBuilder::get_Task()
		IL_002e: ret
	} // end of method Experiment::BAsync

	.method private hidebysig static 
		class [System.Runtime]System.Threading.Tasks.Task CAsync () cil managed 
	{
		.custom instance void [System.Runtime]System.Runtime.CompilerServices.AsyncStateMachineAttribute::.ctor(class [System.Runtime]System.Type) = (
			01 00 17 45 78 70 65 72 69 6d 65 6e 74 2b 3c 43
			41 73 79 6e 63 3e 64 5f 5f 35 00 00
		)
		// Method begins at RVA 0x21cc
		// Header size: 12
		// Code size: 47 (0x2f)
		.maxstack 2
		.locals init (
			[0] valuetype Experiment/'<CAsync>d__5'
		)

		IL_0000: ldloca.s 0
		IL_0002: call valuetype [System.Runtime]System.Runtime.CompilerServices.AsyncTaskMethodBuilder [System.Runtime]System.Runtime.CompilerServices.AsyncTaskMethodBuilder::Create()
		IL_0007: stfld valuetype [System.Runtime]System.Runtime.CompilerServices.AsyncTaskMethodBuilder Experiment/'<CAsync>d__5'::'<>t__builder'
		IL_000c: ldloca.s 0
		IL_000e: ldc.i4.m1
		IL_000f: stfld int32 Experiment/'<CAsync>d__5'::'<>1__state'
		IL_0014: ldloca.s 0
		IL_0016: ldflda valuetype [System.Runtime]System.Runtime.CompilerServices.AsyncTaskMethodBuilder Experiment/'<CAsync>d__5'::'<>t__builder'
		IL_001b: ldloca.s 0
		IL_001d: call instance void [System.Runtime]System.Runtime.CompilerServices.AsyncTaskMethodBuilder::Start<valuetype Experiment/'<CAsync>d__5'>(!!0&)
		IL_0022: ldloca.s 0
		IL_0024: ldflda valuetype [System.Runtime]System.Runtime.CompilerServices.AsyncTaskMethodBuilder Experiment/'<CAsync>d__5'::'<>t__builder'
		IL_0029: call instance class [System.Runtime]System.Threading.Tasks.Task [System.Runtime]System.Runtime.CompilerServices.AsyncTaskMethodBuilder::get_Task()
		IL_002e: ret
	} // end of method Experiment::CAsync

} // end of class Experiment

