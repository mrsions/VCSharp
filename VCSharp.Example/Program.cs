using System.Numerics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace VCSharp.Example
{
    public unsafe class Program
    {
        public struct Vector3
        {
            public int x, y, z;

            public override string ToString()
            {
                return $"({x},{y},{z})";
            }
        }

        static void Main(string[] args)
        {
            var func = (delegate*<object, string>)typeof(Vector3).GetMethod(nameof(Vector3.ToString)).MethodHandle.GetFunctionPointer();

            var v = new Vector3() { x = 1, y = 2, z = 3 };
            object o = v;

            Console.WriteLine(func(v));
            Console.WriteLine(func(o));
        }

        static unsafe string BytesToHexString(byte* pointer, int length)
        {
            char[] hexChars = new char[length * 3];
            const string hexAlphabet = "0123456789ABCDEF";

            for (int i = 0; i < length; i++)
            {
                byte value = *(pointer + i);
                hexChars[i * 3] = hexAlphabet[value >> 4];
                hexChars[i * 3 + 1] = hexAlphabet[value & 0x0F];
                hexChars[i * 3 + 2] = ' ';
            }

            Console.WriteLine(hexChars);
            return new string(hexChars);
        }
    }
}