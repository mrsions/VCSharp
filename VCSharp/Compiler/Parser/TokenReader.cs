using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using VCSharp.Compiler.Tokens;
using System.Collections;
using VCSharp.Inline;
using System.Diagnostics.CodeAnalysis;

namespace VCSharp.Compiler.Parser
{
    public class TokenReader
    {
        public List<Token> Tokens;

        public int index;

        public TokenReader(List<Token> tokens)
        {
            Tokens = tokens;
            index = 0;
        }

        public bool IsEnd() => index >= Tokens.Count;

        public bool TryReadNext([NotNullWhen(true)] out Token? token, bool escapeWhiteSpace = false)
        {
            if (index < Tokens.Count)
            {
                token = Tokens[index++];
                if (escapeWhiteSpace && token is WhiteSpaceToken)
                {
                    return TryReadNext(out token);
                }
                else
                {
                    return true;
                }
            }
            else
            {
                token = null;
                return false;
            }
        }

        public Token? ReadNext()
        {
            return Tokens[index++];
        }

        public void Redo()
        {
            index--;
        }
        public void Redo(int v)
        {
            index -= v;
        }

        public IEnumerable<Token> GetNextAll()
        {
            for (int i = index; i < Tokens.Count; i++)
            {
                yield return Tokens[i];
            }
        }

        public List<Token> ReadToNextSemicolon() => ReadToNext(static (token) => token != PunctuatorToken.Semicolon, true, false);

        public List<Token> ReadToNext(Func<Token, bool> allow, bool consumeBreak, bool includeBreak)
        {
            List<Token> result = new List<Token>();
            ReadToNext(result, allow, consumeBreak, includeBreak);
            return result;
        }

        public void ReadToNext(List<Token> result, Func<Token, bool> allow, bool consumeBreak, bool includeBreak)
        {
            while (TryReadNext(out var token))
            {
                if (allow(token))
                {
                    result.Add(token);
                }
                else
                {
                    if (includeBreak)
                    {
                        result.Add(token);
                    }

                    if (!consumeBreak)
                    {
                        Redo();
                    }

                    break;
                }
            }
        }

    }
}