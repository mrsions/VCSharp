using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using VCSharp.Compiler.Tokens;
using System.Collections;
using VCSharp.Inline;
using static VCSharp.Compiler.Parser.CompileException;
using System.Reflection;
using VCSharp.Reflection;

namespace VCSharp.Compiler.Parser
{

    public class BraceToken : Token
    {
        public BraceToken parent;
        public Token startToken;
        public Token endToken;
        public bool isClosed;
        public List<Token> tokens = new();
    }

    public class SyntaxParser
    {
        public Lexer lexer;
        public Stack<Token> reuse_token = new();

        public SyntaxParser(Lexer lexer)
        {
            this.lexer = lexer;
        }

        ///////////////////////////////////////////////////////////////////////////////////////
        //
        //                    COMPILE
        //
        ///////////////////////////////////////////////////////////////////////////////////////

        public void Compile(Lexer lexer)
        {
            BraceToken brace = MakeBrace(lexer);

            var nop = new NamespaceScope();

            CompileReader(nop, brace.tokens);
        }

        private BraceToken MakeBrace(Lexer lexer)
        {
            BraceToken root = new BraceToken();
            BraceToken current = root;

            Token token;
            while ((token = lexer.NextToken()) != Token.EndOfToken)
            {
                if (token is WhiteSpaceToken) continue;

                //---------------------------------------------------------------------------------
                if (token == PunctuatorToken.LeftBrace)
                {
                    current = OpenBrace(current, token, PunctuatorToken.RightBrace);
                }
                else if (token == PunctuatorToken.RightBrace)
                {
                    current = CloseBrace(current, token, () => CS1513());
                }
                //---------------------------------------------------------------------------------
                else if (token == PunctuatorToken.LeftBracket)
                {
                    current = OpenBrace(current, token, PunctuatorToken.RightBracket);
                }
                else if (token == PunctuatorToken.RightBracket)
                {
                    current = CloseBrace(current, token, () => CS1026());
                }
                //---------------------------------------------------------------------------------
                else if (token == PunctuatorToken.LeftParenthesis)
                {
                    current = OpenBrace(current, token, PunctuatorToken.LeftParenthesis);
                }
                else if (token == PunctuatorToken.RightParenthesis)
                {
                    current = CloseBrace(current, token, () => CS1586());
                }
                //---------------------------------------------------------------------------------
                else
                {
                    current.tokens.Add(token);
                }
            }

            if (!root.isClosed)
            {
                throw CS1513();
            }

            return root;

            static BraceToken OpenBrace(BraceToken current, Token startToken, Token endToken)
            {
                BraceToken newBrace = new BraceToken();
                newBrace.parent = current;
                newBrace.startToken = startToken;
                newBrace.endToken = endToken;

                current.tokens.Add(newBrace);
                return newBrace;
            }

            static BraceToken CloseBrace(BraceToken current, Token token, Func<Exception> Exception)
            {
                if (current.endToken != token)
                {
                    throw Exception();
                }

                if (current.parent == null)
                {
                    throw Exception();
                }

                current.isClosed = true;
                return current.parent;
            }
        }

        public void CompileReader(Scope scope, List<Token> list)
        {
            CompileReader(scope, new TokenReader(list));
        }
        public void CompileReader(Scope scope, TokenReader reader)
        {
            List<Token> tokens = new();
            while (!reader.IsEnd())
            {
                tokens.Clear();
                reader.ReadToNext(tokens, static (token) =>
                {
                    return token == KeywordToken.Class
                        || token == KeywordToken.Struct
                        || token == KeywordToken.Record
                        || token == KeywordToken.Delegate
                        || token == KeywordToken.Interface
                        || token == KeywordToken.Enum
                        || token == KeywordToken.Namespace
                        || token == KeywordToken.Using;
                }, true, false);

                if (tokens.Count == 0) break;

                var token = tokens.Last();
                if (token == KeywordToken.Using)
                {
                    ReadUsingScope(scope, reader);
                }
                else if (token == KeywordToken.Namespace)
                {
                    ReadNamespaceScope(scope, reader);
                }
                else if (token == KeywordToken.Class)
                {
                    ReadClassScope(scope, tokens, reader);
                }
                else if (token == KeywordToken.Struct)
                {
                }
                else if (token == KeywordToken.Record)
                {

                }
                else if (token == KeywordToken.Delegate)
                {

                }
                else if (token == KeywordToken.Interface)
                {

                }
                else if (token == KeywordToken.Enum)
                {
                }
                else
                {
                    continue;
                }
            }
        }

        private void ReadClassScope(Scope scope, List<Token> tokens, TokenReader reader)
        {
            ClassScope curCS = new ClassScope();
            curCS.parent = scope;

            // 모디파이어 읽기
            VMemberFlags flags = 0;
            foreach (var token in tokens)
            {
                if (ApplyAccessModifier(token, ref flags, true, true))
                {
                    // applied
                }
                else if (token == KeywordToken.Sealed)
                {
                    SetMemberFlags(token, VMemberFlags.Sealed, ref flags);
                }
                else if (token == KeywordToken.File)
                {
                    SetMemberFlags(token, VMemberFlags.File, ref flags);
                }
                else if (token == KeywordToken.Static)
                {
                    SetMemberFlags(token, VMemberFlags.Static, ref flags);
                }
                else if (token == KeywordToken.Unsafe)
                {
                    SetMemberFlags(token, VMemberFlags.Unsafe, ref flags);
                }
                else if (token == KeywordToken.Partial)
                {
                    SetMemberFlags(token, VMemberFlags.Partial, ref flags);
                }
                else if (token == KeywordToken.Abstract)
                {
                    SetMemberFlags(token, VMemberFlags.Abstract, ref flags);
                }
                else if (token == KeywordToken.Partial)
                {
                    SetMemberFlags(token, VMemberFlags.Partial, ref flags);
                }
                else
                {
                    throw CS1519(KeywordToken.Class.Text);
                }
            }

            curCS.name = ReadDefineTypeName(reader, true, false);

        }

        private static void SetMemberFlags(Token token, VMemberFlags flag, ref VMemberFlags flags)
        {
            if ((flags & flag) != 0)
            {
                throw CS1004(token.Text);
            }

            flags |= flag;
        }

        private bool ApplyAccessModifier(Token token, ref VMemberFlags modifier, bool useFile, bool useProtected)
        {
            VMemberFlags flag;
            if (token == KeywordToken.Public) flag = VMemberFlags.Public;
            else if (token == KeywordToken.Private) flag = VMemberFlags.Private;
            else if (token == KeywordToken.Internal) flag = VMemberFlags.Internal;
            else if (useProtected && token == KeywordToken.Protected) flag = VMemberFlags.Protected;
            else if (useFile && token == KeywordToken.File) flag = VMemberFlags.File;
            else return false;

            // 같은 token 중복
            if ((modifier & flag) != 0)
            {
                throw CS1004(token.Text);
            }

            VMemberFlags befType = modifier & VMemberFlags.AccessModifer;
            switch (flag)
            {
                case VMemberFlags.Private:
                    if (befType == VMemberFlags.Protected)
                    {
                        flag = VMemberFlags.ProtectedPrivate;
                    }
                    else goto default;
                    break;

                case VMemberFlags.Internal:
                    if (befType == VMemberFlags.Protected)
                    {
                        flag = VMemberFlags.ProtectedInternal;
                    }
                    else goto default;
                    break;

                case VMemberFlags.Protected:
                    if (befType == VMemberFlags.Private)
                    {
                        flag = VMemberFlags.ProtectedPrivate;
                    }
                    else if (befType == VMemberFlags.Internal)
                    {
                        flag = VMemberFlags.ProtectedInternal;
                    }
                    else goto default;
                    break;

                default:
                    if (befType != 0) throw CS0107();
                    break;
            }

            modifier = (modifier & ~VMemberFlags.AccessModifer) | flag;
            return true;
        }

        private NameScope ReadDefineTypeName(TokenReader reader, bool useGeneric, bool useInOut)
        {
            NameScope root = new NameScope();
            NameScope curr = root;
            int genericDepth = useGeneric ? 1 : 0;
            int depth = 0;
            while (reader.TryReadNext(out var token))
            {
                if (token is WhiteSpaceToken)
                {
                    continue;
                }

                else if (token.IsIdentifierToken)
                {
                    curr.Names.Add(token);
                }

                #region Generic
                else if (token == PunctuatorToken.LessThan && (depth < genericDepth))
                {
                    if (curr.Children != null)
                    {
                        throw CS1003(PunctuatorToken.Comma.Text);
                    }

                    depth++;
                    curr.Children = new List<NameScope>();
                    curr = curr.NewChild();
                }
                else if (token == PunctuatorToken.GreaterThan && curr.Children != null)
                {
                    if (curr.parent is NameScope parent)
                    {
                        curr = parent;
                    }
                    else
                    {
                        break;
                    }
                }
                #endregion Generic

                #region Generic Argument
                else if (token == PunctuatorToken.Comma)
                {
                    if (curr.parent is NameScope parent)
                    {
                        curr = parent.NewChild();
                    }
                    else
                    {
                        reader.Redo();
                        break;
                    }
                }
                else if ((token == KeywordToken.In || token == KeywordToken.Out) && useInOut)
                {
                    if (curr.Names.Count != 0)
                    {
                        throw CS1003(PunctuatorToken.Comma.Text);
                    }

                    curr.Names.Add(token);
                }
                #endregion Generic Argument

                else if (token == PunctuatorToken.Colon || token is BraceToken)
                {
                    reuse_token.Push(token);
                    break;
                }
                else
                {
                    throw CS1519(token.Text);
                }
            }

            if (curr.parent != null)
            {
                throw CS1003(PunctuatorToken.GreaterThan.Text);
            }

            return curr;
        }

        static KeywordToken[] allow_type_names = new KeywordToken[]
        {
            KeywordToken.Byte,
            KeywordToken.Sbyte,
            KeywordToken.Short,
            KeywordToken.Ushort,
            KeywordToken.Int,
            KeywordToken.Uint,
            KeywordToken.Long,
            KeywordToken.Ulong,
            KeywordToken.Float,
            KeywordToken.Double,
            KeywordToken.Decimal,
            KeywordToken.Bool,
            KeywordToken.Char,
            KeywordToken.String,
            KeywordToken.Object,
            KeywordToken.Nint,
            KeywordToken.Nuint,
        };

        private NameScope ReadReferenceTypeName(TokenReader reader, int genericDepth, bool useInOut, Func<Token, bool> breakFunc)
        {
            NameScope root = new NameScope();
            NameScope curr = root;
            int depth = 0;
            while (reader.TryReadNext(out var token))
            {
                if (token is WhiteSpaceToken)
                {
                    continue;
                }

                else if (token.IsIdentifierToken)
                {
                    var last = curr.Names.LastOrDefault();
                    if (last != null && last != SpecialToken.Dot)
                    {
                        // 최상위에서 이전이 '.' 이 아닌 상태에서 identity가 나오면 종료
                        if (curr.parent == null)
                        {
                            reader.Redo();
                            break;
                        }
                        else
                        {
                            throw CS1001();
                        }
                    }

                    curr.Names.Add(token);
                }

                else if (token is KeywordToken kt && Array.IndexOf(allow_type_names, token) != -1)
                {
                    if (curr.Names.Count != 0)
                    {
                        throw CS1001();
                    }

                    curr.Names.Add(token);
                }

                else if (token == SpecialToken.Dot)
                {
                    if (curr.Names.Count == 0)
                    {
                        throw CS1519(token.Text);
                    }

                    var last = curr.Names.Last();
                    if (last == SpecialToken.Dot)
                    {
                        throw CS1001();
                    }

                    curr.Names.Add(token);
                }

                #region Generic
                else if (token == PunctuatorToken.LessThan && (depth < genericDepth))
                {
                    if (curr.Children != null)
                    {
                        throw CS1003(PunctuatorToken.Comma.Text);
                    }

                    depth++;
                    curr.Children = new List<NameScope>();
                    curr = curr.NewChild();
                }
                else if (token == PunctuatorToken.GreaterThan && curr.Children != null)
                {
                    if (curr.parent is NameScope parent)
                    {
                        curr = parent;
                    }
                    else
                    {
                        break;
                    }
                }
                #endregion Generic

                #region Generic Argument
                else if (token == PunctuatorToken.Comma)
                {
                    if (curr.parent is NameScope parent)
                    {
                        curr = parent.NewChild();
                    }
                    else
                    {
                        reader.Redo();
                        break;
                    }
                }
                else if ((token == KeywordToken.In || token == KeywordToken.Out) && useInOut)
                {
                    if (curr.Names.Count != 0)
                    {
                        throw CS1003(PunctuatorToken.Comma.Text);
                    }

                    curr.Names.Add(token);
                }
                #endregion Generic Argument

                else if(token == PunctuatorToken.LeftBracket || token == PunctuatorToken.Conditional)
                {
                    reuse_token.Push(token);
                    break;
                }

                else if (breakFunc(token))
                {
                    reuse_token.Push(token);
                    break;
                }

                else
                {
                    throw CS1519(token.Text);
                }
            }

            if (curr.parent != null)
            {
                throw CS1003(PunctuatorToken.GreaterThan.Text);
            }

            return curr;
        }

        private void ReadUsingScope(Scope scope, TokenReader reader)
        {
            var line = reader.ReadToNextSemicolon();

            var nsScope = scope as NamespaceScope;

            // using이 가능한것은 namespace scope일때만 가능하다.
            if (nsScope == null) throw CS0687();

            if (!nsScope.CanUsing) throw CS1529();

            UsingOperator op = new UsingOperator();
            List<Token> tokens = new List<Token>();

            foreach (var token in line)
            {
                // using static
                if (token == KeywordToken.Static)
                {
                    if (tokens.Count != 0) throw CS0116();

                    if (op.IsStatis) throw CS1041(token.Text);

                    op.IsStatis = true;
                }
                // using {id} = 
                else if (token == PunctuatorToken.Assignment)
                {
                    if (tokens.Count != 1// '='는 앞에 한개만 있어야한다.
                        || tokens[0] is not IdentifierToken // 앞은 무조건 식별자여야한다.
                        || op.IsStatis) // static이면 안된다.
                    {
                        throw CS1525(token.Text);
                    }

                    op.alias = tokens[0].Text;
                    tokens.Clear();
                }
                // using (static) ({alias} =) NameSpace.
                else if (token == SpecialToken.Dot)
                {
                    // dot으로 시작할 수 없다.
                    if (tokens.Count == 0) throw CS1001();

                    // 연속된 dot 오류
                    if (tokens.Last() == SpecialToken.Dot) throw CS1001();

                    tokens.Add(token);
                }
                // 식별자
                else if (token is IdentifierToken)
                {
                    // 연속된 식별자를 입력할 수 없다.
                    if (tokens.LastOrDefault() == SpecialToken.Dot) throw CS0210();

                    tokens.Add(token);
                }
                // 오류
                else
                {
                    if (token == PunctuatorToken.LessThan)
                    {
                        throw CS0307();
                    }

                    throw CS1525(token.Text);
                }
            }

            op.value = ToString(tokens);

            nsScope.usings.Add(op);
        }

        private void ReadNamespaceScope(Scope scope, TokenReader reader)
        {
            // 상위가 namespace가 아니면 제외된다.
            if (scope is not NamespaceScope curNS)
            {
                throw CS8955();
            }

            // 현재 namespace가 filenamespace라면 제외된다.
            if (curNS.isFileNamespace)
            {
                throw CS8955();
            }

            var line = reader.ReadToNext(static (token) =>
            {
                return (token is IdentifierToken || token == SpecialToken.Dot)
                        && token != PunctuatorToken.Semicolon
                        && token is not BraceToken;
            }, true, true);
            var lineLast = line.Last();

            #region 이름 찾기
            List<Token> tokens = new List<Token>();
            foreach (var token in line.Take(line.Count - 1))
            {
                if (token == SpecialToken.Dot)
                {
                    // dot으로 시작할 수 없다.
                    if (tokens.Count == 0) throw CS1001();

                    // 연속된 dot 오류
                    if (tokens.Last() == SpecialToken.Dot) throw CS1001();

                    tokens.Add(token);
                }
                // 식별자
                else if (token is IdentifierToken)
                {
                    // 연속된 식별자를 입력할 수 없다.
                    if (tokens.LastOrDefault() == SpecialToken.Dot) throw CS0210();

                    tokens.Add(token);
                }
                // 사용할 수 없는 별칭
                else
                {
                    throw CS0000();
                }
            }
            #endregion
            string name = ToString(tokens);

            // 파일 네임스페이스
            if (lineLast == PunctuatorToken.Semicolon)
            {
                // 파일네임스페이스인데 이전에 선언된 네임스페이스가 있다면?
                if (curNS.name != null || curNS.parent != null)
                {
                    throw CS8955();
                }

                curNS.name = name;
                curNS.isFileNamespace = true;
                curNS.tokens.AddRange(reader.GetNextAll());
            }
            // brace로 이뤄진 네임스페이스
            else if (lineLast is BraceToken brace)
            {
                // 첫이름의 경우, 기본 namespace를 그대로 사용한다.
                if (curNS.name == null)
                {
                    curNS.name = name;
                }
                // 새로운 네임스페이스를 생성한다
                else
                {
                    curNS = curNS.CreateSubNamespace(name);
                }

                curNS.tokens.AddRange(brace.tokens);
            }
            else
            {
                throw CS0000();
            }

            CompileReader(curNS, curNS.tokens);
        }

        ///////////////////////////////////////////////////////////////////////////////////////
        //
        //                    HELPER
        //
        ///////////////////////////////////////////////////////////////////////////////////////

        private string ToString(List<Token> tokens)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var v in tokens)
            {
                if (v is PunctuatorToken pt)
                {
                    sb.Append(pt.Text);
                }
                else if (v is IdentifierToken id)
                {
                    sb.Append(id.Text);
                }
                else
                {
                    throw CS0000();
                }
            }
            return sb.ToString();
        }

    }
}