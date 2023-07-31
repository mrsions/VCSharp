using .System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using VCSharp.Compiler.Tokens;
using System.Collections;
using VCSharp.Inline;

namespace VCSharp.Compiler.Parser
{
    public class CompileException : Exception
    {
        public CompileException(string code, string message) : base($"{code}: {message}")
        {
        }

        public static Exception CS0000() => new CompileException("CS0000", "Unkown.");
        public static CompileException CS0116() => new CompileException("CS0116", "A namespace cannot directly contain members such as fields or methods.");
        public static CompileException CS0107() => new CompileException("CS0107", "More than one protection modifier");
        public static CompileException CS0210() => new CompileException("CS0210", "You must provide an initializer in a fixed or using statement declaration");
        public static CompileException CS0307() => new CompileException("CS0307", "The 'construct' 'identifier' is not a generic method. If you intended an expression list, use parentheses around the < expression.");
        public static CompileException CS0687() => new CompileException("CS0687", "A using clause must precede all other elements defined in the namespace except extern alias declarations.");
        public static CompileException CS1001() => new CompileException("CS1001", "Identifier expected");
        public static CompileException CS1003(string modifier) => new CompileException("CS1003", $"Syntax error, '{modifier}' expected");
        public static CompileException CS1004(string modifier) => new CompileException("CS1004", $"Duplicate '{modifier}' modifier");
        public static CompileException CS1041(string keyword) => new CompileException("CS1041", $"Identifier expected, '{keyword}' is a keyword");
        public static CompileException CS1519(string keyword) => new CompileException("CS1519", $"Invalid token '{keyword}' in class, struct, or interface member declaration");
        public static CompileException CS1026() => new CompileException("CS1026", ") expected");
        public static CompileException CS1513() => new CompileException("CS1513", "} expected");
        public static CompileException CS1525(string text) => throw new CompileException("CS1525", $"Invalid expression term '{text}'");
        public static CompileException CS1529() => throw new CompileException("CS1529", $"A using clause must precede all other elements defined in the namespace except extern alias declarations.");
        public static CompileException CS1586() => new CompileException("CS1586", "Array creation must have array size or array initializer");
        public static CompileException CS8955() => new CompileException("CS8955", "Source file can not contain both file-scoped and normal namespace declarations.");

    }
}