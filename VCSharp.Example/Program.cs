using System.Numerics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using VCSharp.Compiler.Lexer;
using VCSharp.Compiler.Parser;
using VCSharp.Compiler.Tokens;

namespace VCSharp.Example
{
    public unsafe class Program
    {
        static void Main(string[] args)
        {
            string s = File.ReadAllText("C:\\Users\\mrsio\\source\\repos\\VCSharp\\VCSharp\\Compiler\\Lexer\\PunctuatorToken.cs");
            StringReader sReader = new StringReader(s);
            var lexer = new Lexer(sReader);
            var parser = new SyntaxParser();

            int i = 0;
            Token? token;
            while ((token = lexer.NextToken()) != Token.EndOfToken)
            {
                if (token is WhiteSpaceToken) continue;

                if (token is ErrorToken)
                {
                    Console.WriteLine($"{++i} | {token}");
                    Console.WriteLine($"Continue to press return(enter)...");
                    Console.ReadLine();
                }
                Console.WriteLine($"{++i} | {token.ToViewString()}");
                parser.AddToken(token);
            }


        }
    }
}