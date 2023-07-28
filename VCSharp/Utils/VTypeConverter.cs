using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using VCSharp.Inline;

namespace VCSharp.Utils
{
    public class VTypeConverter
    {
        public static StackValueType ConvertToStackValueType(Type type)
        {
            if (type == typeof(sbyte)) return StackValueType.i4;
            else if (type == typeof(short)) return StackValueType.i4;
            else if (type == typeof(int)) return StackValueType.i4;
            else if (type == typeof(long)) return StackValueType.i8;
            else if (type == typeof(nint)) return StackValueType.i8;

            else if (type == typeof(byte)) return StackValueType.u4;
            else if (type == typeof(ushort)) return StackValueType.u4;
            else if (type == typeof(uint)) return StackValueType.u4;
            else if (type == typeof(char)) return StackValueType.i4;
            else if (type == typeof(ulong)) return StackValueType.u8;
            else if (type == typeof(nuint)) return StackValueType.u8;

            else if (type == typeof(float)) return StackValueType.r4;
            else if (type == typeof(double)) return StackValueType.r8;

            else if (type == typeof(bool)) return StackValueType.b;

            else if (type.IsValueType) return StackValueType.st;

            else return StackValueType.obj;
        }

        public static int GetSize(StackValueType type)
        {
            if (type < StackValueType.s9)
            {
                switch (type)
                {
                    case StackValueType.i4:
                    case StackValueType.u4:
                    case StackValueType.r4:
                    case StackValueType.b:
                        return 4;
                    default:
                        return 8;
                }
            }
            else if (type <= StackValueType.s243)
            {
                return (int)type;
            }
            else
            {
                return 256 << type - StackValueType.s256;
            }
        }

        internal static StackValueTypeCompare CombineType(StackValueType type1, StackValueType type2)
        {
            throw new NotImplementedException();
        }

        internal static StackValueType RealType(StackValueType t)
        {
            if (t <= StackValueType.st)
            {
                return t;
            }
            else
            {
                return StackValueType.st;
            }
        }

        internal static StackValueType SizeToType(int size)
        {
            if (size <= (int)StackValueType.s9)
            {
                return StackValueType.st;
            }
            else if (size < (int)StackValueType.s243)
            {
                return (StackValueType)size;
            }
            else if (size <= 256) return StackValueType.s256;
            else if (size <= 512) return StackValueType.s512;
            else if (size <= 1024) return StackValueType.s1024;
            else if (size <= 2048) return StackValueType.s2048;
            else if (size <= 4096) return StackValueType.s4096;
            else if (size <= 8192) return StackValueType.s8192;
            else if (size <= 16384) return StackValueType.s16384;
            else if (size <= 32768) return StackValueType.s32768;
            else if (size <= 65536) return StackValueType.s65536;
            else if (size <= 131072) return StackValueType.s131072;
            else if (size <= 262144) return StackValueType.s262144;
            else if (size <= 524288) return StackValueType.s524288;
            else throw new StackOverflowException();
        }
    }
}
