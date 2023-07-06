using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VCSharp
{
    public class Operator
    {
        internal ILOpCode Opcode;

        public Operator(ILOpCode opcode)
        {
            Opcode = opcode;
        }
    }

    //public class OpSet : Operator
    //{
    //}

    //public class OpLdloc : Operator
    //{
    //}
}
