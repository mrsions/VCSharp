using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VCSharp.Reflection
{
    public enum VMemberAccessType
    {
        Private,
        Protected,
        Public,
        Internal
    }

    public abstract class VMemberInfo
    {
        public string Name;
        public VMemberAccessType AccessType;
        public bool Static;
    }
}
