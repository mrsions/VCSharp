using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace VCSharp
{
    public enum StackValueType : byte
    {
        i4,
        i8,
        u4,
        u8,
        r4,
        r8,
        b,
        obj,
        str
    }

    public enum StackValueTypeCompare : byte
    {
        i4_i4,
        i4_i8,
        i4_u4,
        i4_u8,
        i4_r4,
        i4_r8,
        i4_b,
        i8_i4,
        i8_i8,
        i8_u4,
        i8_u8,
        i8_r4,
        i8_r8,
        i8_b,
        u4_i4,
        u4_i8,
        u4_u4,
        u4_u8,
        u4_r4,
        u4_r8,
        u4_b,
        u8_i4,
        u8_i8,
        u8_u4,
        u8_u8,
        u8_r4,
        u8_r8,
        u8_b,
        r4_i4,
        r4_i8,
        r4_u4,
        r4_u8,
        r4_r4,
        r4_r8,
        r4_b,
        r8_i4,
        r8_i8,
        r8_u4,
        r8_u8,
        r8_r4,
        r8_r8,
        r8_b,
        b_i4,
        b_i8,
        b_u4,
        b_u8,
        b_r4,
        b_r8,
        b_b,
    }

    [StructLayout(LayoutKind.Explicit, Pack = 1, Size = 9)]
    internal unsafe struct StackValue
    {
        public static readonly StackValue I4_m1 = new StackValue { type = StackValueType.i4, value = -1 };
        public static readonly StackValue I4_0 = new StackValue { type = StackValueType.i4, value = 0 };
        public static readonly StackValue I4_1 = new StackValue { type = StackValueType.i4, value = 1 };
        public static readonly StackValue I4_2 = new StackValue { type = StackValueType.i4, value = 2 };
        public static readonly StackValue I4_3 = new StackValue { type = StackValueType.i4, value = 3 };
        public static readonly StackValue I4_4 = new StackValue { type = StackValueType.i4, value = 4 };
        public static readonly StackValue I4_5 = new StackValue { type = StackValueType.i4, value = 5 };
        public static readonly StackValue I4_6 = new StackValue { type = StackValueType.i4, value = 6 };
        public static readonly StackValue I4_7 = new StackValue { type = StackValueType.i4, value = 7 };
        public static readonly StackValue I4_8 = new StackValue { type = StackValueType.i4, value = 8 };
        public static readonly StackValue I8_0 = new StackValue { type = StackValueType.i8, value = 0 };
        public static readonly StackValue I8_1 = new StackValue { type = StackValueType.i8, value = 1 };
        public static readonly StackValue I8_2 = new StackValue { type = StackValueType.i8, value = 2 };
        public static readonly StackValue I8_3 = new StackValue { type = StackValueType.i8, value = 3 };
        public static readonly StackValue R4_0 = new StackValue { type = StackValueType.r4, value = 0 };
        public static readonly StackValue R4_1 = new StackValue { type = StackValueType.r4, value = 1 };
        public static readonly StackValue R4_2 = new StackValue { type = StackValueType.r4, value = 2 };
        public static readonly StackValue R4_3 = new StackValue { type = StackValueType.r4, value = 3 };
        public static readonly StackValue R8_0 = new StackValue { type = StackValueType.r8, value = 0 };
        public static readonly StackValue R8_1 = new StackValue { type = StackValueType.r8, value = 1 };
        public static readonly StackValue R8_2 = new StackValue { type = StackValueType.r8, value = 2 };
        public static readonly StackValue R8_3 = new StackValue { type = StackValueType.r8, value = 3 };

        [FieldOffset(0)]
        public StackValueType type;

        [FieldOffset(0)]
        public byte typeNum;

        [FieldOffset(1)]
        public long value;

        [FieldOffset(1)]
        public byte* ptr;

        [FieldOffset(1)]
        public bool b;

        [FieldOffset(1)]
        public int i4;

        [FieldOffset(1)]
        public long i8;

        [FieldOffset(1)]
        public uint u4;

        [FieldOffset(1)]
        public ulong u8;

        [FieldOffset(1)]
        public float r4;

        [FieldOffset(1)]
        public double r8;

        [FieldOffset(5)]
        public uint Last4;
    }

    internal unsafe class StackMemory
    {
        private static ThreadLocal<StackMemory> m_Current = new ThreadLocal<StackMemory>();
        internal static StackMemory Current
        {
            get
            {
                StackMemory? result = m_Current.Value;
                if (result == null)
                {
                    m_Current.Value = result = new StackMemory();
                }
                return result;
            }
        }

        public byte* MemoryStart;
        public byte* MemoryEnd;
        public byte* MemoryCurrent;
        public int MemoryLength;

        public ObjPtr StackObjects;

        public StackMemory(int memorySize = 1024 * 1024, int objectStackSize = 1024) // 1Mb
        {
            MemoryLength = memorySize;
            MemoryStart = (byte*)Marshal.AllocHGlobal(memorySize);
            MemoryEnd = MemoryStart + MemoryLength;
            MemoryCurrent = MemoryStart;

            StackObjects = new ObjPtr(new List<object?>(), 0);
        }

        ~StackMemory()
        {
            Marshal.FreeHGlobal((IntPtr)MemoryStart);
        }
    }
}
