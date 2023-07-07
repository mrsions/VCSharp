using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VCSharp
{
    public enum EVTypeType
    {
        Object,
        Struct,
        Primitive_Int8,
        Primitive_Int16,
        Primitive_Int32,
        Primitive_Int64,
        Primitive_UInt8,
        Primitive_UInt16,
        Primitive_UInt32,
        Primitive_UInt64,
        Primitive_Char,
        Primitive_Single,
        Primitive_Double,
        Primitive_Decimal
    }

    public class VType : VMemberInfo
    {
        public string Namespace;

        public EVTypeType VirtualType;
        public int Size;

        public List<VMemberInfo> Members = new List<VMemberInfo>();
        public List<VFieldInfo> Fields = new List<VFieldInfo>();
        public List<VMethodInfo> Methods = new List<VMethodInfo>();
        public VType Type;

        internal int StructSize;
        internal int ObjectSize;
        private Type type;

        public VType(Type type)
        {
            this.type = type;
        }
    }
}
