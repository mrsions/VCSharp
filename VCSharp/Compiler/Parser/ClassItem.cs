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
    public class Scope
    {
        public Scope? parent;
    }

    public class NamespaceScope : Scope
    {
        public string name;
        public List<UsingOperator> usings = new();
        public bool isFileNamespace;
        public bool CanUsing;

        public List<Token> tokens = new();

        public NamespaceScope CreateSubNamespace(string name)
        {
            var newNS = new NamespaceScope();
            newNS.name = name;
            newNS.parent = this;
            return newNS;
        }
    }

    public class UsingOperator : Scope
    {
        public string alias;
        public string value;

        public Dictionary<string, Type> types;

        public bool IsStatis { get; internal set; }
    }

    public class NameScope : Scope
    {
        public List<Token> Names = new List<Token>();
        public List<NameScope>? Children;

        public NameScope NewChild()
        {
            var scope = new NameScope()
            {
                parent = this
            };

            Children!.Add(scope);

            return scope;
        }
    }

    public class TypeScope : Scope
    {
        public NameScope name;
    }

    public class ClassScope : TypeScope
    {
    }
}
