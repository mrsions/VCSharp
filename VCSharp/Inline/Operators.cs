using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace VCSharp
{
    public class Operator2
    {
        internal ILOpCode Opcode;

        public Operator2(ILOpCode opcode)
        {
            Opcode = opcode;
        }
    }

    [StructLayout(LayoutKind.Explicit, Pack = 1)]
    public struct Operator
    {
        [FieldOffset(0)]
        public ILOpCode Op;

        [FieldOffset(2)]
        public byte v21; // offset 2에, 1바이트

        [FieldOffset(2)]
        public short v22; // offset 2에, 2바이트

        [FieldOffset(2)]
        public int v24; // offset 2에, 4바이트

        [FieldOffset(2)]
        public int v28; // offset 2에, 8바이트
    }

    //public class OpSet : Operator
    //{
    //}

    //public class OpLdloc : Operator
    //{
    //}
}
