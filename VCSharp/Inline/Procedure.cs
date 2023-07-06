using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace VCSharp
{
    public unsafe class Procedure
    {
        public class Variable
        {
            public VType Type;
            public int Size;
            public StackValueType ValueType;
            public string Name;
            public bool IsAdd => Size > 8;
        }
        public List<Variable> Variables = new List<Variable>();
        public int VariableTotalSize;
        public int VariableObjectCount;

        public int MaxStack;

        public List<Operator> OperatorList = new List<Operator>();

        public void AddOp(Operator op)
        {
            OperatorList.Add(op);
        }

        public void AddVar(Variable var)
        {
            Variables.Add(var);
            switch (var.Type.VirtualType)
            {
                case EVTypeType.Object:
                    {
                        var.Size = 8;
                        VariableTotalSize += var.Size;
                        VariableObjectCount++;
                    }
                    break;
                default:
                    {
                        var.Size = var.Type.Size;
                        if (var.IsAdd)
                        {
                            VariableTotalSize += var.Size;
                        }
                    }
                    break;
            }
        }

        internal unsafe void Call(StackMemory stackMem, byte* beforeStacks, VObject? caller)
        {
            // 스택 가져오기
            if (stackMem == null)
            {
                stackMem = StackMemory.Current;
                beforeStacks = stackMem.MemoryCurrent;
            }

            #region 스택 초기화
            StackValue* stack = stackalloc StackValue[MaxStack];
            StackValue* v1;
            StackValue* v2;
            int cursor = 0;
            #endregion

            #region 지역변수 초기화
            int localVariableLength = Variables.Count;
            StackValue* local = stackalloc StackValue[localVariableLength];
            byte* localMemory = stackalloc byte[VariableTotalSize];

            if (localVariableLength > 0)
            {
                byte* lastLocalMemory = localMemory;
                for (int i = 0; i < localVariableLength; i++)
                {
                    var v = Variables[i];
                    local[i].type = v.ValueType;
                    if (Variables[i].IsAdd)
                    {
                        local[i].ptr = lastLocalMemory;
                        lastLocalMemory += v.Size;
                    }
                }
            }
            #endregion

            byte** seek = null;
            byte** ops = seek;

        NEXT:
            byte* op = *(ops++);
            byte* opv = (byte*)op + 2;
            switch (*(ILOpCode*)op)
            {
                case ILOpCode.Nop:
                    {
                    }
                    goto NEXT;
                case ILOpCode.Break:
                    {
                    }
                    goto NEXT;
                case ILOpCode.Ldarg_0:
                    {
                    }
                    goto NEXT;
                case ILOpCode.Ldarg_1:
                    {
                    }
                    goto NEXT;
                case ILOpCode.Ldarg_2:
                    {
                    }
                    goto NEXT;
                case ILOpCode.Ldarg_3:
                    {
                    }
                    goto NEXT;

                #region Local Variable
                #region Load
                case ILOpCode.Ldloc:
                case ILOpCode.Ldloca:
                    {
                        *(stack++) = local[*(ushort*)opv];
                    }
                    goto NEXT;
                case ILOpCode.Ldloc_s:
                case ILOpCode.Ldloca_s:
                    {
                        *(stack++) = local[*opv];
                    }
                    goto NEXT;

                case ILOpCode.Ldloc_0:
                    {
                        *(stack++) = local[0];
                    }
                    goto NEXT;
                case ILOpCode.Ldloc_1:
                    {
                        *(stack++) = local[1];
                    }
                    goto NEXT;
                case ILOpCode.Ldloc_2:
                    {
                        *(stack++) = local[2];
                    }
                    goto NEXT;
                case ILOpCode.Ldloc_3:
                    {
                        *(stack++) = local[3];
                    }
                    goto NEXT;
                #endregion Load

                #region Store
                case ILOpCode.Stloc:
                    {
                        local[*(ushort*)opv] = *(--stack);
                    }
                    goto NEXT;
                case ILOpCode.Stloc_s:
                    {
                        local[*opv] = *(--stack);
                    }
                    goto NEXT;
                case ILOpCode.Stloc_0:
                    {
                        local[0] = *(--stack);
                    }
                    goto NEXT;
                case ILOpCode.Stloc_1:
                    {
                        local[1] = *(--stack);
                    }
                    goto NEXT;
                case ILOpCode.Stloc_2:
                    {
                        local[2] = *(--stack);
                    }
                    goto NEXT;
                case ILOpCode.Stloc_3:
                    {
                        local[3] = *(--stack);
                    }
                    goto NEXT;
                #endregion Store
                #endregion Local_Variable

                case ILOpCode.Ldarg_s:
                    {
                    }
                    goto NEXT;
                case ILOpCode.Ldarga_s:
                    {
                    }
                    goto NEXT;
                case ILOpCode.Starg_s:
                    {
                    }
                    goto NEXT;
                case ILOpCode.Ldnull:
                    {
                    }
                    goto NEXT;

                #region Create Value
                case ILOpCode.Ldc_i4_m1:
                    {
                        *(stack++) = StackValue.I4_m1;
                    }
                    goto NEXT;
                case ILOpCode.Ldc_i4_0:
                    {
                        *(stack++) = StackValue.I4_0;
                    }
                    goto NEXT;
                case ILOpCode.Ldc_i4_1:
                    {
                        *(stack++) = StackValue.I4_1;
                    }
                    goto NEXT;
                case ILOpCode.Ldc_i4_2:
                    {
                        *(stack++) = StackValue.I4_2;
                    }
                    goto NEXT;
                case ILOpCode.Ldc_i4_3:
                    {
                        *(stack++) = StackValue.I4_3;
                    }
                    goto NEXT;
                case ILOpCode.Ldc_i4_4:
                    {
                        *(stack++) = StackValue.I4_4;
                    }
                    goto NEXT;
                case ILOpCode.Ldc_i4_5:
                    {
                        *(stack++) = StackValue.I4_5;
                    }
                    goto NEXT;
                case ILOpCode.Ldc_i4_6:
                    {
                        *(stack++) = StackValue.I4_6;
                    }
                    goto NEXT;
                case ILOpCode.Ldc_i4_7:
                    {
                        *(stack++) = StackValue.I4_7;
                    }
                    goto NEXT;
                case ILOpCode.Ldc_i4_8:
                    {
                        *(stack++) = StackValue.I4_8;
                    }
                    goto NEXT;
                case ILOpCode.Ldc_i4_s:
                    {
                        *(stack++) = new StackValue { type = StackValueType.Int4, value = *opv };
                    }
                    goto NEXT;
                case ILOpCode.Ldc_i4:
                    {
                        *(stack++) = new StackValue { type = StackValueType.Int4, value = *(int*)opv };
                    }
                    goto NEXT;
                case ILOpCode.Ldc_i8:
                    {
                        *(stack++) = new StackValue { type = StackValueType.Int8, value = *(long*)opv };
                    }
                    goto NEXT;
                case ILOpCode.Ldc_r4:
                    {
                        *(stack++) = new StackValue { type = StackValueType.Real4, r4 = *(float*)opv };
                    }
                    goto NEXT;
                case ILOpCode.Ldc_r8:
                    {
                        *(stack++) = new StackValue { type = StackValueType.Real8, r8 = *(double*)opv };
                    }
                    goto NEXT;
                #endregion

                case ILOpCode.Dup:
                    {
                        *(stack++) = *(stack - 1);
                    }
                    goto NEXT;
                case ILOpCode.Pop:
                    {
                        --stack;
                    }
                    goto NEXT;

                case ILOpCode.Jmp:
                    {
                    }
                    goto NEXT;

                case ILOpCode.Call:
                    {
                        var m = methods[*opv];
                        var parameters = m.GetParameters();
                        object[] param = null;
                        if (parameters != null)
                        {
                            param = new object[parameters.Length];
                            for (i = param.Length - 1; i >= 0; i--)
                            {
                                var p = parameters[i];
                                if (p.ParameterType == typeof(int))
                                {
                                    param[i] = *(int*)(--stack);
                                }
                            }
                        }
                        m.Invoke(null, param);
                    }
                    goto NEXT;
                case ILOpCode.Calli:
                    {
                    }
                    goto NEXT;
                case ILOpCode.Ret:
                    {
                        return;
                    }

                #region condition jump

                case ILOpCode.Br:
                    {
                        ops = ((seek + *(int*)opv));
                    }
                    goto NEXT;
                case ILOpCode.Br_s:
                    {
                        ops = ((seek + *opv));
                    }
                    goto NEXT;

                #region Br?(true)
                case ILOpCode.Brfalse:
                    {
                        if (!(--stack)->b)
                        {
                            ops = (seek + *(int*)opv);
                        }
                    }
                    goto NEXT;
                case ILOpCode.Brfalse_s:
                    {
                        if (!(--stack)->b)
                        {
                            ops = (seek + *opv);
                        }
                    }
                    goto NEXT;
                case ILOpCode.Brtrue:
                    {
                        if (!(--stack)->b)
                        {
                            ops = (seek + *(int*)opv);
                        }
                    }
                    goto NEXT;
                case ILOpCode.Brtrue_s:
                    {
                        if ((--stack)->b)
                        {
                            ops = (seek + *opv);
                        }
                    }
                    goto NEXT;
                case ILOpCode.Beq:
                    {
                        if ((--stack)->value == (--stack)->value)
                        {
                            ops = (seek + *(int*)opv);
                        }
                    }
                    goto NEXT;
                case ILOpCode.Beq_s:
                    {
                        if ((--stack)->value == (--stack)->value)
                        {
                            ops = (seek + *opv);
                        }
                    }
                    goto NEXT;
                case ILOpCode.Bne_un:
                    {
                        if ((--stack)->value != (--stack)->value)
                        {
                            ops = (seek + *(int*)opv);
                        }
                    }
                    goto NEXT;
                case ILOpCode.Bne_un_s:
                    {
                        if ((--stack)->value == (--stack)->value)
                        {
                            ops = (seek + *opv);
                        }
                    }
                    goto NEXT;
                #endregion Br
                #region Bge(a <= b)
                case ILOpCode.Bge:
                case ILOpCode.Bge_un:
                    {
                        v1 = --stack;
                        v2 = --stack;
                        switch ((StackValueTypeCompare)(v1->typeNum * 6 + v2->typeNum))
                        {
                            case StackValueTypeCompare.Int4_Int4: if (v1->i4 >= v2->i4) { ops = (seek + *(int*)opv); } break;
                            case StackValueTypeCompare.Int4_Int8: if (v1->i4 >= v2->i8) { ops = (seek + *(int*)opv); } break;
                            case StackValueTypeCompare.Int4_Unt4: if (v1->i4 >= v2->u4) { ops = (seek + *(int*)opv); } break;
                            case StackValueTypeCompare.Int4_Unt8: if (v1->i4 >= 0 && v1->u4 >= v2->u8) { ops = (seek + *(int*)opv); } break;
                            case StackValueTypeCompare.Int4_Real4: if (v1->i4 >= v2->r4) { ops = (seek + *(int*)opv); } break;
                            case StackValueTypeCompare.Int4_Real8: if (v1->i4 >= v2->r8) { ops = (seek + *(int*)opv); } break;
                            case StackValueTypeCompare.Int8_Int4: if (v1->i8 >= v2->i4) { ops = (seek + *(int*)opv); } break;
                            case StackValueTypeCompare.Int8_Int8: if (v1->i8 >= v2->i8) { ops = (seek + *(int*)opv); } break;
                            case StackValueTypeCompare.Int8_Unt4: if (v1->i8 >= v2->u4) { ops = (seek + *(int*)opv); } break;
                            case StackValueTypeCompare.Int8_Unt8: if (v1->i8 >= 0 && v1->u8 >= v2->u8) { ops = (seek + *(int*)opv); } break;
                            case StackValueTypeCompare.Int8_Real4: if (v1->i8 >= v2->r4) { ops = (seek + *(int*)opv); } break;
                            case StackValueTypeCompare.Int8_Real8: if (v1->i8 >= v2->r8) { ops = (seek + *(int*)opv); } break;
                            case StackValueTypeCompare.Unt4_Int4: if (v1->u4 >= v2->i4) { ops = (seek + *(int*)opv); } break;
                            case StackValueTypeCompare.Unt4_Int8: if (v1->u4 >= v2->i8) { ops = (seek + *(int*)opv); } break;
                            case StackValueTypeCompare.Unt4_Unt4: if (v1->u4 >= v2->u4) { ops = (seek + *(int*)opv); } break;
                            case StackValueTypeCompare.Unt4_Unt8: if (v1->u4 >= v2->u8) { ops = (seek + *(int*)opv); } break;
                            case StackValueTypeCompare.Unt4_Real4: if (v1->u4 >= v2->r4) { ops = (seek + *(int*)opv); } break;
                            case StackValueTypeCompare.Unt4_Real8: if (v1->u4 >= v2->r8) { ops = (seek + *(int*)opv); } break;
                            case StackValueTypeCompare.Unt8_Int4: if (v2->i4 >= 0 && v1->u8 >= v2->u4) { ops = (seek + *(int*)opv); } break;
                            case StackValueTypeCompare.Unt8_Int8: if (v2->i8 >= 0 && v1->u8 >= v2->u8) { ops = (seek + *(int*)opv); } break;
                            case StackValueTypeCompare.Unt8_Unt4: if (v1->u8 >= v2->u4) { ops = (seek + *(int*)opv); } break;
                            case StackValueTypeCompare.Unt8_Unt8: if (v1->u8 >= v2->u8) { ops = (seek + *(int*)opv); } break;
                            case StackValueTypeCompare.Unt8_Real4: if (v1->u8 >= v2->r4) { ops = (seek + *(int*)opv); } break;
                            case StackValueTypeCompare.Unt8_Real8: if (v1->u8 >= v2->r8) { ops = (seek + *(int*)opv); } break;
                            case StackValueTypeCompare.Real4_Int4: if (v1->r4 >= v2->i4) { ops = (seek + *(int*)opv); } break;
                            case StackValueTypeCompare.Real4_Int8: if (v1->r4 >= v2->i8) { ops = (seek + *(int*)opv); } break;
                            case StackValueTypeCompare.Real4_Unt4: if (v1->r4 >= v2->u4) { ops = (seek + *(int*)opv); } break;
                            case StackValueTypeCompare.Real4_Unt8: if (v1->r4 >= v2->u8) { ops = (seek + *(int*)opv); } break;
                            case StackValueTypeCompare.Real4_Real4: if (v1->r4 >= v2->r4) { ops = (seek + *(int*)opv); } break;
                            case StackValueTypeCompare.Real4_Real8: if (v1->r4 >= v2->r8) { ops = (seek + *(int*)opv); } break;
                            case StackValueTypeCompare.Real8_Int4: if (v1->r8 >= v2->i4) { ops = (seek + *(int*)opv); } break;
                            case StackValueTypeCompare.Real8_Int8: if (v1->r8 >= v2->i8) { ops = (seek + *(int*)opv); } break;
                            case StackValueTypeCompare.Real8_Unt4: if (v1->r8 >= v2->u4) { ops = (seek + *(int*)opv); } break;
                            case StackValueTypeCompare.Real8_Unt8: if (v1->r8 >= v2->u8) { ops = (seek + *(int*)opv); } break;
                            case StackValueTypeCompare.Real8_Real4: if (v1->r8 >= v2->r4) { ops = (seek + *(int*)opv); } break;
                            case StackValueTypeCompare.Real8_Real8: if (v1->r8 >= v2->r8) { ops = (seek + *(int*)opv); } break;
                        }
                    }
                    goto NEXT;
                case ILOpCode.Bge_s:
                case ILOpCode.Bge_un_s:
                    {
                        v1 = --stack;
                        v2 = --stack;
                        switch ((StackValueTypeCompare)(v1->typeNum * 6 + v2->typeNum))
                        {
                            case StackValueTypeCompare.Int4_Int4: if (v1->i4 >= v2->i4) { ops = (seek + *opv); } break;
                            case StackValueTypeCompare.Int4_Int8: if (v1->i4 >= v2->i8) { ops = (seek + *opv); } break;
                            case StackValueTypeCompare.Int4_Unt4: if (v1->i4 >= v2->u4) { ops = (seek + *opv); } break;
                            case StackValueTypeCompare.Int4_Unt8: if (v1->i4 >= 0 && v1->u4 >= v2->u8) { ops = (seek + *opv); } break;
                            case StackValueTypeCompare.Int4_Real4: if (v1->i4 >= v2->r4) { ops = (seek + *opv); } break;
                            case StackValueTypeCompare.Int4_Real8: if (v1->i4 >= v2->r8) { ops = (seek + *opv); } break;
                            case StackValueTypeCompare.Int8_Int4: if (v1->i8 >= v2->i4) { ops = (seek + *opv); } break;
                            case StackValueTypeCompare.Int8_Int8: if (v1->i8 >= v2->i8) { ops = (seek + *opv); } break;
                            case StackValueTypeCompare.Int8_Unt4: if (v1->i8 >= v2->u4) { ops = (seek + *opv); } break;
                            case StackValueTypeCompare.Int8_Unt8: if (v1->i8 >= 0 && v1->u8 >= v2->u8) { ops = (seek + *opv); } break;
                            case StackValueTypeCompare.Int8_Real4: if (v1->i8 >= v2->r4) { ops = (seek + *opv); } break;
                            case StackValueTypeCompare.Int8_Real8: if (v1->i8 >= v2->r8) { ops = (seek + *opv); } break;
                            case StackValueTypeCompare.Unt4_Int4: if (v1->u4 >= v2->i4) { ops = (seek + *opv); } break;
                            case StackValueTypeCompare.Unt4_Int8: if (v1->u4 >= v2->i8) { ops = (seek + *opv); } break;
                            case StackValueTypeCompare.Unt4_Unt4: if (v1->u4 >= v2->u4) { ops = (seek + *opv); } break;
                            case StackValueTypeCompare.Unt4_Unt8: if (v1->u4 >= v2->u8) { ops = (seek + *opv); } break;
                            case StackValueTypeCompare.Unt4_Real4: if (v1->u4 >= v2->r4) { ops = (seek + *opv); } break;
                            case StackValueTypeCompare.Unt4_Real8: if (v1->u4 >= v2->r8) { ops = (seek + *opv); } break;
                            case StackValueTypeCompare.Unt8_Int4: if (v2->i4 < 0 || v1->u8 >= v2->u4) { ops = (seek + *opv); } break;
                            case StackValueTypeCompare.Unt8_Int8: if (v2->i8 < 0 || v1->u8 >= v2->u8) { ops = (seek + *opv); } break;
                            case StackValueTypeCompare.Unt8_Unt4: if (v1->u8 >= v2->u4) { ops = (seek + *opv); } break;
                            case StackValueTypeCompare.Unt8_Unt8: if (v1->u8 >= v2->u8) { ops = (seek + *opv); } break;
                            case StackValueTypeCompare.Unt8_Real4: if (v1->u8 >= v2->r4) { ops = (seek + *opv); } break;
                            case StackValueTypeCompare.Unt8_Real8: if (v1->u8 >= v2->r8) { ops = (seek + *opv); } break;
                            case StackValueTypeCompare.Real4_Int4: if (v1->r4 >= v2->i4) { ops = (seek + *opv); } break;
                            case StackValueTypeCompare.Real4_Int8: if (v1->r4 >= v2->i8) { ops = (seek + *opv); } break;
                            case StackValueTypeCompare.Real4_Unt4: if (v1->r4 >= v2->u4) { ops = (seek + *opv); } break;
                            case StackValueTypeCompare.Real4_Unt8: if (v1->r4 >= v2->u8) { ops = (seek + *opv); } break;
                            case StackValueTypeCompare.Real4_Real4: if (v1->r4 >= v2->r4) { ops = (seek + *opv); } break;
                            case StackValueTypeCompare.Real4_Real8: if (v1->r4 >= v2->r8) { ops = (seek + *opv); } break;
                            case StackValueTypeCompare.Real8_Int4: if (v1->r8 >= v2->i4) { ops = (seek + *opv); } break;
                            case StackValueTypeCompare.Real8_Int8: if (v1->r8 >= v2->i8) { ops = (seek + *opv); } break;
                            case StackValueTypeCompare.Real8_Unt4: if (v1->r8 >= v2->u4) { ops = (seek + *opv); } break;
                            case StackValueTypeCompare.Real8_Unt8: if (v1->r8 >= v2->u8) { ops = (seek + *opv); } break;
                            case StackValueTypeCompare.Real8_Real4: if (v1->r8 >= v2->r4) { ops = (seek + *opv); } break;
                            case StackValueTypeCompare.Real8_Real8: if (v1->r8 >= v2->r8) { ops = (seek + *opv); } break;
                        }
                    }
                    goto NEXT;
                #endregion Bge
                #region Bgt(a < b)
                case ILOpCode.Bgt:
                case ILOpCode.Bgt_un:
                    {
                        v1 = --stack;
                        v2 = --stack;
                        switch ((StackValueTypeCompare)(v1->typeNum * 6 + v2->typeNum))
                        {
                            case StackValueTypeCompare.Int4_Int4: if (v1->i4 > v2->i4) { ops = (seek + *(int*)opv); } break;
                            case StackValueTypeCompare.Int4_Int8: if (v1->i4 > v2->i8) { ops = (seek + *(int*)opv); } break;
                            case StackValueTypeCompare.Int4_Unt4: if (v1->i4 > v2->u4) { ops = (seek + *(int*)opv); } break;
                            case StackValueTypeCompare.Int4_Unt8: if (v1->i4 > 0 && v1->u4 > v2->u8) { ops = (seek + *(int*)opv); } break;
                            case StackValueTypeCompare.Int4_Real4: if (v1->i4 > v2->r4) { ops = (seek + *(int*)opv); } break;
                            case StackValueTypeCompare.Int4_Real8: if (v1->i4 > v2->r8) { ops = (seek + *(int*)opv); } break;
                            case StackValueTypeCompare.Int8_Int4: if (v1->i8 > v2->i4) { ops = (seek + *(int*)opv); } break;
                            case StackValueTypeCompare.Int8_Int8: if (v1->i8 > v2->i8) { ops = (seek + *(int*)opv); } break;
                            case StackValueTypeCompare.Int8_Unt4: if (v1->i8 > v2->u4) { ops = (seek + *(int*)opv); } break;
                            case StackValueTypeCompare.Int8_Unt8: if (v1->i8 > 0 && v1->u8 > v2->u8) { ops = (seek + *(int*)opv); } break;
                            case StackValueTypeCompare.Int8_Real4: if (v1->i8 > v2->r4) { ops = (seek + *(int*)opv); } break;
                            case StackValueTypeCompare.Int8_Real8: if (v1->i8 > v2->r8) { ops = (seek + *(int*)opv); } break;
                            case StackValueTypeCompare.Unt4_Int4: if (v1->u4 > v2->i4) { ops = (seek + *(int*)opv); } break;
                            case StackValueTypeCompare.Unt4_Int8: if (v1->u4 > v2->i8) { ops = (seek + *(int*)opv); } break;
                            case StackValueTypeCompare.Unt4_Unt4: if (v1->u4 > v2->u4) { ops = (seek + *(int*)opv); } break;
                            case StackValueTypeCompare.Unt4_Unt8: if (v1->u4 > v2->u8) { ops = (seek + *(int*)opv); } break;
                            case StackValueTypeCompare.Unt4_Real4: if (v1->u4 > v2->r4) { ops = (seek + *(int*)opv); } break;
                            case StackValueTypeCompare.Unt4_Real8: if (v1->u4 > v2->r8) { ops = (seek + *(int*)opv); } break;
                            case StackValueTypeCompare.Unt8_Int4: if (v2->i4 < 0 || v1->u8 > v2->u4) { ops = (seek + *(int*)opv); } break;
                            case StackValueTypeCompare.Unt8_Int8: if (v2->i8 < 0 || v1->u8 > v2->u8) { ops = (seek + *(int*)opv); } break;
                            case StackValueTypeCompare.Unt8_Unt4: if (v1->u8 > v2->u4) { ops = (seek + *(int*)opv); } break;
                            case StackValueTypeCompare.Unt8_Unt8: if (v1->u8 > v2->u8) { ops = (seek + *(int*)opv); } break;
                            case StackValueTypeCompare.Unt8_Real4: if (v1->u8 > v2->r4) { ops = (seek + *(int*)opv); } break;
                            case StackValueTypeCompare.Unt8_Real8: if (v1->u8 > v2->r8) { ops = (seek + *(int*)opv); } break;
                            case StackValueTypeCompare.Real4_Int4: if (v1->r4 > v2->i4) { ops = (seek + *(int*)opv); } break;
                            case StackValueTypeCompare.Real4_Int8: if (v1->r4 > v2->i8) { ops = (seek + *(int*)opv); } break;
                            case StackValueTypeCompare.Real4_Unt4: if (v1->r4 > v2->u4) { ops = (seek + *(int*)opv); } break;
                            case StackValueTypeCompare.Real4_Unt8: if (v1->r4 > v2->u8) { ops = (seek + *(int*)opv); } break;
                            case StackValueTypeCompare.Real4_Real4: if (v1->r4 > v2->r4) { ops = (seek + *(int*)opv); } break;
                            case StackValueTypeCompare.Real4_Real8: if (v1->r4 > v2->r8) { ops = (seek + *(int*)opv); } break;
                            case StackValueTypeCompare.Real8_Int4: if (v1->r8 > v2->i4) { ops = (seek + *(int*)opv); } break;
                            case StackValueTypeCompare.Real8_Int8: if (v1->r8 > v2->i8) { ops = (seek + *(int*)opv); } break;
                            case StackValueTypeCompare.Real8_Unt4: if (v1->r8 > v2->u4) { ops = (seek + *(int*)opv); } break;
                            case StackValueTypeCompare.Real8_Unt8: if (v1->r8 > v2->u8) { ops = (seek + *(int*)opv); } break;
                            case StackValueTypeCompare.Real8_Real4: if (v1->r8 > v2->r4) { ops = (seek + *(int*)opv); } break;
                            case StackValueTypeCompare.Real8_Real8: if (v1->r8 > v2->r8) { ops = (seek + *(int*)opv); } break;
                        }
                    }
                    goto NEXT;
                case ILOpCode.Bgt_s:
                case ILOpCode.Bgt_un_s:
                    {
                        v1 = --stack;
                        v2 = --stack;
                        switch ((StackValueTypeCompare)(v1->typeNum * 6 + v2->typeNum))
                        {
                            case StackValueTypeCompare.Int4_Int4: if (v1->i4 > v2->i4) { ops = (seek + *opv); } break;
                            case StackValueTypeCompare.Int4_Int8: if (v1->i4 > v2->i8) { ops = (seek + *opv); } break;
                            case StackValueTypeCompare.Int4_Unt4: if (v1->i4 > v2->u4) { ops = (seek + *opv); } break;
                            case StackValueTypeCompare.Int4_Unt8: if (v1->i4 > 0 && v1->u4 > v2->u8) { ops = (seek + *opv); } break;
                            case StackValueTypeCompare.Int4_Real4: if (v1->i4 > v2->r4) { ops = (seek + *opv); } break;
                            case StackValueTypeCompare.Int4_Real8: if (v1->i4 > v2->r8) { ops = (seek + *opv); } break;
                            case StackValueTypeCompare.Int8_Int4: if (v1->i8 > v2->i4) { ops = (seek + *opv); } break;
                            case StackValueTypeCompare.Int8_Int8: if (v1->i8 > v2->i8) { ops = (seek + *opv); } break;
                            case StackValueTypeCompare.Int8_Unt4: if (v1->i8 > v2->u4) { ops = (seek + *opv); } break;
                            case StackValueTypeCompare.Int8_Unt8: if (v1->i8 > 0 && v1->u8 > v2->u8) { ops = (seek + *opv); } break;
                            case StackValueTypeCompare.Int8_Real4: if (v1->i8 > v2->r4) { ops = (seek + *opv); } break;
                            case StackValueTypeCompare.Int8_Real8: if (v1->i8 > v2->r8) { ops = (seek + *opv); } break;
                            case StackValueTypeCompare.Unt4_Int4: if (v1->u4 > v2->i4) { ops = (seek + *opv); } break;
                            case StackValueTypeCompare.Unt4_Int8: if (v1->u4 > v2->i8) { ops = (seek + *opv); } break;
                            case StackValueTypeCompare.Unt4_Unt4: if (v1->u4 > v2->u4) { ops = (seek + *opv); } break;
                            case StackValueTypeCompare.Unt4_Unt8: if (v1->u4 > v2->u8) { ops = (seek + *opv); } break;
                            case StackValueTypeCompare.Unt4_Real4: if (v1->u4 > v2->r4) { ops = (seek + *opv); } break;
                            case StackValueTypeCompare.Unt4_Real8: if (v1->u4 > v2->r8) { ops = (seek + *opv); } break;
                            case StackValueTypeCompare.Unt8_Int4: if (v2->i4 < 0 || v1->u8 > v2->u4) { ops = (seek + *opv); } break;
                            case StackValueTypeCompare.Unt8_Int8: if (v2->i8 < 0 || v1->u8 > v2->u8) { ops = (seek + *opv); } break;
                            case StackValueTypeCompare.Unt8_Unt4: if (v1->u8 > v2->u4) { ops = (seek + *opv); } break;
                            case StackValueTypeCompare.Unt8_Unt8: if (v1->u8 > v2->u8) { ops = (seek + *opv); } break;
                            case StackValueTypeCompare.Unt8_Real4: if (v1->u8 > v2->r4) { ops = (seek + *opv); } break;
                            case StackValueTypeCompare.Unt8_Real8: if (v1->u8 > v2->r8) { ops = (seek + *opv); } break;
                            case StackValueTypeCompare.Real4_Int4: if (v1->r4 > v2->i4) { ops = (seek + *opv); } break;
                            case StackValueTypeCompare.Real4_Int8: if (v1->r4 > v2->i8) { ops = (seek + *opv); } break;
                            case StackValueTypeCompare.Real4_Unt4: if (v1->r4 > v2->u4) { ops = (seek + *opv); } break;
                            case StackValueTypeCompare.Real4_Unt8: if (v1->r4 > v2->u8) { ops = (seek + *opv); } break;
                            case StackValueTypeCompare.Real4_Real4: if (v1->r4 > v2->r4) { ops = (seek + *opv); } break;
                            case StackValueTypeCompare.Real4_Real8: if (v1->r4 > v2->r8) { ops = (seek + *opv); } break;
                            case StackValueTypeCompare.Real8_Int4: if (v1->r8 > v2->i4) { ops = (seek + *opv); } break;
                            case StackValueTypeCompare.Real8_Int8: if (v1->r8 > v2->i8) { ops = (seek + *opv); } break;
                            case StackValueTypeCompare.Real8_Unt4: if (v1->r8 > v2->u4) { ops = (seek + *opv); } break;
                            case StackValueTypeCompare.Real8_Unt8: if (v1->r8 > v2->u8) { ops = (seek + *opv); } break;
                            case StackValueTypeCompare.Real8_Real4: if (v1->r8 > v2->r4) { ops = (seek + *opv); } break;
                            case StackValueTypeCompare.Real8_Real8: if (v1->r8 > v2->r8) { ops = (seek + *opv); } break;
                        }
                    }
                    goto NEXT;
                #endregion Bgt
                #region Ble(a >= b)
                case ILOpCode.Ble:
                case ILOpCode.Ble_s:
                    {
                        v1 = --stack;
                        v2 = --stack;
                        switch ((StackValueTypeCompare)(v1->typeNum * 6 + v2->typeNum))
                        {
                            case StackValueTypeCompare.Int4_Int4: if (v1->i4 <= v2->i4) { ops = (seek + *(int*)opv); } break;
                            case StackValueTypeCompare.Int4_Int8: if (v1->i4 <= v2->i8) { ops = (seek + *(int*)opv); } break;
                            case StackValueTypeCompare.Int4_Unt4: if (v1->i4 <= v2->u4) { ops = (seek + *(int*)opv); } break;
                            case StackValueTypeCompare.Int4_Unt8: if (v1->i4 < 0 || v1->u4 <= v2->u8) { ops = (seek + *(int*)opv); } break;
                            case StackValueTypeCompare.Int4_Real4: if (v1->i4 <= v2->r4) { ops = (seek + *(int*)opv); } break;
                            case StackValueTypeCompare.Int4_Real8: if (v1->i4 <= v2->r8) { ops = (seek + *(int*)opv); } break;
                            case StackValueTypeCompare.Int8_Int4: if (v1->i8 <= v2->i4) { ops = (seek + *(int*)opv); } break;
                            case StackValueTypeCompare.Int8_Int8: if (v1->i8 <= v2->i8) { ops = (seek + *(int*)opv); } break;
                            case StackValueTypeCompare.Int8_Unt4: if (v1->i8 <= v2->u4) { ops = (seek + *(int*)opv); } break;
                            case StackValueTypeCompare.Int8_Unt8: if (v1->i8 < 0 || v1->u8 <= v2->u8) { ops = (seek + *(int*)opv); } break;
                            case StackValueTypeCompare.Int8_Real4: if (v1->i8 <= v2->r4) { ops = (seek + *(int*)opv); } break;
                            case StackValueTypeCompare.Int8_Real8: if (v1->i8 <= v2->r8) { ops = (seek + *(int*)opv); } break;
                            case StackValueTypeCompare.Unt4_Int4: if (v1->u4 <= v2->i4) { ops = (seek + *(int*)opv); } break;
                            case StackValueTypeCompare.Unt4_Int8: if (v1->u4 <= v2->i8) { ops = (seek + *(int*)opv); } break;
                            case StackValueTypeCompare.Unt4_Unt4: if (v1->u4 <= v2->u4) { ops = (seek + *(int*)opv); } break;
                            case StackValueTypeCompare.Unt4_Unt8: if (v1->u4 <= v2->u8) { ops = (seek + *(int*)opv); } break;
                            case StackValueTypeCompare.Unt4_Real4: if (v1->u4 <= v2->r4) { ops = (seek + *(int*)opv); } break;
                            case StackValueTypeCompare.Unt4_Real8: if (v1->u4 <= v2->r8) { ops = (seek + *(int*)opv); } break;
                            case StackValueTypeCompare.Unt8_Int4: if (v2->i4 > 0 && v1->u8 <= v2->u4) { ops = (seek + *(int*)opv); } break;
                            case StackValueTypeCompare.Unt8_Int8: if (v2->i8 > 0 && v1->u8 <= v2->u8) { ops = (seek + *(int*)opv); } break;
                            case StackValueTypeCompare.Unt8_Unt4: if (v1->u8 <= v2->u4) { ops = (seek + *(int*)opv); } break;
                            case StackValueTypeCompare.Unt8_Unt8: if (v1->u8 <= v2->u8) { ops = (seek + *(int*)opv); } break;
                            case StackValueTypeCompare.Unt8_Real4: if (v1->u8 <= v2->r4) { ops = (seek + *(int*)opv); } break;
                            case StackValueTypeCompare.Unt8_Real8: if (v1->u8 <= v2->r8) { ops = (seek + *(int*)opv); } break;
                            case StackValueTypeCompare.Real4_Int4: if (v1->r4 <= v2->i4) { ops = (seek + *(int*)opv); } break;
                            case StackValueTypeCompare.Real4_Int8: if (v1->r4 <= v2->i8) { ops = (seek + *(int*)opv); } break;
                            case StackValueTypeCompare.Real4_Unt4: if (v1->r4 <= v2->u4) { ops = (seek + *(int*)opv); } break;
                            case StackValueTypeCompare.Real4_Unt8: if (v1->r4 <= v2->u8) { ops = (seek + *(int*)opv); } break;
                            case StackValueTypeCompare.Real4_Real4: if (v1->r4 <= v2->r4) { ops = (seek + *(int*)opv); } break;
                            case StackValueTypeCompare.Real4_Real8: if (v1->r4 <= v2->r8) { ops = (seek + *(int*)opv); } break;
                            case StackValueTypeCompare.Real8_Int4: if (v1->r8 <= v2->i4) { ops = (seek + *(int*)opv); } break;
                            case StackValueTypeCompare.Real8_Int8: if (v1->r8 <= v2->i8) { ops = (seek + *(int*)opv); } break;
                            case StackValueTypeCompare.Real8_Unt4: if (v1->r8 <= v2->u4) { ops = (seek + *(int*)opv); } break;
                            case StackValueTypeCompare.Real8_Unt8: if (v1->r8 <= v2->u8) { ops = (seek + *(int*)opv); } break;
                            case StackValueTypeCompare.Real8_Real4: if (v1->r8 <= v2->r4) { ops = (seek + *(int*)opv); } break;
                            case StackValueTypeCompare.Real8_Real8: if (v1->r8 <= v2->r8) { ops = (seek + *(int*)opv); } break;
                        }
                    }
                    goto NEXT;
                case ILOpCode.Ble_un:
                case ILOpCode.Ble_un_s:
                    {
                        v1 = --stack;
                        v2 = --stack;
                        switch ((StackValueTypeCompare)(v1->typeNum * 6 + v2->typeNum))
                        {
                            case StackValueTypeCompare.Int4_Int4: if (v1->i4 <= v2->i4) { ops = (seek + *opv); } break;
                            case StackValueTypeCompare.Int4_Int8: if (v1->i4 <= v2->i8) { ops = (seek + *opv); } break;
                            case StackValueTypeCompare.Int4_Unt4: if (v1->i4 <= v2->u4) { ops = (seek + *opv); } break;
                            case StackValueTypeCompare.Int4_Unt8: if (v1->i4 < 0 || v1->u4 <= v2->u8) { ops = (seek + *opv); } break;
                            case StackValueTypeCompare.Int4_Real4: if (v1->i4 <= v2->r4) { ops = (seek + *opv); } break;
                            case StackValueTypeCompare.Int4_Real8: if (v1->i4 <= v2->r8) { ops = (seek + *opv); } break;
                            case StackValueTypeCompare.Int8_Int4: if (v1->i8 <= v2->i4) { ops = (seek + *opv); } break;
                            case StackValueTypeCompare.Int8_Int8: if (v1->i8 <= v2->i8) { ops = (seek + *opv); } break;
                            case StackValueTypeCompare.Int8_Unt4: if (v1->i8 <= v2->u4) { ops = (seek + *opv); } break;
                            case StackValueTypeCompare.Int8_Unt8: if (v1->i8 < 0 || v1->u8 <= v2->u8) { ops = (seek + *opv); } break;
                            case StackValueTypeCompare.Int8_Real4: if (v1->i8 <= v2->r4) { ops = (seek + *opv); } break;
                            case StackValueTypeCompare.Int8_Real8: if (v1->i8 <= v2->r8) { ops = (seek + *opv); } break;
                            case StackValueTypeCompare.Unt4_Int4: if (v1->u4 <= v2->i4) { ops = (seek + *opv); } break;
                            case StackValueTypeCompare.Unt4_Int8: if (v1->u4 <= v2->i8) { ops = (seek + *opv); } break;
                            case StackValueTypeCompare.Unt4_Unt4: if (v1->u4 <= v2->u4) { ops = (seek + *opv); } break;
                            case StackValueTypeCompare.Unt4_Unt8: if (v1->u4 <= v2->u8) { ops = (seek + *opv); } break;
                            case StackValueTypeCompare.Unt4_Real4: if (v1->u4 <= v2->r4) { ops = (seek + *opv); } break;
                            case StackValueTypeCompare.Unt4_Real8: if (v1->u4 <= v2->r8) { ops = (seek + *opv); } break;
                            case StackValueTypeCompare.Unt8_Int4: if (v2->i4 >= 0 && v1->u8 <= v2->u4) { ops = (seek + *opv); } break;
                            case StackValueTypeCompare.Unt8_Int8: if (v2->i8 >= 0 && v1->u8 <= v2->u8) { ops = (seek + *opv); } break;
                            case StackValueTypeCompare.Unt8_Unt4: if (v1->u8 <= v2->u4) { ops = (seek + *opv); } break;
                            case StackValueTypeCompare.Unt8_Unt8: if (v1->u8 <= v2->u8) { ops = (seek + *opv); } break;
                            case StackValueTypeCompare.Unt8_Real4: if (v1->u8 <= v2->r4) { ops = (seek + *opv); } break;
                            case StackValueTypeCompare.Unt8_Real8: if (v1->u8 <= v2->r8) { ops = (seek + *opv); } break;
                            case StackValueTypeCompare.Real4_Int4: if (v1->r4 <= v2->i4) { ops = (seek + *opv); } break;
                            case StackValueTypeCompare.Real4_Int8: if (v1->r4 <= v2->i8) { ops = (seek + *opv); } break;
                            case StackValueTypeCompare.Real4_Unt4: if (v1->r4 <= v2->u4) { ops = (seek + *opv); } break;
                            case StackValueTypeCompare.Real4_Unt8: if (v1->r4 <= v2->u8) { ops = (seek + *opv); } break;
                            case StackValueTypeCompare.Real4_Real4: if (v1->r4 <= v2->r4) { ops = (seek + *opv); } break;
                            case StackValueTypeCompare.Real4_Real8: if (v1->r4 <= v2->r8) { ops = (seek + *opv); } break;
                            case StackValueTypeCompare.Real8_Int4: if (v1->r8 <= v2->i4) { ops = (seek + *opv); } break;
                            case StackValueTypeCompare.Real8_Int8: if (v1->r8 <= v2->i8) { ops = (seek + *opv); } break;
                            case StackValueTypeCompare.Real8_Unt4: if (v1->r8 <= v2->u4) { ops = (seek + *opv); } break;
                            case StackValueTypeCompare.Real8_Unt8: if (v1->r8 <= v2->u8) { ops = (seek + *opv); } break;
                            case StackValueTypeCompare.Real8_Real4: if (v1->r8 <= v2->r4) { ops = (seek + *opv); } break;
                            case StackValueTypeCompare.Real8_Real8: if (v1->r8 <= v2->r8) { ops = (seek + *opv); } break;
                        }
                    }
                    goto NEXT;
                #endregion Ble
                #region Blt(a > b)
                case ILOpCode.Blt:
                case ILOpCode.Blt_s:
                    {
                        v1 = --stack;
                        v2 = --stack;
                        switch ((StackValueTypeCompare)(v1->typeNum * 6 + v2->typeNum))
                        {
                            case StackValueTypeCompare.Int4_Int4: if (v1->i4 < v2->i4) { ops = (seek + *(int*)opv); } break;
                            case StackValueTypeCompare.Int4_Int8: if (v1->i4 < v2->i8) { ops = (seek + *(int*)opv); } break;
                            case StackValueTypeCompare.Int4_Unt4: if (v1->i4 < v2->u4) { ops = (seek + *(int*)opv); } break;
                            case StackValueTypeCompare.Int4_Unt8: if (v1->i4 < 0 || v1->u4 < v2->u8) { ops = (seek + *(int*)opv); } break;
                            case StackValueTypeCompare.Int4_Real4: if (v1->i4 < v2->r4) { ops = (seek + *(int*)opv); } break;
                            case StackValueTypeCompare.Int4_Real8: if (v1->i4 < v2->r8) { ops = (seek + *(int*)opv); } break;
                            case StackValueTypeCompare.Int8_Int4: if (v1->i8 < v2->i4) { ops = (seek + *(int*)opv); } break;
                            case StackValueTypeCompare.Int8_Int8: if (v1->i8 < v2->i8) { ops = (seek + *(int*)opv); } break;
                            case StackValueTypeCompare.Int8_Unt4: if (v1->i8 < v2->u4) { ops = (seek + *(int*)opv); } break;
                            case StackValueTypeCompare.Int8_Unt8: if (v1->i8 < 0 || v1->u8 < v2->u8) { ops = (seek + *(int*)opv); } break;
                            case StackValueTypeCompare.Int8_Real4: if (v1->i8 < v2->r4) { ops = (seek + *(int*)opv); } break;
                            case StackValueTypeCompare.Int8_Real8: if (v1->i8 < v2->r8) { ops = (seek + *(int*)opv); } break;
                            case StackValueTypeCompare.Unt4_Int4: if (v1->u4 < v2->i4) { ops = (seek + *(int*)opv); } break;
                            case StackValueTypeCompare.Unt4_Int8: if (v1->u4 < v2->i8) { ops = (seek + *(int*)opv); } break;
                            case StackValueTypeCompare.Unt4_Unt4: if (v1->u4 < v2->u4) { ops = (seek + *(int*)opv); } break;
                            case StackValueTypeCompare.Unt4_Unt8: if (v1->u4 < v2->u8) { ops = (seek + *(int*)opv); } break;
                            case StackValueTypeCompare.Unt4_Real4: if (v1->u4 < v2->r4) { ops = (seek + *(int*)opv); } break;
                            case StackValueTypeCompare.Unt4_Real8: if (v1->u4 < v2->r8) { ops = (seek + *(int*)opv); } break;
                            case StackValueTypeCompare.Unt8_Int4: if (v2->i4 > 0 && v1->u8 < v2->u4) { ops = (seek + *(int*)opv); } break;
                            case StackValueTypeCompare.Unt8_Int8: if (v2->i8 > 0 && v1->u8 < v2->u8) { ops = (seek + *(int*)opv); } break;
                            case StackValueTypeCompare.Unt8_Unt4: if (v1->u8 < v2->u4) { ops = (seek + *(int*)opv); } break;
                            case StackValueTypeCompare.Unt8_Unt8: if (v1->u8 < v2->u8) { ops = (seek + *(int*)opv); } break;
                            case StackValueTypeCompare.Unt8_Real4: if (v1->u8 < v2->r4) { ops = (seek + *(int*)opv); } break;
                            case StackValueTypeCompare.Unt8_Real8: if (v1->u8 < v2->r8) { ops = (seek + *(int*)opv); } break;
                            case StackValueTypeCompare.Real4_Int4: if (v1->r4 < v2->i4) { ops = (seek + *(int*)opv); } break;
                            case StackValueTypeCompare.Real4_Int8: if (v1->r4 < v2->i8) { ops = (seek + *(int*)opv); } break;
                            case StackValueTypeCompare.Real4_Unt4: if (v1->r4 < v2->u4) { ops = (seek + *(int*)opv); } break;
                            case StackValueTypeCompare.Real4_Unt8: if (v1->r4 < v2->u8) { ops = (seek + *(int*)opv); } break;
                            case StackValueTypeCompare.Real4_Real4: if (v1->r4 < v2->r4) { ops = (seek + *(int*)opv); } break;
                            case StackValueTypeCompare.Real4_Real8: if (v1->r4 < v2->r8) { ops = (seek + *(int*)opv); } break;
                            case StackValueTypeCompare.Real8_Int4: if (v1->r8 < v2->i4) { ops = (seek + *(int*)opv); } break;
                            case StackValueTypeCompare.Real8_Int8: if (v1->r8 < v2->i8) { ops = (seek + *(int*)opv); } break;
                            case StackValueTypeCompare.Real8_Unt4: if (v1->r8 < v2->u4) { ops = (seek + *(int*)opv); } break;
                            case StackValueTypeCompare.Real8_Unt8: if (v1->r8 < v2->u8) { ops = (seek + *(int*)opv); } break;
                            case StackValueTypeCompare.Real8_Real4: if (v1->r8 < v2->r4) { ops = (seek + *(int*)opv); } break;
                            case StackValueTypeCompare.Real8_Real8: if (v1->r8 < v2->r8) { ops = (seek + *(int*)opv); } break;
                        }
                    }
                    goto NEXT;
                case ILOpCode.Blt_un:
                case ILOpCode.Blt_un_s:
                    {
                        v1 = --stack;
                        v2 = --stack;
                        switch ((StackValueTypeCompare)(v1->typeNum * 6 + v2->typeNum))
                        {
                            case StackValueTypeCompare.Int4_Int4: if (v1->i4 < v2->i4) { ops = (seek + *opv); } break;
                            case StackValueTypeCompare.Int4_Int8: if (v1->i4 < v2->i8) { ops = (seek + *opv); } break;
                            case StackValueTypeCompare.Int4_Unt4: if (v1->i4 < v2->u4) { ops = (seek + *opv); } break;
                            case StackValueTypeCompare.Int4_Unt8: if (v1->i4 < 0 || v1->u4 < v2->u8) { ops = (seek + *opv); } break;
                            case StackValueTypeCompare.Int4_Real4: if (v1->i4 < v2->r4) { ops = (seek + *opv); } break;
                            case StackValueTypeCompare.Int4_Real8: if (v1->i4 < v2->r8) { ops = (seek + *opv); } break;
                            case StackValueTypeCompare.Int8_Int4: if (v1->i8 < v2->i4) { ops = (seek + *opv); } break;
                            case StackValueTypeCompare.Int8_Int8: if (v1->i8 < v2->i8) { ops = (seek + *opv); } break;
                            case StackValueTypeCompare.Int8_Unt4: if (v1->i8 < v2->u4) { ops = (seek + *opv); } break;
                            case StackValueTypeCompare.Int8_Unt8: if (v1->i8 < 0 || v1->u8 < v2->u8) { ops = (seek + *opv); } break;
                            case StackValueTypeCompare.Int8_Real4: if (v1->i8 < v2->r4) { ops = (seek + *opv); } break;
                            case StackValueTypeCompare.Int8_Real8: if (v1->i8 < v2->r8) { ops = (seek + *opv); } break;
                            case StackValueTypeCompare.Unt4_Int4: if (v1->u4 < v2->i4) { ops = (seek + *opv); } break;
                            case StackValueTypeCompare.Unt4_Int8: if (v1->u4 < v2->i8) { ops = (seek + *opv); } break;
                            case StackValueTypeCompare.Unt4_Unt4: if (v1->u4 < v2->u4) { ops = (seek + *opv); } break;
                            case StackValueTypeCompare.Unt4_Unt8: if (v1->u4 < v2->u8) { ops = (seek + *opv); } break;
                            case StackValueTypeCompare.Unt4_Real4: if (v1->u4 < v2->r4) { ops = (seek + *opv); } break;
                            case StackValueTypeCompare.Unt4_Real8: if (v1->u4 < v2->r8) { ops = (seek + *opv); } break;
                            case StackValueTypeCompare.Unt8_Int4: if (v2->i4 >= 0 && v1->u8 < v2->u4) { ops = (seek + *opv); } break;
                            case StackValueTypeCompare.Unt8_Int8: if (v2->i8 >= 0 && v1->u8 < v2->u8) { ops = (seek + *opv); } break;
                            case StackValueTypeCompare.Unt8_Unt4: if (v1->u8 < v2->u4) { ops = (seek + *opv); } break;
                            case StackValueTypeCompare.Unt8_Unt8: if (v1->u8 < v2->u8) { ops = (seek + *opv); } break;
                            case StackValueTypeCompare.Unt8_Real4: if (v1->u8 < v2->r4) { ops = (seek + *opv); } break;
                            case StackValueTypeCompare.Unt8_Real8: if (v1->u8 < v2->r8) { ops = (seek + *opv); } break;
                            case StackValueTypeCompare.Real4_Int4: if (v1->r4 < v2->i4) { ops = (seek + *opv); } break;
                            case StackValueTypeCompare.Real4_Int8: if (v1->r4 < v2->i8) { ops = (seek + *opv); } break;
                            case StackValueTypeCompare.Real4_Unt4: if (v1->r4 < v2->u4) { ops = (seek + *opv); } break;
                            case StackValueTypeCompare.Real4_Unt8: if (v1->r4 < v2->u8) { ops = (seek + *opv); } break;
                            case StackValueTypeCompare.Real4_Real4: if (v1->r4 < v2->r4) { ops = (seek + *opv); } break;
                            case StackValueTypeCompare.Real4_Real8: if (v1->r4 < v2->r8) { ops = (seek + *opv); } break;
                            case StackValueTypeCompare.Real8_Int4: if (v1->r8 < v2->i4) { ops = (seek + *opv); } break;
                            case StackValueTypeCompare.Real8_Int8: if (v1->r8 < v2->i8) { ops = (seek + *opv); } break;
                            case StackValueTypeCompare.Real8_Unt4: if (v1->r8 < v2->u4) { ops = (seek + *opv); } break;
                            case StackValueTypeCompare.Real8_Unt8: if (v1->r8 < v2->u8) { ops = (seek + *opv); } break;
                            case StackValueTypeCompare.Real8_Real4: if (v1->r8 < v2->r4) { ops = (seek + *opv); } break;
                            case StackValueTypeCompare.Real8_Real8: if (v1->r8 < v2->r8) { ops = (seek + *opv); } break;
                        }
                    }
                    goto NEXT;
                #endregion

                case ILOpCode.Switch:
                    {
                    }
                    goto NEXT;

                #endregion condition jump

                case ILOpCode.Ldind_i1:
                    {
                    }
                    goto NEXT;
                case ILOpCode.Ldind_u1:
                    {
                    }
                    goto NEXT;
                case ILOpCode.Ldind_i2:
                    {
                    }
                    goto NEXT;
                case ILOpCode.Ldind_u2:
                    {
                    }
                    goto NEXT;
                case ILOpCode.Ldind_i4:
                    {
                    }
                    goto NEXT;
                case ILOpCode.Ldind_u4:
                    {
                    }
                    goto NEXT;
                case ILOpCode.Ldind_i8:
                    {
                    }
                    goto NEXT;
                case ILOpCode.Ldind_i:
                    {
                    }
                    goto NEXT;
                case ILOpCode.Ldind_r4:
                    {
                    }
                    goto NEXT;
                case ILOpCode.Ldind_r8:
                    {
                    }
                    goto NEXT;
                case ILOpCode.Ldind_ref:
                    {
                    }
                    goto NEXT;
                case ILOpCode.Stind_ref:
                    {
                    }
                    goto NEXT;
                case ILOpCode.Stind_i1:
                    {
                    }
                    goto NEXT;
                case ILOpCode.Stind_i2:
                    {
                    }
                    goto NEXT;
                case ILOpCode.Stind_i4:
                    {
                    }
                    goto NEXT;
                case ILOpCode.Stind_i8:
                    {
                    }
                    goto NEXT;
                case ILOpCode.Stind_r4:
                    {
                    }
                    goto NEXT;
                case ILOpCode.Stind_r8:
                    {
                    }
                    goto NEXT;
                case ILOpCode.Add:
                    {
                        v2 = (--stack);
                        v1 = (stack - 1);
                        switch ((StackValueTypeCompare)(v1->typeNum * 6 + v2->typeNum))
                        {
                            case StackValueTypeCompare.Int4_Int4: v1->i4 = v1->i4 + v2->i4; break;
                            case StackValueTypeCompare.Int4_Int8: v1->i4 = v1->i4 + v2->i8;  break;
                            case StackValueTypeCompare.Int4_Unt4: v1->i4 = v1->i4 + v2->u4; break;
                            case StackValueTypeCompare.Int4_Unt8: v1->i4 = v1->i4 + v2->u8; break;
                            case StackValueTypeCompare.Int4_Real4: v1->i4 = v1->i4 + v2->r4; break;
                            case StackValueTypeCompare.Int4_Real8: v1->i4 = v1->i4 + v2->r8; break;
                            case StackValueTypeCompare.Int8_Int4: v1->i8 = v1->i8 + v2->i4; break;
                            case StackValueTypeCompare.Int8_Int8: v1->i8 = v1->i8 + v2->i8; break;
                            case StackValueTypeCompare.Int8_Unt4: v1->i8 = v1->i8 + v2->u4; break;
                            case StackValueTypeCompare.Int8_Unt8: v1->i8 = v1->i8 + v2->u8; break;
                            case StackValueTypeCompare.Int8_Real4: v1->i8 = v1->i8 + v2->r4; break;
                            case StackValueTypeCompare.Int8_Real8: v1->i8 = v1->i8 + v2->r8; break;
                            case StackValueTypeCompare.Unt4_Int4: v1->u4 = v1->u4 + v2->i4; break;
                            case StackValueTypeCompare.Unt4_Int8: v1->u4 = v1->u4 + v2->i8; break;
                            case StackValueTypeCompare.Unt4_Unt4: v1->u4 = v1->u4 + v2->u4; break;
                            case StackValueTypeCompare.Unt4_Unt8: v1->u8 = v1->u4 + v2->u8; v1->type = StackValueType.Unt8; break;
                            case StackValueTypeCompare.Unt4_Real4: throw new Exception("Invalid operation"); //  v1->u4 = v1->u4 + v2->r4; break;
                            case StackValueTypeCompare.Unt4_Real8: throw new Exception("Invalid operation"); //  v1->u4 = v1->u4 + v2->r8; break;
                            case StackValueTypeCompare.Unt8_Int4: throw new Exception("Invalid operation"); // v1->u8 = v1->u8 + v2->i4; break;
                            case StackValueTypeCompare.Unt8_Int8: throw new Exception("Invalid operation"); // v1->u8 = v1->u8 + v2->i8; break;
                            case StackValueTypeCompare.Unt8_Unt4: v1->u8 = v1->u8 + v2->u4; break;
                            case StackValueTypeCompare.Unt8_Unt8: v1->u8 = v1->u8 + v2->u8; break;
                            case StackValueTypeCompare.Unt8_Real4: throw new Exception("Invalid operation"); // v1->u8 = v1->u8 + v2->r4; break;
                            case StackValueTypeCompare.Unt8_Real8: throw new Exception("Invalid operation"); // v1->u8 = v1->u8 + v2->r8; break;
                            case StackValueTypeCompare.Real4_Int4: v1->r4 = v1->r4 + v2->i4; break;
                            case StackValueTypeCompare.Real4_Int8: v1->r4 = v1->r4 + v2->i8; break;
                            case StackValueTypeCompare.Real4_Unt4: v1->r4 = v1->r4 + v2->u4; break;
                            case StackValueTypeCompare.Real4_Unt8: v1->r4 = v1->r4 + v2->u8; break;
                            case StackValueTypeCompare.Real4_Real4: v1->r4 = v1->r4 + v2->r4; break;
                            case StackValueTypeCompare.Real4_Real8: v1->r8 = v1->r4 + v2->r8; v1->type = StackValueType.Real8; break;
                            case StackValueTypeCompare.Real8_Int4: v1->r8 = v1->r8 + v2->i4; break;
                            case StackValueTypeCompare.Real8_Int8: v1->r8 = v1->r8 + v2->i8; break;
                            case StackValueTypeCompare.Real8_Unt4: v1->r8 = v1->r8 + v2->u4; break;
                            case StackValueTypeCompare.Real8_Unt8: v1->r8 = v1->r8 + v2->u8; break;
                            case StackValueTypeCompare.Real8_Real4: v1->r8 = v1->r8 + v2->r4; break;
                            case StackValueTypeCompare.Real8_Real8: v1->r8 = v1->r8 + v2->r8; break;
                        }
                    }
                    goto NEXT;
                case ILOpCode.Sub:
                    {
                    }
                    goto NEXT;
                case ILOpCode.Mul:
                    {
                    }
                    goto NEXT;
                case ILOpCode.Div:
                    {
                    }
                    goto NEXT;
                case ILOpCode.Div_un:
                    {
                    }
                    goto NEXT;
                case ILOpCode.Rem:
                    {
                    }
                    goto NEXT;
                case ILOpCode.Rem_un:
                    {
                    }
                    goto NEXT;
                case ILOpCode.And:
                    {
                    }
                    goto NEXT;
                case ILOpCode.Or:
                    {
                    }
                    goto NEXT;
                case ILOpCode.Xor:
                    {
                    }
                    goto NEXT;
                case ILOpCode.Shl:
                    {
                    }
                    goto NEXT;
                case ILOpCode.Shr:
                    {
                    }
                    goto NEXT;
                case ILOpCode.Shr_un:
                    {
                    }
                    goto NEXT;
                case ILOpCode.Neg:
                    {
                    }
                    goto NEXT;
                case ILOpCode.Not:
                    {
                    }
                    goto NEXT;
                case ILOpCode.Conv_i1:
                    {
                    }
                    goto NEXT;
                case ILOpCode.Conv_i2:
                    {
                    }
                    goto NEXT;
                case ILOpCode.Conv_i4:
                    {
                    }
                    goto NEXT;
                case ILOpCode.Conv_i8:
                    {
                    }
                    goto NEXT;
                case ILOpCode.Conv_r4:
                    {
                    }
                    goto NEXT;
                case ILOpCode.Conv_r8:
                    {
                    }
                    goto NEXT;
                case ILOpCode.Conv_u4:
                    {
                    }
                    goto NEXT;
                case ILOpCode.Conv_u8:
                    {
                    }
                    goto NEXT;
                case ILOpCode.Callvirt:
                    {
                    }
                    goto NEXT;
                case ILOpCode.Cpobj:
                    {
                    }
                    goto NEXT;
                case ILOpCode.Ldobj:
                    {
                    }
                    goto NEXT;
                case ILOpCode.Ldstr:
                    {
                    }
                    goto NEXT;
                case ILOpCode.Newobj:
                    {
                    }
                    goto NEXT;
                case ILOpCode.Castclass:
                    {
                    }
                    goto NEXT;
                case ILOpCode.Isinst:
                    {
                    }
                    goto NEXT;
                case ILOpCode.Conv_r_un:
                    {
                    }
                    goto NEXT;
                case ILOpCode.Unbox:
                    {
                    }
                    goto NEXT;
                case ILOpCode.Throw:
                    {
                    }
                    goto NEXT;
                case ILOpCode.Ldfld:
                    {
                    }
                    goto NEXT;
                case ILOpCode.Ldflda:
                    {
                    }
                    goto NEXT;
                case ILOpCode.Stfld:
                    {
                    }
                    goto NEXT;
                case ILOpCode.Ldsfld:
                    {
                    }
                    goto NEXT;
                case ILOpCode.Ldsflda:
                    {
                    }
                    goto NEXT;
                case ILOpCode.Stsfld:
                    {
                    }
                    goto NEXT;
                case ILOpCode.Stobj:
                    {
                    }
                    goto NEXT;
                case ILOpCode.Conv_ovf_i1_un:
                    {
                    }
                    goto NEXT;
                case ILOpCode.Conv_ovf_i2_un:
                    {
                    }
                    goto NEXT;
                case ILOpCode.Conv_ovf_i4_un:
                    {
                    }
                    goto NEXT;
                case ILOpCode.Conv_ovf_i8_un:
                    {
                    }
                    goto NEXT;
                case ILOpCode.Conv_ovf_u1_un:
                    {
                    }
                    goto NEXT;
                case ILOpCode.Conv_ovf_u2_un:
                    {
                    }
                    goto NEXT;
                case ILOpCode.Conv_ovf_u4_un:
                    {
                    }
                    goto NEXT;
                case ILOpCode.Conv_ovf_u8_un:
                    {
                    }
                    goto NEXT;
                case ILOpCode.Conv_ovf_i_un:
                    {
                    }
                    goto NEXT;
                case ILOpCode.Conv_ovf_u_un:
                    {
                    }
                    goto NEXT;
                case ILOpCode.Box:
                    {
                    }
                    goto NEXT;
                case ILOpCode.Newarr:
                    {
                    }
                    goto NEXT;
                case ILOpCode.Ldlen:
                    {
                    }
                    goto NEXT;
                case ILOpCode.Ldelema:
                    {
                    }
                    goto NEXT;
                case ILOpCode.Ldelem_i1:
                    {
                    }
                    goto NEXT;
                case ILOpCode.Ldelem_u1:
                    {
                    }
                    goto NEXT;
                case ILOpCode.Ldelem_i2:
                    {
                    }
                    goto NEXT;
                case ILOpCode.Ldelem_u2:
                    {
                    }
                    goto NEXT;
                case ILOpCode.Ldelem_i4:
                    {
                    }
                    goto NEXT;
                case ILOpCode.Ldelem_u4:
                    {
                    }
                    goto NEXT;
                case ILOpCode.Ldelem_i8:
                    {
                    }
                    goto NEXT;
                case ILOpCode.Ldelem_i:
                    {
                    }
                    goto NEXT;
                case ILOpCode.Ldelem_r4:
                    {
                    }
                    goto NEXT;
                case ILOpCode.Ldelem_r8:
                    {
                    }
                    goto NEXT;
                case ILOpCode.Ldelem_ref:
                    {
                    }
                    goto NEXT;
                case ILOpCode.Stelem_i:
                    {
                    }
                    goto NEXT;
                case ILOpCode.Stelem_i1:
                    {
                    }
                    goto NEXT;
                case ILOpCode.Stelem_i2:
                    {
                    }
                    goto NEXT;
                case ILOpCode.Stelem_i4:
                    {
                    }
                    goto NEXT;
                case ILOpCode.Stelem_i8:
                    {
                    }
                    goto NEXT;
                case ILOpCode.Stelem_r4:
                    {
                    }
                    goto NEXT;
                case ILOpCode.Stelem_r8:
                    {
                    }
                    goto NEXT;
                case ILOpCode.Stelem_ref:
                    {
                    }
                    goto NEXT;
                case ILOpCode.Ldelem:
                    {
                    }
                    goto NEXT;
                case ILOpCode.Stelem:
                    {
                    }
                    goto NEXT;
                case ILOpCode.Unbox_any:
                    {
                    }
                    goto NEXT;
                case ILOpCode.Conv_ovf_i1:
                    {
                    }
                    goto NEXT;
                case ILOpCode.Conv_ovf_u1:
                    {
                    }
                    goto NEXT;
                case ILOpCode.Conv_ovf_i2:
                    {
                    }
                    goto NEXT;
                case ILOpCode.Conv_ovf_u2:
                    {
                    }
                    goto NEXT;
                case ILOpCode.Conv_ovf_i4:
                    {
                    }
                    goto NEXT;
                case ILOpCode.Conv_ovf_u4:
                    {
                    }
                    goto NEXT;
                case ILOpCode.Conv_ovf_i8:
                    {
                    }
                    goto NEXT;
                case ILOpCode.Conv_ovf_u8:
                    {
                    }
                    goto NEXT;
                case ILOpCode.Refanyval:
                    {
                    }
                    goto NEXT;
                case ILOpCode.Ckfinite:
                    {
                    }
                    goto NEXT;
                case ILOpCode.Mkrefany:
                    {
                    }
                    goto NEXT;
                case ILOpCode.Ldtoken:
                    {
                    }
                    goto NEXT;
                case ILOpCode.Conv_u2:
                    {
                    }
                    goto NEXT;
                case ILOpCode.Conv_u1:
                    {
                    }
                    goto NEXT;
                case ILOpCode.Conv_i:
                    {
                    }
                    goto NEXT;
                case ILOpCode.Conv_ovf_i:
                    {
                    }
                    goto NEXT;
                case ILOpCode.Conv_ovf_u:
                    {
                    }
                    goto NEXT;
                case ILOpCode.Add_ovf:
                    {
                    }
                    goto NEXT;
                case ILOpCode.Add_ovf_un:
                    {
                    }
                    goto NEXT;
                case ILOpCode.Mul_ovf:
                    {
                    }
                    goto NEXT;
                case ILOpCode.Mul_ovf_un:
                    {
                    }
                    goto NEXT;
                case ILOpCode.Sub_ovf:
                    {
                    }
                    goto NEXT;
                case ILOpCode.Sub_ovf_un:
                    {
                    }
                    goto NEXT;
                case ILOpCode.Endfinally:
                    {
                    }
                    goto NEXT;
                case ILOpCode.Leave:
                    {
                    }
                    goto NEXT;
                case ILOpCode.Leave_s:
                    {
                    }
                    goto NEXT;
                case ILOpCode.Stind_i:
                    {
                    }
                    goto NEXT;
                case ILOpCode.Conv_u:
                    {
                    }
                    goto NEXT;
                case ILOpCode.Arglist:
                    {
                    }
                    goto NEXT;
                case ILOpCode.Ceq:
                    {
                    }
                    goto NEXT;
                case ILOpCode.Cgt:
                    {
                    }
                    goto NEXT;
                case ILOpCode.Cgt_un:
                    {
                    }
                    goto NEXT;
                case ILOpCode.Clt:
                    {
                        v2 = *(--stack);
                        v1 = *(--stack);
                        if (v1 < v2) *(stack++) = 1;
                        else *(stack++) = 0;
                    }
                    goto NEXT;
                case ILOpCode.Clt_un:
                    {
                    }
                    goto NEXT;
                case ILOpCode.Ldftn:
                    {
                    }
                    goto NEXT;
                case ILOpCode.Ldvirtftn:
                    {
                    }
                    goto NEXT;
                case ILOpCode.Ldarg:
                    {
                    }
                    goto NEXT;
                case ILOpCode.Ldarga:
                    {
                    }
                    goto NEXT;
                case ILOpCode.Starg:
                    {
                    }
                    goto NEXT;
                case ILOpCode.Localloc:
                    {
                    }
                    goto NEXT;
                case ILOpCode.Endfilter:
                    {
                    }
                    goto NEXT;
                case ILOpCode.Unaligned:
                    {
                    }
                    goto NEXT;
                case ILOpCode.Volatile:
                    {
                    }
                    goto NEXT;
                case ILOpCode.Tail:
                    {
                    }
                    goto NEXT;
                case ILOpCode.Initobj:
                    {
                    }
                    goto NEXT;
                case ILOpCode.Constrained:
                    {
                    }
                    goto NEXT;
                case ILOpCode.Cpblk:
                    {
                    }
                    goto NEXT;
                case ILOpCode.Initblk:
                    {
                    }
                    goto NEXT;
                case ILOpCode.No:
                    {
                    }
                    goto NEXT;
                case ILOpCode.Rethrow:
                    {
                    }
                    goto NEXT;
                case ILOpCode.Sizeof:
                    {
                    }
                    goto NEXT;
                case ILOpCode.Refanytype:
                    {
                    }
                    goto NEXT;
                case ILOpCode.Readonly:
                    {
                    }
                    goto NEXT;

                case ILOpCode.Custom_StLoc_0_Ldc_i4_0: local[0] = (void*)0; goto NEXT;
                case ILOpCode.Custom_StLoc_0_Ldc_i4_1: local[0] = (void*)1; goto NEXT;
                case ILOpCode.Custom_StLoc_0_Ldc_i4_2: local[0] = (void*)2; goto NEXT;
                case ILOpCode.Custom_StLoc_0_Ldc_i4_3: local[0] = (void*)3; goto NEXT;
                case ILOpCode.Custom_StLoc_0_Ldc_i4_4: local[0] = (void*)4; goto NEXT;
                case ILOpCode.Custom_StLoc_0_Ldc_i4_5: local[0] = (void*)5; goto NEXT;
                case ILOpCode.Custom_StLoc_0_Ldc_i4_6: local[0] = (void*)6; goto NEXT;
                case ILOpCode.Custom_StLoc_0_Ldc_i4_7: local[0] = (void*)7; goto NEXT;
                case ILOpCode.Custom_StLoc_0_Ldc_i4_8: local[0] = (void*)8; goto NEXT;
                case ILOpCode.Custom_StLoc_0_Ldc_i4_9: local[0] = (void*)9; goto NEXT;
                case ILOpCode.Custom_StLoc_1_Ldc_i4_0: local[1] = (void*)0; goto NEXT;
                case ILOpCode.Custom_StLoc_1_Ldc_i4_1: local[1] = (void*)1; goto NEXT;
                case ILOpCode.Custom_StLoc_1_Ldc_i4_2: local[1] = (void*)2; goto NEXT;
                case ILOpCode.Custom_StLoc_1_Ldc_i4_3: local[1] = (void*)3; goto NEXT;
                case ILOpCode.Custom_StLoc_1_Ldc_i4_4: local[1] = (void*)4; goto NEXT;
                case ILOpCode.Custom_StLoc_1_Ldc_i4_5: local[1] = (void*)5; goto NEXT;
                case ILOpCode.Custom_StLoc_1_Ldc_i4_6: local[1] = (void*)6; goto NEXT;
                case ILOpCode.Custom_StLoc_1_Ldc_i4_7: local[1] = (void*)7; goto NEXT;
                case ILOpCode.Custom_StLoc_1_Ldc_i4_8: local[1] = (void*)8; goto NEXT;
                case ILOpCode.Custom_StLoc_1_Ldc_i4_9: local[1] = (void*)9; goto NEXT;
                case ILOpCode.Custom_StLoc_2_Ldc_i4_0: local[2] = (void*)0; goto NEXT;
                case ILOpCode.Custom_StLoc_2_Ldc_i4_1: local[2] = (void*)1; goto NEXT;
                case ILOpCode.Custom_StLoc_2_Ldc_i4_2: local[2] = (void*)2; goto NEXT;
                case ILOpCode.Custom_StLoc_2_Ldc_i4_3: local[2] = (void*)3; goto NEXT;
                case ILOpCode.Custom_StLoc_2_Ldc_i4_4: local[2] = (void*)4; goto NEXT;
                case ILOpCode.Custom_StLoc_2_Ldc_i4_5: local[2] = (void*)5; goto NEXT;
                case ILOpCode.Custom_StLoc_2_Ldc_i4_6: local[2] = (void*)6; goto NEXT;
                case ILOpCode.Custom_StLoc_2_Ldc_i4_7: local[2] = (void*)7; goto NEXT;
                case ILOpCode.Custom_StLoc_2_Ldc_i4_8: local[2] = (void*)8; goto NEXT;
                case ILOpCode.Custom_StLoc_2_Ldc_i4_9: local[2] = (void*)9; goto NEXT;
                case ILOpCode.Custom_StLoc_3_Ldc_i4_0: local[3] = (void*)0; goto NEXT;
                case ILOpCode.Custom_StLoc_3_Ldc_i4_1: local[3] = (void*)1; goto NEXT;
                case ILOpCode.Custom_StLoc_3_Ldc_i4_2: local[3] = (void*)2; goto NEXT;
                case ILOpCode.Custom_StLoc_3_Ldc_i4_3: local[3] = (void*)3; goto NEXT;
                case ILOpCode.Custom_StLoc_3_Ldc_i4_4: local[3] = (void*)4; goto NEXT;
                case ILOpCode.Custom_StLoc_3_Ldc_i4_5: local[3] = (void*)5; goto NEXT;
                case ILOpCode.Custom_StLoc_3_Ldc_i4_6: local[3] = (void*)6; goto NEXT;
                case ILOpCode.Custom_StLoc_3_Ldc_i4_7: local[3] = (void*)7; goto NEXT;
                case ILOpCode.Custom_StLoc_3_Ldc_i4_8: local[3] = (void*)8; goto NEXT;
                case ILOpCode.Custom_StLoc_3_Ldc_i4_9: local[3] = (void*)9; goto NEXT;

                case ILOpCode.Custom_Add_0: *(stack - 1) = *(stack - 1) + 0; goto NEXT;
                case ILOpCode.Custom_Add_1: *(stack - 1) = *(stack - 1) + 1; goto NEXT;
                case ILOpCode.Custom_Add_2: *(stack - 1) = *(stack - 1) + 2; goto NEXT;
                case ILOpCode.Custom_Add_3: *(stack - 1) = *(stack - 1) + 3; goto NEXT;
                case ILOpCode.Custom_Add_4: *(stack - 1) = *(stack - 1) + 4; goto NEXT;
                case ILOpCode.Custom_Add_5: *(stack - 1) = *(stack - 1) + 5; goto NEXT;
                case ILOpCode.Custom_Add_6: *(stack - 1) = *(stack - 1) + 6; goto NEXT;
                case ILOpCode.Custom_Add_7: *(stack - 1) = *(stack - 1) + 7; goto NEXT;
                case ILOpCode.Custom_Add_8: *(stack - 1) = *(stack - 1) + 8; goto NEXT;
                case ILOpCode.Custom_Add_9: *(stack - 1) = *(stack - 1) + 9; goto NEXT;

                case ILOpCode.Custom_Ldloc_s_Ldc_s_Add_StLoc_s:
                    local[op[4]] = (void*)((long)local[*opv] + op[3]);
                    goto NEXT;

                case ILOpCode.Custom_Ldloc_s_Ldc_s_Blt_s:
                    if ((long)local[*opv] < op[3])
                    {
                        ops = (seek + op[4]);
                    }
                    goto NEXT;

                case ILOpCode.Custom_Ldloc_s_Ldc_Blt_s:
                    if ((long)local[*opv] < *(int*)(op + 3))
                    {
                        ops = (seek + op[7]);
                    }
                    goto NEXT;

                case ILOpCode.Custom_StLoc_s_Ldc_i4_s:
                    {
                        local[*opv] = (void*)*(op + 3);
                    }
                    goto NEXT;
            }
        }
    }
}
