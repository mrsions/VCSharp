using System;
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
    public class BraceToken : Token
    {

        public BraceToken parent;
        public Token startToken;
        public Token endToken;
        public bool isClosed;
        public List<Token> tokens = new();
    }

    //public class BracketToken : BraceToken
    //{
    //}

    //public class ParenthesisToken : BraceToken
    //{
    //}

    //public class TypeToken : Token
    //{

    //}

    //public class SyntaxPattern
    //{
    //    public object[] Pattern;
    //}

    //public static class SyntaxPatterns
    //{
    //    public static SyntaxPattern classPattern = new SyntaxPattern
    //    {
    //        Pattern = new object[]
    //        {
    //            new[] { null, KeywordToken.Public, KeywordToken.Private, KeywordToken.Internal, KeywordToken.Protected },
    //            typeof(TypeToken),
    //            typeof(IdentifierToken),
    //            typeof(ParenthesisToken),
    //            typeof(BraceToken)
    //        }
    //    };

    //    public static SyntaxPattern fieldPattern = new SyntaxPattern
    //    {
    //        Pattern = new object[]
    //        {
    //            new[] { null, KeywordToken.Public, KeywordToken.Private, KeywordToken.Internal, KeywordToken.Protected },
    //            typeof(TypeToken),
    //            typeof(IdentifierToken),
    //            typeof(ParenthesisToken),
    //            typeof(BraceToken)
    //        }
    //    };

    //    public static SyntaxPattern methodPattern = new SyntaxPattern
    //    {
    //        Pattern = new object[]
    //        {
    //            new[] { null, KeywordToken.Public, KeywordToken.Private, KeywordToken.Internal, KeywordToken.Protected },
    //            typeof(TypeToken),
    //            typeof(IdentifierToken),
    //            typeof(ParenthesisToken),
    //            typeof(BraceToken)
    //        }
    //    };
    //}

    public class SyntaxParser
    {
        //public class TokenItem
        //{
        //    private TokenContainer parent;
        //    public object token;

        //    public TokenContainer Parent
        //    {
        //        get => parent;
        //        set
        //        {
        //            if (parent != value)
        //            {
        //                if (parent != null)
        //                {
        //                    parent.children.Remove(this);
        //                }

        //                parent = value;

        //                if (parent != null && !parent.children.Contains(this))
        //                {
        //                    parent.children.Add(this);
        //                }
        //            }
        //        }
        //    }

        //    public TokenItem(TokenContainer parent, object token)
        //    {
        //        this.Parent = parent;
        //        this.token = token;
        //    }

        //    public virtual TokenItem? Find(object token)
        //    {
        //        if (this == token || this.token == token)
        //        {
        //            return this;
        //        }
        //        return null;
        //    }
        //}

        //public class TokenContainer : Token
        //{
        //    internal List<TokenItem> children = new();
        //    public Token token;

        //    public TokenContainer() : base(null!, null!)
        //    {
        //    }

        //    public TokenContainer(TokenContainer parent, object token) : base(parent, token)
        //    {
        //    }

        //    public override TokenItem? Find(object token)
        //    {
        //        var result = base.Find(token);
        //        if (result == null)
        //        {
        //            foreach (var child in children)
        //            {
        //                result = child.Find(token);
        //                if (result != null) break;
        //            }
        //        }
        //        return result;
        //    }

        //    public IEnumerable<TokenItem> GetEnumerable()
        //    {
        //        yield return this;
        //        foreach (var child in children)
        //        {
        //            yield return child;
        //            if (child is IEnumerable<TokenItem> e)
        //            {
        //                foreach (var c in e)
        //                {
        //                    yield return c;
        //                }
        //            }
        //        }
        //    }

        //    public void Add(TokenItem item)
        //    {
        //        item.Parent = this;
        //    }
        //}

        public Lexer lexer;
        public Stack<Token> reuse_token = new();
        public Operator root;
        public Operator current;

        public SyntaxParser(Lexer lexer)
        {
            this.lexer = lexer;
            current = root = new NamespaceOperator();
        }

        //public bool TryReadToken(out Token token)
        //{
        //    if (reuse_token.Count > 0)
        //    {
        //        token = reuse_token.Pop();
        //    }
        //    else
        //    {
        //        token = lexer.NextToken();
        //    }

        //    return token != Token.EndOfToken;
        //}

        public void Compile(Lexer lexer)
        {
            BraceToken brace = CompileBrace(lexer);

            CompileReader(new TokenReader(brace.tokens));
        }

        public void CompileReader(TokenReader reader)
        {
            while (reader.TryReadNext(out var token))
            {
                if (token is WhiteSpaceToken) continue;

                if (token == KeywordToken.Using)
                {
                    ReadUsing(reader);
                }
                else if (token == KeywordToken.Namespace)
                {
                    ReadNamespace(reader);
                }
            }
        }

        private void ReadNamespace(TokenReader reader)
        {
            // 상위가 namespace가 아니면 제외된다.
            if (current is not NamespaceOperator curNS)
            {
                throw CompileException.CS8955();
            }

            // 현재 namespace가 filenamespace라면 제외된다.
            if (curNS.isFileNamespace)
            {
                throw CompileException.CS8955();
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
                    if (tokens.Count == 0) throw CompileException.CS1001();

                    // 연속된 dot 오류
                    if (tokens.Last() == SpecialToken.Dot) throw CompileException.CS1001();

                    tokens.Add(token);
                }
                // 식별자
                else if (token is IdentifierToken)
                {
                    // 연속된 식별자를 입력할 수 없다.
                    if (tokens.LastOrDefault() == SpecialToken.Dot) throw CompileException.CS0210();

                    tokens.Add(token);
                }
                // 사용할 수 없는 별칭
                else
                {
                    throw CompileException.CS0000();
                }
            }
            #endregion
            string name = ToString(tokens);

            // 파일 네임스페이스
            if (lineLast == PunctuatorToken.Semicolon)
            {
                // 파일네임스페이스인데 이전에 선언된 네임스페이스가 있다면?
                if (curNS.name != null || curNS.fileParent != null)
                {
                    throw CompileException.CS8955();
                }

                curNS.name = name;
                curNS.isFileNamespace = true;
                curNS.tokens.AddRange(reader.ReadNextAll());
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
                    var newNS = new NamespaceOperator();
                    newNS.name = curNS.name + "." + name;
                    newNS.usings.AddRange(curNS.usings);
                    newNS.fileParent = curNS;
                    curNS = newNS;
                }

                curNS.tokens.AddRange(brace.tokens);
            }
            else
            {
                throw CompileException.CS0000();
            }
        }

        private void ReadUsing(TokenReader reader)
        {
            var line = reader.ReadToNextSemicolon();

            var nameSpace = current as NamespaceOperator;

            // using이 가능한것은 namespace scope일때만 가능하다.
            if (nameSpace == null) throw CompileException.CS0687();

            UsingOperator op = new UsingOperator();
            List<Token> tokens = new List<Token>();

            foreach (var token in line)
            {
                // using static
                if (token == KeywordToken.Static)
                {
                    if (tokens.Count != 0) throw CompileException.CS0116();

                    if (op.IsStatis) throw CompileException.CS1041(token.Text);

                    op.IsStatis = true;
                }
                // using {id} = 
                else if (token == PunctuatorToken.Assignment)
                {
                    if (tokens.Count != 1// '='는 앞에 한개만 있어야한다.
                        || tokens[0] is not IdentifierToken // 앞은 무조건 식별자여야한다.
                        || op.IsStatis) // static이면 안된다.
                    {
                        throw CompileException.CS1525(token.Text);
                    }

                    op.alias = tokens[0].Text;
                    tokens.Clear();
                }
                // using (static) ({alias} =) NameSpace.
                else if (token == SpecialToken.Dot)
                {
                    // dot으로 시작할 수 없다.
                    if (tokens.Count == 0) throw CompileException.CS1001();

                    // 연속된 dot 오류
                    if (tokens.Last() == SpecialToken.Dot) throw CompileException.CS1001();

                    tokens.Add(token);
                }
                // 식별자
                else if (token is IdentifierToken)
                {
                    // 연속된 식별자를 입력할 수 없다.
                    if (tokens.LastOrDefault() == SpecialToken.Dot) throw CompileException.CS0210();

                    tokens.Add(token);
                }
                // 오류
                else
                {
                    if (token == PunctuatorToken.LessThan)
                    {
                        throw CompileException.CS0307();
                    }

                    throw CompileException.CS1525(token.Text);
                }
            }

            op.value = ToString(tokens);

            nameSpace.usings.Add(op);
        }

        private BraceToken CompileBrace(Lexer lexer)
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
                    current = CloseBrace(current, token, () => CompileException.CS1513());
                }
                //---------------------------------------------------------------------------------
                else if (token == PunctuatorToken.LeftBracket)
                {
                    current = OpenBrace(current, token, PunctuatorToken.RightBracket);
                }
                else if (token == PunctuatorToken.RightBracket)
                {
                    current = CloseBrace(current, token, () => CompileException.CS1026());
                }
                //---------------------------------------------------------------------------------
                else if (token == PunctuatorToken.LeftParenthesis)
                {
                    current = OpenBrace(current, token, PunctuatorToken.LeftParenthesis);
                }
                else if (token == PunctuatorToken.RightParenthesis)
                {
                    current = CloseBrace(current, token, () => CompileException.CS1586());
                }
                //---------------------------------------------------------------------------------
                else
                {
                    current.tokens.Add(token);
                }
            }

            if (!root.isClosed)
            {
                throw CompileException.CS1513();
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

        //public void Compile()
        //{
        //    var items = root.GetEnumerable().Where(v => v.token == KeywordToken.Class
        //                                                || v.token == KeywordToken.Struct
        //                                                || v.token == KeywordToken.Record
        //                                                || v.token == KeywordToken.Interface).ToArray();
        //    foreach (var item in items)
        //    {
        //        if (item.Parent == null) continue;

        //        if (item.token == KeywordToken.Class)
        //        {

        //        }
        //    }
        //}
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
                    throw CompileException.CS0000();
                }
            }
            return sb.ToString();
        }

    }
}