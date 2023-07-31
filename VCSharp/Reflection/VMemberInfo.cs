using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VCSharp.Reflection
{
    public abstract class VMemberInfo
    {
        public string Name;
        public VMemberFlags Flags;
        public bool Static;
    }
}
