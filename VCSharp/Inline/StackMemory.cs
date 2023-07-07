using System;
using System.Collections;
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
        st
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

    internal unsafe struct LocalStack
    {
        public StackMemory Memory;
        public byte* BStack;
        public ObjPtr OStack;
        public StackValue* local;
        public StackValue* stack;

        public LocalStack(StackMemory m)
        {
            Memory = m;
            BStack = m.BStack;
            OStack = m.OStack;
            local = null;
            stack = null;
        }

        public byte* Alloc(int size)
        {
            // 메모리 할당
            byte* result = BStack;

            // 메모리 증가
            if (BStack + size >= Memory.BStackEnd)
            {
                throw new StackOverflowException();
            }
            BStack += size;

            return result;
        }

        public T* Alloc<T>(int count)
            where T : struct
        {
            return (T*)Alloc(sizeof(T) * count);
        }

        public ObjPtr AllocObj()
        {
            return ++OStack;
        }

        public ObjPtr GetObject(int addr)
        {
            return new ObjPtr(Memory.OStackStart, addr);
        }

        public ObjPtr GetObject(StackValue* v)
        {
            return new ObjPtr(Memory.OStackStart, v->i4);
        }

        public object GetAny(StackValue* v, Type type)
        {
            if (type == typeof(sbyte)) return (sbyte)v->i4;
            else if (type == typeof(short)) return (short)v->i4;
            else if (type == typeof(int)) return (int)v->i4;
            else if (type == typeof(long)) return (long)v->i8;
            else if (type == typeof(nint)) return (nint)v->i8;
            else if (type == typeof(byte)) return (byte)v->u4;
            else if (type == typeof(ushort)) return (ushort)v->u4;
            else if (type == typeof(uint)) return (uint)v->u4;
            else if (type == typeof(char)) return (char)v->u4;
            else if (type == typeof(ulong)) return (ulong)v->u8;
            else if (type == typeof(nuint)) return (nuint)v->u8;
            else if (type == typeof(float)) return (float)v->r4;
            else if (type == typeof(double)) return (double)v->r8;
            else if (type == typeof(decimal)) return (decimal)v->r8;
            else if (type == typeof(bool)) return (bool)v->b;
            else if (type.IsValueType)
            {
                if (v->type == StackValueType.st)
                {
                    // 연결된 주소의 메모리를 기반으로 생성한다.
                    return VActivator.CreateStruct(type, new IntPtr(v->ptr));
                }
                else
                {
                    // 값을 기반으로 생성한다.
                    return VActivator.CreateStruct(type, new IntPtr((byte*)(v + 1)));
                }
            }
            else // object type
            {
                return new ObjPtr(Memory.OStackStart, v->i4);
            }
        }

        public void SetAny(StackValue* v, Type rtype, object? obj)
        {
            if (rtype == typeof(sbyte))
            {
                v->type = StackValueType.i4;
                v->i4 = (sbyte)(obj ?? 0);
            }
            else if (rtype == typeof(short))
            {
                v->type = StackValueType.i4;
                v->i4 = (short)(obj ?? 0);
            }
            else if (rtype == typeof(int))
            {
                v->type = StackValueType.i4;
                v->i4 = (int)(obj ?? 0);
            }
            else if (rtype == typeof(long))
            {
                v->type = StackValueType.i8;
                v->i8 = (long)(obj ?? 0);
            }
            else if (rtype == typeof(nint))
            {
                v->type = StackValueType.i8;
                v->i8 = (nint)(obj ?? 0);
            }
            else if (rtype == typeof(byte))
            {
                v->type = StackValueType.u4;
                v->u4 = (byte)(obj ?? 0);
            }
            else if (rtype == typeof(ushort))
            {
                v->type = StackValueType.u4;
                v->u4 = (ushort)(obj ?? 0);
            }
            else if (rtype == typeof(uint))
            {
                v->type = StackValueType.u4;
                v->u4 = (uint)(obj ?? 0);
            }
            else if (rtype == typeof(char))
            {
                v->type = StackValueType.u4;
                v->u4 = (char)(obj ?? 0);
            }
            else if (rtype == typeof(ulong))
            {
                v->type = StackValueType.u8;
                v->u8 = (ulong)(obj ?? 0);
            }
            else if (rtype == typeof(nuint))
            {
                v->type = StackValueType.u8;
                v->u8 = (nuint)(obj ?? 0);
            }
            else if (rtype == typeof(float))
            {
                v->type = StackValueType.r4;
                v->r4 = (float)(obj ?? 0);
            }
            else if (rtype == typeof(double))
            {
                v->type = StackValueType.r8;
                v->r8 = (double)(obj ?? 0);
            }
            else if (rtype == typeof(bool))
            {
                v->type = StackValueType.b;
                v->b = (bool)(obj ?? 0);
            }
            else if (rtype.IsValueType)
            {
                int size = VAppDomain.Current.GetType(rtype).Size;
                if (size < 8)
                {
                    v->type = StackValueType.i8;
                    if (obj != null)
                    {
                        v->i8 = *(int*)(*(byte*)&obj);
                    }
                    else
                    {
                        v->i8 = 0;
                    }
                }
                else
                {
                    v->type = StackValueType.st;
                    v->ptr = Alloc(size);

                    byte* src = *(byte**)&obj;
                    Unsafe.CopyBlock((void*)v->ptr, (void*)src, (uint)size);
                }
            }
            else
            {
                v->type = StackValueType.obj;
                v->ptr = OStack;
                OStack.Data = obj;
                OStack++;
            }
        }
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

        /// <summary>
        /// 현재 사용하고있는 스텍정보
        /// </summary>
        public byte* BStack;
        public byte* BStackStart;
        public byte* BStackEnd;
        public int BStackLength;

        /// <summary>
        /// 스텍에서 사용중인 객체 정보
        /// </summary>
        public List<object?> OStackStart;
        public ObjPtr OStack;

        public StackMemory(int memorySize = 1024 * 1024, int objectStackSize = 1024) // 1Mb
        {
            BStackLength = memorySize;
            BStackStart = (byte*)Marshal.AllocHGlobal(memorySize);
            BStackEnd = BStackStart + BStackLength;
            BStack = BStackStart;

            OStackStart = new List<object?>(objectStackSize);
            OStack = new ObjPtr(OStackStart, 0);
        }

        ~StackMemory()
        {
            Marshal.FreeHGlobal((IntPtr)BStackStart);
        }

        public byte* Alloc(int size)
        {
            // 메모리 할당
            byte* result = BStack;

            // 메모리 증가
            if (BStack + size >= BStackEnd)
            {
                throw new StackOverflowException();
            }
            BStack += size;

            return result;
        }

        public T* Alloc<T>(int count)
            where T : struct
        {
            return (T*)Alloc(sizeof(T) * count);
        }

        public ObjPtr AddObject(object obj)
        {
            var stack = OStack++;
            stack.Data = obj;
            return stack;
        }

        public ObjPtr CreatePtr(int addr)
        {
            return new ObjPtr(OStackStart, addr);
        }
    }
}
