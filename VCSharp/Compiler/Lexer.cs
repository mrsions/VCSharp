using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Security.Cryptography;
using Microsoft.VisualBasic;
using VCSharp.Compiler.Tokens;

namespace VCSharp.Compiler
{
    public class Lexer
    {
        private TextReader reader;
        private StringBuilder build_buffer;
        private Stack<char> reuse_buffer;
        private bool end_of_input;

        public Lexer(TextReader reader)
        {
            this.reader = reader;
            build_buffer = new StringBuilder(1024);
            reuse_buffer = new();
        }

        private int NextReaderData()
        {
            if (reuse_buffer.Count > 0)
            {
                return reuse_buffer.Pop();
            }

            return reader.Read();
        }

        private bool TryGetChar(out char c)
        {
            int v = NextReaderData();
            if (v != -1)
            {
                c = (char)v;
                return true;
            }

            c = '\uFFFF';
            return false;
        }

        public Token NextToken()
        {
            if (end_of_input || !TryGetChar(out char c))
            {
                return Token.EndOfToken;
            }

            build_buffer.Clear();

            //---------------------------------------------------------------------------------
            if (IsWhiteSpace(c))
            {
                build_buffer.Append(c);
                return ReadSpace();
            }
            //---------------------------------------------------------------------------------
            if (IsComment(c))
            {
                build_buffer.Append(c);
                return ReadComment();
            }
            //---------------------------------------------------------------------------------
            else if (IsKeywordStartChar(c))
            {
                reuse_buffer.Push(c);
                return ReadKeyword();
            }
            //---------------------------------------------------------------------------------
            else if (IsIdentifierStartChar(c))
            {
                reuse_buffer.Push(c);
                return ReadIdentifier();
            }
            //---------------------------------------------------------------------------------
            else if (IsPunctuator(c))
            {
                reuse_buffer.Push(c);
                return ReadPunctuator();
            }
            //---------------------------------------------------------------------------------
            else if (IsNumber(c))
            {
                try
                {
                    build_buffer.Append(c);
                    return ReadNumber();
                }
                catch (OverflowException)
                {
                    return new ErrorToken(build_buffer.ToString(), "CS1021", "Integral constant is too large");
                }
            }
            //---------------------------------------------------------------------------------
            else if (IsStringLiteral(c))
            {
                build_buffer.Append(c);
                return ReadString(false);
            }
            else if (IsCharLiteral(c))
            {
                build_buffer.Append(c);
                return ReadCharLiteral();
            }
            //---------------------------------------------------------------------------------
            else if (c == SpecialToken.StringMultiline) // @
            {
                return ReadStringMultiToken(c, true, SpecialToken.StringExpression);
            }
            else if (c == SpecialToken.StringExpression) // $
            {
                return ReadStringMultiToken(c, false, SpecialToken.StringMultiline);
            }
            //---------------------------------------------------------------------------------
            else if (c == SpecialToken.Dot)
            {
                if (TryGetChar(out char c2))
                {
                    if (IsNumber(c2))
                    {
                        build_buffer.Append('0');
                        build_buffer.Append(c);
                        build_buffer.Append(c2);
                        try
                        {
                            return ReadNumberFloating();
                        }
                        catch (OverflowException)
                        {
                            return new ErrorToken(build_buffer.ToString(), "CS1021", "Integral constant is too large");
                        }
                    }
                    else
                    {
                        reuse_buffer.Push(c2);
                        return SpecialToken.Dot;
                    }
                }
                else
                {
                    return new ErrorToken(build_buffer.ToString(), "CS1022", "Type or namespace definition, or end-of-file expected");
                }
            }
            //---------------------------------------------------------------------------------
            else
            {
                return new ErrorToken(build_buffer.ToString(), "CS1056", $"Unexpected character '{ToViewChar(c)}'");
            }
        }

        #region Punctuator

        ///////////////////////////////////////////////////////////////////////////////////////
        //
        //                    Punctuator
        //
        ///////////////////////////////////////////////////////////////////////////////////////

        private static bool IsPunctuator(char c)
        {
            foreach (var token in PunctuatorToken.TokenStarts)
            {
                if (token == c)
                {
                    return true;
                }
            }
            return false;
        }

        private Token ReadPunctuator()
        {
            while (TryGetChar(out char c))
            {
                build_buffer.Append(c);

                if (PunctuatorToken.TokenDict.TryGetValue(build_buffer.ToString(), out var result))
                {
                    return result;
                }

                if (build_buffer.Length >= PunctuatorToken.TokenMaxLength)
                {
                    break;
                }
            }

            for (int i = build_buffer.Length - 1; i > 0; i--)
            {
                reuse_buffer.Push(build_buffer[i]);
            }
            build_buffer.Remove(1, build_buffer.Length - 1);

            return new ErrorToken(build_buffer.ToString(), "CS1525", $"Invalid expression term '{build_buffer}'");
        }
        #endregion Punctuator

        #region Keyword

        ///////////////////////////////////////////////////////////////////////////////////////
        //
        //                    Keyword
        //
        ///////////////////////////////////////////////////////////////////////////////////////

        private static bool IsKeywordStartChar(char c)
        {
            foreach (var token in KeywordToken.TokenStarts)
            {
                if (token == c)
                {
                    return true;
                }
            }
            return false;
        }

        private Token ReadKeyword()
        {
            while (TryGetChar(out char c))
            {
                build_buffer.Append(c);

                if (KeywordToken.TokenDict.TryGetValue(build_buffer.ToString(), out var result))
                {
                    return result;
                }

                if (build_buffer.Length >= KeywordToken.TokenMaxLength)
                {
                    break;
                }
            }

            for (int i = build_buffer.Length - 1; i > 0; i--)
            {
                reuse_buffer.Push(build_buffer[i]);
            }
            build_buffer.Remove(1, build_buffer.Length - 1);

            if (IsIdentifierStartChar(build_buffer[0]))
            {
                reuse_buffer.Push(build_buffer[0]);
                build_buffer.Clear();
                return ReadIdentifier();
            }
            else
            {
                return new ErrorToken(build_buffer.ToString(), "CS1056", $"Unexpected character '{ToViewChar(build_buffer[0])}'");
            }
        }
        #endregion Keyword

        #region Identifier

        ///////////////////////////////////////////////////////////////////////////////////////
        //
        //                    Identifier
        //
        ///////////////////////////////////////////////////////////////////////////////////////

        private static bool IsIdentifierStartChar(char c)
        {
            UnicodeCategory cat = char.GetUnicodeCategory(c);
            return c == '_' || c == '\\' ||
                cat == UnicodeCategory.UppercaseLetter ||
                cat == UnicodeCategory.LowercaseLetter ||
                cat == UnicodeCategory.TitlecaseLetter ||
                cat == UnicodeCategory.ModifierLetter ||
                cat == UnicodeCategory.OtherLetter ||
                cat == UnicodeCategory.LetterNumber;
        }

        private static bool IsIdentifierChar(int c)
        {
            UnicodeCategory cat = char.GetUnicodeCategory((char)c);
            return c == '\\' ||
                cat == UnicodeCategory.UppercaseLetter ||
                cat == UnicodeCategory.LowercaseLetter ||
                cat == UnicodeCategory.TitlecaseLetter ||
                cat == UnicodeCategory.ModifierLetter ||
                cat == UnicodeCategory.OtherLetter ||
                cat == UnicodeCategory.LetterNumber ||
                cat == UnicodeCategory.NonSpacingMark ||
                cat == UnicodeCategory.SpacingCombiningMark ||
                cat == UnicodeCategory.DecimalDigitNumber ||
                cat == UnicodeCategory.ConnectorPunctuation ||
                c == 0x200C ||  // Zero-width non-joiner.
                c == 0x200D;    // Zero-width joiner.
        }

        private Token ReadIdentifier()
        {
            StringBuilder sb = new StringBuilder();

            while (TryGetChar(out char c))
            {
                if (!IsIdentifierChar(c))
                {
                    reuse_buffer.Push(c);
                    break;
                }

                build_buffer.Append(c);
                if (c == '\\')
                {
                    if (TryGetChar(out char c2))
                    {
                        switch (c2)
                        {
                            case 'u':
                                {
                                    int result = ReadEscapeCharUtf16(out var error);
                                    if (error != null) return error;
                                    sb.Append((char)result);
                                }
                                break;
                            case 'U':
                                {
                                    int result = ReadEscapeCharUtf16(out var error);
                                    if (error != null) return error;
                                    sb.Append(char.ConvertFromUtf32(result));
                                }
                                break;
                            default:
                                return new ErrorToken(build_buffer.ToString(), "CS1056", $"Unexpected character '{ToViewChar(c)}'");
                        }
                    }
                    else
                    {
                        return new ErrorToken(build_buffer.ToString(), "CS1056", $"Unexpected character '{ToViewChar(c)}'");
                    }
                }
                else
                {
                    sb.Append(c);
                }
            }

            return new IdentifierToken(build_buffer.ToString());
        }
        #endregion Identifier

        #region Whitespace

        ///////////////////////////////////////////////////////////////////////////////////////
        //
        //                    WHITESPACE
        //
        ///////////////////////////////////////////////////////////////////////////////////////

        private static bool IsWhiteSpace(char c)
        {
            return c == 0x09 || c == 0x0B || c == 0x0C || c == 0x20 || c == 0xA0 ||
                c == 0x1680 || c == 0x180E || c >= 8192 && c <= 8202 || c == 0x202F ||
                c == 0x205F || c == 0x3000 || c == 0xFEFF || c == '\r' || c == '\n';
        }

        private Token ReadSpace()
        {
            while (TryGetChar(out char c))
            {
                if (IsWhiteSpace(c))
                {
                    build_buffer.Append(c);
                }
                else
                {
                    reuse_buffer.Push(c);
                    break;
                }
            }
            return new WhiteSpaceToken(build_buffer.ToString());
        }

        #endregion Whitespace

        #region NUMBER

        ///////////////////////////////////////////////////////////////////////////////////////
        //
        //                    NUMBER
        //
        ///////////////////////////////////////////////////////////////////////////////////////

        private static bool IsNumber(char c)
        {
            return '0' <= c && c <= '9';
        }

        private Token ReadNumber()
        {
            char c, c2, c3;
            while (TryGetChar(out c))
            {
                if (IsNumber(c))
                {
                    build_buffer.Append(c);
                    continue;
                }

                switch (char.ToLower(c))
                {
                    default:
                        reuse_buffer.Push(c);
                        goto RETURN;

                    case '_': // 단위 표현 리터럴
                        build_buffer.Append(c);
                        break;

                    case '.': // 소수점 표현 리터럴
                        // 다음 글자가 숫자일때만 인정된다.
                        if (TryGetChar(out c2) && IsNumber(c2))
                        {
                            build_buffer.Append(c);
                            build_buffer.Append(c2);
                            return ReadNumberFloating();
                        }
                        // 숫자가 아닌 경우 무시한다.
                        else
                        {
                            reuse_buffer.Push(c2);
                            reuse_buffer.Push(c);
                            goto RETURN;
                        }

                    case 'e': // 지수 표현식
                        {
                            // 다음 글자가 숫자나 +-기호일때만 인정된다.
                            if (TryGetChar(out c2))
                            {
                                if (c2 == PunctuatorToken.Minus || c2 == PunctuatorToken.Plus)
                                {
                                    // 기호 이후에 무조건 숫자가 나와야 Exponential이 성립된다.
                                    if (TryGetChar(out c3) && IsNumber(c3))
                                    {
                                        build_buffer.Append(c);
                                        build_buffer.Append(c2);
                                        build_buffer.Append(c3);
                                        return ReadNumberFloating();
                                    }

                                    // 기호 이후 숫자가 나오지않았다면 재사용버퍼에 입력한다.
                                    reuse_buffer.Push(c3);
                                }

                                // 기호가 없을 경우 숫자만 이뤄졌을때 지수표현식으로 인식한다.
                                else if (IsNumber(c2))
                                {
                                    build_buffer.Append(c);
                                    build_buffer.Append(c2);
                                    return ReadNumberFloating();
                                }
                            }

                            reuse_buffer.Push(c2);
                            reuse_buffer.Push(c);
                            goto RETURN;
                        }

                    case 'f':
                        return ReadNumberFloat(c);

                    case 'd':
                        return ReadNumberDouble(c);

                    case 'm':
                        return ReadNumberDecimal(c);

                    case 'u':
                        if (TryGetChar(out c2))
                        {
                            if (char.ToLower(c2) == 'l')
                            {
                                return ReadNumberUnsignedLong(c, c2);
                            }
                            else
                            {
                                reuse_buffer.Push(c2);
                            }
                        }
                        return ReadNumberUnsigned(c);

                    case 'l':
                        if (TryGetChar(out c2))
                        {
                            if (char.ToLower(c2) == 'u')
                            {
                                return ReadNumberUnsignedLong(c, c2);
                            }
                            else
                            {
                                reuse_buffer.Push(c2);
                            }
                        }
                        return ReadNumberLong(c);

                    case 'x': // x는 0다음에만 올 수 있다.
                        if (build_buffer.Length != 1 || build_buffer[0] != '0')
                        {
                            return new ErrorToken(build_buffer.ToString(), "CS0201", "Only assignment, call, increment, decrement, and new object expressions can be used as a statement.");
                        }
                        build_buffer.Append(c);
                        return ReadNumberHex();

                    case 'b': // b는 0다음에만 올 수 있다.
                        if (build_buffer.Length != 1 || build_buffer[0] != '0')
                        {
                            return new ErrorToken(build_buffer.ToString(), "CS0201", "Only assignment, call, increment, decrement, and new object expressions can be used as a statement.");
                        }
                        build_buffer.Append(c);
                        return ReadNumberBianary();
                }
            }

        RETURN:
            return ReadNumberSigned('\0');
        }

        private NumberToken ReadNumberLong(char c)
        {
            var value = long.Parse(GetBuildBufferEscape("_"));
            string text = build_buffer.Append(c).ToString();
            return new NumberToken(value, text);
        }

        private NumberToken ReadNumberUnsigned(char c)
        {
            var value = uint.Parse(GetBuildBufferEscape("_"));
            string text = build_buffer.Append(c).ToString();
            return new NumberToken(value, text);
        }

        private NumberToken ReadNumberUnsignedLong(char a, char b)
        {
            var value = ulong.Parse(GetBuildBufferEscape("_"));
            string text = build_buffer.Append(a).Append(b).ToString();
            return new NumberToken(value, text);
        }

        private NumberToken ReadNumberSigned(char c = '\0')
        {
            var value = int.Parse(GetBuildBufferEscape("_"));
            string text = BuildBufferAppendNotZero(c).ToString();
            return new NumberToken(value, text);
        }

        private NumberToken ReadNumberHexLong(char c)
        {
            var value = long.Parse(GetBuildBufferEscape("_").Substring(2), NumberStyles.HexNumber);
            string text = build_buffer.Append(c).ToString();
            return new NumberToken(value, text);
        }

        private NumberToken ReadNumberHexUnsigned(char c)
        {
            var value = uint.Parse(GetBuildBufferEscape("_").Substring(2), NumberStyles.HexNumber);
            string text = build_buffer.Append(c).ToString();
            return new NumberToken(value, text);
        }

        private NumberToken ReadNumberHexUnsignedLong(char a, char b)
        {
            var value = ulong.Parse(GetBuildBufferEscape("_").Substring(2), NumberStyles.HexNumber);
            string text = build_buffer.Append(a).Append(b).ToString();
            return new NumberToken(value, text);
        }

        private NumberToken ReadNumberHexSigned(char c = '\0')
        {
            var value = int.Parse(GetBuildBufferEscape("_").Substring(2), NumberStyles.HexNumber);
            string text = BuildBufferAppendNotZero(c).ToString();
            return new NumberToken(value, text);
        }

        private NumberToken ReadNumberFloat(char c)
        {
            var value = float.Parse(GetBuildBufferEscape("_"));
            string text = build_buffer.Append(c).ToString();
            return new NumberToken(value, text);
        }

        private NumberToken ReadNumberDouble(char c = '\0')
        {
            var value = double.Parse(GetBuildBufferEscape("_"));
            string text = BuildBufferAppendNotZero(c).ToString();
            return new NumberToken(value, text);
        }

        private NumberToken ReadNumberDecimal(char c)
        {
            var value = decimal.Parse(GetBuildBufferEscape("_"));
            string text = build_buffer.Append(c).ToString();
            return new NumberToken(value, text);
        }

        private NumberToken ReadNumberFloating()
        {
            char c;
            while (TryGetChar(out c))
            {
                if (IsNumber(c))
                {
                    build_buffer.Append(c);
                    continue;
                }

                switch (char.ToLower(c))
                {
                    case '_': // 단위 표현 리터럴
                        build_buffer.Append(c);
                        break;

                    case 'f':
                        return ReadNumberFloat(c);

                    case 'd':
                        return ReadNumberDouble(c);

                    case 'm':
                        return ReadNumberDecimal(c);

                    default:
                        reuse_buffer.Push(c);
                        goto RETURN;
                }
            }

        RETURN:
            return ReadNumberDouble();
        }

        private Token ReadNumberBianary()
        {
            ulong ui8 = 0;
            bool bitStarted = false;
            int size = 0;

            char c, c2;
            while (TryGetChar(out c))
            {
                if (IsNumber(c))
                {
                    build_buffer.Append(c);
                    continue;
                }

                switch (char.ToLower(c))
                {
                    case '0':
                        if (bitStarted) size++;
                        ui8 = ui8 << 1;
                        build_buffer.Append(c);
                        break;

                    case '1':
                        bitStarted = true;
                        size++;
                        ui8 = ui8 << 1 | 1;
                        build_buffer.Append(c);
                        break;

                    case '_': // 단위 표현 리터럴
                        build_buffer.Append(c);
                        break;

                    case 'u':
                        if (TryGetChar(out c2))
                        {
                            if (char.ToLower(c2) == 'l')
                            {
                                return new NumberToken(ui8, build_buffer.ToString());
                            }
                            else
                            {
                                reuse_buffer.Push(c2);
                            }
                        }
                        return new NumberToken((uint)ui8, build_buffer.ToString());

                    case 'l':
                        if (TryGetChar(out c2))
                        {
                            if (char.ToLower(c2) == 'u')
                            {
                                return new NumberToken(ui8, build_buffer.ToString());
                            }
                            else
                            {
                                reuse_buffer.Push(c2);
                            }
                        }
                        return new NumberToken((long)ui8, build_buffer.ToString());

                    default:
                        reuse_buffer.Push(c);
                        goto RETURN;
                }
            }

        RETURN:
            if (size > 64)
            {
                return new ErrorToken(build_buffer.ToString(), "CS1021", "Integral constant is too large");
            }
            else if (size == 64)
            {
                return new NumberToken(ui8, build_buffer.ToString());
            }
            else if (size > 32)
            {
                return new NumberToken((long)ui8, build_buffer.ToString());
            }
            else if (size == 32)
            {
                return new NumberToken((uint)ui8, build_buffer.ToString());
            }
            else
            {
                return new NumberToken((int)ui8, build_buffer.ToString());
            }
        }

        private NumberToken ReadNumberHex()
        {
            char c, c2;
            while (TryGetChar(out c))
            {
                if (IsNumber(c))
                {
                    build_buffer.Append(c);
                    continue;
                }

                switch (char.ToLower(c))
                {
                    case 'a':
                    case 'b':
                    case 'c':
                    case 'd':
                    case 'e':
                    case 'f':
                        build_buffer.Append(c);
                        break;

                    case '_': // 단위 표현 리터럴
                        build_buffer.Append(c);
                        break;

                    case 'u':
                        if (TryGetChar(out c2))
                        {
                            if (char.ToLower(c2) == 'l')
                            {
                                return ReadNumberHexUnsignedLong(c, c2);
                            }
                            else
                            {
                                reuse_buffer.Push(c2);
                            }
                        }
                        return ReadNumberHexUnsigned(c);

                    case 'l':
                        if (TryGetChar(out c2))
                        {
                            if (char.ToLower(c2) == 'u')
                            {
                                return ReadNumberHexUnsignedLong(c, c2);
                            }
                            else
                            {
                                reuse_buffer.Push(c2);
                            }
                        }
                        return ReadNumberHexLong(c);

                    default:
                        reuse_buffer.Push(c);
                        goto RETURN;
                }
            }

        RETURN:
            ulong value = ulong.Parse(GetBuildBufferEscape("_").Substring(2), NumberStyles.HexNumber);
            if (value > long.MaxValue)
            {
                return new NumberToken(value, build_buffer.ToString());
            }
            else if (value > uint.MaxValue)
            {
                return new NumberToken((long)value, build_buffer.ToString());
            }
            else if (value > int.MaxValue)
            {
                return new NumberToken((uint)value, build_buffer.ToString());
            }
            else
            {
                return new NumberToken((int)value, build_buffer.ToString());
            }
        }
        #endregion

        #region StringLiteral

        ///////////////////////////////////////////////////////////////////////////////////////
        //
        //                    STRING
        //
        ///////////////////////////////////////////////////////////////////////////////////////

        private bool IsStringLiteral(char c)
        {
            return c == '"';
        }

        private Token ReadStringMultiToken(char c, bool firstMultiline, Token secondToken)
        {
            // 주석은 $로 시작하여 secondToken이 @일때를 예를 듦
            if (TryGetChar(out char c2))
            {
                if (c2 == SpecialToken.StringBrace) // $"
                {
                    build_buffer.Append(c);
                    build_buffer.Append(c2);
                    return ReadString(firstMultiline);
                }
                else if (c2 == secondToken) // $@
                {
                    if (TryGetChar(out char c3))
                    {
                        if (c3 == SpecialToken.StringBrace) // $@"
                        {
                            build_buffer.Append(c);
                            build_buffer.Append(c2);
                            build_buffer.Append(c3);
                            return ReadString(true);
                        }
                        else
                        {
                            return new ErrorToken(build_buffer.ToString(), "CS9009", $"String must start with quote character: \"");
                        }
                    }
                }
            }
            return new ErrorToken(build_buffer.ToString(), "CS1056", $"Unexpected character '{ToViewChar(c)}'");
        }

        private Token ReadString(bool includeLineBreak)
        {
            StringBuilder sb = new StringBuilder();
            bool escape = false;
            while (TryGetChar(out char c))
            {
                build_buffer.Append(c);
                switch (c)
                {
                    case '"':
                        if (escape)
                        {
                            sb.Append(c);
                            break;
                        }
                        else
                        {
                            goto RETURN;
                        }

                    case '\\':
                        escape = true;
                        break;

                    case '\r':
                    case '\n':
                        if (includeLineBreak)
                        {
                            sb.Append(c);
                        }
                        else
                        {
                            goto RETURN;
                        }
                        break;

                    default:
                        if (escape)
                        {
                            int v = ReadEscapeChar(c, out var error);
                            if (error is not null) return error;

                            if (v > char.MaxValue)
                            {
                                sb.Append(char.ConvertFromUtf32(v));
                            }
                            else
                            {
                                sb.Append((char)v);
                            }
                        }
                        else
                        {
                            sb.Append(c);
                        }
                        break;
                }
            }

        RETURN:
            return new StringToken(sb.ToString(), build_buffer.ToString());
        }

        private bool IsCharLiteral(char c)
        {
            return c == '\'';
        }

        private Token ReadCharLiteral()
        {
            char result = '\0';
            bool escape = false;
            while (TryGetChar(out char c))
            {
                build_buffer.Append(c);
                switch (c)
                {
                    case '\'':
                        if (escape)
                        {
                            result = c;
                        }
                        goto RETURN;

                    case '\\':
                        escape = true;
                        break;

                    default:
                        if (escape)
                        {
                            int v = ReadEscapeChar(c, out var error);
                            if (error is not null) return error;

                            if (v > char.MaxValue)
                            {
                                return new ErrorToken(build_buffer.ToString(), "CS1009", "Unrecognized escape sequence");
                            }
                            result = (char)v;
                        }
                        else
                        {
                            result = c;
                        }
                        break;
                }

                if (IsWhiteSpace(c))
                {
                    build_buffer.Append(c);
                }
                else
                {
                    reuse_buffer.Push(c);
                    break;
                }
            }

        RETURN:
            return new CharToken(result, build_buffer.ToString());
        }

        private int ReadEscapeChar(char c, out Token? error)
        {
            int result = 0;
            switch (c)
            {
                case 'x':
                    result = ReadEscapeCharHex();
                    break;

                case 'u':
                    return ReadEscapeCharUtf16(out error);

                case 'U':
                    return ReadEscapeCharUtf32(out error);

                case '0':
                    result = '\0';
                    break;
                case 'a':
                    result = '\a';
                    break;
                case 'b':
                    result = '\b';
                    break;
                case 'f':
                    result = '\f';
                    break;
                case 'n':
                    result = '\n';
                    break;
                case 'r':
                    result = '\r';
                    break;
                case 't':
                    result = '\t';
                    break;
                case 'v':
                    result = '\v';
                    break;

                // 문자 그대로 입력
                case '\'':
                case '\\':
                    result = c;
                    break;

                default:
                    error = new ErrorToken(build_buffer.ToString(), "CS1009", "Unrecognized escape sequence");
                    return -1;
            }

            error = null;
            return result;
        }

        private int ReadEscapeCharHex()
        {
            int result = 0;
            for (int i = 0; i < 4; i++)
            {
                if (TryGetChar(out char c2))
                {
                    build_buffer.Append(c2);
                    int? v = ToHexInt(c2);
                    if (v.HasValue)
                    {
                        result = result << 4 | v.Value;
                    }
                    else
                    {
                        reuse_buffer.Push(c2);
                        break;
                    }
                }
                else
                {
                    break;
                }
            }

            return result;
        }

        private int ReadEscapeCharUtf16(out Token? error)
        {
            int result = 0;
            for (int i = 0; i < 4; i++)
            {
                if (TryGetChar(out char c2))
                {
                    build_buffer.Append(c2);
                    int? v = ToHexInt(c2);
                    if (v.HasValue)
                    {
                        result = result << 4 | v.Value;
                        continue;
                    }
                }

                error = new ErrorToken(build_buffer.ToString(), "CS1009", "Unrecognized escape sequence");
                return -1;
            }
            error = null;
            return result;
        }

        private int ReadEscapeCharUtf32(out Token? error)
        {
            int result = 0;
            for (int i = 0; i < 8; i++)
            {
                if (TryGetChar(out char c2))
                {
                    build_buffer.Append(c2);
                    int? v = ToHexInt(c2);
                    if (v.HasValue)
                    {
                        result = result << 4 | v.Value;
                        continue;
                    }
                }

                error = new ErrorToken(build_buffer.ToString(), "CS1009", "Unrecognized escape sequence");
                return -1;
            }
            error = null;
            return result;
        }
        #endregion StringLiteral

        #region COMMENT

        ///////////////////////////////////////////////////////////////////////////////////////
        //
        //                    COMMENT
        //
        ///////////////////////////////////////////////////////////////////////////////////////

        private bool IsComment(char c)
        {
            if (c == '/')
            {
                if (TryGetChar(out char c2))
                {
                    if (c2 == '/' || c2 == '*')
                    {
                        reuse_buffer.Push(c2);
                        return true;
                    }
                    return false;
                }
            }
            return false;
        }

        private Token ReadComment()
        {
            if (!TryGetChar(out char c2))
            {
                return new ErrorToken(build_buffer.ToString(), "CS1733", "Expected expression.");
            }

            build_buffer.Append(c2);
            if (c2 == '/')
            {
                while (TryGetChar(out char c))
                {
                    if (c == '\r' || c == '\n')
                    {
                        reuse_buffer.Push(c);
                        break;
                    }

                    build_buffer.Append(c);
                }
            }
            else // if(c2 == '*')
            {
                while (TryGetChar(out char c))
                {
                    build_buffer.Append(c);
                    if (build_buffer.Length >= 4 && build_buffer[^2] == '*' && build_buffer[^1] == '/')
                    {
                        break;
                    }
                }
            }

            return new CommentToken(build_buffer.ToString());
        }

        #endregion COMMENT


        ///////////////////////////////////////////////////////////////////////////////////////
        //
        //                    UTIL
        //
        ///////////////////////////////////////////////////////////////////////////////////////


        private string GetBuildBufferEscape(string c)
        {
            return build_buffer.ToString().Replace(c, string.Empty);
        }

        private StringBuilder BuildBufferAppendNotZero(char c)
        {
            if (c != '\0') build_buffer.Append(c);
            return build_buffer;
        }

        private int? ToHexInt(char c2)
        {
            if ('0' <= c2 && c2 <= '9')
            {
                return c2 - '0';
            }
            else if ('a' <= c2 && c2 <= 'f')
            {
                return c2 - 'a' + 10;
            }
            else if ('A' <= c2 && c2 <= 'F')
            {
                return c2 - 'A' + 10;
            }
            else
            {
                return null;
            }
        }

        private string ToViewChar(char c)
        {
            if (c < ' ')
            {
                return $"0x{(int)c:X2}";
            }
            else
            {
                return c.ToString();
            }
        }
    }
}