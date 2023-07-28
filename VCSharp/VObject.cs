using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VCSharp.Reflection;

namespace VCSharp
{
    public class VObject
    {
        public byte[]? Body;
        public object?[] BodyObjects;

        public VObject(VType type)
        {
            Body = new byte[type.StructSize];
            BodyObjects = new object[type.ObjectSize];
        }
    }
}
