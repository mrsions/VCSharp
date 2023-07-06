using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace VCSharp
{
    public enum VFieldLayoutType
    {
        Value,
        Object
    }

    public class VFieldInfo : VMemberInfo
    {
        public VFieldLayoutType Layout;
        public int Offset;
        public VType? FieldType;
        public FieldInfo? fieldInfo;

        //public object GetValue(object obj)
        //{
        //    if(obj is VObject vo)
        //    {
        //        switch(Layout)
        //        {
        //            case VFieldLayoutType.Value:
        //                return 
        //        }
        //    }
        //    else
        //    {
        //    }
        //}

        public unsafe ref byte* GT()
        {
            int size = 100;
            byte* a = stackalloc byte[size];
            return ref a;
        }

        public unsafe T GetValueStruct<T>(VObject obj)
            where T : struct
        {
            Debug.Assert(Layout == VFieldLayoutType.Value, "Invalid data layout");

            fixed (byte* ptr = obj.Body)
            {
                return *(T*)(ptr + Offset);
            }
        }

        public ArraySegment<byte> GetValueBytes(VObject obj)
        {
            Debug.Assert(Layout == VFieldLayoutType.Value, "Invalid data layout");
            return new ArraySegment<byte>(obj.Body, Offset, FieldType.Size);
        }

        public object GetValueObject(VObject obj)
        {
            Debug.Assert(Layout == VFieldLayoutType.Object, "Invalid data layout");

            return obj.BodyObjects[Offset];
        }



        public unsafe void SetValueStruct<T>(VObject obj, T value)
        {
            Debug.Assert(Layout == VFieldLayoutType.Value, "Invalid data layout");

            fixed (byte* ptr = obj.Body)
            {
                *(T*)(ptr + Offset) = value;
            }
        }

        public void SetValueBytes(VObject obj, ArraySegment<byte> value)
        {
            Debug.Assert(Layout == VFieldLayoutType.Value, "Invalid data layout");

            ref byte dst = ref obj.Body[Offset];
            ref byte src = ref value.Array[value.Offset];
            uint length = (uint)value.Count;
            Unsafe.CopyBlock(ref dst, ref src, length);
        }

        public void SetValueObject(VObject obj, object value)
        {
            Debug.Assert(Layout == VFieldLayoutType.Object, "Invalid data layout");

            obj.BodyObjects[Offset] = value;
        }
    }
}
