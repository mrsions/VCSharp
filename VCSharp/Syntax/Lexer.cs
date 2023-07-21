using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace VCSharp.Syntax
{
    public class CompilerException : Exception
    {
        public CompilerException(string errorCode, string message) : base(errorCode + ": " + message) { }
    }

    public enum LexerTokenType
    {
        SPACE,
        NUMBER,
        STRING,

    }

    public class Lexer
    {
        internal class FsmContext
        {
            public bool Return;
            public int NextState;
            public Lexer L;
            public int StateStack;
        }

        private delegate bool StateHandler(FsmContext ctx);
        private static readonly int[] fsm_return_table;
        private static readonly StateHandler[] fsm_handler_table;

        private TextReader reader;
        private StringBuilder build_buffer;
        private Stack<char> reuse_buffer;
        private char input_char;
        private bool end_of_input;
        private FsmContext fsm_context;
        private int state = 1;
        private bool returnToken;

        private LexerTokenType TokenType;

        public Lexer(TextReader reader)
        {
            this.reader = reader;
            this.build_buffer = new StringBuilder(1024);
        }

        public int NextChar()
        {
            if (reuse_buffer.Count > 0)
            {
                return reuse_buffer.Pop();
            }

            return reader.Read();
        }

        private bool GetChar()
        {
            if ((input_char = (char)NextChar()) != -1)
            {
                return true;
            }

            end_of_input = true;
            return false;
        }

        public bool NextToken()
        {
            StateHandler handler;
            fsm_context.Return = false;

            while (true)
            {
                handler = fsm_handler_table[state - 1];

                if (!handler(fsm_context))
                    throw new Exception();

                if (end_of_input)
                    return false;

                if (fsm_context.Return)
                {

                }

                state = fsm_context.NextState;
            }
        }

        public IEnumerator<bool> GetEnumerator()
        {
            while (GetChar())
            {
                if (IsSpace())
                {
                    build_buffer.Append(input_char);
                    ReadSpace();
                }
                else if (input_char >= '1' && input_char <= '9')
                {
                    TokenType = LexerTokenType.NUMBER;
                    build_buffer.Append((char)input_char);
                    ReadNumber();
                }

                MakeToken();

                yield return true;
            }
        }

        private void MakeToken()
        {
            throw new NotImplementedException();
        }

        private void ReadSpace()
        {
            while (GetChar())
            {
                if (IsSpace())
                {
                    build_buffer.Append(input_char);
                }
                else
                {
                    reuse_buffer.Append(input_char);
                    break;
                }
            }
        }

        private bool IsSpace()
        {
            return input_char == ' ' || (input_char >= '\t' && input_char <= '\r');
        }

        private void ReadNumber()
        {
            bool hasLong = false;
            bool hasUnsigned = false;

            while (GetChar())
            {
                if (input_char >= '1' && input_char <= '9')
                {
                    build_buffer.Append(input_char);
                    continue;
                }

                switch (input_char)
                {
                    default:
                        double d = 1.253e-3;
                        return;

                    case '_': // 단위 표현 리터럴
                        break;

                    case '.': // 소수점 표현 리터럴
                        // 다음 글자가 숫자일때만 인정된다.
                        if (GetChar())
                        {
                            // 다음은 숫자일 경우에만 허용됨
                            if (input_char >= '1' && input_char <= '9')
                            {
                                build_buffer.Append(input_char);
                                break;
                            }
                            // 숫자가 아닌 경우 무시한다.
                            else
                            {
                                reuse_buffer.Push(input_char);
                                reuse_buffer.Push('.');
                                return;
                            }
                        }
                        else
                        {
                            throw new CompilerException("CS1001", "Identifier expected.");
                        }

                    case 'e': // 지수 표현식
                    case 'E':
                        // 다음 글자가 숫자나 +-기호일때만 인정된다.
                        if (GetChar())
                        {
                            if (input_char == '-' || input_char == '+')
                            {
                                build_buffer.Append(input_char);

                                // 다음글자가 없다면 오류가 표기된다.
                                if (!GetChar())
                                {
                                    throw new CompilerException("CS0595", "Invalid real literal.");
                                }
                            }

                            // 다음이 숫자일 경우에만 허용됨 
                            if (input_char >= '1' && input_char <= '9')
                            {
                                build_buffer.Append(input_char);
                                break;
                            }
                            // 숫자가 아닌 경우 무시한다.
                            else
                            {
                                reuse_buffer.Push(input_char);
                                reuse_buffer.Push('.');
                                return;
                            }
                        }
                        else
                        {
                            return;
                        }

                    case 'f': // 부동소수점 표기는 바로 리턴한다.
                    case 'F':
                    case 'd':
                    case 'D':
                    case 'm':
                    case 'M':
                        build_buffer.Append(input_char);
                        return;

                    case 'u': // unsigned는 두번 나올 수 없다.
                    case 'U':
                        if (!hasUnsigned)
                        {
                            reuse_buffer.Push(input_char);
                            return;
                        }

                        hasUnsigned = true;
                        build_buffer.Append(input_char);
                        break;

                    case 'l': // long은 두번 나올 수 없다.
                    case 'L':
                        if (!hasLong)
                        {
                            reuse_buffer.Push(input_char);
                            return;
                        }

                        hasLong = true;
                        build_buffer.Append(input_char);
                        break;

                    case 'x': // x, b는 0다음에만 올 수 있다.
                    case 'b':
                        if (build_buffer.Length != 1 || build_buffer[0] != '0')
                        {
                            throw new CompilerException("CS0201", "Only assignment, call, increment, decrement, and new object expressions can be used as a statement.");
                        }
                        break;
                }
            }
        }
    }
}
