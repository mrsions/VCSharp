using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace VCSharp.Compiler.Tokens
{
    public class Token
    {
        protected string m_Text;

        public Token()
        {
        }

        public Token(string text)
        {
            m_Text = text;
        }

        public virtual string Text { get => m_Text; set => m_Text = value; }

        public readonly static Token EndOfToken = new Token();

        public char Char => m_Text?.Length == 1 ? m_Text[0] : throw new NotImplementedException();

        public bool IsMatch(char c)
        {
            return m_Text.Length == 1 && m_Text[0] == c;
        }

        public bool IsMatch(string? msg)
        {
            return m_Text == msg;
        }

        public override string ToString()
        {
            return $"{GetType().Name}(Text={m_Text})";
        }

        public virtual string ToViewString()
        {
            return m_Text;
        }

        public bool IsIdentifierToken => this is IdentifierToken || (this is KeywordToken kt && kt.IsIdentifier);

        public static bool operator ==(Token token, char val) => token.IsMatch(val);
        public static bool operator !=(Token token, char val) => !token.IsMatch(val);
        public static bool operator ==(char val, Token token) => token.IsMatch(val);
        public static bool operator !=(char val, Token token) => !token.IsMatch(val);
    }

    public class NumberToken : Token
    {
        public NumberToken(object value, string text) : base(text)
        {
            Value = value;
        }

        public object Value { get; }

        public override string ToString()
        {
            return $"{GetType().Name}(Value={Value}, ValueType={Value.GetType().Name}, Text={m_Text})";
        }
    }

    public class StringToken : Token
    {
        private string Value { get; }

        public StringToken(string value, string text) : base(text)
        {
            Value = value;
        }

        public override string ToString()
        {
            return $"{GetType().Name}(Value={Value}, Text={m_Text})";
        }
    }

    public class CharToken : Token
    {
        private char Value { get; }

        public CharToken(char value, string text) : base(text)
        {
            Value = value;
        }

        public override string ToString()
        {
            return $"{GetType().Name}(Value={Value}, Text={m_Text})";
        }
    }

    public class WhiteSpaceToken : Token
    {
        public WhiteSpaceToken(string text) : base(text)
        {
        }

        public override string ToViewString()
        {
            return "";
        }
    }

    public class IdentifierToken : Token
    {
        public IdentifierToken(string name) : base(name)
        {
        }
    }

    public class ErrorToken : Token
    {
        public ErrorToken(string text, string errorCode, string errorDescription) : base(text)
        {
            ErrorCode = errorCode;
            ErrorDescription = errorDescription;
        }

        public string ErrorCode { get; }
        public string ErrorDescription { get; }

        public override string ToString()
        {
            return $"{GetType().Name}(Error={ErrorCode}, Desc={ErrorDescription}, Text={m_Text})";
        }
    }

    public class CommentToken : Token
    {
        public CommentToken(string name) : base(name)
        {
        }
    }
}
