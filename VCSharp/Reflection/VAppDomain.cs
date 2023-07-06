using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace VCSharp
{
    public class VAppDomain : VMemberInfo
    {
        public static VAppDomain Current = new();

        public List<MethodInfo> Methods = new();
    }
}
