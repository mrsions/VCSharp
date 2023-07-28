using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections.ObjectModel;

namespace VCSharp.Compiler.Tokens
{
    public class PunctuatorToken : Token
    {
        public PunctuatorToken(string name) : base(name)
        {
        }

        ///////////////////////////////////////////////////////////////////////////////////////
        //
        //                    HEADER NAME
        //
        ///////////////////////////////////////////////////////////////////////////////////////

        public readonly static PunctuatorToken[] Tokens;
        public readonly static Dictionary<string, PunctuatorToken> TokenDict;
        public readonly static char[] TokenStarts;
        public readonly static int TokenMaxLength;

        static PunctuatorToken()
        {
            var fields = typeof(PunctuatorToken).GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static)
                .Where(v => v.FieldType == typeof(PunctuatorToken));

            Tokens = fields.Select(v => (PunctuatorToken)v.GetValue(null)!).OrderByDescending(v => v.Text).ToArray();
            TokenDict = Tokens.ToDictionary(k => k.m_Text);
            TokenStarts = Tokens.Select(v => v.Text[0]).Distinct().ToArray();
            TokenMaxLength = Tokens.Max(v => v.Text.Length);
        }

        public readonly static PunctuatorToken LeftBrace = new PunctuatorToken("{");
        public readonly static PunctuatorToken RightBrace = new PunctuatorToken("}");
        public readonly static PunctuatorToken LeftParenthesis = new PunctuatorToken("(");
        public readonly static PunctuatorToken RightParenthesis = new PunctuatorToken(")");
        public readonly static PunctuatorToken LeftBracket = new PunctuatorToken("[");
        public readonly static PunctuatorToken RightBracket = new PunctuatorToken("]");
        public readonly static PunctuatorToken Colon = new PunctuatorToken(":");
        public readonly static PunctuatorToken DoubleColon = new PunctuatorToken("::");
        public readonly static PunctuatorToken Semicolon = new PunctuatorToken(";");
        public readonly static PunctuatorToken Comma = new PunctuatorToken(",");
        public readonly static PunctuatorToken LessThan = new PunctuatorToken("<");
        public readonly static PunctuatorToken GreaterThan = new PunctuatorToken(">");
        public readonly static PunctuatorToken LessThanOrEqual = new PunctuatorToken("<=");
        public readonly static PunctuatorToken GreaterThanOrEqual = new PunctuatorToken(">=");
        public readonly static PunctuatorToken Equality = new PunctuatorToken("==");
        public readonly static PunctuatorToken Inequality = new PunctuatorToken("!=");
        public readonly static PunctuatorToken Plus = new PunctuatorToken("+");
        public readonly static PunctuatorToken Minus = new PunctuatorToken("-");
        public readonly static PunctuatorToken Multiply = new PunctuatorToken("*");
        public readonly static PunctuatorToken Modulo = new PunctuatorToken("%");
        public readonly static PunctuatorToken Increment = new PunctuatorToken("++");
        public readonly static PunctuatorToken Decrement = new PunctuatorToken("--");
        public readonly static PunctuatorToken LeftShift = new PunctuatorToken("<<");
        public readonly static PunctuatorToken SignedRightShift = new PunctuatorToken(">>");
        public readonly static PunctuatorToken UnsignedRightShift = new PunctuatorToken(">>>");
        public readonly static PunctuatorToken BitwiseAnd = new PunctuatorToken("&");
        public readonly static PunctuatorToken BitwiseOr = new PunctuatorToken("|");
        public readonly static PunctuatorToken BitwiseXor = new PunctuatorToken("^");
        public readonly static PunctuatorToken BitwiseNot = new PunctuatorToken("~");
        public readonly static PunctuatorToken LogicalNot = new PunctuatorToken("!");
        public readonly static PunctuatorToken LogicalAnd = new PunctuatorToken("&&");
        public readonly static PunctuatorToken LogicalOr = new PunctuatorToken("||");
        public readonly static PunctuatorToken Conditional = new PunctuatorToken("?");
        public readonly static PunctuatorToken Assignment = new PunctuatorToken("=");
        public readonly static PunctuatorToken CompoundAdd = new PunctuatorToken("+=");
        public readonly static PunctuatorToken CompoundSubtract = new PunctuatorToken("-=");
        public readonly static PunctuatorToken CompoundMultiply = new PunctuatorToken("*=");
        public readonly static PunctuatorToken CompoundModulo = new PunctuatorToken("%=");
        public readonly static PunctuatorToken CompoundLeftShift = new PunctuatorToken("<<=");
        public readonly static PunctuatorToken CompoundSignedRightShift = new PunctuatorToken(">>=");
        public readonly static PunctuatorToken CompoundUnsignedRightShift = new PunctuatorToken(">>>=");
        public readonly static PunctuatorToken CompoundBitwiseAnd = new PunctuatorToken("&=");
        public readonly static PunctuatorToken CompoundBitwiseOr = new PunctuatorToken("|=");
        public readonly static PunctuatorToken CompoundBitwiseXor = new PunctuatorToken("^=");
        public readonly static PunctuatorToken LambdaArrow = new PunctuatorToken("=>");

        //public readonly static PunctuatorToken Dot = new PunctuatorToken(".");
        //public readonly static PunctuatorToken StringBrace = new PunctuatorToken("\"");
        //public readonly static PunctuatorToken StringExpression = new PunctuatorToken("$");
        //public readonly static PunctuatorToken StringMultiline = new PunctuatorToken("@");
    }
}
