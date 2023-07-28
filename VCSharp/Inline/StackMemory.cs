using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using VCSharp.Reflection;
using VCSharp.Utils;

namespace VCSharp.Inline
{
    /// <summary>
    /// switch(l->type)
    /// {
    ///     case StackValueType.i4: break;
    ///     case StackValueType.i8: break;
    ///     case StackValueType.u4: break;
    ///     case StackValueType.u8: break;
    ///     case StackValueType.r4: break;
    ///     case StackValueType.r8: break;
    ///     case StackValueType.b: break;
    ///     case StackValueType.obj: break;
    ///     default:
    ///         {
    /// 
    ///         }
    ///         break;
    /// }
    /// </summary>
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
        st,

        s9 = 9,
        s243 = 243,
        s256,
        s512,
        s1024,
        s2048,
        s4096,
        s8192,
        s16384,
        s32768,
        s65536,
        s131072,
        s262144,
        s524288,
    }

    public enum StackValueTypeCombine : byte
    {
        i4_i4,
        i4_i8,
        i4_u4,
        i4_u8,
        i4_r4,
        i4_r8,
        i4_b,
        i4_obj,
        i4_st,
        i8_i4,
        i8_i8,
        i8_u4,
        i8_u8,
        i8_r4,
        i8_r8,
        i8_b,
        i8_obj,
        i8_st,
        u4_i4,
        u4_i8,
        u4_u4,
        u4_u8,
        u4_r4,
        u4_r8,
        u4_b,
        u4_obj,
        u4_st,
        u8_i4,
        u8_i8,
        u8_u4,
        u8_u8,
        u8_r4,
        u8_r8,
        u8_b,
        u8_obj,
        u8_st,
        r4_i4,
        r4_i8,
        r4_u4,
        r4_u8,
        r4_r4,
        r4_r8,
        r4_b,
        r4_obj,
        r4_st,
        r8_i4,
        r8_i8,
        r8_u4,
        r8_u8,
        r8_r4,
        r8_r8,
        r8_b,
        r8_obj,
        r8_st,
        b_i4,
        b_i8,
        b_u4,
        b_u8,
        b_r4,
        b_r8,
        b_b,
        b_obj,
        b_st,
        obj_i4,
        obj_i8,
        obj_u4,
        obj_u8,
        obj_r4,
        obj_r8,
        obj_b,
        obj_obj,
        obj_st,
        st_i4,
        st_i8,
        st_u4,
        st_u8,
        st_r4,
        st_r8,
        st_b,
        st_obj,
        st_st,
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
    internal unsafe struct StackValueAddress
    {
        [FieldOffset(0)]
        public StackValueType type;

        [FieldOffset(1)]
        public long value;

        [FieldOffset(1)]
        public byte* ptr;

        [FieldOffset(1)]
        public int i4;
    }

    [StructLayout(LayoutKind.Explicit, Pack = 1, Size = 5)]
    internal unsafe struct StackValue4
    {
        [FieldOffset(0)]
        public StackValueType type;

        [FieldOffset(1)]
        public bool b;

        [FieldOffset(1)]
        public int i4;

        [FieldOffset(1)]
        public uint u4;

        [FieldOffset(1)]
        public float r4;
    }

    [StructLayout(LayoutKind.Explicit, Pack = 1, Size = 9)]
    internal unsafe struct StackValue8
    {
        [FieldOffset(0)]
        public StackValueType type;

        [FieldOffset(1)]
        public long i8;

        [FieldOffset(1)]
        public ulong u8;

        [FieldOffset(1)]
        public double r8;
    }

    [StructLayout(LayoutKind.Explicit, Pack = 1, Size = 9)]
    internal unsafe struct StackValue
    {
        //public static readonly StackValue NULL = new StackValue { type = StackValueType.i4, value = 0 };
        //public static readonly StackValue I4_m1 = new StackValue { type = StackValueType.i4, i4 = -1 };
        //public static readonly StackValue I4_0 = new StackValue { type = StackValueType.i4, i4 = 0 };
        //public static readonly StackValue I4_1 = new StackValue { type = StackValueType.i4, i4 = 1 };
        //public static readonly StackValue I4_2 = new StackValue { type = StackValueType.i4, i4 = 2 };
        //public static readonly StackValue I4_3 = new StackValue { type = StackValueType.i4, i4 = 3 };
        //public static readonly StackValue I4_4 = new StackValue { type = StackValueType.i4, i4 = 4 };
        //public static readonly StackValue I4_5 = new StackValue { type = StackValueType.i4, i4 = 5 };
        //public static readonly StackValue I4_6 = new StackValue { type = StackValueType.i4, i4 = 6 };
        //public static readonly StackValue I4_7 = new StackValue { type = StackValueType.i4, i4 = 7 };
        //public static readonly StackValue I4_8 = new StackValue { type = StackValueType.i4, i4 = 8 };
        //public static readonly StackValue R4_0 = new StackValue { type = StackValueType.r4, r4 = 0 };
        //public static readonly StackValue R4_1 = new StackValue { type = StackValueType.r4, r4 = 1 };
        //public static readonly StackValue R4_2 = new StackValue { type = StackValueType.r4, r4 = 2 };
        //public static readonly StackValue R4_3 = new StackValue { type = StackValueType.r4, r4 = 3 };
        //public static readonly StackValue I8_0 = new StackValue { type = StackValueType.i8, i8 = 0 };
        //public static readonly StackValue I8_1 = new StackValue { type = StackValueType.i8, i8 = 1 };
        //public static readonly StackValue I8_2 = new StackValue { type = StackValueType.i8, i8 = 2 };
        //public static readonly StackValue I8_3 = new StackValue { type = StackValueType.i8, i8 = 3 };
        //public static readonly StackValue R8_0 = new StackValue { type = StackValueType.r8, r8 = 0 };
        //public static readonly StackValue R8_1 = new StackValue { type = StackValueType.r8, r8 = 1 };
        //public static readonly StackValue R8_2 = new StackValue { type = StackValueType.r8, r8 = 2 };
        //public static readonly StackValue R8_3 = new StackValue { type = StackValueType.r8, r8 = 3 };

        [FieldOffset(0)]
        public StackValueType type;

        [FieldOffset(0)]
        public byte typeNum;

        [FieldOffset(1)]
        public long value;

        [FieldOffset(1)]
        public byte* ptr;

        [FieldOffset(1)]
        public void* vptr;

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
        const MethodImplOptions METHODIMPL = MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization;

        public StackMemory Memory;
        public byte* BStack;
        public ObjPtr OStack;
        public StackValue** local;
        public StackValue** stack;
        public StackValue** arguments;
        public StackValue** result;

        public LocalStack(StackMemory m)
        {
            Memory = m;
            BStack = m.BStack;
            OStack = m.OStack;
            local = null;
            stack = null;
            arguments = null;
            result = null;
        }

        public byte* Alloc(int size)
        {
            // 메모리 증가
            if (BStack + size >= Memory.BStackEnd)
            {
                throw new StackOverflowException();
            }

            // 메모리 할당
            byte* result = BStack;
            Unsafe.InitBlock(result, 0, (uint)size);

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
            else if (type == typeof(int)) return v->i4;
            else if (type == typeof(long)) return v->i8;
            else if (type == typeof(nint)) return (nint)v->i8;
            else if (type == typeof(byte)) return (byte)v->u4;
            else if (type == typeof(ushort)) return (ushort)v->u4;
            else if (type == typeof(uint)) return v->u4;
            else if (type == typeof(char)) return (char)v->u4;
            else if (type == typeof(ulong)) return v->u8;
            else if (type == typeof(nuint)) return (nuint)v->u8;
            else if (type == typeof(float)) return v->r4;
            else if (type == typeof(double)) return v->r8;
            else if (type == typeof(decimal)) return (decimal)v->r8;
            else if (type == typeof(bool)) return v->b;
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

        internal StackValue* GetLocalVariable(int index)
        {
            return *(local + index);
        }

        internal void PushStack(StackValue* src)
        {
            int size = VTypeConverter.GetSize(src->type);

            var v2 = *stack++ = (StackValue*)Alloc(size + 1);
            v2->type = src->type;
            Unsafe.CopyBlock((byte*)(v2 + 1), (byte*)src + 1, (uint)size);
        }

        internal void PushStack(StackValueType type, void* value)
        {
            int size = VTypeConverter.GetSize(type);

            var v2 = *stack++ = (StackValue*)Alloc(size + 1);
            v2->type = type;
            Unsafe.CopyBlock((byte*)(v2 + 1), (byte*)value, (uint)size);
        }

        internal void PushStack(StackValueType type, void* value, int size)
        {
            var v2 = *stack++ = (StackValue*)Alloc(size + 1);
            v2->type = type;
            Unsafe.CopyBlock((byte*)(v2 + 1), (byte*)value, (uint)size);
        }

        internal void PushStack(int i4)
        {
            var v2 = *stack++ = (StackValue*)Alloc(sizeof(StackValue4));
            v2->type = StackValueType.i4;
            v2->i4 = i4;
        }

        internal void PushStackAddress(StackValue* src)
        {
            switch (src->type)
            {
                case StackValueType.obj:
                    {
                        var dst = *stack++ = (StackValue*)Alloc(sizeof(int) + 1);
                        dst->type = StackValueType.obj;
                        dst->i4 = src->i4;
                    }
                    break;
                default:
                    {
                        var dst = *stack++ = (StackValue*)Alloc(sizeof(nint) + 1);
                        dst->type = StackValueType.st;
                        dst->vptr = src + 1;
                    }
                    break;
            }
        }
        public void PushStack(Type rtype, object? obj)
        {
            if (rtype == typeof(sbyte))
            {
                PushStack((sbyte)(obj ?? 0));
            }
            else if (rtype == typeof(short))
            {
                PushStack((short)(obj ?? 0));
            }
            else if (rtype == typeof(int))
            {
                PushStack((int)(obj ?? 0));
            }
            else if (rtype == typeof(long))
            {
                long v = (long)(obj ?? 0L);
                PushStack(StackValueType.i8, &v);
            }
            else if (rtype == typeof(nint))
            {
                nint v = (nint)(obj ?? 0L);
                PushStack(StackValueType.i8, &v);
            }
            else if (rtype == typeof(byte))
            {
                byte v = (byte)(obj ?? 0L);
                PushStack(StackValueType.u4, &v);
            }
            else if (rtype == typeof(ushort))
            {
                ushort v = (ushort)(obj ?? 0L);
                PushStack(StackValueType.u4, &v);
            }
            else if (rtype == typeof(uint))
            {
                uint v = (uint)(obj ?? 0L);
                PushStack(StackValueType.u4, &v);
            }
            else if (rtype == typeof(char))
            {
                char v = (char)(obj ?? 0L);
                PushStack(StackValueType.u4, &v);
            }
            else if (rtype == typeof(ulong))
            {
                ulong v = (char)(obj ?? 0L);
                PushStack(StackValueType.u8, &v);
            }
            else if (rtype == typeof(nuint))
            {
                nuint v = (nuint)(obj ?? 0L);
                PushStack(StackValueType.u8, &v);
            }
            else if (rtype == typeof(float))
            {
                float v = (float)(obj ?? 0L);
                PushStack(StackValueType.r4, &v);
            }
            else if (rtype == typeof(double))
            {
                double v = (double)(obj ?? 0L);
                PushStack(StackValueType.r8, &v);
            }
            else if (rtype == typeof(bool))
            {
                PushStack((bool)(obj ?? false) ? 1 : 0);
            }
            else if (rtype.IsValueType)
            {
                int size = VAppDomain.Current.GetType(rtype).Size;
                if (size <= 8)
                {
                    nint ptr = 0;
                    if (obj != null)
                    {
                        ptr = *(int*)*(byte*)&obj;
                    }
                    PushStack(StackValueType.u8, &ptr);
                }
                else
                {
                    var type = VTypeConverter.SizeToType(size);
                    size = VTypeConverter.GetSize(type);
                    byte* src = *(byte**)&obj;

                    PushStack(type, src, size);
                }
            }
            else
            {
                int idx = OStack;
                PushStack(StackValueType.obj, &idx);
                OStack.Data = obj;
                OStack++;
            }
        }

        internal void SetLocalVariable(int index, StackValue* v)
        {
            StackValue* l = local[index];

            switch (l->type)
            {
                case StackValueType.b:
                case StackValueType.i4:
                case StackValueType.u4:
                case StackValueType.r4:
                case StackValueType.obj:
                    l->i4 = v->i4;
                    break;

                case StackValueType.i8:
                case StackValueType.u8:
                case StackValueType.r8:
                case StackValueType.st:
                    l->i8 = v->i8;
                    break;

                default:
                    {
                        int size = VTypeConverter.GetSize(l->type);
                        Unsafe.CopyBlock(v + 1, l + 1, (uint)size);
                    }
                    break;
            }
        }

        internal StackValue* PopStack()
        {
            BStack = (byte*)*stack;
            var result = *--stack;
            if (result->type == StackValueType.obj)
            {
                OStack--;
            }
            return result;
        }

        internal void PopStackVoid()
        {
            BStack = (byte*)*stack;
            var result = *--stack;
            if (result->type == StackValueType.obj)
            {
                OStack--;
            }
        }

        internal StackValue* GetArgument(short index)
        {
            return arguments[index];
        }

        internal void SetArgument(short index, StackValue* v)
        {
            StackValue* l = arguments[index];

            switch (l->type)
            {
                case StackValueType.b:
                case StackValueType.i4:
                case StackValueType.u4:
                case StackValueType.r4:
                case StackValueType.obj:
                    l->i4 = v->i4;
                    break;

                case StackValueType.i8:
                case StackValueType.u8:
                case StackValueType.r8:
                case StackValueType.st:
                    l->i8 = v->i8;
                    break;

                default:
                    {
                        int size = VTypeConverter.GetSize(l->type);
                        Unsafe.CopyBlock(v + 1, l + 1, (uint)size);
                    }
                    break;
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
