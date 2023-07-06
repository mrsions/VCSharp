namespace VCSharp
{
    public enum ILOpCode : ushort
    {
        ///<summary>
        /// 아무것도하지 않습니다 (작동 없음).
        /// (Base instruction) Do nothing (No operation).
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.nop?view=net-6.0"/>
        Nop = 0x0000,

        ///<summary>
        /// 브레이크 포인트에 도달했음을 디버거에게 알리십시오.
        /// (Base instruction) Inform a debugger that a breakpoint has been reached.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.break?view=net-6.0"/>
        Break = 0x0001,

        ///<summary>
        /// 인수 0을 스택에로드합니다.
        /// (Base instruction) Load argument 0 onto the stack.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.ldarg_0?view=net-6.0"/>
        Ldarg_0 = 0x0002,

        ///<summary>
        /// 인수 1을 스택에로드하십시오.
        /// (Base instruction) Load argument 1 onto the stack.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.ldarg_1?view=net-6.0"/>
        Ldarg_1 = 0x0003,

        ///<summary>
        /// 인수 2를 스택에로드합니다.
        /// (Base instruction) Load argument 2 onto the stack.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.ldarg_2?view=net-6.0"/>
        Ldarg_2 = 0x0004,

        ///<summary>
        /// 인수 3을 스택에로드합니다.
        /// (Base instruction) Load argument 3 onto the stack.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.ldarg_3?view=net-6.0"/>
        Ldarg_3 = 0x0005,

        ///<summary>
        /// 로컬 변수 0을 스택에로드하십시오.
        /// (Base instruction) Load local variable 0 onto stack.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.ldloc_0?view=net-6.0"/>
        Ldloc_0 = 0x0006,

        ///<summary>
        /// 로컬 변수 1을 스택에로드하십시오.
        /// (Base instruction) Load local variable 1 onto stack.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.ldloc_1?view=net-6.0"/>
        Ldloc_1 = 0x0007,

        ///<summary>
        /// 로컬 변수 2를 스택에로드하십시오.
        /// (Base instruction) Load local variable 2 onto stack.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.ldloc_2?view=net-6.0"/>
        Ldloc_2 = 0x0008,

        ///<summary>
        /// 로컬 변수 3을 스택에로드하십시오.
        /// (Base instruction) Load local variable 3 onto stack.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.ldloc_3?view=net-6.0"/>
        Ldloc_3 = 0x0009,

        ///<summary>
        /// 스택에서 로컬 변수 0으로 값을 팝하십시오.
        /// (Base instruction) Pop a value from stack into local variable 0.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.stloc_0?view=net-6.0"/>
        Stloc_0 = 0x000A,

        ///<summary>
        /// 스택에서 로컬 변수로 값을 팝하십시오 1.
        /// (Base instruction) Pop a value from stack into local variable 1.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.stloc_1?view=net-6.0"/>
        Stloc_1 = 0x000B,

        ///<summary>
        /// 스택에서 로컬 변수로 값을 팝하십시오.
        /// (Base instruction) Pop a value from stack into local variable 2.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.stloc_2?view=net-6.0"/>
        Stloc_2 = 0x000C,

        ///<summary>
        /// 스택에서 로컬 변수로 값을 팝하십시오.
        /// (Base instruction) Pop a value from stack into local variable 3.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.stloc_3?view=net-6.0"/>
        Stloc_3 = 0x000D,

        ///<summary>
        /// 로드 인수 번호 번호를 스택에 짧은 양식에 번호로 번호를 매겼습니다.
        /// (Base instruction) Load argument numbered num onto the stack, short form.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.ldarg_s?view=net-6.0"/>
        Ldarg_s = 0x000E,

        ///<summary>
        /// 인수 Argnum, 짧은 형태의 주소를 가져 오십시오.
        /// (Base instruction) Fetch the address of argument argNum, short form.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.ldarga_s?view=net-6.0"/>
        Ldarga_s = 0x000F,

        ///<summary>
        /// 인수 번호가 매수 된 NUM, 짧은 형식에 값을 저장하십시오.
        /// (Base instruction) Store value to the argument numbered num, short form.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.starg_s?view=net-6.0"/>
        Starg_s = 0x0010,

        ///<summary>
        /// 인덱스 indx의 로컬 변수를 스택에로드, 짧은 형태로로드하십시오.
        /// (Base instruction) Load local variable of index indx onto stack, short form.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.ldloc_s?view=net-6.0"/>
        Ldloc_s = 0x0011,

        ///<summary>
        /// 인덱스 indx, 짧은 형태를 갖는 로컬 변수의로드 주소.
        /// (Base instruction) Load address of local variable with index indx, short form.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.ldloca_s?view=net-6.0"/>
        Ldloca_s = 0x0012,

        ///<summary>
        /// 스택에서 로컬 변수 indx, 짧은 형태로 값을 팝하십시오.
        /// (Base instruction) Pop a value from stack into local variable indx, short form.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.stloc_s?view=net-6.0"/>
        Stloc_s = 0x0013,

        ///<summary>
        /// 스택에서 널 참조를 밉니다.
        /// (Base instruction) Push a null reference on the stack.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.ldnull?view=net-6.0"/>
        Ldnull = 0x0014,

        ///<summary>
        /// -1을 int32로 스택에 -1을 밀어 넣습니다 (ldc.i4.m1의 별칭).
        /// (Base instruction) Push -1 onto the stack as int32 (alias for ldc.i4.m1).
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.ldc_i4_m1?view=net-6.0"/>
        Ldc_i4_m1 = 0x0015,

        ///<summary>
        /// int32로 스택에 0을 밀어 넣습니다.
        /// (Base instruction) Push 0 onto the stack as int32.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.ldc_i4_0?view=net-6.0"/>
        Ldc_i4_0 = 0x0016,

        ///<summary>
        /// int32로 스택에 1을 밀어 넣습니다.
        /// (Base instruction) Push 1 onto the stack as int32.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.ldc_i4_1?view=net-6.0"/>
        Ldc_i4_1 = 0x0017,

        ///<summary>
        /// int32로 스택에 2를 밀어 넣습니다.
        /// (Base instruction) Push 2 onto the stack as int32.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.ldc_i4_2?view=net-6.0"/>
        Ldc_i4_2 = 0x0018,

        ///<summary>
        /// int32로 스택에 3을 밀어 넣습니다.
        /// (Base instruction) Push 3 onto the stack as int32.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.ldc_i4_3?view=net-6.0"/>
        Ldc_i4_3 = 0x0019,

        ///<summary>
        /// 4를 int32로 스택에 밀어 넣습니다.
        /// (Base instruction) Push 4 onto the stack as int32.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.ldc_i4_4?view=net-6.0"/>
        Ldc_i4_4 = 0x001A,

        ///<summary>
        /// 5를 int32로 스택에 밀어 넣습니다.
        /// (Base instruction) Push 5 onto the stack as int32.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.ldc_i4_5?view=net-6.0"/>
        Ldc_i4_5 = 0x001B,

        ///<summary>
        /// 6을 int32로 스택에 밀어 넣습니다.
        /// (Base instruction) Push 6 onto the stack as int32.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.ldc_i4_6?view=net-6.0"/>
        Ldc_i4_6 = 0x001C,

        ///<summary>
        /// 7을 int32로 스택에 밀어 넣습니다.
        /// (Base instruction) Push 7 onto the stack as int32.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.ldc_i4_7?view=net-6.0"/>
        Ldc_i4_7 = 0x001D,

        ///<summary>
        /// 8을 int32로 스택에 밀어 넣습니다.
        /// (Base instruction) Push 8 onto the stack as int32.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.ldc_i4_8?view=net-6.0"/>
        Ldc_i4_8 = 0x001E,

        ///<summary>
        /// 짧은 형태 인 int32로 스택에 NUM을 밀어 넣으십시오.
        /// (Base instruction) Push num onto the stack as int32, short form.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.ldc_i4_s?view=net-6.0"/>
        Ldc_i4_s = 0x001F,

        ///<summary>
        /// 유형 int32의 NUM을 int32로 스택에 밀어 넣습니다.
        /// (Base instruction) Push num of type int32 onto the stack as int32.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.ldc_i4?view=net-6.0"/>
        Ldc_i4 = 0x0020,

        ///<summary>
        /// 유형 int64의 NUM을 int64로 스택에 푸시하십시오.
        /// (Base instruction) Push num of type int64 onto the stack as int64.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.ldc_i8?view=net-6.0"/>
        Ldc_i8 = 0x0021,

        ///<summary>
        /// Float32 유형의 NUM을 F로 스택에 밀어 넣습니다.
        /// (Base instruction) Push num of type float32 onto the stack as F.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.ldc_r4?view=net-6.0"/>
        Ldc_r4 = 0x0022,

        ///<summary>
        /// Float64 유형의 Num을 F로 스택에 밀어 넣습니다.
        /// (Base instruction) Push num of type float64 onto the stack as F.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.ldc_r8?view=net-6.0"/>
        Ldc_r8 = 0x0023,

        ///<summary>
        /// 스택 상단의 값을 복제하십시오.
        /// (Base instruction) Duplicate the value on the top of the stack.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.dup?view=net-6.0"/>
        Dup = 0x0025,

        ///<summary>
        /// 스택에서 팝 값.
        /// (Base instruction) Pop value from the stack.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.pop?view=net-6.0"/>
        Pop = 0x0026,

        ///<summary>
        /// 현재 방법을 종료하고 지정된 방법으로 이동하십시오.
        /// (Base instruction) Exit current method and jump to the specified method.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.jmp?view=net-6.0"/>
        Jmp = 0x0027,

        ///<summary>
        /// 메소드로 설명 된 통화 방법.
        /// (Base instruction) Call method described by method.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.call?view=net-6.0"/>
        Call = 0x0028,

        ///<summary>
        /// CallitedEscr이 설명한 인수와 함께 스택에 표시된 통화 방법.
        /// (Base instruction) Call method indicated on the stack with arguments described by callsitedescr.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.calli?view=net-6.0"/>
        Calli = 0x0029,

        ///<summary>
        /// 아마도 값으로 메소드에서 돌아갑니다.
        /// (Base instruction) Return from method, possibly with a value.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.ret?view=net-6.0"/>
        Ret = 0x002A,

        ///<summary>
        /// 대상, 짧은 형태.
        /// (Base instruction) Branch to target, short form.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.br_s?view=net-6.0"/>
        Br_s = 0x002B,

        ///<summary>
        /// 값이 0 인 경우 대상 지점 (Brfalse.s의 별칭), 짧은 형태.
        /// (Base instruction) Branch to target if value is zero (alias for brfalse.s), short form.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.brfalse_s?view=net-6.0"/>
        Brfalse_s = 0x002C,

        ///<summary>
        /// 값이 0이 아닌 경우 (true), 짧은 형식 인 경우 대상 지점.
        /// (Base instruction) Branch to target if value is non-zero (true), short form.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.brtrue_s?view=net-6.0"/>
        Brtrue_s = 0x002D,

        ///<summary>
        /// 동일하고 짧은 형태 인 경우 대상 지점.
        /// (Base instruction) Branch to target if equal, short form.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.beq_s?view=net-6.0"/>
        Beq_s = 0x002E,

        ///<summary>
        /// 짧은 형태보다 크거나 같은 경우 대상 지점.
        /// (Base instruction) Branch to target if greater than or equal to, short form.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.bge_s?view=net-6.0"/>
        Bge_s = 0x002F,

        ///<summary>
        /// 짧은 형태보다 더 큰 경우 대상 지점.
        /// (Base instruction) Branch to target if greater than, short form.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.bgt_s?view=net-6.0"/>
        Bgt_s = 0x0030,

        ///<summary>
        /// 짧은 형태보다 적거나 동일 한 경우 대상 지점.
        /// (Base instruction) Branch to target if less than or equal to, short form.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.ble_s?view=net-6.0"/>
        Ble_s = 0x0031,

        ///<summary>
        /// 짧은 형태보다 작은 경우 대상 지점.
        /// (Base instruction) Branch to target if less than, short form.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.blt_s?view=net-6.0"/>
        Blt_s = 0x0032,

        ///<summary>
        /// 불평등하거나 변하지 않은 짧은 형태 인 경우 대상 지점.
        /// (Base instruction) Branch to target if unequal or unordered, short form.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.bne_un_s?view=net-6.0"/>
        Bne_un_s = 0x0033,

        ///<summary>
        /// (서명되지 않거나 정렬되지 않은), 짧은 형태보다 크거나 같은 경우 대상 지점.
        /// (Base instruction) Branch to target if greater than or equal to (unsigned or unordered), short form.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.bge_un_s?view=net-6.0"/>
        Bge_un_s = 0x0034,

        ///<summary>
        /// (서명되지 않거나 정렬되지 않은), 짧은 형태보다 더 큰 경우 대상 지점.
        /// (Base instruction) Branch to target if greater than (unsigned or unordered), short form.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.bgt_un_s?view=net-6.0"/>
        Bgt_un_s = 0x0035,

        ///<summary>
        /// (서명되지 않거나 정렬되지 않은), 짧은 형태보다 적은 경우 대상 지점.
        /// (Base instruction) Branch to target if less than or equal to (unsigned or unordered), short form.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.ble_un_s?view=net-6.0"/>
        Ble_un_s = 0x0036,

        ///<summary>
        /// (서명되지 않거나 정렬되지 않은), 짧은 형태보다 적은 경우 대상 지점.
        /// (Base instruction) Branch to target if less than (unsigned or unordered), short form.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.blt_un_s?view=net-6.0"/>
        Blt_un_s = 0x0037,

        ///<summary>
        /// 대상 지점.
        /// (Base instruction) Branch to target.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.br?view=net-6.0"/>
        Br = 0x0038,

        ///<summary>
        /// 값이 0 인 경우 대상 지점 (BRFALSE의 별칭).
        /// (Base instruction) Branch to target if value is zero (alias for brfalse).
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.brfalse?view=net-6.0"/>
        Brfalse = 0x0039,

        ///<summary>
        /// 값이 0이 아닌 경우 대상 지점 (true).
        /// (Base instruction) Branch to target if value is non-zero (true).
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.brtrue?view=net-6.0"/>
        Brtrue = 0x003A,

        ///<summary>
        /// 동일하면 대상을 지정합니다.
        /// (Base instruction) Branch to target if equal.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.beq?view=net-6.0"/>
        Beq = 0x003B,

        ///<summary>
        /// 더 크거나 같은 경우 대상 지점.
        /// (Base instruction) Branch to target if greater than or equal to.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.bge?view=net-6.0"/>
        Bge = 0x003C,

        ///<summary>
        /// 더 큰 경우 대상 지점.
        /// (Base instruction) Branch to target if greater than.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.bgt?view=net-6.0"/>
        Bgt = 0x003D,

        ///<summary>
        /// 보다 적거나 같은 경우 대상 지점.
        /// (Base instruction) Branch to target if less than or equal to.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.ble?view=net-6.0"/>
        Ble = 0x003E,

        ///<summary>
        /// 더 적은 경우 대상 지점.
        /// (Base instruction) Branch to target if less than.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.blt?view=net-6.0"/>
        Blt = 0x003F,

        ///<summary>
        /// 불평등하거나 변하지 않은 경우 지점을 대상으로합니다.
        /// (Base instruction) Branch to target if unequal or unordered.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.bne_un?view=net-6.0"/>
        Bne_un = 0x0040,

        ///<summary>
        /// (서명되지 않거나 정렬되지 않은)보다 크거나 같은 경우 대상 지점.
        /// (Base instruction) Branch to target if greater than or equal to (unsigned or unordered).
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.bge_un?view=net-6.0"/>
        Bge_un = 0x0041,

        ///<summary>
        /// (서명되지 않거나 정렬되지 않은)보다 큰 경우 대상 지점.
        /// (Base instruction) Branch to target if greater than (unsigned or unordered).
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.bgt_un?view=net-6.0"/>
        Bgt_un = 0x0042,

        ///<summary>
        /// (서명되지 않거나 정렬되지 않은)보다 적은 경우 대상 지점.
        /// (Base instruction) Branch to target if less than or equal to (unsigned or unordered).
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.ble_un?view=net-6.0"/>
        Ble_un = 0x0043,

        ///<summary>
        /// (서명되지 않거나 정렬되지 않은)보다 적은 경우 대상 지점.
        /// (Base instruction) Branch to target if less than (unsigned or unordered).
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.blt_un?view=net-6.0"/>
        Blt_un = 0x0044,

        ///<summary>
        /// N 값 중 하나로 이동하십시오.
        /// (Base instruction) Jump to one of n values.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.switch?view=net-6.0"/>
        Switch = 0x0045,

        ///<summary>
        /// 스택에서 int32로 유형 int8의 간접로드 값.
        /// (Base instruction) Indirect load value of type int8 as int32 on the stack.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.ldind_i1?view=net-6.0"/>
        Ldind_i1 = 0x0046,

        ///<summary>
        /// 스택에서 int32로 유형이없는 int8의 간접로드 값.
        /// (Base instruction) Indirect load value of type unsigned int8 as int32 on the stack.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.ldind_u1?view=net-6.0"/>
        Ldind_u1 = 0x0047,

        ///<summary>
        /// 스택에서 int32로 유형 int16의 간접 하중 값.
        /// (Base instruction) Indirect load value of type int16 as int32 on the stack.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.ldind_i2?view=net-6.0"/>
        Ldind_i2 = 0x0048,

        ///<summary>
        /// 스택에서 int32로 유형이없는 int16의 간접로드 값.
        /// (Base instruction) Indirect load value of type unsigned int16 as int32 on the stack.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.ldind_u2?view=net-6.0"/>
        Ldind_u2 = 0x0049,

        ///<summary>
        /// 스택에서 int32로 유형 int32의 간접로드 값.
        /// (Base instruction) Indirect load value of type int32 as int32 on the stack.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.ldind_i4?view=net-6.0"/>
        Ldind_i4 = 0x004A,

        ///<summary>
        /// 스택에서 int32로 유형이없는 int32의 간접로드 값.
        /// (Base instruction) Indirect load value of type unsigned int32 as int32 on the stack.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.ldind_u4?view=net-6.0"/>
        Ldind_u4 = 0x004B,

        ///<summary>
        /// 스택에서 int64로 유형이없는 int64의 간접 하중 값 (ldind.i8의 별칭).
        /// (Base instruction) Indirect load value of type unsigned int64 as int64 on the stack (alias for ldind.i8).
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.ldind_i8?view=net-6.0"/>
        Ldind_i8 = 0x004C,

        ///<summary>
        /// 스택에서 네이티브 int로서 유형의 기본 int의 간접로드 값.
        /// (Base instruction) Indirect load value of type native int as native int on the stack.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.ldind_i?view=net-6.0"/>
        Ldind_i = 0x004D,

        ///<summary>
        /// 스택에서 Float32 유형의 간접로드 값.
        /// (Base instruction) Indirect load value of type float32 as F on the stack.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.ldind_r4?view=net-6.0"/>
        Ldind_r4 = 0x004E,

        ///<summary>
        /// 스택에서 float64 유형의 간접로드 값.
        /// (Base instruction) Indirect load value of type float64 as F on the stack.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.ldind_r8?view=net-6.0"/>
        Ldind_r8 = 0x004F,

        ///<summary>
        /// 스택에서 O와 같이 유형 객체 ref의 간접로드 값.
        /// (Base instruction) Indirect load value of type object ref as O on the stack.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.ldind_ref?view=net-6.0"/>
        Ldind_ref = 0x0050,

        ///<summary>
        /// 유형 객체 ref (type o)의 값을 주소에서 메모리에 저장합니다.
        /// (Base instruction) Store value of type object ref (type O) into memory at address.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.stind_ref?view=net-6.0"/>
        Stind_ref = 0x0051,

        ///<summary>
        /// 유형 int8의 값을 주소에서 메모리에 저장하십시오.
        /// (Base instruction) Store value of type int8 into memory at address.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.stind_i1?view=net-6.0"/>
        Stind_i1 = 0x0052,

        ///<summary>
        /// 유형 int16의 값을 주소에서 메모리에 저장하십시오.
        /// (Base instruction) Store value of type int16 into memory at address.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.stind_i2?view=net-6.0"/>
        Stind_i2 = 0x0053,

        ///<summary>
        /// 유형 int32의 값을 주소에서 메모리에 저장하십시오.
        /// (Base instruction) Store value of type int32 into memory at address.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.stind_i4?view=net-6.0"/>
        Stind_i4 = 0x0054,

        ///<summary>
        /// 유형 int64의 값을 주소에서 메모리에 저장하십시오.
        /// (Base instruction) Store value of type int64 into memory at address.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.stind_i8?view=net-6.0"/>
        Stind_i8 = 0x0055,

        ///<summary>
        /// Float32 유형 값을 주소에서 메모리에 저장하십시오.
        /// (Base instruction) Store value of type float32 into memory at address.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.stind_r4?view=net-6.0"/>
        Stind_r4 = 0x0056,

        ///<summary>
        /// Float64 유형의 값을 주소에서 메모리에 저장합니다.
        /// (Base instruction) Store value of type float64 into memory at address.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.stind_r8?view=net-6.0"/>
        Stind_r8 = 0x0057,

        ///<summary>
        /// 새 값을 반환하여 두 값을 추가하십시오.
        /// (Base instruction) Add two values, returning a new value.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.add?view=net-6.0"/>
        Add = 0x0058,

        ///<summary>
        /// value1에서 value2를 빼고 새 값을 반환합니다.
        /// (Base instruction) Subtract value2 from value1, returning a new value.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.sub?view=net-6.0"/>
        Sub = 0x0059,

        ///<summary>
        /// 값을 곱합니다.
        /// (Base instruction) Multiply values.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.mul?view=net-6.0"/>
        Mul = 0x005A,

        ///<summary>
        /// 몫 또는 부동 소수점 결과를 반환하기 위해 두 값을 나누십시오.
        /// (Base instruction) Divide two values to return a quotient or floating-point result.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.div?view=net-6.0"/>
        Div = 0x005B,

        ///<summary>
        /// 서명되지 않은 두 가지 값을 나누고 몫을 반환합니다.
        /// (Base instruction) Divide two values, unsigned, returning a quotient.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.div_un?view=net-6.0"/>
        Div_un = 0x005C,

        ///<summary>
        /// 한 값을 다른 값으로 나눌 때 나머지.
        /// (Base instruction) Remainder when dividing one value by another.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.rem?view=net-6.0"/>
        Rem = 0x005D,

        ///<summary>
        /// 하나의 서명되지 않은 값을 다른 사람으로 나눌 때 나머지.
        /// (Base instruction) Remainder when dividing one unsigned value by another.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.rem_un?view=net-6.0"/>
        Rem_un = 0x005E,

        ///<summary>
        /// 비트와 두 가지 적분 값 중 하나는 적분 값을 반환합니다.
        /// (Base instruction) Bitwise AND of two integral values, returns an integral value.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.and?view=net-6.0"/>
        And = 0x005F,

        ///<summary>
        /// 비트 또는 두 정수 값 중 정수를 반환합니다.
        /// (Base instruction) Bitwise OR of two integer values, returns an integer.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.or?view=net-6.0"/>
        Or = 0x0060,

        ///<summary>
        /// 정수 값의 Bitwise Xor는 정수를 반환합니다.
        /// (Base instruction) Bitwise XOR of integer values, returns an integer.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.xor?view=net-6.0"/>
        Xor = 0x0061,

        ///<summary>
        /// 정수를 왼쪽으로 바꾸고 (0에서 이동) 정수를 반환하십시오.
        /// (Base instruction) Shift an integer left (shifting in zeros), return an integer.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.shl?view=net-6.0"/>
        Shl = 0x0062,

        ///<summary>
        /// 정수를 오른쪽으로 바꾸고 (간판의 이동) 정수를 반환하십시오.
        /// (Base instruction) Shift an integer right (shift in sign), return an integer.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.shr?view=net-6.0"/>
        Shr = 0x0063,

        ///<summary>
        /// 정수를 오른쪽으로 바꾸고 (0에서 이동) 정수를 반환하십시오.
        /// (Base instruction) Shift an integer right (shift in zero), return an integer.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.shr_un?view=net-6.0"/>
        Shr_un = 0x0064,

        ///<summary>
        /// 가치를 부정하십시오.
        /// (Base instruction) Negate value.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.neg?view=net-6.0"/>
        Neg = 0x0065,

        ///<summary>
        /// 비트 보완.
        /// (Base instruction) Bitwise complement.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.not?view=net-6.0"/>
        Not = 0x0066,

        ///<summary>
        /// int8로 변환하여 스택에서 int32를 누릅니다.
        /// (Base instruction) Convert to int8, pushing int32 on stack.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.conv_i1?view=net-6.0"/>
        Conv_i1 = 0x0067,

        ///<summary>
        /// int16으로 변환하여 스택에서 int32를 밀어냅니다.
        /// (Base instruction) Convert to int16, pushing int32 on stack.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.conv_i2?view=net-6.0"/>
        Conv_i2 = 0x0068,

        ///<summary>
        /// int32로 변환하여 스택에서 int32를 밀어냅니다.
        /// (Base instruction) Convert to int32, pushing int32 on stack.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.conv_i4?view=net-6.0"/>
        Conv_i4 = 0x0069,

        ///<summary>
        /// int64로 변환하고 스택에서 int64를 밀어 넣습니다.
        /// (Base instruction) Convert to int64, pushing int64 on stack.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.conv_i8?view=net-6.0"/>
        Conv_i8 = 0x006A,

        ///<summary>
        /// 플로트 32로 변환하여 스택에서 f를 누릅니다.
        /// (Base instruction) Convert to float32, pushing F on stack.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.conv_r4?view=net-6.0"/>
        Conv_r4 = 0x006B,

        ///<summary>
        /// 플로트 64로 변환하여 스택에서 f를 누릅니다.
        /// (Base instruction) Convert to float64, pushing F on stack.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.conv_r8?view=net-6.0"/>
        Conv_r8 = 0x006C,

        ///<summary>
        /// 서명되지 않은 int32로 변환하여 Stack에서 int32를 밀어 넣습니다.
        /// (Base instruction) Convert to unsigned int32, pushing int32 on stack.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.conv_u4?view=net-6.0"/>
        Conv_u4 = 0x006D,

        ///<summary>
        /// 서명되지 않은 int64로 변환하여 Stack에서 int64를 밀어 넣습니다.
        /// (Base instruction) Convert to unsigned int64, pushing int64 on stack.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.conv_u8?view=net-6.0"/>
        Conv_u8 = 0x006E,

        ///<summary>
        /// 객체와 관련된 메소드를 호출하십시오.
        /// (Object model instruction) Call a method associated with an object.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.callvirt?view=net-6.0"/>
        Callvirt = 0x006F,

        ///<summary>
        /// SRC에서 Dest로 값 유형을 복사하십시오.
        /// (Object model instruction) Copy a value type from src to dest.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.cpobj?view=net-6.0"/>
        Cpobj = 0x0070,

        ///<summary>
        /// 주소 SRC에 저장된 값을 스택에 복사하십시오.
        /// (Object model instruction) Copy the value stored at address src to the stack.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.ldobj?view=net-6.0"/>
        Ldobj = 0x0071,

        ///<summary>
        /// 문자 그대로 문자열 객체를 누릅니다.
        /// (Object model instruction) Push a string object for the literal string.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.ldstr?view=net-6.0"/>
        Ldstr = 0x0072,

        ///<summary>
        /// 초기화되지 않은 객체 또는 값 유형을 할당하고 ctor를 호출하십시오.
        /// (Object model instruction) Allocate an uninitialized object or value type and call ctor.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.newobj?view=net-6.0"/>
        Newobj = 0x0073,

        ///<summary>
        /// 수업에 OBJ를 캐스팅하십시오.
        /// (Object model instruction) Cast obj to class.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.castclass?view=net-6.0"/>
        Castclass = 0x0074,

        ///<summary>
        /// OBJ가 클래스 인스턴스인지, 널 리턴 또는 해당 클래스 또는 인터페이스의 인스턴스인지 테스트하십시오.
        /// (Object model instruction) Test if obj is an instance of class, returning null or an instance of that class or interface.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.isinst?view=net-6.0"/>
        Isinst = 0x0075,

        ///<summary>
        /// 서명되지 않은 정수를 플로팅 포인트로 변환하여 스택에 f를 밀어냅니다.
        /// (Base instruction) Convert unsigned integer to floating-point, pushing F on stack.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.conv_r_un?view=net-6.0"/>
        Conv_r_un = 0x0076,

        ///<summary>
        /// Boxed 표현, OBJ에서 값 유형을 추출하고 제어 된 무성성 관리 포인터를 스택 상단으로 밀어 넣습니다.
        /// (Object model instruction) Extract a value-type from obj, its boxed representation, and push a controlled-mutability managed pointer to it to the top of the stack.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.unbox?view=net-6.0"/>
        Unbox = 0x0079,

        ///<summary>
        /// 예외를 던져.
        /// (Object model instruction) Throw an exception.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.throw?view=net-6.0"/>
        Throw = 0x007A,

        ///<summary>
        /// 객체 (또는 값 유형) obj 필드 값을 스택으로 밀어 넣습니다.
        /// (Object model instruction) Push the value of field of object (or value type) obj, onto the stack.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.ldfld?view=net-6.0"/>
        Ldfld = 0x007B,

        ///<summary>
        /// 스택에서 물체 obj 필드 주소를 밀어 넣으십시오.
        /// (Object model instruction) Push the address of field of object obj on the stack.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.ldflda?view=net-6.0"/>
        Ldflda = 0x007C,

        ///<summary>
        /// OBJ의 필드 값을 값으로 바꾸십시오.
        /// (Object model instruction) Replace the value of field of the object obj with value.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.stfld?view=net-6.0"/>
        Stfld = 0x007D,

        ///<summary>
        /// 스택에서 정적 필드의 값을 누릅니다.
        /// (Object model instruction) Push the value of the static field on the stack.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.ldsfld?view=net-6.0"/>
        Ldsfld = 0x007E,

        ///<summary>
        /// 정적 필드 필드의 주소를 스택에 밀어 넣으십시오.
        /// (Object model instruction) Push the address of the static field, field, on the stack.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.ldsflda?view=net-6.0"/>
        Ldsflda = 0x007F,

        ///<summary>
        /// 정적 필드의 값을 Val로 교체하십시오.
        /// (Object model instruction) Replace the value of the static field with val.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.stsfld?view=net-6.0"/>
        Stsfld = 0x0080,

        ///<summary>
        /// Type Typetok의 값을 주소에 저장하십시오.
        /// (Object model instruction) Store a value of type typeTok at an address.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.stobj?view=net-6.0"/>
        Stobj = 0x0081,

        ///<summary>
        /// 서명되지 않은 int8 (int32로 스택에서)으로 변환하고 오버플로에 예외를 던집니다.
        /// (Base instruction) Convert unsigned to an int8 (on the stack as int32) and throw an exception on overflow.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.conv_ovf_i1_un?view=net-6.0"/>
        Conv_ovf_i1_un = 0x0082,

        ///<summary>
        /// 서명되지 않은 int16 (int32로 스택에서)으로 변환하고 오버플로에 예외를 던집니다.
        /// (Base instruction) Convert unsigned to an int16 (on the stack as int32) and throw an exception on overflow.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.conv_ovf_i2_un?view=net-6.0"/>
        Conv_ovf_i2_un = 0x0083,

        ///<summary>
        /// 서명되지 않은 int32 (int32로 스택에서)로 변환하고 오버플로에 예외를 던집니다.
        /// (Base instruction) Convert unsigned to an int32 (on the stack as int32) and throw an exception on overflow.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.conv_ovf_i4_un?view=net-6.0"/>
        Conv_ovf_i4_un = 0x0084,

        ///<summary>
        /// 서명되지 않은 int64 (int64로 스택에서)로 변환하고 오버플로에 예외를 던집니다.
        /// (Base instruction) Convert unsigned to an int64 (on the stack as int64) and throw an exception on overflow.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.conv_ovf_i8_un?view=net-6.0"/>
        Conv_ovf_i8_un = 0x0085,

        ///<summary>
        /// 서명되지 않은 int8 (int32로 스택에서)으로 변환하고 오버플로에 예외를 던집니다.
        /// (Base instruction) Convert unsigned to an unsigned int8 (on the stack as int32) and throw an exception on overflow.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.conv_ovf_u1_un?view=net-6.0"/>
        Conv_ovf_u1_un = 0x0086,

        ///<summary>
        /// 서명되지 않은 int16 (int32로 스택에서)으로 변환하고 오버플로에 예외를 던집니다.
        /// (Base instruction) Convert unsigned to an unsigned int16 (on the stack as int32) and throw an exception on overflow.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.conv_ovf_u2_un?view=net-6.0"/>
        Conv_ovf_u2_un = 0x0087,

        ///<summary>
        /// 서명되지 않은 int32 (int32로 스택에서)로 변환하고 오버플로에 예외를 던집니다.
        /// (Base instruction) Convert unsigned to an unsigned int32 (on the stack as int32) and throw an exception on overflow.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.conv_ovf_u4_un?view=net-6.0"/>
        Conv_ovf_u4_un = 0x0088,

        ///<summary>
        /// 서명되지 않은 int64 (int64로 스택에서)로 변환하고 오버플로에 예외를 던집니다.
        /// (Base instruction) Convert unsigned to an unsigned int64 (on the stack as int64) and throw an exception on overflow.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.conv_ovf_u8_un?view=net-6.0"/>
        Conv_ovf_u8_un = 0x0089,

        ///<summary>
        /// 서명되지 않은 int (스택에서 네이티브 INT)로 변환하고 오버플로에 예외를 던집니다.
        /// (Base instruction) Convert unsigned to a native int (on the stack as native int) and throw an exception on overflow.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.conv_ovf_i_un?view=net-6.0"/>
        Conv_ovf_i_un = 0x008A,

        ///<summary>
        /// 서명되지 않은 서명되지 않은 int (스택에서 기본 INT)로 변환하고 오버플로에 예외를 던집니다.
        /// (Base instruction) Convert unsigned to a native unsigned int (on the stack as native int) and throw an exception on overflow.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.conv_ovf_u_un?view=net-6.0"/>
        Conv_ovf_u_un = 0x008B,

        ///<summary>
        /// 박스 가능한 값을 상자 형태로 변환하십시오.
        /// (Object model instruction) Convert a boxable value to its boxed form.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.box?view=net-6.0"/>
        Box = 0x008C,

        ///<summary>
        /// Etype 유형의 요소로 새 배열을 만듭니다.
        /// (Object model instruction) Create a new array with elements of type etype.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.newarr?view=net-6.0"/>
        Newarr = 0x008D,

        ///<summary>
        /// 스택에서 배열의 길이 (유형 기본 부호없는 int)를 밀어 넣습니다.
        /// (Object model instruction) Push the length (of type native unsigned int) of array on the stack.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.ldlen?view=net-6.0"/>
        Ldlen = 0x008E,

        ///<summary>
        /// 인덱스의 요소 주소를 스택 상단에로드하십시오.
        /// (Object model instruction) Load the address of element at index onto the top of the stack.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.ldelema?view=net-6.0"/>
        Ldelema = 0x008F,

        ///<summary>
        /// 인덱스에서 int8 유형의 요소를 int32로 스택의 상단에로드하십시오.
        /// (Object model instruction) Load the element with type int8 at index onto the top of the stack as an int32.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.ldelem_i1?view=net-6.0"/>
        Ldelem_i1 = 0x0090,

        ///<summary>
        /// 인덱스에서 유형이없는 int8을 int32로 스택의 상단에로드하십시오.
        /// (Object model instruction) Load the element with type unsigned int8 at index onto the top of the stack as an int32.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.ldelem_u1?view=net-6.0"/>
        Ldelem_u1 = 0x0091,

        ///<summary>
        /// 인덱스에서 int16 유형의 요소를 int32로 스택의 상단에로드하십시오.
        /// (Object model instruction) Load the element with type int16 at index onto the top of the stack as an int32.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.ldelem_i2?view=net-6.0"/>
        Ldelem_i2 = 0x0092,

        ///<summary>
        /// 인덱스에서 유형이없는 int16을 int32로 스택의 상단에로드하십시오.
        /// (Object model instruction) Load the element with type unsigned int16 at index onto the top of the stack as an int32.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.ldelem_u2?view=net-6.0"/>
        Ldelem_u2 = 0x0093,

        ///<summary>
        /// 인덱스에서 int32 유형으로 요소를 int32로 스택의 상단에로드하십시오.
        /// (Object model instruction) Load the element with type int32 at index onto the top of the stack as an int32.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.ldelem_i4?view=net-6.0"/>
        Ldelem_i4 = 0x0094,

        ///<summary>
        /// 인덱스에서 유형이없는 int32 유형의 요소를 int32로 스택의 상단에로드하십시오.
        /// (Object model instruction) Load the element with type unsigned int32 at index onto the top of the stack as an int32.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.ldelem_u4?view=net-6.0"/>
        Ldelem_u4 = 0x0095,

        ///<summary>
        /// 인덱스에서 유형이없는 int64를 int64 (ldelem.i8의 별칭)로 스택 상단에 유형으로로드하십시오.
        /// (Object model instruction) Load the element with type unsigned int64 at index onto the top of the stack as an int64 (alias for ldelem.i8).
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.ldelem_i8?view=net-6.0"/>
        Ldelem_i8 = 0x0096,

        ///<summary>
        /// 인덱스에서 유형의 네이티브 int가있는 요소를 스택의 상단에 기본 INT로로드하십시오.
        /// (Object model instruction) Load the element with type native int at index onto the top of the stack as a native int.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.ldelem_i?view=net-6.0"/>
        Ldelem_i = 0x0097,

        ///<summary>
        /// 인덱스에서 float32 유형으로 요소를 스택의 상단에 F로로드하십시오.
        /// (Object model instruction) Load the element with type float32 at index onto the top of the stack as an F.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.ldelem_r4?view=net-6.0"/>
        Ldelem_r4 = 0x0098,

        ///<summary>
        /// 인덱스에서 float64 유형으로 요소를 스택의 상단에 F로로드하십시오.
        /// (Object model instruction) Load the element with type float64 at index onto the top of the stack as an F.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.ldelem_r8?view=net-6.0"/>
        Ldelem_r8 = 0x0099,

        ///<summary>
        /// 인덱스의 요소를 O로 스택의 상단에로드하십시오. O의 유형은 CIL 스택에 푸시 된 배열의 요소 유형과 동일합니다.
        /// (Object model instruction) Load the element at index onto the top of the stack as an O. The type of the O is the same as the element type of the array pushed on the CIL stack.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.ldelem_ref?view=net-6.0"/>
        Ldelem_ref = 0x009A,

        ///<summary>
        /// 인덱스의 배열 요소를 스택의 i 값으로 바꾸십시오.
        /// (Object model instruction) Replace array element at index with the i value on the stack.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.stelem_i?view=net-6.0"/>
        Stelem_i = 0x009B,

        ///<summary>
        /// 인덱스의 배열 요소를 스택의 int8 값으로 바꾸십시오.
        /// (Object model instruction) Replace array element at index with the int8 value on the stack.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.stelem_i1?view=net-6.0"/>
        Stelem_i1 = 0x009C,

        ///<summary>
        /// 인덱스의 배열 요소를 스택의 int16 값으로 바꾸십시오.
        /// (Object model instruction) Replace array element at index with the int16 value on the stack.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.stelem_i2?view=net-6.0"/>
        Stelem_i2 = 0x009D,

        ///<summary>
        /// 인덱스의 배열 요소를 스택의 int32 값으로 바꾸십시오.
        /// (Object model instruction) Replace array element at index with the int32 value on the stack.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.stelem_i4?view=net-6.0"/>
        Stelem_i4 = 0x009E,

        ///<summary>
        /// 인덱스의 배열 요소를 스택의 int64 값으로 바꾸십시오.
        /// (Object model instruction) Replace array element at index with the int64 value on the stack.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.stelem_i8?view=net-6.0"/>
        Stelem_i8 = 0x009F,

        ///<summary>
        /// 인덱스의 배열 요소를 스택의 float32 값으로 바꾸십시오.
        /// (Object model instruction) Replace array element at index with the float32 value on the stack.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.stelem_r4?view=net-6.0"/>
        Stelem_r4 = 0x00A0,

        ///<summary>
        /// 인덱스의 배열 요소를 스택의 float64 값으로 바꾸십시오.
        /// (Object model instruction) Replace array element at index with the float64 value on the stack.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.stelem_r8?view=net-6.0"/>
        Stelem_r8 = 0x00A1,

        ///<summary>
        /// 인덱스의 배열 요소를 스택의 Ref 값으로 바꾸십시오.
        /// (Object model instruction) Replace array element at index with the ref value on the stack.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.stelem_ref?view=net-6.0"/>
        Stelem_ref = 0x00A2,

        ///<summary>
        /// 인덱스의 요소를 스택 상단에로드하십시오.
        /// (Object model instruction) Load the element at index onto the top of the stack.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.ldelem?view=net-6.0"/>
        Ldelem = 0x00A3,

        ///<summary>
        /// 인덱스의 배열 요소를 스택의 값으로 바꾸십시오.
        /// (Object model instruction) Replace array element at index with the value on the stack.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.stelem?view=net-6.0"/>
        Stelem = 0x00A4,

        ///<summary>
        /// OBJ에서 값 유형을 추출한 박스형 표현을 추출하고 스택 상단에 복사하십시오.
        /// (Object model instruction) Extract a value-type from obj, its boxed representation, and copy to the top of the stack.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.unbox_any?view=net-6.0"/>
        Unbox_any = 0x00A5,

        ///<summary>
        /// int8 (int32로 스택에서)으로 변환하고 오버플로에 예외를 던집니다.
        /// (Base instruction) Convert to an int8 (on the stack as int32) and throw an exception on overflow.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.conv_ovf_i1?view=net-6.0"/>
        Conv_ovf_i1 = 0x00B3,

        ///<summary>
        /// 서명되지 않은 int8 (int32로 스택에서)으로 변환하고 오버플로에 예외를 던집니다.
        /// (Base instruction) Convert to an unsigned int8 (on the stack as int32) and throw an exception on overflow.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.conv_ovf_u1?view=net-6.0"/>
        Conv_ovf_u1 = 0x00B4,

        ///<summary>
        /// int16 (int32로 스택에서)으로 변환하고 오버플로에 예외를 던집니다.
        /// (Base instruction) Convert to an int16 (on the stack as int32) and throw an exception on overflow.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.conv_ovf_i2?view=net-6.0"/>
        Conv_ovf_i2 = 0x00B5,

        ///<summary>
        /// 서명되지 않은 int16 (int32로 스택에서)으로 변환하고 오버플로에 예외를 던집니다.
        /// (Base instruction) Convert to an unsigned int16 (on the stack as int32) and throw an exception on overflow.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.conv_ovf_u2?view=net-6.0"/>
        Conv_ovf_u2 = 0x00B6,

        ///<summary>
        /// int32 (int32로 스택에서)로 변환하고 오버플로에 예외를 던집니다.
        /// (Base instruction) Convert to an int32 (on the stack as int32) and throw an exception on overflow.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.conv_ovf_i4?view=net-6.0"/>
        Conv_ovf_i4 = 0x00B7,

        ///<summary>
        /// 서명되지 않은 int32 (int32로 스택에서)로 변환하고 오버플로에 예외를 던집니다.
        /// (Base instruction) Convert to an unsigned int32 (on the stack as int32) and throw an exception on overflow.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.conv_ovf_u4?view=net-6.0"/>
        Conv_ovf_u4 = 0x00B8,

        ///<summary>
        /// int64 (int64로 스택에서)로 변환하고 오버플로에 예외를 던집니다.
        /// (Base instruction) Convert to an int64 (on the stack as int64) and throw an exception on overflow.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.conv_ovf_i8?view=net-6.0"/>
        Conv_ovf_i8 = 0x00B9,

        ///<summary>
        /// 서명되지 않은 int64 (int64로 스택에서)로 변환하고 오버플로에 예외를 던집니다.
        /// (Base instruction) Convert to an unsigned int64 (on the stack as int64) and throw an exception on overflow.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.conv_ovf_u8?view=net-6.0"/>
        Conv_ovf_u8 = 0x00BA,

        ///<summary>
        /// 유형의 참조로 저장된 주소를 밀어 넣습니다.
        /// (Object model instruction) Push the address stored in a typed reference.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.refanyval?view=net-6.0"/>
        Refanyval = 0x00C2,

        ///<summary>
        /// 값이 유한 번호가 아닌 경우 Arithmeticexception 던지십시오.
        /// (Base instruction) Throw ArithmeticException if value is not a finite number.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.ckfinite?view=net-6.0"/>
        Ckfinite = 0x00C3,

        ///<summary>
        /// 유형 클래스의 PTR에 대한 유형의 참조를 스택에 푸시하십시오.
        /// (Object model instruction) Push a typed reference to ptr of type class onto the stack.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.mkrefany?view=net-6.0"/>
        Mkrefany = 0x00C6,

        ///<summary>
        /// 메타 데이터 토큰을 런타임 표현으로 변환하십시오.
        /// (Object model instruction) Convert metadata token to its runtime representation.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.ldtoken?view=net-6.0"/>
        Ldtoken = 0x00D0,

        ///<summary>
        /// 서명되지 않은 int16으로 변환하여 스택에서 int32를 푸시합니다.
        /// (Base instruction) Convert to unsigned int16, pushing int32 on stack.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.conv_u2?view=net-6.0"/>
        Conv_u2 = 0x00D1,

        ///<summary>
        /// 서명되지 않은 int8로 변환하여 Stack에서 int32를 밀어 넣습니다.
        /// (Base instruction) Convert to unsigned int8, pushing int32 on stack.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.conv_u1?view=net-6.0"/>
        Conv_u1 = 0x00D2,

        ///<summary>
        /// 기본 INT로 변환하여 스택에 네이티브 int를 밀어 넣습니다.
        /// (Base instruction) Convert to native int, pushing native int on stack.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.conv_i?view=net-6.0"/>
        Conv_i = 0x00D3,

        ///<summary>
        /// 기본 INT (스택에서 네이티브 INT)로 변환하고 오버플로에 예외를 던집니다.
        /// (Base instruction) Convert to a native int (on the stack as native int) and throw an exception on overflow.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.conv_ovf_i?view=net-6.0"/>
        Conv_ovf_i = 0x00D4,

        ///<summary>
        /// 기본 부호없는 int (스택에서 네이티브 INT)로 변환하고 오버플로에 예외를 던집니다.
        /// (Base instruction) Convert to a native unsigned int (on the stack as native int) and throw an exception on overflow.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.conv_ovf_u?view=net-6.0"/>
        Conv_ovf_u = 0x00D5,

        ///<summary>
        /// 오버 플로우 점검으로 서명 된 정수 값을 추가하십시오.
        /// (Base instruction) Add signed integer values with overflow check.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.add_ovf?view=net-6.0"/>
        Add_ovf = 0x00D6,

        ///<summary>
        /// 오버플로 점검으로 서명되지 않은 정수 값을 추가하십시오.
        /// (Base instruction) Add unsigned integer values with overflow check.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.add_ovf_un?view=net-6.0"/>
        Add_ovf_un = 0x00D7,

        ///<summary>
        /// 서명 된 정수 값을 곱하십시오. 서명 된 결과는 같은 크기로 맞습니다.
        /// (Base instruction) Multiply signed integer values. Signed result shall fit in same size.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.mul_ovf?view=net-6.0"/>
        Mul_ovf = 0x00D8,

        ///<summary>
        /// 서명되지 않은 정수 값을 곱하십시오. 서명되지 않은 결과는 같은 크기에 맞습니다.
        /// (Base instruction) Multiply unsigned integer values. Unsigned result shall fit in same size.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.mul_ovf_un?view=net-6.0"/>
        Mul_ovf_un = 0x00D9,

        ///<summary>
        /// 기본 INT에서 기본 INT를 빼십시오. 서명 된 결과는 같은 크기로 맞습니다.
        /// (Base instruction) Subtract native int from a native int. Signed result shall fit in same size.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.sub_ovf?view=net-6.0"/>
        Sub_ovf = 0x00DA,

        ///<summary>
        /// 네이티브 부호없는 int에서 네이티브 부호없는 int를 빼십시오. 서명되지 않은 결과는 같은 크기에 맞습니다.
        /// (Base instruction) Subtract native unsigned int from a native unsigned int. Unsigned result shall fit in same size.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.sub_ovf_un?view=net-6.0"/>
        Sub_ovf_un = 0x00DB,

        ///<summary>
        /// 마지막으로 예외 블록의 조항.
        /// (Base instruction) End finally clause of an exception block.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.endfinally?view=net-6.0"/>
        Endfinally = 0x00DC,

        ///<summary>
        /// 보호 된 코드 영역을 종료하십시오.
        /// (Base instruction) Exit a protected region of code.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.leave?view=net-6.0"/>
        Leave = 0x00DD,

        ///<summary>
        /// 보호 된 코드 영역, 짧은 형식을 종료하십시오.
        /// (Base instruction) Exit a protected region of code, short form.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.leave_s?view=net-6.0"/>
        Leave_s = 0x00DE,

        ///<summary>
        /// Type Native Int의 값을 주소에서 메모리에 저장합니다.
        /// (Base instruction) Store value of type native int into memory at address.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.stind_i?view=net-6.0"/>
        Stind_i = 0x00DF,

        ///<summary>
        /// 기본 부호없는 int로 변환하여 스택에 네이티브 INT를 밀어 넣습니다.
        /// (Base instruction) Convert to native unsigned int, pushing native int on stack.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.conv_u?view=net-6.0"/>
        Conv_u = 0x00E0,

        ///<summary>
        /// 현재 방법에 대한 인수 목록 핸들.
        /// (Base instruction) Return argument list handle for the current method.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.arglist?view=net-6.0"/>
        Arglist = 0xFE00,

        ///<summary>
        /// value1이 value2와 같은 경우 푸시 1 (int32 유형)의 푸시 1, 그렇지 않으면 0을 누릅니다.
        /// (Base instruction) Push 1 (of type int32) if value1 equals value2, else push 0.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.ceq?view=net-6.0"/>
        Ceq = 0xFE01,

        ///<summary>
        /// 값 1이 값 2가 더 크면 1 (int32 유형)을 푸시하면 0을 누르면 0을 누릅니다.
        /// (Base instruction) Push 1 (of type int32) if value1 greater that value2, else push 0.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.cgt?view=net-6.0"/>
        Cgt = 0xFE02,

        ///<summary>
        /// 값 1이 값 2, 서명되지 않거나 정렬되지 않은 경우 값 1 (int32 유형)을 푸시하면 0을 누르면 0을 누릅니다.
        /// (Base instruction) Push 1 (of type int32) if value1 greater that value2, unsigned or unordered, else push 0.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.cgt_un?view=net-6.0"/>
        Cgt_un = 0xFE03,

        ///<summary>
        /// 값 1보다 낮은 경우 1 (int32 유형)의 푸시 1 (value2보다 낮은 경우).
        /// (Base instruction) Push 1 (of type int32) if value1 lower than value2, else push 0.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.clt?view=net-6.0"/>
        Clt = 0xFE04,

        ///<summary>
        /// value1이 value2보다 낮거나 서명되지 않은 경우 값 1 (int32 유형)을 푸시하십시오. 그렇지 않으면 0을 누르십시오.
        /// (Base instruction) Push 1 (of type int32) if value1 lower than value2, unsigned or unordered, else push 0.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.clt_un?view=net-6.0"/>
        Clt_un = 0xFE05,

        ///<summary>
        /// 스택에서 메소드에 의해 참조 된 메소드에 포인터를 푸시하십시오.
        /// (Base instruction) Push a pointer to a method referenced by method, on the stack.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.ldftn?view=net-6.0"/>
        Ldftn = 0xFE06,

        ///<summary>
        /// 스택에서 가상 메소드의 주소를 누릅니다.
        /// (Object model instruction) Push address of virtual method on the stack.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.ldvirtftn?view=net-6.0"/>
        Ldvirtftn = 0xFE07,

        ///<summary>
        /// 로드 인수 번호 번호를 스택에 NUM을 번호로 번호를 매겼습니다.
        /// (Base instruction) Load argument numbered num onto the stack.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.ldarg?view=net-6.0"/>
        Ldarg = 0xFE09,

        ///<summary>
        /// 인수 Argnum의 주소를 가져 오십시오.
        /// (Base instruction) Fetch the address of argument argNum.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.ldarga?view=net-6.0"/>
        Ldarga = 0xFE0A,

        ///<summary>
        /// 인수 번호 번호에 대한 값을 저장하십시오.
        /// (Base instruction) Store value to the argument numbered num.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.starg?view=net-6.0"/>
        Starg = 0xFE0B,

        ///<summary>
        /// 인덱스 INDX의 로컬 변수를 스택에로드하십시오.
        /// (Base instruction) Load local variable of index indx onto stack.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.ldloc?view=net-6.0"/>
        Ldloc = 0xFE0C,

        ///<summary>
        /// Index Indx를 사용한 로컬 변수의로드 주소.
        /// (Base instruction) Load address of local variable with index indx.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.ldloca?view=net-6.0"/>
        Ldloca = 0xFE0D,

        ///<summary>
        /// 스택에서 로컬 변수 indx로 값을 팝하십시오.
        /// (Base instruction) Pop a value from stack into local variable indx.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.stloc?view=net-6.0"/>
        Stloc = 0xFE0E,

        ///<summary>
        /// 로컬 메모리 풀에서 공간을 할당하십시오.
        /// (Base instruction) Allocate space from the local memory pool.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.localloc?view=net-6.0"/>
        Localloc = 0xFE0F,

        ///<summary>
        /// 예외 처리 필터 절을 종료하십시오.
        /// (Base instruction) End an exception handling filter clause.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.endfilter?view=net-6.0"/>
        Endfilter = 0xFE11,

        ///<summary>
        /// 후속 포인터 명령은 정렬되지 않을 수 있습니다.
        /// (Prefix to instruction) Subsequent pointer instruction might be unaligned.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.unaligned?view=net-6.0"/>
        Unaligned = 0xFE12,

        ///<summary>
        /// 후속 포인터 참조는 휘발성입니다.
        /// (Prefix to instruction) Subsequent pointer reference is volatile.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.volatile?view=net-6.0"/>
        Volatile = 0xFE13,

        ///<summary>
        /// 후속 통화는 현재 방법을 종료합니다.
        /// (Prefix to instruction) Subsequent call terminates current method.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.tail?view=net-6.0"/>
        Tail = 0xFE14,

        ///<summary>
        /// 주소 대상에서 값을 초기화하십시오.
        /// (Object model instruction) Initialize the value at address dest.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.initobj?view=net-6.0"/>
        Initobj = 0xFE15,

        ///<summary>
        /// 타입 T가되도록 제한된 유형의 가상 메소드를 호출하십시오.
        /// (Prefix to instruction) Call a virtual method on a type constrained to be type T.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.constrained?view=net-6.0"/>
        Constrained = 0xFE16,

        ///<summary>
        /// 메모리에서 메모리로 데이터를 복사하십시오.
        /// (Base instruction) Copy data from memory to memory.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.cpblk?view=net-6.0"/>
        Cpblk = 0xFE17,

        ///<summary>
        /// 메모리 블록의 모든 바이트를 주어진 바이트 값으로 설정하십시오.
        /// (Base instruction) Set all bytes in a block of memory to a given byte value.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.initblk?view=net-6.0"/>
        Initblk = 0xFE18,

        ///<summary>
        /// 지정된 결함 검사는 일반적으로 후속 명령어 실행의 일부로 수행 될 수 있습니다.
        /// (Prefix to instruction) The specified fault check(s) normally performed as part of the execution of the subsequent instruction can/shall be skipped.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.no?view=net-6.0"/>
        No = 0xFE19,

        ///<summary>
        /// 현재 예외를 재확인하십시오.
        /// (Object model instruction) Rethrow the current exception.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.rethrow?view=net-6.0"/>
        Rethrow = 0xFE1A,

        ///<summary>
        /// 서명되지 않은 int32로 유형의 크기를 바이트로 밀어 넣으십시오.
        /// (Object model instruction) Push the size, in bytes, of a type as an unsigned int32.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.sizeof?view=net-6.0"/>
        Sizeof = 0xFE1C,

        ///<summary>
        /// 유형의 참조로 저장된 타입 토큰을 밀어 넣습니다.
        /// (Object model instruction) Push the type token stored in a typed reference.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.refanytype?view=net-6.0"/>
        Refanytype = 0xFE1D,

        ///<summary>
        /// 후속 배열 주소 조작은 런타임시 유형 확인을 수행하지 않으며 제어중성 관리 포인터를 반환하도록 지정하십시오.
        /// (Prefix to instruction) Specify that the subsequent array address operation performs no type check at runtime, and that it returns a controlled-mutability managed pointer.
        ///</summary>
        ///<inheritdoc path="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.readonly?view=net-6.0"/>
        Readonly = 0xFE1E,

        Custom_StLoc_0_Ldc_i4_0 = Readonly + 1,
        Custom_StLoc_0_Ldc_i4_1 = Readonly + 2,
        Custom_StLoc_0_Ldc_i4_2 = Readonly + 3,
        Custom_StLoc_0_Ldc_i4_3 = Readonly + 4,
        Custom_StLoc_0_Ldc_i4_4 = Readonly + 5,
        Custom_StLoc_0_Ldc_i4_5 = Readonly + 6,
        Custom_StLoc_0_Ldc_i4_6 = Readonly + 7,
        Custom_StLoc_0_Ldc_i4_7 = Readonly + 8,
        Custom_StLoc_0_Ldc_i4_8 = Readonly + 9,
        Custom_StLoc_0_Ldc_i4_9 = Readonly + 10,
        Custom_StLoc_1_Ldc_i4_0 = Readonly + 11,
        Custom_StLoc_1_Ldc_i4_1 = Readonly + 12,
        Custom_StLoc_1_Ldc_i4_2 = Readonly + 13,
        Custom_StLoc_1_Ldc_i4_3 = Readonly + 14,
        Custom_StLoc_1_Ldc_i4_4 = Readonly + 15,
        Custom_StLoc_1_Ldc_i4_5 = Readonly + 16,
        Custom_StLoc_1_Ldc_i4_6 = Readonly + 17,
        Custom_StLoc_1_Ldc_i4_7 = Readonly + 18,
        Custom_StLoc_1_Ldc_i4_8 = Readonly + 19,
        Custom_StLoc_1_Ldc_i4_9 = Readonly + 20,
        Custom_StLoc_2_Ldc_i4_0 = Readonly + 21,
        Custom_StLoc_2_Ldc_i4_1 = Readonly + 22,
        Custom_StLoc_2_Ldc_i4_2 = Readonly + 23,
        Custom_StLoc_2_Ldc_i4_3 = Readonly + 24,
        Custom_StLoc_2_Ldc_i4_4 = Readonly + 25,
        Custom_StLoc_2_Ldc_i4_5 = Readonly + 26,
        Custom_StLoc_2_Ldc_i4_6 = Readonly + 27,
        Custom_StLoc_2_Ldc_i4_7 = Readonly + 28,
        Custom_StLoc_2_Ldc_i4_8 = Readonly + 29,
        Custom_StLoc_2_Ldc_i4_9 = Readonly + 30,
        Custom_StLoc_3_Ldc_i4_0 = Readonly + 31,
        Custom_StLoc_3_Ldc_i4_1 = Readonly + 32,
        Custom_StLoc_3_Ldc_i4_2 = Readonly + 33,
        Custom_StLoc_3_Ldc_i4_3 = Readonly + 34,
        Custom_StLoc_3_Ldc_i4_4 = Readonly + 35,
        Custom_StLoc_3_Ldc_i4_5 = Readonly + 36,
        Custom_StLoc_3_Ldc_i4_6 = Readonly + 37,
        Custom_StLoc_3_Ldc_i4_7 = Readonly + 38,
        Custom_StLoc_3_Ldc_i4_8 = Readonly + 39,
        Custom_StLoc_3_Ldc_i4_9 = Readonly + 40,

        Custom_Add_0 = Readonly + 41,
        Custom_Add_1 = Readonly + 42,
        Custom_Add_2 = Readonly + 43,
        Custom_Add_3 = Readonly + 44,
        Custom_Add_4 = Readonly + 45,
        Custom_Add_5 = Readonly + 46,
        Custom_Add_6 = Readonly + 47,
        Custom_Add_7 = Readonly + 48,
        Custom_Add_8 = Readonly + 49,
        Custom_Add_9 = Readonly + 50,

        Custom_Ldloc_s_Ldc_s_Add_StLoc_s = Readonly + 51,
        Custom_Ldloc_s_Ldc_s_Blt_s = Readonly + 52,
        Custom_Ldloc_s_Ldc_Blt_s = Readonly + 53,
        Custom_StLoc_s_Ldc_i4_s = Readonly + 54,
    }
}
