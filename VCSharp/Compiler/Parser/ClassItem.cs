using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using VCSharp.Compiler.Tokens;
using System.Collections;
using System.Reflection;

namespace VCSharp.Compiler.Parser
{
    public class Operator
    {

    }

    public class NamespaceOperator : Operator
    {
        public NamespaceOperator fileParent;

        public string? name;
        public List<UsingOperator> usings = new();
        public bool isFileNamespace;

        public List<Token> tokens = new();
    }

    public class UsingOperator : Operator
    {
        public string alias;
        public string value;

        public Dictionary<string, Type> types;

        public bool IsStatis { get; internal set; }
    }
}
