using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace VCSharp
{
    public static class VActivator
    {
        private static MethodInfo s_CreateStructMethodInfo;
        private static Dictionary<Type, Func<IntPtr, object>> s_CreateStructFuncDict = new();

        static VActivator()
        {
            s_CreateStructMethodInfo = typeof(VTypeConverter).GetMethod(nameof(CreateStructInternal), BindingFlags.NonPublic | BindingFlags.Static)
                ?? throw new EntryPointNotFoundException("VActivator.CreateStruct");
        }

        public static object CreateStruct(Type type, IntPtr data)
        {
            if (!s_CreateStructFuncDict.TryGetValue(type, out var func))
            {
                lock (s_CreateStructFuncDict)
                {
                    s_CreateStructFuncDict[type] = func = s_CreateStructMethodInfo.MakeGenericMethod(type).CreateDelegate<Func<IntPtr, object>>();
                }
            }

            return func(data);
        }

        internal static unsafe object CreateStructInternal<T>(IntPtr data)
            where T : struct
        {
#pragma warning disable CS8500 // 주소를 가져오거나, 크기를 가져오거나, 관리되는 형식에 대한 포인터를 선언합니다.
            return *(T*)data;
#pragma warning restore CS8500 // 주소를 가져오거나, 크기를 가져오거나, 관리되는 형식에 대한 포인터를 선언합니다.
        }
    }
}
