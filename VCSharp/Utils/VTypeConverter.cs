using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace VCSharp
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

    }
}
