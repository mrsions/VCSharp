using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections.ObjectModel;

namespace VCSharp.Compiler.Tokens
{
    public class KeywordToken : Token
    {
        public readonly static KeywordToken[] Tokens;
        public readonly static Dictionary<string, KeywordToken> TokenDict;
        public readonly static char[] TokenStarts;
        public readonly static int TokenMaxLength;

        static KeywordToken()
        {
            var fields = typeof(KeywordToken).GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static)
                .Where(v => v.FieldType == typeof(KeywordToken));

            Tokens = fields.Select(v => (KeywordToken)v.GetValue(null)!).OrderByDescending(v => v.Text).ToArray();
            TokenDict = Tokens.ToDictionary(k => k.m_Text);
            TokenStarts = Tokens.Select(v => v.Text[0]).Distinct().ToArray();
            TokenMaxLength = Tokens.Max(v => v.Text.Length);
        }

        public KeywordToken(string name) : base(name)
        {
        }

        public readonly static KeywordToken As = new KeywordToken("as");
        public readonly static KeywordToken Base = new KeywordToken("base");
        public readonly static KeywordToken Bool = new KeywordToken("bool");
        public readonly static KeywordToken Break = new KeywordToken("break");
        public readonly static KeywordToken Byte = new KeywordToken("byte");
        public readonly static KeywordToken Case = new KeywordToken("case");
        public readonly static KeywordToken Catch = new KeywordToken("catch");
        public readonly static KeywordToken Char = new KeywordToken("char");
        public readonly static KeywordToken Checked = new KeywordToken("checked");
        public readonly static KeywordToken Class = new KeywordToken("class");
        public readonly static KeywordToken Const = new KeywordToken("const");
        public readonly static KeywordToken Continue = new KeywordToken("continue");
        public readonly static KeywordToken Decimal = new KeywordToken("decimal");
        public readonly static KeywordToken Default = new KeywordToken("default");
        public readonly static KeywordToken Delegate = new KeywordToken("delegate");
        public readonly static KeywordToken Do = new KeywordToken("do");
        public readonly static KeywordToken Double = new KeywordToken("double");
        public readonly static KeywordToken Else = new KeywordToken("else");
        public readonly static KeywordToken Enum = new KeywordToken("enum");
        public readonly static KeywordToken Event = new KeywordToken("event");
        public readonly static KeywordToken Explicit = new KeywordToken("explicit");
        public readonly static KeywordToken Extern = new KeywordToken("extern");
        public readonly static KeywordToken False = new KeywordToken("false");
        public readonly static KeywordToken Finally = new KeywordToken("finally");
        public readonly static KeywordToken Fixed = new KeywordToken("fixed");
        public readonly static KeywordToken Float = new KeywordToken("float");
        public readonly static KeywordToken For = new KeywordToken("for");
        public readonly static KeywordToken Foreach = new KeywordToken("foreach");
        public readonly static KeywordToken Goto = new KeywordToken("goto");
        public readonly static KeywordToken If = new KeywordToken("if");
        public readonly static KeywordToken Implicit = new KeywordToken("implicit");
        public readonly static KeywordToken In = new KeywordToken("in");
        public readonly static KeywordToken Int = new KeywordToken("int");
        public readonly static KeywordToken Interface = new KeywordToken("interface");
        public readonly static KeywordToken Internal = new KeywordToken("internal");
        public readonly static KeywordToken Is = new KeywordToken("is");
        public readonly static KeywordToken Lock = new KeywordToken("lock");
        public readonly static KeywordToken Long = new KeywordToken("long");
        public readonly static KeywordToken Namespace = new KeywordToken("namespace");
        public readonly static KeywordToken New = new KeywordToken("new");
        public readonly static KeywordToken Null = new KeywordToken("null");
        public readonly static KeywordToken Object = new KeywordToken("object");
        public readonly static KeywordToken Operator = new KeywordToken("operator");
        public readonly static KeywordToken Out = new KeywordToken("out");
        public readonly static KeywordToken Override = new KeywordToken("override");
        public readonly static KeywordToken Params = new KeywordToken("params");
        public readonly static KeywordToken Private = new KeywordToken("private");
        public readonly static KeywordToken Protected = new KeywordToken("protected");
        public readonly static KeywordToken Public = new KeywordToken("public");
        public readonly static KeywordToken Readonly = new KeywordToken("readonly");
        public readonly static KeywordToken Ref = new KeywordToken("ref");
        public readonly static KeywordToken Return = new KeywordToken("return");
        public readonly static KeywordToken Sbyte = new KeywordToken("sbyte");
        public readonly static KeywordToken Sealed = new KeywordToken("sealed");
        public readonly static KeywordToken Short = new KeywordToken("short");
        public readonly static KeywordToken Sizeof = new KeywordToken("sizeof");
        public readonly static KeywordToken Stackalloc = new KeywordToken("stackalloc");
        public readonly static KeywordToken Static = new KeywordToken("static");
        public readonly static KeywordToken String = new KeywordToken("string");
        public readonly static KeywordToken Struct = new KeywordToken("struct");
        public readonly static KeywordToken Record = new KeywordToken("record");
        public readonly static KeywordToken Switch = new KeywordToken("switch");
        public readonly static KeywordToken This = new KeywordToken("this");
        public readonly static KeywordToken Throw = new KeywordToken("throw");
        public readonly static KeywordToken True = new KeywordToken("true");
        public readonly static KeywordToken Try = new KeywordToken("try");
        public readonly static KeywordToken Typeof = new KeywordToken("typeof");
        public readonly static KeywordToken Uint = new KeywordToken("uint");
        public readonly static KeywordToken Ulong = new KeywordToken("ulong");
        public readonly static KeywordToken Unchecked = new KeywordToken("unchecked");
        public readonly static KeywordToken Unsafe = new KeywordToken("unsafe");
        public readonly static KeywordToken Ushort = new KeywordToken("ushort");
        public readonly static KeywordToken Using = new KeywordToken("using");
        public readonly static KeywordToken Virtual = new KeywordToken("virtual");
        public readonly static KeywordToken Void = new KeywordToken("void");
        public readonly static KeywordToken Volatile = new KeywordToken("volatile");
        public readonly static KeywordToken While = new KeywordToken("while");
    }
}
