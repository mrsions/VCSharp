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
        Int4,
        Int8,
        Unt4,
        Unt8,
        Real4,
        Real8,
        Object,
        Struct
    }

    public enum StackValueTypeCompare : byte
    {
        Int4_Int4,
        Int4_Int8,
        Int4_Unt4,
        Int4_Unt8,
        Int4_Real4,
        Int4_Real8,
        Int8_Int4,
        Int8_Int8,
        Int8_Unt4,
        Int8_Unt8,
        Int8_Real4,
        Int8_Real8,
        Unt4_Int4,
        Unt4_Int8,
        Unt4_Unt4,
        Unt4_Unt8,
        Unt4_Real4,
        Unt4_Real8,
        Unt8_Int4,
        Unt8_Int8,
        Unt8_Unt4,
        Unt8_Unt8,
        Unt8_Real4,
        Unt8_Real8,
        Real4_Int4,
        Real4_Int8,
        Real4_Unt4,
        Real4_Unt8,
        Real4_Real4,
        Real4_Real8,
        Real8_Int4,
        Real8_Int8,
        Real8_Unt4,
        Real8_Unt8,
        Real8_Real4,
        Real8_Real8
    }

    [StructLayout(LayoutKind.Explicit, Pack = 1, Size = 9)]
    internal unsafe struct StackValue
    {
        public static readonly StackValue I4_m1 = new StackValue { type = StackValueType.Int4, value = -1 };
        public static readonly StackValue I4_0 = new StackValue { type = StackValueType.Int4, value = 0 };
        public static readonly StackValue I4_1 = new StackValue { type = StackValueType.Int4, value = 1 };
        public static readonly StackValue I4_2 = new StackValue { type = StackValueType.Int4, value = 2 };
        public static readonly StackValue I4_3 = new StackValue { type = StackValueType.Int4, value = 3 };
        public static readonly StackValue I4_4 = new StackValue { type = StackValueType.Int4, value = 4 };
        public static readonly StackValue I4_5 = new StackValue { type = StackValueType.Int4, value = 5 };
        public static readonly StackValue I4_6 = new StackValue { type = StackValueType.Int4, value = 6 };
        public static readonly StackValue I4_7 = new StackValue { type = StackValueType.Int4, value = 7 };
        public static readonly StackValue I4_8 = new StackValue { type = StackValueType.Int4, value = 8 };
        public static readonly StackValue I8_0 = new StackValue { type = StackValueType.Int8, value = 0 };
        public static readonly StackValue I8_1 = new StackValue { type = StackValueType.Int8, value = 1 };
        public static readonly StackValue I8_2 = new StackValue { type = StackValueType.Int8, value = 2 };
        public static readonly StackValue I8_3 = new StackValue { type = StackValueType.Int8, value = 3 };
        public static readonly StackValue R4_0 = new StackValue { type = StackValueType.Real4, value = 0 };
        public static readonly StackValue R4_1 = new StackValue { type = StackValueType.Real4, value = 1 };
        public static readonly StackValue R4_2 = new StackValue { type = StackValueType.Real4, value = 2 };
        public static readonly StackValue R4_3 = new StackValue { type = StackValueType.Real4, value = 3 };
        public static readonly StackValue R8_0 = new StackValue { type = StackValueType.Real8, value = 0 };
        public static readonly StackValue R8_1 = new StackValue { type = StackValueType.Real8, value = 1 };
        public static readonly StackValue R8_2 = new StackValue { type = StackValueType.Real8, value = 2 };
        public static readonly StackValue R8_3 = new StackValue { type = StackValueType.Real8, value = 3 };

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

        public object[] StackObjects;

        public StackMemory(int memorySize = 1024 * 1024, int objectStackSize = 1024) // 1Mb
        {
            MemoryLength = memorySize;
            MemoryStart = (byte*)Marshal.AllocHGlobal(memorySize);
            MemoryEnd = MemoryStart + MemoryLength;
            MemoryCurrent = MemoryStart;

            StackObjects = new object[objectStackSize];
        }

        ~StackMemory()
        {
            Marshal.FreeHGlobal((IntPtr)MemoryStart);
        }

        public void ReallocObjectStack(int size)
        {
            Array.Resize(ref StackObjects, size);
        }
    }
}
