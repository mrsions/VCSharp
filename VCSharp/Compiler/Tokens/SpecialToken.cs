using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections.ObjectModel;

namespace VCSharp.Compiler.Tokens
{
    public class SpecialToken : Token
    {
        public SpecialToken(string name) : base(name)
        {
        }

        public override string ToString()
        {
            return Text;
        }

        ///////////////////////////////////////////////////////////////////////////////////////
        //
        //                    HEADER NAME
        //
        ///////////////////////////////////////////////////////////////////////////////////////

        public readonly static SpecialToken[] Tokens;
        public readonly static Dictionary<string, SpecialToken> TokenDict;

        static SpecialToken()
        {
            var fields = typeof(SpecialToken).GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static)
                .Where(v => v.FieldType == typeof(SpecialToken));

            Tokens = fields.Select(v => (SpecialToken)v.GetValue(null)!).ToArray();
            TokenDict = Tokens.ToDictionary(k => k.m_Text);
        }

        public readonly static SpecialToken Dot = new SpecialToken(".");
        public readonly static SpecialToken StringBrace = new SpecialToken("\"");
        public readonly static SpecialToken StringExpression = new SpecialToken("$");
        public readonly static SpecialToken StringMultiline = new SpecialToken("@");
    }
}
