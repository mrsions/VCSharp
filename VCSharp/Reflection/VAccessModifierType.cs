using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace VCSharp.Reflection
{
    [Flags]
    public enum VMemberFlags
    {
        Private = 0,
        Protected,
        Public,
        Internal,
        ProtectedInternal, // Protected or Internal로 동작함. 기본적으로 Protected와 같지만 같은 assembly 내에서는 public처럼 접근 가능함
        ProtectedPrivate, // Protected and Internal로 동작함. 같은 어셈블리 내에서만 Protected가 유지되고 외부 어셈블리에서는 private처럼 취급됨. (외부에서는 상속해도 못사용함)
        File, // 파일 내부에서만 사용되며 외부에서는 private이 아니라, 존재하지않는 것으로 처리됨.

        AccessModifer = 0x0F, // 4bit (1~15)

        Sealed = 1 << 4,
        Static = 1 << 5,
        Unsafe = 1 << 6,
        Partial = 1 << 7,
        Abstract = 1 << 8,
    }
}
