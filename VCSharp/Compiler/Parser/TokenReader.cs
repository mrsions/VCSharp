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

        public bool TryReadNext([NotNullWhen(true)] out Token? token)
        {
            if (index < Tokens.Count)
            {
                token = Tokens[index++];
                return true;
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

        public IEnumerable<Token> ReadNextAll()
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

            return result;
        }

    }
}