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
        public List<VType> Types = new();
        public Dictionary<Type, VType> TypesDict = new();

        public MethodInfo GetMethodInfo(int id)
        {
            return Methods[id];
        }

        public VType GetType(int id)
        {
            return Types[id];
        }

        public VType GetType(Type type)
        {
            if (!TypesDict.TryGetValue(type, out var result))
            {
                lock (TypesDict)
                {
                    TypesDict[type] = result = new VType(type);
                }
            }

            return result;
        }
    }
}
