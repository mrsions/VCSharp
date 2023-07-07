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
        const int COMPARE_TYPE_MAX = 7;

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

        internal unsafe void Call(LocalStack stacks, VObject? caller)
        {
            // 초기화
            stacks.Memory ??= StackMemory.Current;
            byte* bstack = (byte*)&stacks.BStack;
            ref ObjPtr ostack = ref stacks.OStack;

            //---------------------------------------------------------------------------------
            // 지역변수 삽입 (지역변수가 스텍보다 먼저 쌓여야한다)
            int localVariableLength = Variables.Count;
            StackValue* local = stacks.Alloc<StackValue>(localVariableLength);
            byte* localMemory = stacks.Alloc(VariableTotalSize);

            if (localVariableLength > 0)
            {
                byte* lastLocalMemory = localMemory;
                for (int i = 0; i < localVariableLength; i++)
                {
                    var v = Variables[i];
                    local[i].type = v.ValueType;

                    // 8바이트 이상의 메모리일 경우 
                    if (Variables[i].IsAdd)
                    {
                        local[i].ptr = lastLocalMemory;
                        lastLocalMemory += v.Size;
                    }

                    // 객체참조일 경우
                    else if (v.ValueType == StackValueType.obj)
                    {
                        local[i].i8 = stacks.AllocObj();
                    }
                }
            }

            //---------------------------------------------------------------------------------
            // 스텍변수 초기화
            StackValue* stack = stacks.Alloc<StackValue>(MaxStack);
            StackValue* v1;
            StackValue* v2;

            // 프로시져
            byte** seek = null;
            byte** ops = seek;

        NEXT:
            {
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

                            *(stack++) = *(local + *(ushort*)opv);
                        }
                        goto NEXT;
                    case ILOpCode.Ldloc_s:
                    case ILOpCode.Ldloca_s:
                        {
                            *(stack++) = *(local + *opv);
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
                            *(stack++) = new StackValue { type = StackValueType.i4, value = *opv };
                        }
                        goto NEXT;
                    case ILOpCode.Ldc_i4:
                        {
                            *(stack++) = new StackValue { type = StackValueType.i4, value = *(int*)opv };
                        }
                        goto NEXT;
                    case ILOpCode.Ldc_i8:
                        {
                            *(stack++) = new StackValue { type = StackValueType.i8, value = *(long*)opv };
                        }
                        goto NEXT;
                    case ILOpCode.Ldc_r4:
                        {
                            *(stack++) = new StackValue { type = StackValueType.r4, r4 = *(float*)opv };
                        }
                        goto NEXT;
                    case ILOpCode.Ldc_r8:
                        {
                            *(stack++) = new StackValue { type = StackValueType.r8, r8 = *(double*)opv };
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
                            var m = VAppDomain.Current.GetMethodInfo(*(int*)opv);

                            var parameters = m.GetParameters();
                            object[]? param = null;
                            if (parameters != null)
                            {
                                param = new object[parameters.Length];
                                for (int i = param.Length - 1; i >= 0; i--)
                                {
                                    var p = parameters[i];
                                    v1 = (--stack);
                                    param[i] = stacks.GetAny(v1, p.ParameterType);
                                }
                            }

                            v1 = (stack++);

                            object? result = m.Invoke(null, param);
                            stacks.SetAny(v1, m.ReturnType, result);
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
                            v1 = --stack;
                            v2 = --stack;
                            switch ((StackValueTypeCompare)(v1->typeNum * COMPARE_TYPE_MAX + v2->typeNum))
                            {
                                case StackValueTypeCompare.i4_i4: if (v1->i4 == v2->i4) break; goto NEXT;
                                case StackValueTypeCompare.i4_i8: if (v1->i4 == v2->i8) break; goto NEXT;
                                case StackValueTypeCompare.i4_u4: if (v1->i4 == v2->u4) break; goto NEXT;
                                case StackValueTypeCompare.i4_u8: if (v1->u4 == v2->u8) break; goto NEXT;
                                case StackValueTypeCompare.i4_r4: if (v1->i4 == v2->r4) break; goto NEXT;
                                case StackValueTypeCompare.i4_r8: if (v1->i4 == v2->r8) break; goto NEXT;
                                case StackValueTypeCompare.i8_i4: if (v1->i8 == v2->i4) break; goto NEXT;
                                case StackValueTypeCompare.i8_i8: if (v1->i8 == v2->i8) break; goto NEXT;
                                case StackValueTypeCompare.i8_u4: if (v1->i8 == v2->u4) break; goto NEXT;
                                case StackValueTypeCompare.i8_u8: if (v1->u8 == v2->u8) break; goto NEXT;
                                case StackValueTypeCompare.i8_r4: if (v1->i8 == v2->r4) break; goto NEXT;
                                case StackValueTypeCompare.i8_r8: if (v1->i8 == v2->r8) break; goto NEXT;
                                case StackValueTypeCompare.u4_i4: if (v1->u4 == v2->i4) break; goto NEXT;
                                case StackValueTypeCompare.u4_i8: if (v1->u4 == v2->i8) break; goto NEXT;
                                case StackValueTypeCompare.u4_u4: if (v1->u4 == v2->u4) break; goto NEXT;
                                case StackValueTypeCompare.u4_u8: if (v1->u4 == v2->u8) break; goto NEXT;
                                case StackValueTypeCompare.u4_r4: if (v1->u4 == v2->r4) break; goto NEXT;
                                case StackValueTypeCompare.u4_r8: if (v1->u4 == v2->r8) break; goto NEXT;
                                case StackValueTypeCompare.u8_i4: if (v1->u8 == v2->u4) break; goto NEXT;
                                case StackValueTypeCompare.u8_i8: if (v1->u8 == v2->u8) break; goto NEXT;
                                case StackValueTypeCompare.u8_u4: if (v1->u8 == v2->u4) break; goto NEXT;
                                case StackValueTypeCompare.u8_u8: if (v1->u8 == v2->u8) break; goto NEXT;
                                case StackValueTypeCompare.u8_r4: if (v1->u8 == v2->r4) break; goto NEXT;
                                case StackValueTypeCompare.u8_r8: if (v1->u8 == v2->r8) break; goto NEXT;
                                case StackValueTypeCompare.r4_i4: if (v1->r4 == v2->i4) break; goto NEXT;
                                case StackValueTypeCompare.r4_i8: if (v1->r4 == v2->i8) break; goto NEXT;
                                case StackValueTypeCompare.r4_u4: if (v1->r4 == v2->u4) break; goto NEXT;
                                case StackValueTypeCompare.r4_u8: if (v1->r4 == v2->u8) break; goto NEXT;
                                case StackValueTypeCompare.r4_r4: if (v1->r4 == v2->r4) break; goto NEXT;
                                case StackValueTypeCompare.r4_r8: if (v1->r4 == v2->r8) break; goto NEXT;
                                case StackValueTypeCompare.r8_i4: if (v1->r8 == v2->i4) break; goto NEXT;
                                case StackValueTypeCompare.r8_i8: if (v1->r8 == v2->i8) break; goto NEXT;
                                case StackValueTypeCompare.r8_u4: if (v1->r8 == v2->u4) break; goto NEXT;
                                case StackValueTypeCompare.r8_u8: if (v1->r8 == v2->u8) break; goto NEXT;
                                case StackValueTypeCompare.r8_r4: if (v1->r8 == v2->r4) break; goto NEXT;
                                case StackValueTypeCompare.r8_r8: if (v1->r8 == v2->r8) break; goto NEXT;
                                case StackValueTypeCompare.b_b: if (v1->b == v2->b) break; goto NEXT;
                                default: if (v1->value == v2->value) break; goto NEXT;
                            }
                            ops = (seek + *(int*)opv);
                        }
                        goto NEXT;
                    case ILOpCode.Beq_s:
                        {
                            v1 = --stack;
                            v2 = --stack;
                            switch ((StackValueTypeCompare)(v1->typeNum * COMPARE_TYPE_MAX + v2->typeNum))
                            {
                                case StackValueTypeCompare.i4_i4: if (v1->i4 == v2->i4) break; goto NEXT;
                                case StackValueTypeCompare.i4_i8: if (v1->i4 == v2->i8) break; goto NEXT;
                                case StackValueTypeCompare.i4_u4: if (v1->i4 == v2->u4) break; goto NEXT;
                                case StackValueTypeCompare.i4_u8: if (v1->u4 == v2->u8) break; goto NEXT;
                                case StackValueTypeCompare.i4_r4: if (v1->i4 == v2->r4) break; goto NEXT;
                                case StackValueTypeCompare.i4_r8: if (v1->i4 == v2->r8) break; goto NEXT;
                                case StackValueTypeCompare.i8_i4: if (v1->i8 == v2->i4) break; goto NEXT;
                                case StackValueTypeCompare.i8_i8: if (v1->i8 == v2->i8) break; goto NEXT;
                                case StackValueTypeCompare.i8_u4: if (v1->i8 == v2->u4) break; goto NEXT;
                                case StackValueTypeCompare.i8_u8: if (v1->u8 == v2->u8) break; goto NEXT;
                                case StackValueTypeCompare.i8_r4: if (v1->i8 == v2->r4) break; goto NEXT;
                                case StackValueTypeCompare.i8_r8: if (v1->i8 == v2->r8) break; goto NEXT;
                                case StackValueTypeCompare.u4_i4: if (v1->u4 == v2->i4) break; goto NEXT;
                                case StackValueTypeCompare.u4_i8: if (v1->u4 == v2->i8) break; goto NEXT;
                                case StackValueTypeCompare.u4_u4: if (v1->u4 == v2->u4) break; goto NEXT;
                                case StackValueTypeCompare.u4_u8: if (v1->u4 == v2->u8) break; goto NEXT;
                                case StackValueTypeCompare.u4_r4: if (v1->u4 == v2->r4) break; goto NEXT;
                                case StackValueTypeCompare.u4_r8: if (v1->u4 == v2->r8) break; goto NEXT;
                                case StackValueTypeCompare.u8_i4: if (v1->u8 == v2->u4) break; goto NEXT;
                                case StackValueTypeCompare.u8_i8: if (v1->u8 == v2->u8) break; goto NEXT;
                                case StackValueTypeCompare.u8_u4: if (v1->u8 == v2->u4) break; goto NEXT;
                                case StackValueTypeCompare.u8_u8: if (v1->u8 == v2->u8) break; goto NEXT;
                                case StackValueTypeCompare.u8_r4: if (v1->u8 == v2->r4) break; goto NEXT;
                                case StackValueTypeCompare.u8_r8: if (v1->u8 == v2->r8) break; goto NEXT;
                                case StackValueTypeCompare.r4_i4: if (v1->r4 == v2->i4) break; goto NEXT;
                                case StackValueTypeCompare.r4_i8: if (v1->r4 == v2->i8) break; goto NEXT;
                                case StackValueTypeCompare.r4_u4: if (v1->r4 == v2->u4) break; goto NEXT;
                                case StackValueTypeCompare.r4_u8: if (v1->r4 == v2->u8) break; goto NEXT;
                                case StackValueTypeCompare.r4_r4: if (v1->r4 == v2->r4) break; goto NEXT;
                                case StackValueTypeCompare.r4_r8: if (v1->r4 == v2->r8) break; goto NEXT;
                                case StackValueTypeCompare.r8_i4: if (v1->r8 == v2->i4) break; goto NEXT;
                                case StackValueTypeCompare.r8_i8: if (v1->r8 == v2->i8) break; goto NEXT;
                                case StackValueTypeCompare.r8_u4: if (v1->r8 == v2->u4) break; goto NEXT;
                                case StackValueTypeCompare.r8_u8: if (v1->r8 == v2->u8) break; goto NEXT;
                                case StackValueTypeCompare.r8_r4: if (v1->r8 == v2->r4) break; goto NEXT;
                                case StackValueTypeCompare.r8_r8: if (v1->r8 == v2->r8) break; goto NEXT;
                                case StackValueTypeCompare.b_b: if (v1->b == v2->b) break; goto NEXT;
                                default: if (v1->value == v2->value) break; goto NEXT;
                            }
                            ops = (seek + *opv);
                        }
                        goto NEXT;
                    case ILOpCode.Bne_un:
                        {
                            v1 = --stack;
                            v2 = --stack;
                            switch ((StackValueTypeCompare)(v1->typeNum * COMPARE_TYPE_MAX + v2->typeNum))
                            {
                                case StackValueTypeCompare.i4_i4: if (v1->i4 != v2->i4) break; goto NEXT;
                                case StackValueTypeCompare.i4_i8: if (v1->i4 != v2->i8) break; goto NEXT;
                                case StackValueTypeCompare.i4_u4: if (v1->i4 != v2->u4) break; goto NEXT;
                                case StackValueTypeCompare.i4_u8: if (v1->u4 != v2->u8) break; goto NEXT;
                                case StackValueTypeCompare.i4_r4: if (v1->i4 != v2->r4) break; goto NEXT;
                                case StackValueTypeCompare.i4_r8: if (v1->i4 != v2->r8) break; goto NEXT;
                                case StackValueTypeCompare.i8_i4: if (v1->i8 != v2->i4) break; goto NEXT;
                                case StackValueTypeCompare.i8_i8: if (v1->i8 != v2->i8) break; goto NEXT;
                                case StackValueTypeCompare.i8_u4: if (v1->i8 != v2->u4) break; goto NEXT;
                                case StackValueTypeCompare.i8_u8: if (v1->u8 != v2->u8) break; goto NEXT;
                                case StackValueTypeCompare.i8_r4: if (v1->i8 != v2->r4) break; goto NEXT;
                                case StackValueTypeCompare.i8_r8: if (v1->i8 != v2->r8) break; goto NEXT;
                                case StackValueTypeCompare.u4_i4: if (v1->u4 != v2->i4) break; goto NEXT;
                                case StackValueTypeCompare.u4_i8: if (v1->u4 != v2->i8) break; goto NEXT;
                                case StackValueTypeCompare.u4_u4: if (v1->u4 != v2->u4) break; goto NEXT;
                                case StackValueTypeCompare.u4_u8: if (v1->u4 != v2->u8) break; goto NEXT;
                                case StackValueTypeCompare.u4_r4: if (v1->u4 != v2->r4) break; goto NEXT;
                                case StackValueTypeCompare.u4_r8: if (v1->u4 != v2->r8) break; goto NEXT;
                                case StackValueTypeCompare.u8_i4: if (v1->u8 != v2->u4) break; goto NEXT;
                                case StackValueTypeCompare.u8_i8: if (v1->u8 != v2->u8) break; goto NEXT;
                                case StackValueTypeCompare.u8_u4: if (v1->u8 != v2->u4) break; goto NEXT;
                                case StackValueTypeCompare.u8_u8: if (v1->u8 != v2->u8) break; goto NEXT;
                                case StackValueTypeCompare.u8_r4: if (v1->u8 != v2->r4) break; goto NEXT;
                                case StackValueTypeCompare.u8_r8: if (v1->u8 != v2->r8) break; goto NEXT;
                                case StackValueTypeCompare.r4_i4: if (v1->r4 != v2->i4) break; goto NEXT;
                                case StackValueTypeCompare.r4_i8: if (v1->r4 != v2->i8) break; goto NEXT;
                                case StackValueTypeCompare.r4_u4: if (v1->r4 != v2->u4) break; goto NEXT;
                                case StackValueTypeCompare.r4_u8: if (v1->r4 != v2->u8) break; goto NEXT;
                                case StackValueTypeCompare.r4_r4: if (v1->r4 != v2->r4) break; goto NEXT;
                                case StackValueTypeCompare.r4_r8: if (v1->r4 != v2->r8) break; goto NEXT;
                                case StackValueTypeCompare.r8_i4: if (v1->r8 != v2->i4) break; goto NEXT;
                                case StackValueTypeCompare.r8_i8: if (v1->r8 != v2->i8) break; goto NEXT;
                                case StackValueTypeCompare.r8_u4: if (v1->r8 != v2->u4) break; goto NEXT;
                                case StackValueTypeCompare.r8_u8: if (v1->r8 != v2->u8) break; goto NEXT;
                                case StackValueTypeCompare.r8_r4: if (v1->r8 != v2->r4) break; goto NEXT;
                                case StackValueTypeCompare.r8_r8: if (v1->r8 != v2->r8) break; goto NEXT;
                                case StackValueTypeCompare.b_b: if (v1->b != v2->b) break; goto NEXT;
                                default: if (v1->value != v2->value) break; goto NEXT;
                            }
                            ops = (seek + *(int*)opv);
                        }
                        goto NEXT;
                    case ILOpCode.Bne_un_s:
                        {
                            v1 = --stack;
                            v2 = --stack;
                            switch ((StackValueTypeCompare)(v1->typeNum * COMPARE_TYPE_MAX + v2->typeNum))
                            {
                                case StackValueTypeCompare.i4_i4: if (v1->i4 != v2->i4) break; goto NEXT;
                                case StackValueTypeCompare.i4_i8: if (v1->i4 != v2->i8) break; goto NEXT;
                                case StackValueTypeCompare.i4_u4: if (v1->i4 != v2->u4) break; goto NEXT;
                                case StackValueTypeCompare.i4_u8: if (v1->u4 != v2->u8) break; goto NEXT;
                                case StackValueTypeCompare.i4_r4: if (v1->i4 != v2->r4) break; goto NEXT;
                                case StackValueTypeCompare.i4_r8: if (v1->i4 != v2->r8) break; goto NEXT;
                                case StackValueTypeCompare.i8_i4: if (v1->i8 != v2->i4) break; goto NEXT;
                                case StackValueTypeCompare.i8_i8: if (v1->i8 != v2->i8) break; goto NEXT;
                                case StackValueTypeCompare.i8_u4: if (v1->i8 != v2->u4) break; goto NEXT;
                                case StackValueTypeCompare.i8_u8: if (v1->u8 != v2->u8) break; goto NEXT;
                                case StackValueTypeCompare.i8_r4: if (v1->i8 != v2->r4) break; goto NEXT;
                                case StackValueTypeCompare.i8_r8: if (v1->i8 != v2->r8) break; goto NEXT;
                                case StackValueTypeCompare.u4_i4: if (v1->u4 != v2->i4) break; goto NEXT;
                                case StackValueTypeCompare.u4_i8: if (v1->u4 != v2->i8) break; goto NEXT;
                                case StackValueTypeCompare.u4_u4: if (v1->u4 != v2->u4) break; goto NEXT;
                                case StackValueTypeCompare.u4_u8: if (v1->u4 != v2->u8) break; goto NEXT;
                                case StackValueTypeCompare.u4_r4: if (v1->u4 != v2->r4) break; goto NEXT;
                                case StackValueTypeCompare.u4_r8: if (v1->u4 != v2->r8) break; goto NEXT;
                                case StackValueTypeCompare.u8_i4: if (v1->u8 != v2->u4) break; goto NEXT;
                                case StackValueTypeCompare.u8_i8: if (v1->u8 != v2->u8) break; goto NEXT;
                                case StackValueTypeCompare.u8_u4: if (v1->u8 != v2->u4) break; goto NEXT;
                                case StackValueTypeCompare.u8_u8: if (v1->u8 != v2->u8) break; goto NEXT;
                                case StackValueTypeCompare.u8_r4: if (v1->u8 != v2->r4) break; goto NEXT;
                                case StackValueTypeCompare.u8_r8: if (v1->u8 != v2->r8) break; goto NEXT;
                                case StackValueTypeCompare.r4_i4: if (v1->r4 != v2->i4) break; goto NEXT;
                                case StackValueTypeCompare.r4_i8: if (v1->r4 != v2->i8) break; goto NEXT;
                                case StackValueTypeCompare.r4_u4: if (v1->r4 != v2->u4) break; goto NEXT;
                                case StackValueTypeCompare.r4_u8: if (v1->r4 != v2->u8) break; goto NEXT;
                                case StackValueTypeCompare.r4_r4: if (v1->r4 != v2->r4) break; goto NEXT;
                                case StackValueTypeCompare.r4_r8: if (v1->r4 != v2->r8) break; goto NEXT;
                                case StackValueTypeCompare.r8_i4: if (v1->r8 != v2->i4) break; goto NEXT;
                                case StackValueTypeCompare.r8_i8: if (v1->r8 != v2->i8) break; goto NEXT;
                                case StackValueTypeCompare.r8_u4: if (v1->r8 != v2->u4) break; goto NEXT;
                                case StackValueTypeCompare.r8_u8: if (v1->r8 != v2->u8) break; goto NEXT;
                                case StackValueTypeCompare.r8_r4: if (v1->r8 != v2->r4) break; goto NEXT;
                                case StackValueTypeCompare.r8_r8: if (v1->r8 != v2->r8) break; goto NEXT;
                                case StackValueTypeCompare.b_b: if (v1->b != v2->b) break; goto NEXT;
                                default: if (v1->value != v2->value) break; goto NEXT;
                            }
                            ops = (seek + *opv);
                        }
                        goto NEXT;
                    #endregion Br
                    #region Bge(a <= b)
                    case ILOpCode.Bge:
                    case ILOpCode.Bge_un:
                        {
                            v1 = --stack;
                            v2 = --stack;
                            switch ((StackValueTypeCompare)(v1->typeNum * COMPARE_TYPE_MAX + v2->typeNum))
                            {
                                case StackValueTypeCompare.i4_i4: if (v1->i4 >= v2->i4) break; goto NEXT;
                                case StackValueTypeCompare.i4_i8: if (v1->i4 >= v2->i8) break; goto NEXT;
                                case StackValueTypeCompare.i4_u4: if (v1->i4 >= v2->u4) break; goto NEXT;
                                case StackValueTypeCompare.i4_u8: if (v1->i4 >= 0 && v1->u4 >= v2->u8) break; goto NEXT;
                                case StackValueTypeCompare.i4_r4: if (v1->i4 >= v2->r4) break; goto NEXT;
                                case StackValueTypeCompare.i4_r8: if (v1->i4 >= v2->r8) break; goto NEXT;
                                case StackValueTypeCompare.i8_i4: if (v1->i8 >= v2->i4) break; goto NEXT;
                                case StackValueTypeCompare.i8_i8: if (v1->i8 >= v2->i8) break; goto NEXT;
                                case StackValueTypeCompare.i8_u4: if (v1->i8 >= v2->u4) break; goto NEXT;
                                case StackValueTypeCompare.i8_u8: if (v1->i8 >= 0 && v1->u8 >= v2->u8) break; goto NEXT;
                                case StackValueTypeCompare.i8_r4: if (v1->i8 >= v2->r4) break; goto NEXT;
                                case StackValueTypeCompare.i8_r8: if (v1->i8 >= v2->r8) break; goto NEXT;
                                case StackValueTypeCompare.u4_i4: if (v1->u4 >= v2->i4) break; goto NEXT;
                                case StackValueTypeCompare.u4_i8: if (v1->u4 >= v2->i8) break; goto NEXT;
                                case StackValueTypeCompare.u4_u4: if (v1->u4 >= v2->u4) break; goto NEXT;
                                case StackValueTypeCompare.u4_u8: if (v1->u4 >= v2->u8) break; goto NEXT;
                                case StackValueTypeCompare.u4_r4: if (v1->u4 >= v2->r4) break; goto NEXT;
                                case StackValueTypeCompare.u4_r8: if (v1->u4 >= v2->r8) break; goto NEXT;
                                case StackValueTypeCompare.u8_i4: if (v2->i4 >= 0 && v1->u8 >= v2->u4) break; goto NEXT;
                                case StackValueTypeCompare.u8_i8: if (v2->i8 >= 0 && v1->u8 >= v2->u8) break; goto NEXT;
                                case StackValueTypeCompare.u8_u4: if (v1->u8 >= v2->u4) break; goto NEXT;
                                case StackValueTypeCompare.u8_u8: if (v1->u8 >= v2->u8) break; goto NEXT;
                                case StackValueTypeCompare.u8_r4: if (v1->u8 >= v2->r4) break; goto NEXT;
                                case StackValueTypeCompare.u8_r8: if (v1->u8 >= v2->r8) break; goto NEXT;
                                case StackValueTypeCompare.r4_i4: if (v1->r4 >= v2->i4) break; goto NEXT;
                                case StackValueTypeCompare.r4_i8: if (v1->r4 >= v2->i8) break; goto NEXT;
                                case StackValueTypeCompare.r4_u4: if (v1->r4 >= v2->u4) break; goto NEXT;
                                case StackValueTypeCompare.r4_u8: if (v1->r4 >= v2->u8) break; goto NEXT;
                                case StackValueTypeCompare.r4_r4: if (v1->r4 >= v2->r4) break; goto NEXT;
                                case StackValueTypeCompare.r4_r8: if (v1->r4 >= v2->r8) break; goto NEXT;
                                case StackValueTypeCompare.r8_i4: if (v1->r8 >= v2->i4) break; goto NEXT;
                                case StackValueTypeCompare.r8_i8: if (v1->r8 >= v2->i8) break; goto NEXT;
                                case StackValueTypeCompare.r8_u4: if (v1->r8 >= v2->u4) break; goto NEXT;
                                case StackValueTypeCompare.r8_u8: if (v1->r8 >= v2->u8) break; goto NEXT;
                                case StackValueTypeCompare.r8_r4: if (v1->r8 >= v2->r4) break; goto NEXT;
                                case StackValueTypeCompare.r8_r8: if (v1->r8 >= v2->r8) break; goto NEXT;
                                default: throw new Exception("Invalid operation");
                            }
                            ops = (seek + *(int*)opv);
                        }
                        goto NEXT;
                    case ILOpCode.Bge_s:
                    case ILOpCode.Bge_un_s:
                        {
                            v1 = --stack;
                            v2 = --stack;
                            switch ((StackValueTypeCompare)(v1->typeNum * COMPARE_TYPE_MAX + v2->typeNum))
                            {
                                case StackValueTypeCompare.i4_i4: if (v1->i4 >= v2->i4) break; goto NEXT;
                                case StackValueTypeCompare.i4_i8: if (v1->i4 >= v2->i8) break; goto NEXT;
                                case StackValueTypeCompare.i4_u4: if (v1->i4 >= v2->u4) break; goto NEXT;
                                case StackValueTypeCompare.i4_u8: if (v1->i4 >= 0 && v1->u4 >= v2->u8) break; goto NEXT;
                                case StackValueTypeCompare.i4_r4: if (v1->i4 >= v2->r4) break; goto NEXT;
                                case StackValueTypeCompare.i4_r8: if (v1->i4 >= v2->r8) break; goto NEXT;
                                case StackValueTypeCompare.i8_i4: if (v1->i8 >= v2->i4) break; goto NEXT;
                                case StackValueTypeCompare.i8_i8: if (v1->i8 >= v2->i8) break; goto NEXT;
                                case StackValueTypeCompare.i8_u4: if (v1->i8 >= v2->u4) break; goto NEXT;
                                case StackValueTypeCompare.i8_u8: if (v1->i8 >= 0 && v1->u8 >= v2->u8) break; goto NEXT;
                                case StackValueTypeCompare.i8_r4: if (v1->i8 >= v2->r4) break; goto NEXT;
                                case StackValueTypeCompare.i8_r8: if (v1->i8 >= v2->r8) break; goto NEXT;
                                case StackValueTypeCompare.u4_i4: if (v1->u4 >= v2->i4) break; goto NEXT;
                                case StackValueTypeCompare.u4_i8: if (v1->u4 >= v2->i8) break; goto NEXT;
                                case StackValueTypeCompare.u4_u4: if (v1->u4 >= v2->u4) break; goto NEXT;
                                case StackValueTypeCompare.u4_u8: if (v1->u4 >= v2->u8) break; goto NEXT;
                                case StackValueTypeCompare.u4_r4: if (v1->u4 >= v2->r4) break; goto NEXT;
                                case StackValueTypeCompare.u4_r8: if (v1->u4 >= v2->r8) break; goto NEXT;
                                case StackValueTypeCompare.u8_i4: if (v2->i4 >= 0 && v1->u8 >= v2->u4) break; goto NEXT;
                                case StackValueTypeCompare.u8_i8: if (v2->i8 >= 0 && v1->u8 >= v2->u8) break; goto NEXT;
                                case StackValueTypeCompare.u8_u4: if (v1->u8 >= v2->u4) break; goto NEXT;
                                case StackValueTypeCompare.u8_u8: if (v1->u8 >= v2->u8) break; goto NEXT;
                                case StackValueTypeCompare.u8_r4: if (v1->u8 >= v2->r4) break; goto NEXT;
                                case StackValueTypeCompare.u8_r8: if (v1->u8 >= v2->r8) break; goto NEXT;
                                case StackValueTypeCompare.r4_i4: if (v1->r4 >= v2->i4) break; goto NEXT;
                                case StackValueTypeCompare.r4_i8: if (v1->r4 >= v2->i8) break; goto NEXT;
                                case StackValueTypeCompare.r4_u4: if (v1->r4 >= v2->u4) break; goto NEXT;
                                case StackValueTypeCompare.r4_u8: if (v1->r4 >= v2->u8) break; goto NEXT;
                                case StackValueTypeCompare.r4_r4: if (v1->r4 >= v2->r4) break; goto NEXT;
                                case StackValueTypeCompare.r4_r8: if (v1->r4 >= v2->r8) break; goto NEXT;
                                case StackValueTypeCompare.r8_i4: if (v1->r8 >= v2->i4) break; goto NEXT;
                                case StackValueTypeCompare.r8_i8: if (v1->r8 >= v2->i8) break; goto NEXT;
                                case StackValueTypeCompare.r8_u4: if (v1->r8 >= v2->u4) break; goto NEXT;
                                case StackValueTypeCompare.r8_u8: if (v1->r8 >= v2->u8) break; goto NEXT;
                                case StackValueTypeCompare.r8_r4: if (v1->r8 >= v2->r4) break; goto NEXT;
                                case StackValueTypeCompare.r8_r8: if (v1->r8 >= v2->r8) break; goto NEXT;
                                default: throw new Exception("Invalid operation");
                            }
                            ops = (seek + *opv);
                        }
                        goto NEXT;
                    #endregion Bge
                    #region Bgt(a < b)
                    case ILOpCode.Bgt:
                    case ILOpCode.Bgt_un:
                        {
                            v1 = --stack;
                            v2 = --stack;
                            switch ((StackValueTypeCompare)(v1->typeNum * COMPARE_TYPE_MAX + v2->typeNum))
                            {
                                case StackValueTypeCompare.i4_i4: if (v1->i4 > v2->i4) break; goto NEXT;
                                case StackValueTypeCompare.i4_i8: if (v1->i4 > v2->i8) break; goto NEXT;
                                case StackValueTypeCompare.i4_u4: if (v1->i4 > v2->u4) break; goto NEXT;
                                case StackValueTypeCompare.i4_u8: if (v1->i4 > 0 && v1->u4 > v2->u8) break; goto NEXT;
                                case StackValueTypeCompare.i4_r4: if (v1->i4 > v2->r4) break; goto NEXT;
                                case StackValueTypeCompare.i4_r8: if (v1->i4 > v2->r8) break; goto NEXT;
                                case StackValueTypeCompare.i8_i4: if (v1->i8 > v2->i4) break; goto NEXT;
                                case StackValueTypeCompare.i8_i8: if (v1->i8 > v2->i8) break; goto NEXT;
                                case StackValueTypeCompare.i8_u4: if (v1->i8 > v2->u4) break; goto NEXT;
                                case StackValueTypeCompare.i8_u8: if (v1->i8 > 0 && v1->u8 > v2->u8) break; goto NEXT;
                                case StackValueTypeCompare.i8_r4: if (v1->i8 > v2->r4) break; goto NEXT;
                                case StackValueTypeCompare.i8_r8: if (v1->i8 > v2->r8) break; goto NEXT;
                                case StackValueTypeCompare.u4_i4: if (v1->u4 > v2->i4) break; goto NEXT;
                                case StackValueTypeCompare.u4_i8: if (v1->u4 > v2->i8) break; goto NEXT;
                                case StackValueTypeCompare.u4_u4: if (v1->u4 > v2->u4) break; goto NEXT;
                                case StackValueTypeCompare.u4_u8: if (v1->u4 > v2->u8) break; goto NEXT;
                                case StackValueTypeCompare.u4_r4: if (v1->u4 > v2->r4) break; goto NEXT;
                                case StackValueTypeCompare.u4_r8: if (v1->u4 > v2->r8) break; goto NEXT;
                                case StackValueTypeCompare.u8_i4: if (v2->i4 < 0 || v1->u8 > v2->u4) break; goto NEXT;
                                case StackValueTypeCompare.u8_i8: if (v2->i8 < 0 || v1->u8 > v2->u8) break; goto NEXT;
                                case StackValueTypeCompare.u8_u4: if (v1->u8 > v2->u4) break; goto NEXT;
                                case StackValueTypeCompare.u8_u8: if (v1->u8 > v2->u8) break; goto NEXT;
                                case StackValueTypeCompare.u8_r4: if (v1->u8 > v2->r4) break; goto NEXT;
                                case StackValueTypeCompare.u8_r8: if (v1->u8 > v2->r8) break; goto NEXT;
                                case StackValueTypeCompare.r4_i4: if (v1->r4 > v2->i4) break; goto NEXT;
                                case StackValueTypeCompare.r4_i8: if (v1->r4 > v2->i8) break; goto NEXT;
                                case StackValueTypeCompare.r4_u4: if (v1->r4 > v2->u4) break; goto NEXT;
                                case StackValueTypeCompare.r4_u8: if (v1->r4 > v2->u8) break; goto NEXT;
                                case StackValueTypeCompare.r4_r4: if (v1->r4 > v2->r4) break; goto NEXT;
                                case StackValueTypeCompare.r4_r8: if (v1->r4 > v2->r8) break; goto NEXT;
                                case StackValueTypeCompare.r8_i4: if (v1->r8 > v2->i4) break; goto NEXT;
                                case StackValueTypeCompare.r8_i8: if (v1->r8 > v2->i8) break; goto NEXT;
                                case StackValueTypeCompare.r8_u4: if (v1->r8 > v2->u4) break; goto NEXT;
                                case StackValueTypeCompare.r8_u8: if (v1->r8 > v2->u8) break; goto NEXT;
                                case StackValueTypeCompare.r8_r4: if (v1->r8 > v2->r4) break; goto NEXT;
                                case StackValueTypeCompare.r8_r8: if (v1->r8 > v2->r8) break; goto NEXT;
                                default: throw new Exception("Invalid operation");
                            }
                            ops = (seek + *(int*)opv);
                        }
                        goto NEXT;
                    case ILOpCode.Bgt_s:
                    case ILOpCode.Bgt_un_s:
                        {
                            v1 = --stack;
                            v2 = --stack;
                            switch ((StackValueTypeCompare)(v1->typeNum * COMPARE_TYPE_MAX + v2->typeNum))
                            {
                                case StackValueTypeCompare.i4_i4: if (v1->i4 > v2->i4) break; goto NEXT;
                                case StackValueTypeCompare.i4_i8: if (v1->i4 > v2->i8) break; goto NEXT;
                                case StackValueTypeCompare.i4_u4: if (v1->i4 > v2->u4) break; goto NEXT;
                                case StackValueTypeCompare.i4_u8: if (v1->i4 > 0 && v1->u4 > v2->u8) break; goto NEXT;
                                case StackValueTypeCompare.i4_r4: if (v1->i4 > v2->r4) break; goto NEXT;
                                case StackValueTypeCompare.i4_r8: if (v1->i4 > v2->r8) break; goto NEXT;
                                case StackValueTypeCompare.i8_i4: if (v1->i8 > v2->i4) break; goto NEXT;
                                case StackValueTypeCompare.i8_i8: if (v1->i8 > v2->i8) break; goto NEXT;
                                case StackValueTypeCompare.i8_u4: if (v1->i8 > v2->u4) break; goto NEXT;
                                case StackValueTypeCompare.i8_u8: if (v1->i8 > 0 && v1->u8 > v2->u8) break; goto NEXT;
                                case StackValueTypeCompare.i8_r4: if (v1->i8 > v2->r4) break; goto NEXT;
                                case StackValueTypeCompare.i8_r8: if (v1->i8 > v2->r8) break; goto NEXT;
                                case StackValueTypeCompare.u4_i4: if (v1->u4 > v2->i4) break; goto NEXT;
                                case StackValueTypeCompare.u4_i8: if (v1->u4 > v2->i8) break; goto NEXT;
                                case StackValueTypeCompare.u4_u4: if (v1->u4 > v2->u4) break; goto NEXT;
                                case StackValueTypeCompare.u4_u8: if (v1->u4 > v2->u8) break; goto NEXT;
                                case StackValueTypeCompare.u4_r4: if (v1->u4 > v2->r4) break; goto NEXT;
                                case StackValueTypeCompare.u4_r8: if (v1->u4 > v2->r8) break; goto NEXT;
                                case StackValueTypeCompare.u8_i4: if (v2->i4 < 0 || v1->u8 > v2->u4) break; goto NEXT;
                                case StackValueTypeCompare.u8_i8: if (v2->i8 < 0 || v1->u8 > v2->u8) break; goto NEXT;
                                case StackValueTypeCompare.u8_u4: if (v1->u8 > v2->u4) break; goto NEXT;
                                case StackValueTypeCompare.u8_u8: if (v1->u8 > v2->u8) break; goto NEXT;
                                case StackValueTypeCompare.u8_r4: if (v1->u8 > v2->r4) break; goto NEXT;
                                case StackValueTypeCompare.u8_r8: if (v1->u8 > v2->r8) break; goto NEXT;
                                case StackValueTypeCompare.r4_i4: if (v1->r4 > v2->i4) break; goto NEXT;
                                case StackValueTypeCompare.r4_i8: if (v1->r4 > v2->i8) break; goto NEXT;
                                case StackValueTypeCompare.r4_u4: if (v1->r4 > v2->u4) break; goto NEXT;
                                case StackValueTypeCompare.r4_u8: if (v1->r4 > v2->u8) break; goto NEXT;
                                case StackValueTypeCompare.r4_r4: if (v1->r4 > v2->r4) break; goto NEXT;
                                case StackValueTypeCompare.r4_r8: if (v1->r4 > v2->r8) break; goto NEXT;
                                case StackValueTypeCompare.r8_i4: if (v1->r8 > v2->i4) break; goto NEXT;
                                case StackValueTypeCompare.r8_i8: if (v1->r8 > v2->i8) break; goto NEXT;
                                case StackValueTypeCompare.r8_u4: if (v1->r8 > v2->u4) break; goto NEXT;
                                case StackValueTypeCompare.r8_u8: if (v1->r8 > v2->u8) break; goto NEXT;
                                case StackValueTypeCompare.r8_r4: if (v1->r8 > v2->r4) break; goto NEXT;
                                case StackValueTypeCompare.r8_r8: if (v1->r8 > v2->r8) break; goto NEXT;
                                default: throw new Exception("Invalid operation");
                            }
                            ops = (seek + *opv);
                        }
                        goto NEXT;
                    #endregion Bgt
                    #region Ble(a >= b)
                    case ILOpCode.Ble:
                    case ILOpCode.Ble_un:
                        {
                            v1 = --stack;
                            v2 = --stack;
                            switch ((StackValueTypeCompare)(v1->typeNum * COMPARE_TYPE_MAX + v2->typeNum))
                            {
                                case StackValueTypeCompare.i4_i4: if (v1->i4 <= v2->i4) break; goto NEXT;
                                case StackValueTypeCompare.i4_i8: if (v1->i4 <= v2->i8) break; goto NEXT;
                                case StackValueTypeCompare.i4_u4: if (v1->i4 <= v2->u4) break; goto NEXT;
                                case StackValueTypeCompare.i4_u8: if (v1->i4 < 0 || v1->u4 <= v2->u8) break; goto NEXT;
                                case StackValueTypeCompare.i4_r4: if (v1->i4 <= v2->r4) break; goto NEXT;
                                case StackValueTypeCompare.i4_r8: if (v1->i4 <= v2->r8) break; goto NEXT;
                                case StackValueTypeCompare.i8_i4: if (v1->i8 <= v2->i4) break; goto NEXT;
                                case StackValueTypeCompare.i8_i8: if (v1->i8 <= v2->i8) break; goto NEXT;
                                case StackValueTypeCompare.i8_u4: if (v1->i8 <= v2->u4) break; goto NEXT;
                                case StackValueTypeCompare.i8_u8: if (v1->i8 < 0 || v1->u8 <= v2->u8) break; goto NEXT;
                                case StackValueTypeCompare.i8_r4: if (v1->i8 <= v2->r4) break; goto NEXT;
                                case StackValueTypeCompare.i8_r8: if (v1->i8 <= v2->r8) break; goto NEXT;
                                case StackValueTypeCompare.u4_i4: if (v1->u4 <= v2->i4) break; goto NEXT;
                                case StackValueTypeCompare.u4_i8: if (v1->u4 <= v2->i8) break; goto NEXT;
                                case StackValueTypeCompare.u4_u4: if (v1->u4 <= v2->u4) break; goto NEXT;
                                case StackValueTypeCompare.u4_u8: if (v1->u4 <= v2->u8) break; goto NEXT;
                                case StackValueTypeCompare.u4_r4: if (v1->u4 <= v2->r4) break; goto NEXT;
                                case StackValueTypeCompare.u4_r8: if (v1->u4 <= v2->r8) break; goto NEXT;
                                case StackValueTypeCompare.u8_i4: if (v2->i4 > 0 && v1->u8 <= v2->u4) break; goto NEXT;
                                case StackValueTypeCompare.u8_i8: if (v2->i8 > 0 && v1->u8 <= v2->u8) break; goto NEXT;
                                case StackValueTypeCompare.u8_u4: if (v1->u8 <= v2->u4) break; goto NEXT;
                                case StackValueTypeCompare.u8_u8: if (v1->u8 <= v2->u8) break; goto NEXT;
                                case StackValueTypeCompare.u8_r4: if (v1->u8 <= v2->r4) break; goto NEXT;
                                case StackValueTypeCompare.u8_r8: if (v1->u8 <= v2->r8) break; goto NEXT;
                                case StackValueTypeCompare.r4_i4: if (v1->r4 <= v2->i4) break; goto NEXT;
                                case StackValueTypeCompare.r4_i8: if (v1->r4 <= v2->i8) break; goto NEXT;
                                case StackValueTypeCompare.r4_u4: if (v1->r4 <= v2->u4) break; goto NEXT;
                                case StackValueTypeCompare.r4_u8: if (v1->r4 <= v2->u8) break; goto NEXT;
                                case StackValueTypeCompare.r4_r4: if (v1->r4 <= v2->r4) break; goto NEXT;
                                case StackValueTypeCompare.r4_r8: if (v1->r4 <= v2->r8) break; goto NEXT;
                                case StackValueTypeCompare.r8_i4: if (v1->r8 <= v2->i4) break; goto NEXT;
                                case StackValueTypeCompare.r8_i8: if (v1->r8 <= v2->i8) break; goto NEXT;
                                case StackValueTypeCompare.r8_u4: if (v1->r8 <= v2->u4) break; goto NEXT;
                                case StackValueTypeCompare.r8_u8: if (v1->r8 <= v2->u8) break; goto NEXT;
                                case StackValueTypeCompare.r8_r4: if (v1->r8 <= v2->r4) break; goto NEXT;
                                case StackValueTypeCompare.r8_r8: if (v1->r8 <= v2->r8) break; goto NEXT;
                                default: throw new Exception("Invalid operation");
                            }
                            ops = (seek + *(int*)opv);
                        }
                        goto NEXT;
                    case ILOpCode.Ble_s:
                    case ILOpCode.Ble_un_s:
                        {
                            v1 = --stack;
                            v2 = --stack;
                            switch ((StackValueTypeCompare)(v1->typeNum * COMPARE_TYPE_MAX + v2->typeNum))
                            {
                                case StackValueTypeCompare.i4_i4: if (v1->i4 <= v2->i4) break; goto NEXT;
                                case StackValueTypeCompare.i4_i8: if (v1->i4 <= v2->i8) break; goto NEXT;
                                case StackValueTypeCompare.i4_u4: if (v1->i4 <= v2->u4) break; goto NEXT;
                                case StackValueTypeCompare.i4_u8: if (v1->i4 < 0 || v1->u4 <= v2->u8) break; goto NEXT;
                                case StackValueTypeCompare.i4_r4: if (v1->i4 <= v2->r4) break; goto NEXT;
                                case StackValueTypeCompare.i4_r8: if (v1->i4 <= v2->r8) break; goto NEXT;
                                case StackValueTypeCompare.i8_i4: if (v1->i8 <= v2->i4) break; goto NEXT;
                                case StackValueTypeCompare.i8_i8: if (v1->i8 <= v2->i8) break; goto NEXT;
                                case StackValueTypeCompare.i8_u4: if (v1->i8 <= v2->u4) break; goto NEXT;
                                case StackValueTypeCompare.i8_u8: if (v1->i8 < 0 || v1->u8 <= v2->u8) break; goto NEXT;
                                case StackValueTypeCompare.i8_r4: if (v1->i8 <= v2->r4) break; goto NEXT;
                                case StackValueTypeCompare.i8_r8: if (v1->i8 <= v2->r8) break; goto NEXT;
                                case StackValueTypeCompare.u4_i4: if (v1->u4 <= v2->i4) break; goto NEXT;
                                case StackValueTypeCompare.u4_i8: if (v1->u4 <= v2->i8) break; goto NEXT;
                                case StackValueTypeCompare.u4_u4: if (v1->u4 <= v2->u4) break; goto NEXT;
                                case StackValueTypeCompare.u4_u8: if (v1->u4 <= v2->u8) break; goto NEXT;
                                case StackValueTypeCompare.u4_r4: if (v1->u4 <= v2->r4) break; goto NEXT;
                                case StackValueTypeCompare.u4_r8: if (v1->u4 <= v2->r8) break; goto NEXT;
                                case StackValueTypeCompare.u8_i4: if (v2->i4 >= 0 && v1->u8 <= v2->u4) break; goto NEXT;
                                case StackValueTypeCompare.u8_i8: if (v2->i8 >= 0 && v1->u8 <= v2->u8) break; goto NEXT;
                                case StackValueTypeCompare.u8_u4: if (v1->u8 <= v2->u4) break; goto NEXT;
                                case StackValueTypeCompare.u8_u8: if (v1->u8 <= v2->u8) break; goto NEXT;
                                case StackValueTypeCompare.u8_r4: if (v1->u8 <= v2->r4) break; goto NEXT;
                                case StackValueTypeCompare.u8_r8: if (v1->u8 <= v2->r8) break; goto NEXT;
                                case StackValueTypeCompare.r4_i4: if (v1->r4 <= v2->i4) break; goto NEXT;
                                case StackValueTypeCompare.r4_i8: if (v1->r4 <= v2->i8) break; goto NEXT;
                                case StackValueTypeCompare.r4_u4: if (v1->r4 <= v2->u4) break; goto NEXT;
                                case StackValueTypeCompare.r4_u8: if (v1->r4 <= v2->u8) break; goto NEXT;
                                case StackValueTypeCompare.r4_r4: if (v1->r4 <= v2->r4) break; goto NEXT;
                                case StackValueTypeCompare.r4_r8: if (v1->r4 <= v2->r8) break; goto NEXT;
                                case StackValueTypeCompare.r8_i4: if (v1->r8 <= v2->i4) break; goto NEXT;
                                case StackValueTypeCompare.r8_i8: if (v1->r8 <= v2->i8) break; goto NEXT;
                                case StackValueTypeCompare.r8_u4: if (v1->r8 <= v2->u4) break; goto NEXT;
                                case StackValueTypeCompare.r8_u8: if (v1->r8 <= v2->u8) break; goto NEXT;
                                case StackValueTypeCompare.r8_r4: if (v1->r8 <= v2->r4) break; goto NEXT;
                                case StackValueTypeCompare.r8_r8: if (v1->r8 <= v2->r8) break; goto NEXT;
                                default: throw new Exception("Invalid operation");
                            }
                            ops = (seek + *opv);
                        }
                        goto NEXT;
                    #endregion Ble
                    #region Blt(a > b)
                    case ILOpCode.Blt:
                    case ILOpCode.Blt_un:
                        {
                            v1 = --stack;
                            v2 = --stack;
                            switch ((StackValueTypeCompare)(v1->typeNum * COMPARE_TYPE_MAX + v2->typeNum))
                            {
                                case StackValueTypeCompare.i4_i4: if (v1->i4 < v2->i4) break; goto NEXT;
                                case StackValueTypeCompare.i4_i8: if (v1->i4 < v2->i8) break; goto NEXT;
                                case StackValueTypeCompare.i4_u4: if (v1->i4 < v2->u4) break; goto NEXT;
                                case StackValueTypeCompare.i4_u8: if (v1->i4 < 0 || v1->u4 < v2->u8) break; goto NEXT;
                                case StackValueTypeCompare.i4_r4: if (v1->i4 < v2->r4) break; goto NEXT;
                                case StackValueTypeCompare.i4_r8: if (v1->i4 < v2->r8) break; goto NEXT;
                                case StackValueTypeCompare.i8_i4: if (v1->i8 < v2->i4) break; goto NEXT;
                                case StackValueTypeCompare.i8_i8: if (v1->i8 < v2->i8) break; goto NEXT;
                                case StackValueTypeCompare.i8_u4: if (v1->i8 < v2->u4) break; goto NEXT;
                                case StackValueTypeCompare.i8_u8: if (v1->i8 < 0 || v1->u8 < v2->u8) break; goto NEXT;
                                case StackValueTypeCompare.i8_r4: if (v1->i8 < v2->r4) break; goto NEXT;
                                case StackValueTypeCompare.i8_r8: if (v1->i8 < v2->r8) break; goto NEXT;
                                case StackValueTypeCompare.u4_i4: if (v1->u4 < v2->i4) break; goto NEXT;
                                case StackValueTypeCompare.u4_i8: if (v1->u4 < v2->i8) break; goto NEXT;
                                case StackValueTypeCompare.u4_u4: if (v1->u4 < v2->u4) break; goto NEXT;
                                case StackValueTypeCompare.u4_u8: if (v1->u4 < v2->u8) break; goto NEXT;
                                case StackValueTypeCompare.u4_r4: if (v1->u4 < v2->r4) break; goto NEXT;
                                case StackValueTypeCompare.u4_r8: if (v1->u4 < v2->r8) break; goto NEXT;
                                case StackValueTypeCompare.u8_i4: if (v2->i4 > 0 && v1->u8 < v2->u4) break; goto NEXT;
                                case StackValueTypeCompare.u8_i8: if (v2->i8 > 0 && v1->u8 < v2->u8) break; goto NEXT;
                                case StackValueTypeCompare.u8_u4: if (v1->u8 < v2->u4) break; goto NEXT;
                                case StackValueTypeCompare.u8_u8: if (v1->u8 < v2->u8) break; goto NEXT;
                                case StackValueTypeCompare.u8_r4: if (v1->u8 < v2->r4) break; goto NEXT;
                                case StackValueTypeCompare.u8_r8: if (v1->u8 < v2->r8) break; goto NEXT;
                                case StackValueTypeCompare.r4_i4: if (v1->r4 < v2->i4) break; goto NEXT;
                                case StackValueTypeCompare.r4_i8: if (v1->r4 < v2->i8) break; goto NEXT;
                                case StackValueTypeCompare.r4_u4: if (v1->r4 < v2->u4) break; goto NEXT;
                                case StackValueTypeCompare.r4_u8: if (v1->r4 < v2->u8) break; goto NEXT;
                                case StackValueTypeCompare.r4_r4: if (v1->r4 < v2->r4) break; goto NEXT;
                                case StackValueTypeCompare.r4_r8: if (v1->r4 < v2->r8) break; goto NEXT;
                                case StackValueTypeCompare.r8_i4: if (v1->r8 < v2->i4) break; goto NEXT;
                                case StackValueTypeCompare.r8_i8: if (v1->r8 < v2->i8) break; goto NEXT;
                                case StackValueTypeCompare.r8_u4: if (v1->r8 < v2->u4) break; goto NEXT;
                                case StackValueTypeCompare.r8_u8: if (v1->r8 < v2->u8) break; goto NEXT;
                                case StackValueTypeCompare.r8_r4: if (v1->r8 < v2->r4) break; goto NEXT;
                                case StackValueTypeCompare.r8_r8: if (v1->r8 < v2->r8) break; goto NEXT;
                                default: throw new Exception("Invalid operation");
                            }
                            ops = (seek + *(int*)opv);
                        }
                        goto NEXT;
                    case ILOpCode.Blt_s:
                    case ILOpCode.Blt_un_s:
                        {
                            v1 = --stack;
                            v2 = --stack;
                            switch ((StackValueTypeCompare)(v1->typeNum * COMPARE_TYPE_MAX + v2->typeNum))
                            {
                                case StackValueTypeCompare.i4_i4: if (v1->i4 < v2->i4) break; goto NEXT;
                                case StackValueTypeCompare.i4_i8: if (v1->i4 < v2->i8) break; goto NEXT;
                                case StackValueTypeCompare.i4_u4: if (v1->i4 < v2->u4) break; goto NEXT;
                                case StackValueTypeCompare.i4_u8: if (v1->i4 < 0 || v1->u4 < v2->u8) break; goto NEXT;
                                case StackValueTypeCompare.i4_r4: if (v1->i4 < v2->r4) break; goto NEXT;
                                case StackValueTypeCompare.i4_r8: if (v1->i4 < v2->r8) break; goto NEXT;
                                case StackValueTypeCompare.i8_i4: if (v1->i8 < v2->i4) break; goto NEXT;
                                case StackValueTypeCompare.i8_i8: if (v1->i8 < v2->i8) break; goto NEXT;
                                case StackValueTypeCompare.i8_u4: if (v1->i8 < v2->u4) break; goto NEXT;
                                case StackValueTypeCompare.i8_u8: if (v1->i8 < 0 || v1->u8 < v2->u8) break; goto NEXT;
                                case StackValueTypeCompare.i8_r4: if (v1->i8 < v2->r4) break; goto NEXT;
                                case StackValueTypeCompare.i8_r8: if (v1->i8 < v2->r8) break; goto NEXT;
                                case StackValueTypeCompare.u4_i4: if (v1->u4 < v2->i4) break; goto NEXT;
                                case StackValueTypeCompare.u4_i8: if (v1->u4 < v2->i8) break; goto NEXT;
                                case StackValueTypeCompare.u4_u4: if (v1->u4 < v2->u4) break; goto NEXT;
                                case StackValueTypeCompare.u4_u8: if (v1->u4 < v2->u8) break; goto NEXT;
                                case StackValueTypeCompare.u4_r4: if (v1->u4 < v2->r4) break; goto NEXT;
                                case StackValueTypeCompare.u4_r8: if (v1->u4 < v2->r8) break; goto NEXT;
                                case StackValueTypeCompare.u8_i4: if (v2->i4 >= 0 && v1->u8 < v2->u4) break; goto NEXT;
                                case StackValueTypeCompare.u8_i8: if (v2->i8 >= 0 && v1->u8 < v2->u8) break; goto NEXT;
                                case StackValueTypeCompare.u8_u4: if (v1->u8 < v2->u4) break; goto NEXT;
                                case StackValueTypeCompare.u8_u8: if (v1->u8 < v2->u8) break; goto NEXT;
                                case StackValueTypeCompare.u8_r4: if (v1->u8 < v2->r4) break; goto NEXT;
                                case StackValueTypeCompare.u8_r8: if (v1->u8 < v2->r8) break; goto NEXT;
                                case StackValueTypeCompare.r4_i4: if (v1->r4 < v2->i4) break; goto NEXT;
                                case StackValueTypeCompare.r4_i8: if (v1->r4 < v2->i8) break; goto NEXT;
                                case StackValueTypeCompare.r4_u4: if (v1->r4 < v2->u4) break; goto NEXT;
                                case StackValueTypeCompare.r4_u8: if (v1->r4 < v2->u8) break; goto NEXT;
                                case StackValueTypeCompare.r4_r4: if (v1->r4 < v2->r4) break; goto NEXT;
                                case StackValueTypeCompare.r4_r8: if (v1->r4 < v2->r8) break; goto NEXT;
                                case StackValueTypeCompare.r8_i4: if (v1->r8 < v2->i4) break; goto NEXT;
                                case StackValueTypeCompare.r8_i8: if (v1->r8 < v2->i8) break; goto NEXT;
                                case StackValueTypeCompare.r8_u4: if (v1->r8 < v2->u4) break; goto NEXT;
                                case StackValueTypeCompare.r8_u8: if (v1->r8 < v2->u8) break; goto NEXT;
                                case StackValueTypeCompare.r8_r4: if (v1->r8 < v2->r4) break; goto NEXT;
                                case StackValueTypeCompare.r8_r8: if (v1->r8 < v2->r8) break; goto NEXT;
                                default: throw new Exception("Invalid operation");
                            }
                            ops = (seek + *opv);
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

                    #region 사칙연산-------------------------------------------------------
                    case ILOpCode.Add:
                        {
                            v2 = (--stack);
                            v1 = (stack - 1);
                            switch ((StackValueTypeCompare)(v1->typeNum * COMPARE_TYPE_MAX + v2->typeNum))
                            {
                                case StackValueTypeCompare.i4_i4: v1->i4 = v1->i4 + v2->i4; break;
                                case StackValueTypeCompare.i4_i8: v1->i8 = v1->i4 + v2->i8; v1->type = StackValueType.i8; break;
                                case StackValueTypeCompare.i4_u4: throw new Exception("Invalid operation"); // v1->i4 = v1->i4 + v2->u4; break;
                                case StackValueTypeCompare.i4_u8: throw new Exception("Invalid operation"); // v1->i4 = v1->i4 + v2->u8; break;
                                case StackValueTypeCompare.i4_r4: v1->r4 = v1->i4 + v2->r4; v1->type = StackValueType.r4; break;
                                case StackValueTypeCompare.i4_r8: v1->r8 = v1->i4 + v2->r8; v1->type = StackValueType.r8; break;
                                case StackValueTypeCompare.i8_i4: v1->i8 = v1->i8 + v2->i4; break;
                                case StackValueTypeCompare.i8_i8: v1->i8 = v1->i8 + v2->i8; break;
                                case StackValueTypeCompare.i8_u4: v1->i8 = v1->i8 + v2->u4; break;
                                case StackValueTypeCompare.i8_u8: throw new Exception("Invalid operation"); // v1->i8 = v1->i8 + v2->u8; break;
                                case StackValueTypeCompare.i8_r4: v1->r4 = v1->i8 + v2->r4; v1->type = StackValueType.r4; break;
                                case StackValueTypeCompare.i8_r8: v1->r8 = v1->i8 + v2->r8; v1->type = StackValueType.r8; break;
                                case StackValueTypeCompare.u4_i4: throw new Exception("Invalid operation"); // v1->u4 = v1->u4 + v2->i4; break;
                                case StackValueTypeCompare.u4_i8: v1->i8 = v1->u4 + v2->i8; v1->type = StackValueType.i8; break;
                                case StackValueTypeCompare.u4_u4: v1->u4 = v1->u4 + v2->u4; break;
                                case StackValueTypeCompare.u4_u8: v1->u8 = v1->u4 + v2->u8; v1->type = StackValueType.u8; break;
                                case StackValueTypeCompare.u4_r4: v1->r4 = v1->u4 + v2->r4; v1->type = StackValueType.r4; break;
                                case StackValueTypeCompare.u4_r8: v1->r8 = v1->u4 + v2->r8; v1->type = StackValueType.r8; break;
                                case StackValueTypeCompare.u8_i4: throw new Exception("Invalid operation"); // v1->u8 = v1->u8 + v2->i4; break;
                                case StackValueTypeCompare.u8_i8: throw new Exception("Invalid operation"); // v1->u8 = v1->u8 + v2->i8; break;
                                case StackValueTypeCompare.u8_u4: v1->u8 = v1->u8 + v2->u4; break;
                                case StackValueTypeCompare.u8_u8: v1->u8 = v1->u8 + v2->u8; break;
                                case StackValueTypeCompare.u8_r4: v1->r4 = v1->u8 + v2->r4; v1->type = StackValueType.r4; break;
                                case StackValueTypeCompare.u8_r8: v1->r8 = v1->u8 + v2->r8; v1->type = StackValueType.r8; break;
                                case StackValueTypeCompare.r4_i4: v1->r4 = v1->r4 + v2->i4; break;
                                case StackValueTypeCompare.r4_i8: v1->r4 = v1->r4 + v2->i8; break;
                                case StackValueTypeCompare.r4_u4: v1->r4 = v1->r4 + v2->u4; break;
                                case StackValueTypeCompare.r4_u8: v1->r4 = v1->r4 + v2->u8; break;
                                case StackValueTypeCompare.r4_r4: v1->r4 = v1->r4 + v2->r4; break;
                                case StackValueTypeCompare.r4_r8: v1->r8 = v1->r4 + v2->r8; v1->type = StackValueType.r8; break;
                                case StackValueTypeCompare.r8_i4: v1->r8 = v1->r8 + v2->i4; break;
                                case StackValueTypeCompare.r8_i8: v1->r8 = v1->r8 + v2->i8; break;
                                case StackValueTypeCompare.r8_u4: v1->r8 = v1->r8 + v2->u4; break;
                                case StackValueTypeCompare.r8_u8: v1->r8 = v1->r8 + v2->u8; break;
                                case StackValueTypeCompare.r8_r4: v1->r8 = v1->r8 + v2->r4; break;
                                case StackValueTypeCompare.r8_r8: v1->r8 = v1->r8 + v2->r8; break;
                                default: throw new Exception("Invalid operation");
                            }
                        }
                        goto NEXT;
                    case ILOpCode.Sub:
                        {
                            v2 = (--stack);
                            v1 = (stack - 1);
                            switch ((StackValueTypeCompare)(v1->typeNum * COMPARE_TYPE_MAX + v2->typeNum))
                            {
                                case StackValueTypeCompare.i4_i4: v1->i4 = v1->i4 - v2->i4; break;
                                case StackValueTypeCompare.i4_i8: v1->i8 = v1->i4 - v2->i8; v1->type = StackValueType.i8; break;
                                case StackValueTypeCompare.i4_u4: throw new Exception("Invalid operation"); // v1->i4 = v1->i4 - v2->u4; break;
                                case StackValueTypeCompare.i4_u8: throw new Exception("Invalid operation"); // v1->i4 = v1->i4 - v2->u8; break;
                                case StackValueTypeCompare.i4_r4: v1->r4 = v1->i4 - v2->r4; v1->type = StackValueType.r4; break;
                                case StackValueTypeCompare.i4_r8: v1->r8 = v1->i4 - v2->r8; v1->type = StackValueType.r8; break;
                                case StackValueTypeCompare.i8_i4: v1->i8 = v1->i8 - v2->i4; break;
                                case StackValueTypeCompare.i8_i8: v1->i8 = v1->i8 - v2->i8; break;
                                case StackValueTypeCompare.i8_u4: v1->i8 = v1->i8 - v2->u4; break;
                                case StackValueTypeCompare.i8_u8: throw new Exception("Invalid operation"); // v1->i8 = v1->i8 - v2->u8; break;
                                case StackValueTypeCompare.i8_r4: v1->r4 = v1->i8 - v2->r4; v1->type = StackValueType.r4; break;
                                case StackValueTypeCompare.i8_r8: v1->r8 = v1->i8 - v2->r8; v1->type = StackValueType.r8; break;
                                case StackValueTypeCompare.u4_i4: throw new Exception("Invalid operation"); // v1->u4 = v1->u4 - v2->i4; break;
                                case StackValueTypeCompare.u4_i8: v1->i8 = v1->u4 - v2->i8; v1->type = StackValueType.i8; break;
                                case StackValueTypeCompare.u4_u4: v1->u4 = v1->u4 - v2->u4; break;
                                case StackValueTypeCompare.u4_u8: v1->u8 = v1->u4 - v2->u8; v1->type = StackValueType.u8; break;
                                case StackValueTypeCompare.u4_r4: v1->r4 = v1->u4 - v2->r4; v1->type = StackValueType.r4; break;
                                case StackValueTypeCompare.u4_r8: v1->r8 = v1->u4 - v2->r8; v1->type = StackValueType.r8; break;
                                case StackValueTypeCompare.u8_i4: throw new Exception("Invalid operation"); // v1->u8 = v1->u8 - v2->i4; break;
                                case StackValueTypeCompare.u8_i8: throw new Exception("Invalid operation"); // v1->u8 = v1->u8 - v2->i8; break;
                                case StackValueTypeCompare.u8_u4: v1->u8 = v1->u8 - v2->u4; break;
                                case StackValueTypeCompare.u8_u8: v1->u8 = v1->u8 - v2->u8; break;
                                case StackValueTypeCompare.u8_r4: v1->r4 = v1->u8 - v2->r4; v1->type = StackValueType.r4; break;
                                case StackValueTypeCompare.u8_r8: v1->r8 = v1->u8 - v2->r8; v1->type = StackValueType.r8; break;
                                case StackValueTypeCompare.r4_i4: v1->r4 = v1->r4 - v2->i4; break;
                                case StackValueTypeCompare.r4_i8: v1->r4 = v1->r4 - v2->i8; break;
                                case StackValueTypeCompare.r4_u4: v1->r4 = v1->r4 - v2->u4; break;
                                case StackValueTypeCompare.r4_u8: v1->r4 = v1->r4 - v2->u8; break;
                                case StackValueTypeCompare.r4_r4: v1->r4 = v1->r4 - v2->r4; break;
                                case StackValueTypeCompare.r4_r8: v1->r8 = v1->r4 - v2->r8; v1->type = StackValueType.r8; break;
                                case StackValueTypeCompare.r8_i4: v1->r8 = v1->r8 - v2->i4; break;
                                case StackValueTypeCompare.r8_i8: v1->r8 = v1->r8 - v2->i8; break;
                                case StackValueTypeCompare.r8_u4: v1->r8 = v1->r8 - v2->u4; break;
                                case StackValueTypeCompare.r8_u8: v1->r8 = v1->r8 - v2->u8; break;
                                case StackValueTypeCompare.r8_r4: v1->r8 = v1->r8 - v2->r4; break;
                                case StackValueTypeCompare.r8_r8: v1->r8 = v1->r8 - v2->r8; break;
                                default: throw new Exception("Invalid operation");
                            }
                        }
                        goto NEXT;
                    case ILOpCode.Mul:
                        {
                            v2 = (--stack);
                            v1 = (stack - 1);
                            switch ((StackValueTypeCompare)(v1->typeNum * COMPARE_TYPE_MAX + v2->typeNum))
                            {
                                case StackValueTypeCompare.i4_i4: v1->i4 = v1->i4 * v2->i4; break;
                                case StackValueTypeCompare.i4_i8: v1->i8 = v1->i4 * v2->i8; v1->type = StackValueType.i8; break;
                                case StackValueTypeCompare.i4_u4: throw new Exception("Invalid operation"); // v1->i4 = v1->i4 * v2->u4; break;
                                case StackValueTypeCompare.i4_u8: throw new Exception("Invalid operation"); // v1->i4 = v1->i4 * v2->u8; break;
                                case StackValueTypeCompare.i4_r4: v1->r4 = v1->i4 * v2->r4; v1->type = StackValueType.r4; break;
                                case StackValueTypeCompare.i4_r8: v1->r8 = v1->i4 * v2->r8; v1->type = StackValueType.r8; break;
                                case StackValueTypeCompare.i8_i4: v1->i8 = v1->i8 * v2->i4; break;
                                case StackValueTypeCompare.i8_i8: v1->i8 = v1->i8 * v2->i8; break;
                                case StackValueTypeCompare.i8_u4: v1->i8 = v1->i8 * v2->u4; break;
                                case StackValueTypeCompare.i8_u8: throw new Exception("Invalid operation"); // v1->i8 = v1->i8 * v2->u8; break;
                                case StackValueTypeCompare.i8_r4: v1->r4 = v1->i8 * v2->r4; v1->type = StackValueType.r4; break;
                                case StackValueTypeCompare.i8_r8: v1->r8 = v1->i8 * v2->r8; v1->type = StackValueType.r8; break;
                                case StackValueTypeCompare.u4_i4: throw new Exception("Invalid operation"); // v1->u4 = v1->u4 * v2->i4; break;
                                case StackValueTypeCompare.u4_i8: v1->i8 = v1->u4 * v2->i8; v1->type = StackValueType.i8; break;
                                case StackValueTypeCompare.u4_u4: v1->u4 = v1->u4 * v2->u4; break;
                                case StackValueTypeCompare.u4_u8: v1->u8 = v1->u4 * v2->u8; v1->type = StackValueType.u8; break;
                                case StackValueTypeCompare.u4_r4: v1->r4 = v1->u4 * v2->r4; v1->type = StackValueType.r4; break;
                                case StackValueTypeCompare.u4_r8: v1->r8 = v1->u4 * v2->r8; v1->type = StackValueType.r8; break;
                                case StackValueTypeCompare.u8_i4: throw new Exception("Invalid operation"); // v1->u8 = v1->u8 * v2->i4; break;
                                case StackValueTypeCompare.u8_i8: throw new Exception("Invalid operation"); // v1->u8 = v1->u8 * v2->i8; break;
                                case StackValueTypeCompare.u8_u4: v1->u8 = v1->u8 * v2->u4; break;
                                case StackValueTypeCompare.u8_u8: v1->u8 = v1->u8 * v2->u8; break;
                                case StackValueTypeCompare.u8_r4: v1->r4 = v1->u8 * v2->r4; v1->type = StackValueType.r4; break;
                                case StackValueTypeCompare.u8_r8: v1->r8 = v1->u8 * v2->r8; v1->type = StackValueType.r8; break;
                                case StackValueTypeCompare.r4_i4: v1->r4 = v1->r4 * v2->i4; break;
                                case StackValueTypeCompare.r4_i8: v1->r4 = v1->r4 * v2->i8; break;
                                case StackValueTypeCompare.r4_u4: v1->r4 = v1->r4 * v2->u4; break;
                                case StackValueTypeCompare.r4_u8: v1->r4 = v1->r4 * v2->u8; break;
                                case StackValueTypeCompare.r4_r4: v1->r4 = v1->r4 * v2->r4; break;
                                case StackValueTypeCompare.r4_r8: v1->r8 = v1->r4 * v2->r8; v1->type = StackValueType.r8; break;
                                case StackValueTypeCompare.r8_i4: v1->r8 = v1->r8 * v2->i4; break;
                                case StackValueTypeCompare.r8_i8: v1->r8 = v1->r8 * v2->i8; break;
                                case StackValueTypeCompare.r8_u4: v1->r8 = v1->r8 * v2->u4; break;
                                case StackValueTypeCompare.r8_u8: v1->r8 = v1->r8 * v2->u8; break;
                                case StackValueTypeCompare.r8_r4: v1->r8 = v1->r8 * v2->r4; break;
                                case StackValueTypeCompare.r8_r8: v1->r8 = v1->r8 * v2->r8; break;
                                default: throw new Exception("Invalid operation");
                            }
                        }
                        goto NEXT;
                    case ILOpCode.Div:
                    case ILOpCode.Div_un:
                        {
                            v2 = (--stack);
                            v1 = (stack - 1);
                            switch ((StackValueTypeCompare)(v1->typeNum * COMPARE_TYPE_MAX + v2->typeNum))
                            {
                                case StackValueTypeCompare.i4_i4: v1->i4 = v1->i4 / v2->i4; break;
                                case StackValueTypeCompare.i4_i8: v1->i8 = v1->i4 / v2->i8; v1->type = StackValueType.i8; break;
                                case StackValueTypeCompare.i4_u4: throw new Exception("Invalid operation"); // v1->i4 = v1->i4 / v2->u4; break;
                                case StackValueTypeCompare.i4_u8: throw new Exception("Invalid operation"); // v1->i4 = v1->i4 / v2->u8; break;
                                case StackValueTypeCompare.i4_r4: v1->r4 = v1->i4 / v2->r4; v1->type = StackValueType.r4; break;
                                case StackValueTypeCompare.i4_r8: v1->r8 = v1->i4 / v2->r8; v1->type = StackValueType.r8; break;
                                case StackValueTypeCompare.i8_i4: v1->i8 = v1->i8 / v2->i4; break;
                                case StackValueTypeCompare.i8_i8: v1->i8 = v1->i8 / v2->i8; break;
                                case StackValueTypeCompare.i8_u4: v1->i8 = v1->i8 / v2->u4; break;
                                case StackValueTypeCompare.i8_u8: throw new Exception("Invalid operation"); // v1->i8 = v1->i8 / v2->u8; break;
                                case StackValueTypeCompare.i8_r4: v1->r4 = v1->i8 / v2->r4; v1->type = StackValueType.r4; break;
                                case StackValueTypeCompare.i8_r8: v1->r8 = v1->i8 / v2->r8; v1->type = StackValueType.r8; break;
                                case StackValueTypeCompare.u4_i4: throw new Exception("Invalid operation"); // v1->u4 = v1->u4 / v2->i4; break;
                                case StackValueTypeCompare.u4_i8: v1->i8 = v1->u4 / v2->i8; v1->type = StackValueType.i8; break;
                                case StackValueTypeCompare.u4_u4: v1->u4 = v1->u4 / v2->u4; break;
                                case StackValueTypeCompare.u4_u8: v1->u8 = v1->u4 / v2->u8; v1->type = StackValueType.u8; break;
                                case StackValueTypeCompare.u4_r4: v1->r4 = v1->u4 / v2->r4; v1->type = StackValueType.r4; break;
                                case StackValueTypeCompare.u4_r8: v1->r8 = v1->u4 / v2->r8; v1->type = StackValueType.r8; break;
                                case StackValueTypeCompare.u8_i4: throw new Exception("Invalid operation"); // v1->u8 = v1->u8 / v2->i4; break;
                                case StackValueTypeCompare.u8_i8: throw new Exception("Invalid operation"); // v1->u8 = v1->u8 / v2->i8; break;
                                case StackValueTypeCompare.u8_u4: v1->u8 = v1->u8 / v2->u4; break;
                                case StackValueTypeCompare.u8_u8: v1->u8 = v1->u8 / v2->u8; break;
                                case StackValueTypeCompare.u8_r4: v1->r4 = v1->u8 / v2->r4; v1->type = StackValueType.r4; break;
                                case StackValueTypeCompare.u8_r8: v1->r8 = v1->u8 / v2->r8; v1->type = StackValueType.r8; break;
                                case StackValueTypeCompare.r4_i4: v1->r4 = v1->r4 / v2->i4; break;
                                case StackValueTypeCompare.r4_i8: v1->r4 = v1->r4 / v2->i8; break;
                                case StackValueTypeCompare.r4_u4: v1->r4 = v1->r4 / v2->u4; break;
                                case StackValueTypeCompare.r4_u8: v1->r4 = v1->r4 / v2->u8; break;
                                case StackValueTypeCompare.r4_r4: v1->r4 = v1->r4 / v2->r4; break;
                                case StackValueTypeCompare.r4_r8: v1->r8 = v1->r4 / v2->r8; v1->type = StackValueType.r8; break;
                                case StackValueTypeCompare.r8_i4: v1->r8 = v1->r8 / v2->i4; break;
                                case StackValueTypeCompare.r8_i8: v1->r8 = v1->r8 / v2->i8; break;
                                case StackValueTypeCompare.r8_u4: v1->r8 = v1->r8 / v2->u4; break;
                                case StackValueTypeCompare.r8_u8: v1->r8 = v1->r8 / v2->u8; break;
                                case StackValueTypeCompare.r8_r4: v1->r8 = v1->r8 / v2->r4; break;
                                case StackValueTypeCompare.r8_r8: v1->r8 = v1->r8 / v2->r8; break;
                                default: throw new Exception("Invalid operation");
                            }
                        }
                        goto NEXT;
                    case ILOpCode.Rem:
                    case ILOpCode.Rem_un:
                        {
                            v2 = (--stack);
                            v1 = (stack - 1);
                            switch ((StackValueTypeCompare)(v1->typeNum * COMPARE_TYPE_MAX + v2->typeNum))
                            {
                                case StackValueTypeCompare.i4_i4: v1->i4 = v1->i4 % v2->i4; break;
                                case StackValueTypeCompare.i4_i8: v1->i8 = v1->i4 % v2->i8; v1->type = StackValueType.i8; break;
                                case StackValueTypeCompare.i4_u4: throw new Exception("Invalid operation"); // v1->i4 = v1->i4 % v2->u4; break;
                                case StackValueTypeCompare.i4_u8: throw new Exception("Invalid operation"); // v1->i4 = v1->i4 % v2->u8; break;
                                case StackValueTypeCompare.i4_r4: v1->r4 = v1->i4 % v2->r4; v1->type = StackValueType.r4; break;
                                case StackValueTypeCompare.i4_r8: v1->r8 = v1->i4 % v2->r8; v1->type = StackValueType.r8; break;
                                case StackValueTypeCompare.i8_i4: v1->i8 = v1->i8 % v2->i4; break;
                                case StackValueTypeCompare.i8_i8: v1->i8 = v1->i8 % v2->i8; break;
                                case StackValueTypeCompare.i8_u4: v1->i8 = v1->i8 % v2->u4; break;
                                case StackValueTypeCompare.i8_u8: throw new Exception("Invalid operation"); // v1->i8 = v1->i8 % v2->u8; break;
                                case StackValueTypeCompare.i8_r4: v1->r4 = v1->i8 % v2->r4; v1->type = StackValueType.r4; break;
                                case StackValueTypeCompare.i8_r8: v1->r8 = v1->i8 % v2->r8; v1->type = StackValueType.r8; break;
                                case StackValueTypeCompare.u4_i4: throw new Exception("Invalid operation"); // v1->u4 = v1->u4 % v2->i4; break;
                                case StackValueTypeCompare.u4_i8: v1->i8 = v1->u4 % v2->i8; v1->type = StackValueType.i8; break;
                                case StackValueTypeCompare.u4_u4: v1->u4 = v1->u4 % v2->u4; break;
                                case StackValueTypeCompare.u4_u8: v1->u8 = v1->u4 % v2->u8; v1->type = StackValueType.u8; break;
                                case StackValueTypeCompare.u4_r4: v1->r4 = v1->u4 % v2->r4; v1->type = StackValueType.r4; break;
                                case StackValueTypeCompare.u4_r8: v1->r8 = v1->u4 % v2->r8; v1->type = StackValueType.r8; break;
                                case StackValueTypeCompare.u8_i4: throw new Exception("Invalid operation"); // v1->u8 = v1->u8 % v2->i4; break;
                                case StackValueTypeCompare.u8_i8: throw new Exception("Invalid operation"); // v1->u8 = v1->u8 % v2->i8; break;
                                case StackValueTypeCompare.u8_u4: v1->u8 = v1->u8 % v2->u4; break;
                                case StackValueTypeCompare.u8_u8: v1->u8 = v1->u8 % v2->u8; break;
                                case StackValueTypeCompare.u8_r4: v1->r4 = v1->u8 % v2->r4; v1->type = StackValueType.r4; break;
                                case StackValueTypeCompare.u8_r8: v1->r8 = v1->u8 % v2->r8; v1->type = StackValueType.r8; break;
                                case StackValueTypeCompare.r4_i4: v1->r4 = v1->r4 % v2->i4; break;
                                case StackValueTypeCompare.r4_i8: v1->r4 = v1->r4 % v2->i8; break;
                                case StackValueTypeCompare.r4_u4: v1->r4 = v1->r4 % v2->u4; break;
                                case StackValueTypeCompare.r4_u8: v1->r4 = v1->r4 % v2->u8; break;
                                case StackValueTypeCompare.r4_r4: v1->r4 = v1->r4 % v2->r4; break;
                                case StackValueTypeCompare.r4_r8: v1->r8 = v1->r4 % v2->r8; v1->type = StackValueType.r8; break;
                                case StackValueTypeCompare.r8_i4: v1->r8 = v1->r8 % v2->i4; break;
                                case StackValueTypeCompare.r8_i8: v1->r8 = v1->r8 % v2->i8; break;
                                case StackValueTypeCompare.r8_u4: v1->r8 = v1->r8 % v2->u4; break;
                                case StackValueTypeCompare.r8_u8: v1->r8 = v1->r8 % v2->u8; break;
                                case StackValueTypeCompare.r8_r4: v1->r8 = v1->r8 % v2->r4; break;
                                case StackValueTypeCompare.r8_r8: v1->r8 = v1->r8 % v2->r8; break;
                                default: throw new Exception("Invalid operation");
                            }
                        }
                        goto NEXT;
                    #endregion 사칙연산-------------------------------------------------------
                    #region 비트연산-------------------------------------------------------
                    case ILOpCode.And:
                        {
                            v2 = (--stack);
                            v1 = (stack - 1);
                            switch ((StackValueTypeCompare)(v1->typeNum * COMPARE_TYPE_MAX + v2->typeNum))
                            {
                                case StackValueTypeCompare.i4_i4: v1->i4 = v1->i4 & v2->i4; break;
                                case StackValueTypeCompare.i4_i8: v1->i8 = v1->i4 & v2->i8; v1->type = StackValueType.i8; break;
                                case StackValueTypeCompare.i4_u4: v1->i8 = v1->i4 & v2->u4; v1->type = StackValueType.i8; break;
                                case StackValueTypeCompare.i4_u8: throw new Exception("Invalid operation"); // v1->i4 = v1->i4 & v2->u8; break;
                                case StackValueTypeCompare.i4_r4: throw new Exception("Invalid operation"); // v1->r4 = v1->i4 & v2->r4; v1->type = StackValueType.r4; break;
                                case StackValueTypeCompare.i4_r8: throw new Exception("Invalid operation"); // v1->r8 = v1->i4 & v2->r8; v1->type = StackValueType.r8; break;
                                case StackValueTypeCompare.i8_i4: v1->i8 = v1->i8 & v2->i4; break;
                                case StackValueTypeCompare.i8_i8: v1->i8 = v1->i8 & v2->i8; break;
                                case StackValueTypeCompare.i8_u4: v1->i8 = v1->i8 & v2->u4; break;
                                case StackValueTypeCompare.i8_u8: throw new Exception("Invalid operation"); // v1->i8 = v1->i8 & v2->u8; break;
                                case StackValueTypeCompare.i8_r4: throw new Exception("Invalid operation"); // v1->r4 = v1->i8 & v2->r4; v1->type = StackValueType.r4; break;
                                case StackValueTypeCompare.i8_r8: throw new Exception("Invalid operation"); // v1->r8 = v1->i8 & v2->r8; v1->type = StackValueType.r8; break;
                                case StackValueTypeCompare.u4_i4: v1->i8 = v1->u4 & v2->i4; v1->type = StackValueType.i8; break;
                                case StackValueTypeCompare.u4_i8: v1->i8 = v1->u4 & v2->i8; v1->type = StackValueType.i8; break;
                                case StackValueTypeCompare.u4_u4: v1->u4 = v1->u4 & v2->u4; break;
                                case StackValueTypeCompare.u4_u8: v1->u8 = v1->u4 & v2->u8; v1->type = StackValueType.u8; break;
                                case StackValueTypeCompare.u4_r4: throw new Exception("Invalid operation"); // v1->r4 = v1->u4 & v2->r4; v1->type = StackValueType.r4; break;
                                case StackValueTypeCompare.u4_r8: throw new Exception("Invalid operation"); // v1->r8 = v1->u4 & v2->r8; v1->type = StackValueType.r8; break;
                                case StackValueTypeCompare.u8_i4: throw new Exception("Invalid operation"); // v1->u8 = v1->u8 & v2->i4; break;
                                case StackValueTypeCompare.u8_i8: throw new Exception("Invalid operation"); // v1->u8 = v1->u8 & v2->i8; break;
                                case StackValueTypeCompare.u8_u4: v1->u8 = v1->u8 & v2->u4; break;
                                case StackValueTypeCompare.u8_u8: v1->u8 = v1->u8 & v2->u8; break;
                                case StackValueTypeCompare.u8_r4: throw new Exception("Invalid operation"); //  v1->r4 = v1->u8 & v2->r4; v1->type = StackValueType.r4; break;
                                case StackValueTypeCompare.u8_r8: throw new Exception("Invalid operation"); //  v1->r8 = v1->u8 & v2->r8; v1->type = StackValueType.r8; break;
                                case StackValueTypeCompare.r4_i4: throw new Exception("Invalid operation"); // v1->r4 = v1->r4 & v2->i4; break;
                                case StackValueTypeCompare.r4_i8: throw new Exception("Invalid operation"); // v1->r4 = v1->r4 & v2->i8; break;
                                case StackValueTypeCompare.r4_u4: throw new Exception("Invalid operation"); // v1->r4 = v1->r4 & v2->u4; break;
                                case StackValueTypeCompare.r4_u8: throw new Exception("Invalid operation"); // v1->r4 = v1->r4 & v2->u8; break;
                                case StackValueTypeCompare.r4_r4: throw new Exception("Invalid operation"); //  v1->r4 = v1->r4 & v2->r4; break;
                                case StackValueTypeCompare.r4_r8: throw new Exception("Invalid operation"); //  v1->r8 = v1->r4 & v2->r8; v1->type = StackValueType.r8; break;
                                case StackValueTypeCompare.r8_i4: throw new Exception("Invalid operation"); // v1->r8 = v1->r8 & v2->i4; break;
                                case StackValueTypeCompare.r8_i8: throw new Exception("Invalid operation"); // v1->r8 = v1->r8 & v2->i8; break;
                                case StackValueTypeCompare.r8_u4: throw new Exception("Invalid operation"); // v1->r8 = v1->r8 & v2->u4; break;
                                case StackValueTypeCompare.r8_u8: throw new Exception("Invalid operation"); // v1->r8 = v1->r8 & v2->u8; break;
                                case StackValueTypeCompare.r8_r4: throw new Exception("Invalid operation"); //  v1->r8 = v1->r8 & v2->r4; break;
                                case StackValueTypeCompare.r8_r8: throw new Exception("Invalid operation"); //  v1->r8 = v1->r8 & v2->r8; break;
                                case StackValueTypeCompare.b_b: v1->b = v1->b & v2->b; break;
                                default: throw new Exception("Invalid operation");
                            }
                        }
                        goto NEXT;
                    case ILOpCode.Or:
                        {
                            v2 = (--stack);
                            v1 = (stack - 1);
                            switch ((StackValueTypeCompare)(v1->typeNum * COMPARE_TYPE_MAX + v2->typeNum))
                            {
                                case StackValueTypeCompare.i4_i4: v1->i4 = v1->i4 | v2->i4; break;
                                case StackValueTypeCompare.i4_i8: v1->i8 = (long)v1->i4 | v2->i8; v1->type = StackValueType.i8; break;
                                case StackValueTypeCompare.i4_u4: v1->i8 = (long)v1->i4 | (long)v2->u4; v1->type = StackValueType.i8; break;
                                case StackValueTypeCompare.i4_u8: throw new Exception("Invalid operation"); // v1->i4 = v1->i4 | v2->u8; break;
                                case StackValueTypeCompare.i4_r4: throw new Exception("Invalid operation"); // v1->r4 = v1->i4 | v2->r4; v1->type = StackValueType.r4; break;
                                case StackValueTypeCompare.i4_r8: throw new Exception("Invalid operation"); // v1->r8 = v1->i4 | v2->r8; v1->type = StackValueType.r8; break;
                                case StackValueTypeCompare.i8_i4: v1->i8 = v1->i8 | (long)v2->i4; break;
                                case StackValueTypeCompare.i8_i8: v1->i8 = v1->i8 | v2->i8; break;
                                case StackValueTypeCompare.i8_u4: v1->i8 = v1->i8 | v2->u4; break;
                                case StackValueTypeCompare.i8_u8: throw new Exception("Invalid operation"); // v1->i8 = v1->i8 | v2->u8; break;
                                case StackValueTypeCompare.i8_r4: throw new Exception("Invalid operation"); // v1->r4 = v1->i8 | v2->r4; v1->type = StackValueType.r4; break;
                                case StackValueTypeCompare.i8_r8: throw new Exception("Invalid operation"); // v1->r8 = v1->i8 | v2->r8; v1->type = StackValueType.r8; break;
                                case StackValueTypeCompare.u4_i4: v1->i8 = v1->u4 | (long)v2->i4; v1->type = StackValueType.i8; break;
                                case StackValueTypeCompare.u4_i8: v1->i8 = v1->u4 | v2->i8; v1->type = StackValueType.i8; break;
                                case StackValueTypeCompare.u4_u4: v1->u4 = v1->u4 | v2->u4; break;
                                case StackValueTypeCompare.u4_u8: v1->u8 = v1->u4 | v2->u8; v1->type = StackValueType.u8; break;
                                case StackValueTypeCompare.u4_r4: throw new Exception("Invalid operation"); // v1->r4 = v1->u4 | v2->r4; v1->type = StackValueType.r4; break;
                                case StackValueTypeCompare.u4_r8: throw new Exception("Invalid operation"); // v1->r8 = v1->u4 | v2->r8; v1->type = StackValueType.r8; break;
                                case StackValueTypeCompare.u8_i4: throw new Exception("Invalid operation"); // v1->u8 = v1->u8 | v2->i4; break;
                                case StackValueTypeCompare.u8_i8: throw new Exception("Invalid operation"); // v1->u8 = v1->u8 | v2->i8; break;
                                case StackValueTypeCompare.u8_u4: v1->u8 = v1->u8 | v2->u4; break;
                                case StackValueTypeCompare.u8_u8: v1->u8 = v1->u8 | v2->u8; break;
                                case StackValueTypeCompare.u8_r4: throw new Exception("Invalid operation"); //  v1->r4 = v1->u8 | v2->r4; v1->type = StackValueType.r4; break;
                                case StackValueTypeCompare.u8_r8: throw new Exception("Invalid operation"); //  v1->r8 = v1->u8 | v2->r8; v1->type = StackValueType.r8; break;
                                case StackValueTypeCompare.r4_i4: throw new Exception("Invalid operation"); // v1->r4 = v1->r4 | v2->i4; break;
                                case StackValueTypeCompare.r4_i8: throw new Exception("Invalid operation"); // v1->r4 = v1->r4 | v2->i8; break;
                                case StackValueTypeCompare.r4_u4: throw new Exception("Invalid operation"); // v1->r4 = v1->r4 | v2->u4; break;
                                case StackValueTypeCompare.r4_u8: throw new Exception("Invalid operation"); // v1->r4 = v1->r4 | v2->u8; break;
                                case StackValueTypeCompare.r4_r4: throw new Exception("Invalid operation"); //  v1->r4 = v1->r4 | v2->r4; break;
                                case StackValueTypeCompare.r4_r8: throw new Exception("Invalid operation"); //  v1->r8 = v1->r4 | v2->r8; v1->type = StackValueType.r8; break;
                                case StackValueTypeCompare.r8_i4: throw new Exception("Invalid operation"); // v1->r8 = v1->r8 | v2->i4; break;
                                case StackValueTypeCompare.r8_i8: throw new Exception("Invalid operation"); // v1->r8 = v1->r8 | v2->i8; break;
                                case StackValueTypeCompare.r8_u4: throw new Exception("Invalid operation"); // v1->r8 = v1->r8 | v2->u4; break;
                                case StackValueTypeCompare.r8_u8: throw new Exception("Invalid operation"); // v1->r8 = v1->r8 | v2->u8; break;
                                case StackValueTypeCompare.r8_r4: throw new Exception("Invalid operation"); //  v1->r8 = v1->r8 | v2->r4; break;
                                case StackValueTypeCompare.r8_r8: throw new Exception("Invalid operation"); //  v1->r8 = v1->r8 | v2->r8; break;
                                case StackValueTypeCompare.b_b: v1->b = v1->b | v2->b; break;
                                default: throw new Exception("Invalid operation");
                            }
                        }
                        goto NEXT;
                    case ILOpCode.Xor:
                        {
                            v2 = (--stack);
                            v1 = (stack - 1);
                            switch ((StackValueTypeCompare)(v1->typeNum * COMPARE_TYPE_MAX + v2->typeNum))
                            {
                                case StackValueTypeCompare.i4_i4: v1->i4 = v1->i4 ^ v2->i4; break;
                                case StackValueTypeCompare.i4_i8: v1->i8 = v1->i4 ^ v2->i8; v1->type = StackValueType.i8; break;
                                case StackValueTypeCompare.i4_u4: v1->i8 = v1->i4 ^ v2->u4; v1->type = StackValueType.i8; break;
                                case StackValueTypeCompare.i4_u8: throw new Exception("Invalid operation"); // v1->i4 = v1->i4 ^ v2->u8; break;
                                case StackValueTypeCompare.i4_r4: throw new Exception("Invalid operation"); // v1->r4 = v1->i4 ^ v2->r4; v1->type = StackValueType.r4; break;
                                case StackValueTypeCompare.i4_r8: throw new Exception("Invalid operation"); // v1->r8 = v1->i4 ^ v2->r8; v1->type = StackValueType.r8; break;
                                case StackValueTypeCompare.i8_i4: v1->i8 = v1->i8 ^ v2->i4; break;
                                case StackValueTypeCompare.i8_i8: v1->i8 = v1->i8 ^ v2->i8; break;
                                case StackValueTypeCompare.i8_u4: v1->i8 = v1->i8 ^ v2->u4; break;
                                case StackValueTypeCompare.i8_u8: throw new Exception("Invalid operation"); // v1->i8 = v1->i8 ^ v2->u8; break;
                                case StackValueTypeCompare.i8_r4: throw new Exception("Invalid operation"); // v1->r4 = v1->i8 ^ v2->r4; v1->type = StackValueType.r4; break;
                                case StackValueTypeCompare.i8_r8: throw new Exception("Invalid operation"); // v1->r8 = v1->i8 ^ v2->r8; v1->type = StackValueType.r8; break;
                                case StackValueTypeCompare.u4_i4: v1->i8 = v1->u4 ^ v2->i4; v1->type = StackValueType.i8; break;
                                case StackValueTypeCompare.u4_i8: v1->i8 = v1->u4 ^ v2->i8; v1->type = StackValueType.i8; break;
                                case StackValueTypeCompare.u4_u4: v1->u4 = v1->u4 ^ v2->u4; break;
                                case StackValueTypeCompare.u4_u8: v1->u8 = v1->u4 ^ v2->u8; v1->type = StackValueType.u8; break;
                                case StackValueTypeCompare.u4_r4: throw new Exception("Invalid operation"); // v1->r4 = v1->u4 ^ v2->r4; v1->type = StackValueType.r4; break;
                                case StackValueTypeCompare.u4_r8: throw new Exception("Invalid operation"); // v1->r8 = v1->u4 ^ v2->r8; v1->type = StackValueType.r8; break;
                                case StackValueTypeCompare.u8_i4: throw new Exception("Invalid operation"); // v1->u8 = v1->u8 ^ v2->i4; break;
                                case StackValueTypeCompare.u8_i8: throw new Exception("Invalid operation"); // v1->u8 = v1->u8 ^ v2->i8; break;
                                case StackValueTypeCompare.u8_u4: v1->u8 = v1->u8 ^ v2->u4; break;
                                case StackValueTypeCompare.u8_u8: v1->u8 = v1->u8 ^ v2->u8; break;
                                case StackValueTypeCompare.u8_r4: throw new Exception("Invalid operation"); //  v1->r4 = v1->u8 ^ v2->r4; v1->type = StackValueType.r4; break;
                                case StackValueTypeCompare.u8_r8: throw new Exception("Invalid operation"); //  v1->r8 = v1->u8 ^ v2->r8; v1->type = StackValueType.r8; break;
                                case StackValueTypeCompare.r4_i4: throw new Exception("Invalid operation"); // v1->r4 = v1->r4 ^ v2->i4; break;
                                case StackValueTypeCompare.r4_i8: throw new Exception("Invalid operation"); // v1->r4 = v1->r4 ^ v2->i8; break;
                                case StackValueTypeCompare.r4_u4: throw new Exception("Invalid operation"); // v1->r4 = v1->r4 ^ v2->u4; break;
                                case StackValueTypeCompare.r4_u8: throw new Exception("Invalid operation"); // v1->r4 = v1->r4 ^ v2->u8; break;
                                case StackValueTypeCompare.r4_r4: throw new Exception("Invalid operation"); //  v1->r4 = v1->r4 ^ v2->r4; break;
                                case StackValueTypeCompare.r4_r8: throw new Exception("Invalid operation"); //  v1->r8 = v1->r4 ^ v2->r8; v1->type = StackValueType.r8; break;
                                case StackValueTypeCompare.r8_i4: throw new Exception("Invalid operation"); // v1->r8 = v1->r8 ^ v2->i4; break;
                                case StackValueTypeCompare.r8_i8: throw new Exception("Invalid operation"); // v1->r8 = v1->r8 ^ v2->i8; break;
                                case StackValueTypeCompare.r8_u4: throw new Exception("Invalid operation"); // v1->r8 = v1->r8 ^ v2->u4; break;
                                case StackValueTypeCompare.r8_u8: throw new Exception("Invalid operation"); // v1->r8 = v1->r8 ^ v2->u8; break;
                                case StackValueTypeCompare.r8_r4: throw new Exception("Invalid operation"); //  v1->r8 = v1->r8 ^ v2->r4; break;
                                case StackValueTypeCompare.r8_r8: throw new Exception("Invalid operation"); //  v1->r8 = v1->r8 ^ v2->r8; break;
                                case StackValueTypeCompare.b_b: v1->b = v1->b ^ v2->b; break;
                                default: throw new Exception("Invalid operation");
                            }
                        }
                        goto NEXT;
                    case ILOpCode.Neg:
                        {
                            v1 = (stack - 1);
                            switch (v1->type)
                            {
                                case StackValueType.i4: v1->i4 = -(v1->i4); break;
                                case StackValueType.i8: v1->i8 = -(v1->i8); break;
                                case StackValueType.u4: throw new Exception("Invalid operation"); // v1->u4 = -(v1->u4); break;
                                case StackValueType.u8: throw new Exception("Invalid operation"); // v1->u8 = -(v1->u8); break;
                                case StackValueType.r4: v1->r4 = -(v1->r4); break;
                                case StackValueType.r8: v1->r8 = -(v1->r8); break;
                                case StackValueType.b: v1->b = !(v1->b); break;
                            }
                        }
                        goto NEXT;
                    case ILOpCode.Not:
                        {
                            v1 = (stack - 1);
                            switch (v1->type)
                            {
                                case StackValueType.i4: v1->i4 = ~(v1->i4); break;
                                case StackValueType.i8: v1->i8 = ~(v1->i8); break;
                                case StackValueType.u4: v1->u4 = ~(v1->u4); break;
                                case StackValueType.u8: v1->u8 = ~(v1->u8); break;
                                case StackValueType.r4: throw new Exception("Invalid operation"); // v1->r4 = ~(v1->r4); break;
                                case StackValueType.r8: throw new Exception("Invalid operation"); // v1->r8 = ~(v1->r8); break;
                                case StackValueType.b: v1->b = !(v1->b); break;
                            }
                        }
                        goto NEXT;
                    #endregion 비트연산-------------------------------------------------------
                    #region 비트쉬프트-------------------------------------------------------
                    case ILOpCode.Shl:
                        {
                            v2 = (--stack);
                            v1 = (stack - 1);
                            switch ((StackValueTypeCompare)(v1->typeNum * COMPARE_TYPE_MAX + v2->typeNum))
                            {
                                case StackValueTypeCompare.i4_i4: v1->i4 = v1->i4 << v2->i4; break;
                                case StackValueTypeCompare.i4_i8: throw new Exception("Invalid operation"); // v1->i8 = v1->i4 << v2->i8; v1->type = StackValueType.i8; break;
                                case StackValueTypeCompare.i4_u4: throw new Exception("Invalid operation"); // v1->i8 = v1->i4 << v2->u4; v1->type = StackValueType.i8; break;
                                case StackValueTypeCompare.i4_u8: throw new Exception("Invalid operation"); // v1->i4 = v1->i4 << v2->u8; break;
                                case StackValueTypeCompare.i4_r4: throw new Exception("Invalid operation"); // v1->r4 = v1->i4 << v2->r4; v1->type = StackValueType.r4; break;
                                case StackValueTypeCompare.i4_r8: throw new Exception("Invalid operation"); // v1->r8 = v1->i4 << v2->r8; v1->type = StackValueType.r8; break;
                                case StackValueTypeCompare.i8_i4: v1->i8 = v1->i8 << v2->i4; break;
                                case StackValueTypeCompare.i8_i8: throw new Exception("Invalid operation"); // v1->i8 = v1->i8 << v2->i8; break;
                                case StackValueTypeCompare.i8_u4: throw new Exception("Invalid operation"); // v1->i8 = v1->i8 << v2->u4; break;
                                case StackValueTypeCompare.i8_u8: throw new Exception("Invalid operation"); // v1->i8 = v1->i8 << v2->u8; break;
                                case StackValueTypeCompare.i8_r4: throw new Exception("Invalid operation"); //  v1->r4 = v1->i8 << v2->r4; v1->type = StackValueType.r4; break;
                                case StackValueTypeCompare.i8_r8: throw new Exception("Invalid operation"); //  v1->r8 = v1->i8 << v2->r8; v1->type = StackValueType.r8; break;
                                case StackValueTypeCompare.u4_i4: v1->u4 = v1->u4 << v2->i4; v1->type = StackValueType.u4; break;
                                case StackValueTypeCompare.u4_i8: throw new Exception("Invalid operation"); // v1->i8 = v1->u4 << v2->i8; v1->type = StackValueType.i8; break;
                                case StackValueTypeCompare.u4_u4: throw new Exception("Invalid operation"); // v1->u4 = v1->u4 << v2->u4; break;
                                case StackValueTypeCompare.u4_u8: throw new Exception("Invalid operation"); // v1->u8 = v1->u4 << v2->u8; v1->type = StackValueType.u8; break;
                                case StackValueTypeCompare.u4_r4: throw new Exception("Invalid operation"); //  v1->r4 = v1->u4 << v2->r4; v1->type = StackValueType.r4; break;
                                case StackValueTypeCompare.u4_r8: throw new Exception("Invalid operation"); //  v1->r8 = v1->u4 << v2->r8; v1->type = StackValueType.r8; break;
                                case StackValueTypeCompare.u8_i4: v1->u8 = v1->u8 << v2->i4; break;
                                case StackValueTypeCompare.u8_i8: throw new Exception("Invalid operation"); // v1->u8 = v1->u8 << v2->i8; break;
                                case StackValueTypeCompare.u8_u4: throw new Exception("Invalid operation"); // v1->u8 = v1->u8 << v2->u4; break;
                                case StackValueTypeCompare.u8_u8: throw new Exception("Invalid operation"); // v1->u8 = v1->u8 << v2->u8; break;
                                case StackValueTypeCompare.u8_r4: throw new Exception("Invalid operation"); //  v1->r4 = v1->u8 << v2->r4; v1->type = StackValueType.r4; break;
                                case StackValueTypeCompare.u8_r8: throw new Exception("Invalid operation"); //  v1->r8 = v1->u8 << v2->r8; v1->type = StackValueType.r8; break;
                                case StackValueTypeCompare.r4_i4: throw new Exception("Invalid operation"); //  v1->r4 = v1->r4 << v2->i4; break;
                                case StackValueTypeCompare.r4_i8: throw new Exception("Invalid operation"); //  v1->r4 = v1->r4 << v2->i8; break;
                                case StackValueTypeCompare.r4_u4: throw new Exception("Invalid operation"); //  v1->r4 = v1->r4 << v2->u4; break;
                                case StackValueTypeCompare.r4_u8: throw new Exception("Invalid operation"); //  v1->r4 = v1->r4 << v2->u8; break;
                                case StackValueTypeCompare.r4_r4: throw new Exception("Invalid operation"); //  v1->r4 = v1->r4 << v2->r4; break;
                                case StackValueTypeCompare.r4_r8: throw new Exception("Invalid operation"); //  v1->r8 = v1->r4 << v2->r8; v1->type = StackValueType.r8; break;
                                case StackValueTypeCompare.r8_i4: throw new Exception("Invalid operation"); // v1->r8 = v1->r8 << v2->i4; break;
                                case StackValueTypeCompare.r8_i8: throw new Exception("Invalid operation"); // v1->r8 = v1->r8 << v2->i8; break;
                                case StackValueTypeCompare.r8_u4: throw new Exception("Invalid operation"); // v1->r8 = v1->r8 << v2->u4; break;
                                case StackValueTypeCompare.r8_u8: throw new Exception("Invalid operation"); // v1->r8 = v1->r8 << v2->u8; break;
                                case StackValueTypeCompare.r8_r4: throw new Exception("Invalid operation"); //  v1->r8 = v1->r8 << v2->r4; break;
                                case StackValueTypeCompare.r8_r8: throw new Exception("Invalid operation"); //  v1->r8 = v1->r8 << v2->r8; break;
                                default: throw new Exception("Invalid operation");
                            }
                        }
                        goto NEXT;
                    case ILOpCode.Shr:
                    case ILOpCode.Shr_un:
                        {
                            v2 = (--stack);
                            v1 = (stack - 1);
                            switch ((StackValueTypeCompare)(v1->typeNum * COMPARE_TYPE_MAX + v2->typeNum))
                            {
                                case StackValueTypeCompare.i4_i4: v1->i4 = v1->i4 >> v2->i4; break;
                                case StackValueTypeCompare.i4_i8: throw new Exception("Invalid operation"); // v1->i8 = v1->i4 >> v2->i8; v1->type = StackValueType.i8; break;
                                case StackValueTypeCompare.i4_u4: throw new Exception("Invalid operation"); // v1->i8 = v1->i4 >> v2->u4; v1->type = StackValueType.i8; break;
                                case StackValueTypeCompare.i4_u8: throw new Exception("Invalid operation"); // v1->i4 = v1->i4 >> v2->u8; break;
                                case StackValueTypeCompare.i4_r4: throw new Exception("Invalid operation"); // v1->r4 = v1->i4 >> v2->r4; v1->type = StackValueType.r4; break;
                                case StackValueTypeCompare.i4_r8: throw new Exception("Invalid operation"); // v1->r8 = v1->i4 >> v2->r8; v1->type = StackValueType.r8; break;
                                case StackValueTypeCompare.i8_i4: v1->i8 = v1->i8 >> v2->i4; break;
                                case StackValueTypeCompare.i8_i8: throw new Exception("Invalid operation"); // v1->i8 = v1->i8 >> v2->i8; break;
                                case StackValueTypeCompare.i8_u4: throw new Exception("Invalid operation"); // v1->i8 = v1->i8 >> v2->u4; break;
                                case StackValueTypeCompare.i8_u8: throw new Exception("Invalid operation"); // v1->i8 = v1->i8 >> v2->u8; break;
                                case StackValueTypeCompare.i8_r4: throw new Exception("Invalid operation"); //  v1->r4 = v1->i8 >> v2->r4; v1->type = StackValueType.r4; break;
                                case StackValueTypeCompare.i8_r8: throw new Exception("Invalid operation"); //  v1->r8 = v1->i8 >> v2->r8; v1->type = StackValueType.r8; break;
                                case StackValueTypeCompare.u4_i4: v1->u4 = v1->u4 >> v2->i4; v1->type = StackValueType.u4; break;
                                case StackValueTypeCompare.u4_i8: throw new Exception("Invalid operation"); // v1->i8 = v1->u4 >> v2->i8; v1->type = StackValueType.i8; break;
                                case StackValueTypeCompare.u4_u4: throw new Exception("Invalid operation"); // v1->u4 = v1->u4 >> v2->u4; break;
                                case StackValueTypeCompare.u4_u8: throw new Exception("Invalid operation"); // v1->u8 = v1->u4 >> v2->u8; v1->type = StackValueType.u8; break;
                                case StackValueTypeCompare.u4_r4: throw new Exception("Invalid operation"); //  v1->r4 = v1->u4 >> v2->r4; v1->type = StackValueType.r4; break;
                                case StackValueTypeCompare.u4_r8: throw new Exception("Invalid operation"); //  v1->r8 = v1->u4 >> v2->r8; v1->type = StackValueType.r8; break;
                                case StackValueTypeCompare.u8_i4: v1->u8 = v1->u8 >> v2->i4; break;
                                case StackValueTypeCompare.u8_i8: throw new Exception("Invalid operation"); // v1->u8 = v1->u8 >> v2->i8; break;
                                case StackValueTypeCompare.u8_u4: throw new Exception("Invalid operation"); // v1->u8 = v1->u8 >> v2->u4; break;
                                case StackValueTypeCompare.u8_u8: throw new Exception("Invalid operation"); // v1->u8 = v1->u8 >> v2->u8; break;
                                case StackValueTypeCompare.u8_r4: throw new Exception("Invalid operation"); //  v1->r4 = v1->u8 >> v2->r4; v1->type = StackValueType.r4; break;
                                case StackValueTypeCompare.u8_r8: throw new Exception("Invalid operation"); //  v1->r8 = v1->u8 >> v2->r8; v1->type = StackValueType.r8; break;
                                case StackValueTypeCompare.r4_i4: throw new Exception("Invalid operation"); //  v1->r4 = v1->r4 >> v2->i4; break;
                                case StackValueTypeCompare.r4_i8: throw new Exception("Invalid operation"); //  v1->r4 = v1->r4 >> v2->i8; break;
                                case StackValueTypeCompare.r4_u4: throw new Exception("Invalid operation"); //  v1->r4 = v1->r4 >> v2->u4; break;
                                case StackValueTypeCompare.r4_u8: throw new Exception("Invalid operation"); //  v1->r4 = v1->r4 >> v2->u8; break;
                                case StackValueTypeCompare.r4_r4: throw new Exception("Invalid operation"); //  v1->r4 = v1->r4 >> v2->r4; break;
                                case StackValueTypeCompare.r4_r8: throw new Exception("Invalid operation"); //  v1->r8 = v1->r4 >> v2->r8; v1->type = StackValueType.r8; break;
                                case StackValueTypeCompare.r8_i4: throw new Exception("Invalid operation"); // v1->r8 = v1->r8 >> v2->i4; break;
                                case StackValueTypeCompare.r8_i8: throw new Exception("Invalid operation"); // v1->r8 = v1->r8 >> v2->i8; break;
                                case StackValueTypeCompare.r8_u4: throw new Exception("Invalid operation"); // v1->r8 = v1->r8 >> v2->u4; break;
                                case StackValueTypeCompare.r8_u8: throw new Exception("Invalid operation"); // v1->r8 = v1->r8 >> v2->u8; break;
                                case StackValueTypeCompare.r8_r4: throw new Exception("Invalid operation"); //  v1->r8 = v1->r8 >> v2->r4; break;
                                case StackValueTypeCompare.r8_r8: throw new Exception("Invalid operation"); //  v1->r8 = v1->r8 >> v2->r8; break;
                                default: throw new Exception("Invalid operation");
                            }
                        }
                        goto NEXT;
                    #endregion-------------------------------------------------------
                    #region Convert-------------------------------------------------------
                    #region Integer
                    case ILOpCode.Conv_i:
                        {
                            v1 = (stack - 1);
                            switch (v1->type)
                            {
                                case StackValueType.i4: v1->i8 = (nint)(v1->i4); break;
                                case StackValueType.i8: v1->i8 = (nint)(v1->i8); break;
                                case StackValueType.u4: v1->i8 = (nint)(v1->u4); break;
                                case StackValueType.u8: v1->i8 = (nint)(v1->u8); break;
                                case StackValueType.r4: v1->i8 = (nint)(v1->r4); break;
                                case StackValueType.r8: v1->i8 = (nint)(v1->r8); break;
                                default: throw new Exception("Invalid operation");
                            }
                            v1->type = StackValueType.i8;
                        }
                        goto NEXT;
                    case ILOpCode.Conv_i1:
                        {
                            v1 = (stack - 1);
                            switch (v1->type)
                            {
                                case StackValueType.i4: v1->i8 = (sbyte)(v1->i4); break;
                                case StackValueType.i8: v1->i8 = (sbyte)(v1->i8); break;
                                case StackValueType.u4: v1->i8 = (sbyte)(v1->u4); break;
                                case StackValueType.u8: v1->i8 = (sbyte)(v1->u8); break;
                                case StackValueType.r4: v1->i8 = (sbyte)(v1->r4); break;
                                case StackValueType.r8: v1->i8 = (sbyte)(v1->r8); break;
                                default: throw new Exception("Invalid operation");
                            }
                            v1->type = StackValueType.i4;
                        }
                        goto NEXT;
                    case ILOpCode.Conv_i2:
                        {
                            v1 = (stack - 1);
                            switch (v1->type)
                            {
                                case StackValueType.i4: v1->i8 = (short)(v1->i4); break;
                                case StackValueType.i8: v1->i8 = (short)(v1->i8); break;
                                case StackValueType.u4: v1->i8 = (short)(v1->u4); break;
                                case StackValueType.u8: v1->i8 = (short)(v1->u8); break;
                                case StackValueType.r4: v1->i8 = (short)(v1->r4); break;
                                case StackValueType.r8: v1->i8 = (short)(v1->r8); break;
                                default: throw new Exception("Invalid operation");
                            }
                            v1->type = StackValueType.i4;
                        }
                        goto NEXT;
                    case ILOpCode.Conv_i4:
                        {
                            v1 = (stack - 1);
                            switch (v1->type)
                            {
                                case StackValueType.i4: break;
                                case StackValueType.i8: v1->i8 = (int)(v1->i8); break;
                                case StackValueType.u4: v1->i8 = (int)(v1->u4); break;
                                case StackValueType.u8: v1->i8 = (int)(v1->u8); break;
                                case StackValueType.r4: v1->i8 = (int)(v1->r4); break;
                                case StackValueType.r8: v1->i8 = (int)(v1->r8); break;
                                default: throw new Exception("Invalid operation");
                            }
                            v1->type = StackValueType.i4;
                        }
                        goto NEXT;
                    case ILOpCode.Conv_i8:
                        {
                            v1 = (stack - 1);
                            switch (v1->type)
                            {
                                case StackValueType.i4: v1->i8 = (long)(v1->i4); break;
                                case StackValueType.i8: break;
                                case StackValueType.u4: v1->i8 = (long)(v1->u4); break;
                                case StackValueType.u8: v1->i8 = (long)(v1->u8); break;
                                case StackValueType.r4: v1->i8 = (long)(v1->r4); break;
                                case StackValueType.r8: v1->i8 = (long)(v1->r8); break;
                                default: throw new Exception("Invalid operation");
                            }
                            v1->type = StackValueType.i8;
                        }
                        goto NEXT;
                    case ILOpCode.Conv_ovf_i:
                    case ILOpCode.Conv_ovf_i_un:
                        {
                            v1 = (stack - 1);
                            switch (v1->type)
                            {
                                case StackValueType.i4: v1->i8 = checked((nint)(v1->i4)); break;
                                case StackValueType.i8: v1->i8 = checked((nint)(v1->i8)); break;
                                case StackValueType.u4: v1->i8 = checked((nint)(v1->u4)); break;
                                case StackValueType.u8: v1->i8 = checked((nint)(v1->u8)); break;
                                case StackValueType.r4: v1->i8 = checked((nint)(v1->r4)); break;
                                case StackValueType.r8: v1->i8 = checked((nint)(v1->r8)); break;
                                default: throw new Exception("Invalid operation");
                            }
                            v1->type = StackValueType.i8;
                        }
                        goto NEXT;
                    case ILOpCode.Conv_ovf_i1:
                    case ILOpCode.Conv_ovf_i1_un:
                        {
                            v1 = (stack - 1);
                            switch (v1->type)
                            {
                                case StackValueType.i4: v1->i8 = checked((sbyte)(v1->i4)); break;
                                case StackValueType.i8: v1->i8 = checked((sbyte)(v1->i8)); break;
                                case StackValueType.u4: v1->i8 = checked((sbyte)(v1->u4)); break;
                                case StackValueType.u8: v1->i8 = checked((sbyte)(v1->u8)); break;
                                case StackValueType.r4: v1->i8 = checked((sbyte)(v1->r4)); break;
                                case StackValueType.r8: v1->i8 = checked((sbyte)(v1->r8)); break;
                                default: throw new Exception("Invalid operation");
                            }
                            v1->type = StackValueType.i4;
                        }
                        goto NEXT;
                    case ILOpCode.Conv_ovf_i2:
                    case ILOpCode.Conv_ovf_i2_un:
                        {
                            v1 = (stack - 1);
                            switch (v1->type)
                            {
                                case StackValueType.i4: v1->i8 = checked((short)(v1->i4)); break;
                                case StackValueType.i8: v1->i8 = checked((short)(v1->i8)); break;
                                case StackValueType.u4: v1->i8 = checked((short)(v1->u4)); break;
                                case StackValueType.u8: v1->i8 = checked((short)(v1->u8)); break;
                                case StackValueType.r4: v1->i8 = checked((short)(v1->r4)); break;
                                case StackValueType.r8: v1->i8 = checked((short)(v1->r8)); break;
                                default: throw new Exception("Invalid operation");
                            }
                            v1->type = StackValueType.i4;
                        }
                        goto NEXT;
                    case ILOpCode.Conv_ovf_i4:
                    case ILOpCode.Conv_ovf_i4_un:
                        {
                            v1 = (stack - 1);
                            switch (v1->type)
                            {
                                case StackValueType.i4: break;
                                case StackValueType.i8: v1->i8 = checked((int)(v1->i8)); break;
                                case StackValueType.u4: v1->i8 = checked((int)(v1->u4)); break;
                                case StackValueType.u8: v1->i8 = checked((int)(v1->u8)); break;
                                case StackValueType.r4: v1->i8 = checked((int)(v1->r4)); break;
                                case StackValueType.r8: v1->i8 = checked((int)(v1->r8)); break;
                                default: throw new Exception("Invalid operation");
                            }
                            v1->type = StackValueType.i4;
                        }
                        goto NEXT;
                    case ILOpCode.Conv_ovf_i8:
                    case ILOpCode.Conv_ovf_i8_un:
                        {
                            v1 = (stack - 1);
                            switch (v1->type)
                            {
                                case StackValueType.i4: v1->i8 = checked((long)(v1->i4)); break;
                                case StackValueType.i8: break;
                                case StackValueType.u4: v1->i8 = checked((long)(v1->u4)); break;
                                case StackValueType.u8: v1->i8 = checked((long)(v1->u8)); break;
                                case StackValueType.r4: v1->i8 = checked((long)(v1->r4)); break;
                                case StackValueType.r8: v1->i8 = checked((long)(v1->r8)); break;
                                default: throw new Exception("Invalid operation");
                            }
                            v1->type = StackValueType.i8;
                        }
                        goto NEXT;
                    #endregion Integer
                    #region Real
                    case ILOpCode.Conv_r_un:
                    case ILOpCode.Conv_r4:
                        {
                            v1 = (stack - 1);
                            switch (v1->type)
                            {
                                case StackValueType.i4: v1->r4 = (float)(v1->i4); break;
                                case StackValueType.i8: v1->r4 = (float)(v1->i8); break;
                                case StackValueType.u4: v1->r4 = (float)(v1->u4); break;
                                case StackValueType.u8: v1->r4 = (float)(v1->u8); break;
                                case StackValueType.r4: v1->r4 = (float)(v1->r4); break;
                                case StackValueType.r8: v1->r4 = (float)(v1->r8); break;
                                default: throw new Exception("Invalid operation");
                            }
                            v1->type = StackValueType.r4;
                            v1->Last4 = 0;
                        }
                        goto NEXT;
                    case ILOpCode.Conv_r8:
                        {
                            v1 = (stack - 1);
                            switch (v1->type)
                            {
                                case StackValueType.i4: v1->r8 = (double)(v1->i4); break;
                                case StackValueType.i8: v1->r8 = (double)(v1->i8); break;
                                case StackValueType.u4: v1->r8 = (double)(v1->u4); break;
                                case StackValueType.u8: v1->r8 = (double)(v1->u8); break;
                                case StackValueType.r4: v1->r8 = (double)(v1->r4); break;
                                case StackValueType.r8: v1->r8 = (double)(v1->r8); break;
                                default: throw new Exception("Invalid operation");
                            }
                            v1->type = StackValueType.r8;
                        }
                        goto NEXT;
                    #endregion Real
                    #region Unsigned
                    case ILOpCode.Conv_u:
                        {
                            v1 = (stack - 1);
                            switch (v1->type)
                            {
                                case StackValueType.i4: v1->u8 = (nuint)(v1->i4); break;
                                case StackValueType.i8: v1->u8 = (nuint)(v1->i8); break;
                                case StackValueType.u4: v1->u8 = (nuint)(v1->u4); break;
                                case StackValueType.u8: v1->u8 = (nuint)(v1->u8); break;
                                case StackValueType.r4: v1->u8 = (nuint)(v1->r4); break;
                                case StackValueType.r8: v1->u8 = (nuint)(v1->r8); break;
                                default: throw new Exception("Invalid operation");
                            }
                            v1->type = StackValueType.u8;
                        }
                        goto NEXT;
                    case ILOpCode.Conv_u1:
                        {
                            v1 = (stack - 1);
                            switch (v1->type)
                            {
                                case StackValueType.i4: v1->u8 = (byte)(v1->i4); break;
                                case StackValueType.i8: v1->u8 = (byte)(v1->i8); break;
                                case StackValueType.u4: v1->u8 = (byte)(v1->u4); break;
                                case StackValueType.u8: v1->u8 = (byte)(v1->u8); break;
                                case StackValueType.r4: v1->u8 = (byte)(v1->r4); break;
                                case StackValueType.r8: v1->u8 = (byte)(v1->r8); break;
                                default: throw new Exception("Invalid operation");
                            }
                            v1->type = StackValueType.u4;
                        }
                        goto NEXT;
                    case ILOpCode.Conv_u2:
                        {
                            v1 = (stack - 1);
                            switch (v1->type)
                            {
                                case StackValueType.i4: v1->u8 = (ushort)(v1->i4); break;
                                case StackValueType.i8: v1->u8 = (ushort)(v1->i8); break;
                                case StackValueType.u4: v1->u8 = (ushort)(v1->u4); break;
                                case StackValueType.u8: v1->u8 = (ushort)(v1->u8); break;
                                case StackValueType.r4: v1->u8 = (ushort)(v1->r4); break;
                                case StackValueType.r8: v1->u8 = (ushort)(v1->r8); break;
                                default: throw new Exception("Invalid operation");
                            }
                            v1->type = StackValueType.u4;
                        }
                        goto NEXT;
                    case ILOpCode.Conv_u4:
                        {
                            v1 = (stack - 1);
                            switch (v1->type)
                            {
                                case StackValueType.i4: v1->u8 = (uint)(v1->i4); break;
                                case StackValueType.i8: v1->u8 = (uint)(v1->i8); break;
                                case StackValueType.u4: v1->u8 = (uint)(v1->u4); break;
                                case StackValueType.u8: v1->u8 = (uint)(v1->u8); break;
                                case StackValueType.r4: v1->u8 = (uint)(v1->r4); break;
                                case StackValueType.r8: v1->u8 = (uint)(v1->r8); break;
                                default: throw new Exception("Invalid operation");
                            }
                            v1->type = StackValueType.u4;
                        }
                        goto NEXT;
                    case ILOpCode.Conv_u8:
                        {
                            v1 = (stack - 1);
                            switch (v1->type)
                            {
                                case StackValueType.i4: v1->u8 = (ulong)(v1->i4); break;
                                case StackValueType.i8: v1->u8 = (ulong)(v1->i8); break;
                                case StackValueType.u4: v1->u8 = (ulong)(v1->u4); break;
                                case StackValueType.u8: v1->u8 = (ulong)(v1->u8); break;
                                case StackValueType.r4: v1->u8 = (ulong)(v1->r4); break;
                                case StackValueType.r8: v1->u8 = (ulong)(v1->r8); break;
                                default: throw new Exception("Invalid operation");
                            }
                            v1->type = StackValueType.u8;
                        }
                        goto NEXT;
                    case ILOpCode.Conv_ovf_u:
                    case ILOpCode.Conv_ovf_u_un:
                        {
                            v1 = (stack - 1);
                            switch (v1->type)
                            {
                                case StackValueType.i4: v1->u8 = checked((nuint)(v1->i4)); break;
                                case StackValueType.i8: v1->u8 = checked((nuint)(v1->i8)); break;
                                case StackValueType.u4: v1->u8 = checked((nuint)(v1->u4)); break;
                                case StackValueType.u8: v1->u8 = checked((nuint)(v1->u8)); break;
                                case StackValueType.r4: v1->u8 = checked((nuint)(v1->r4)); break;
                                case StackValueType.r8: v1->u8 = checked((nuint)(v1->r8)); break;
                                default: throw new Exception("Invalid operation");
                            }
                            v1->type = StackValueType.u8;
                        }
                        goto NEXT;
                    case ILOpCode.Conv_ovf_u1:
                    case ILOpCode.Conv_ovf_u1_un:
                        {
                            v1 = (stack - 1);
                            switch (v1->type)
                            {
                                case StackValueType.i4: v1->u8 = checked((byte)(v1->i4)); break;
                                case StackValueType.i8: v1->u8 = checked((byte)(v1->i8)); break;
                                case StackValueType.u4: v1->u8 = checked((byte)(v1->u4)); break;
                                case StackValueType.u8: v1->u8 = checked((byte)(v1->u8)); break;
                                case StackValueType.r4: v1->u8 = checked((byte)(v1->r4)); break;
                                case StackValueType.r8: v1->u8 = checked((byte)(v1->r8)); break;
                                default: throw new Exception("Invalid operation");
                            }
                            v1->type = StackValueType.u4;
                        }
                        goto NEXT;
                    case ILOpCode.Conv_ovf_u2:
                    case ILOpCode.Conv_ovf_u2_un:
                        {
                            v1 = (stack - 1);
                            switch (v1->type)
                            {
                                case StackValueType.i4: v1->u8 = checked((ushort)(v1->i4)); break;
                                case StackValueType.i8: v1->u8 = checked((ushort)(v1->i8)); break;
                                case StackValueType.u4: v1->u8 = checked((ushort)(v1->u4)); break;
                                case StackValueType.u8: v1->u8 = checked((ushort)(v1->u8)); break;
                                case StackValueType.r4: v1->u8 = checked((ushort)(v1->r4)); break;
                                case StackValueType.r8: v1->u8 = checked((ushort)(v1->r8)); break;
                                default: throw new Exception("Invalid operation");
                            }
                            v1->type = StackValueType.u4;
                        }
                        goto NEXT;
                    case ILOpCode.Conv_ovf_u4:
                    case ILOpCode.Conv_ovf_u4_un:
                        {
                            v1 = (stack - 1);
                            switch (v1->type)
                            {
                                case StackValueType.i4: v1->u8 = checked((uint)(v1->i4)); break;
                                case StackValueType.i8: v1->u8 = checked((uint)(v1->i8)); break;
                                case StackValueType.u4: v1->u8 = checked((uint)(v1->u4)); break;
                                case StackValueType.u8: v1->u8 = checked((uint)(v1->u8)); break;
                                case StackValueType.r4: v1->u8 = checked((uint)(v1->r4)); break;
                                case StackValueType.r8: v1->u8 = checked((uint)(v1->r8)); break;
                                default: throw new Exception("Invalid operation");
                            }
                            v1->type = StackValueType.u4;
                        }
                        goto NEXT;
                    case ILOpCode.Conv_ovf_u8:
                    case ILOpCode.Conv_ovf_u8_un:
                        {
                            v1 = (stack - 1);
                            switch (v1->type)
                            {
                                case StackValueType.i4: v1->u8 = checked((ulong)(v1->i4)); break;
                                case StackValueType.i8: v1->u8 = checked((ulong)(v1->i8)); break;
                                case StackValueType.u4: v1->u8 = checked((ulong)(v1->u4)); break;
                                case StackValueType.u8: v1->u8 = checked((ulong)(v1->u8)); break;
                                case StackValueType.r4: v1->u8 = checked((ulong)(v1->r4)); break;
                                case StackValueType.r8: v1->u8 = checked((ulong)(v1->r8)); break;
                                default: throw new Exception("Invalid operation");
                            }
                            v1->type = StackValueType.u8;
                        }
                        goto NEXT;
                    #endregion Unsigned
                    #endregion Convert-------------------------------------------------------



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
                    case ILOpCode.Arglist:
                        {
                        }
                        goto NEXT;
                    case ILOpCode.Ceq:
                        {
                            v1 = --stack;
                            v2 = --stack;
                            switch ((StackValueTypeCompare)(v1->typeNum * COMPARE_TYPE_MAX + v2->typeNum))
                            {
                                case StackValueTypeCompare.i4_i4: if (v1->i4 == v2->i4) goto TRUE; goto FALSE;
                                case StackValueTypeCompare.i4_i8: if (v1->i4 == v2->i8) goto TRUE; goto FALSE;
                                case StackValueTypeCompare.i4_u4: if (v1->i4 == v2->u4) goto TRUE; goto FALSE;
                                case StackValueTypeCompare.i4_u8: if (v1->u4 == v2->u8) goto TRUE; goto FALSE;
                                case StackValueTypeCompare.i4_r4: if (v1->i4 == v2->r4) goto TRUE; goto FALSE;
                                case StackValueTypeCompare.i4_r8: if (v1->i4 == v2->r8) goto TRUE; goto FALSE;
                                case StackValueTypeCompare.i8_i4: if (v1->i8 == v2->i4) goto TRUE; goto FALSE;
                                case StackValueTypeCompare.i8_i8: if (v1->i8 == v2->i8) goto TRUE; goto FALSE;
                                case StackValueTypeCompare.i8_u4: if (v1->i8 == v2->u4) goto TRUE; goto FALSE;
                                case StackValueTypeCompare.i8_u8: if (v1->u8 == v2->u8) goto TRUE; goto FALSE;
                                case StackValueTypeCompare.i8_r4: if (v1->i8 == v2->r4) goto TRUE; goto FALSE;
                                case StackValueTypeCompare.i8_r8: if (v1->i8 == v2->r8) goto TRUE; goto FALSE;
                                case StackValueTypeCompare.u4_i4: if (v1->u4 == v2->i4) goto TRUE; goto FALSE;
                                case StackValueTypeCompare.u4_i8: if (v1->u4 == v2->i8) goto TRUE; goto FALSE;
                                case StackValueTypeCompare.u4_u4: if (v1->u4 == v2->u4) goto TRUE; goto FALSE;
                                case StackValueTypeCompare.u4_u8: if (v1->u4 == v2->u8) goto TRUE; goto FALSE;
                                case StackValueTypeCompare.u4_r4: if (v1->u4 == v2->r4) goto TRUE; goto FALSE;
                                case StackValueTypeCompare.u4_r8: if (v1->u4 == v2->r8) goto TRUE; goto FALSE;
                                case StackValueTypeCompare.u8_i4: if (v1->u8 == v2->u4) goto TRUE; goto FALSE;
                                case StackValueTypeCompare.u8_i8: if (v1->u8 == v2->u8) goto TRUE; goto FALSE;
                                case StackValueTypeCompare.u8_u4: if (v1->u8 == v2->u4) goto TRUE; goto FALSE;
                                case StackValueTypeCompare.u8_u8: if (v1->u8 == v2->u8) goto TRUE; goto FALSE;
                                case StackValueTypeCompare.u8_r4: if (v1->u8 == v2->r4) goto TRUE; goto FALSE;
                                case StackValueTypeCompare.u8_r8: if (v1->u8 == v2->r8) goto TRUE; goto FALSE;
                                case StackValueTypeCompare.r4_i4: if (v1->r4 == v2->i4) goto TRUE; goto FALSE;
                                case StackValueTypeCompare.r4_i8: if (v1->r4 == v2->i8) goto TRUE; goto FALSE;
                                case StackValueTypeCompare.r4_u4: if (v1->r4 == v2->u4) goto TRUE; goto FALSE;
                                case StackValueTypeCompare.r4_u8: if (v1->r4 == v2->u8) goto TRUE; goto FALSE;
                                case StackValueTypeCompare.r4_r4: if (v1->r4 == v2->r4) goto TRUE; goto FALSE;
                                case StackValueTypeCompare.r4_r8: if (v1->r4 == v2->r8) goto TRUE; goto FALSE;
                                case StackValueTypeCompare.r8_i4: if (v1->r8 == v2->i4) goto TRUE; goto FALSE;
                                case StackValueTypeCompare.r8_i8: if (v1->r8 == v2->i8) goto TRUE; goto FALSE;
                                case StackValueTypeCompare.r8_u4: if (v1->r8 == v2->u4) goto TRUE; goto FALSE;
                                case StackValueTypeCompare.r8_u8: if (v1->r8 == v2->u8) goto TRUE; goto FALSE;
                                case StackValueTypeCompare.r8_r4: if (v1->r8 == v2->r4) goto TRUE; goto FALSE;
                                case StackValueTypeCompare.r8_r8: if (v1->r8 == v2->r8) goto TRUE; goto FALSE;
                                case StackValueTypeCompare.b_b: if (v1->b == v2->b) goto TRUE; goto FALSE;
                                default: if (v1->value == v2->value) goto TRUE; goto FALSE;
                            }
                        TRUE:
                            v2->type = StackValueType.i4;
                            v2->value = 1;
                            goto NEXT;
                        FALSE:
                            v2->type = StackValueType.i4;
                            v2->value = 0;
                            goto NEXT;
                        }
                    case ILOpCode.Cgt:
                    case ILOpCode.Cgt_un:
                        {
                            v1 = --stack;
                            v2 = --stack;
                            switch ((StackValueTypeCompare)(v1->typeNum * COMPARE_TYPE_MAX + v2->typeNum))
                            {
                                case StackValueTypeCompare.i4_i4: if (v1->i4 > v2->i4) goto TRUE; goto FALSE;
                                case StackValueTypeCompare.i4_i8: if (v1->i4 > v2->i8) goto TRUE; goto FALSE;
                                case StackValueTypeCompare.i4_u4: if (v1->i4 > v2->u4) goto TRUE; goto FALSE;
                                case StackValueTypeCompare.i4_u8: if (v1->i4 > 0 && v1->u4 > v2->u8) goto TRUE; goto FALSE;
                                case StackValueTypeCompare.i4_r4: if (v1->i4 > v2->r4) goto TRUE; goto FALSE;
                                case StackValueTypeCompare.i4_r8: if (v1->i4 > v2->r8) goto TRUE; goto FALSE;
                                case StackValueTypeCompare.i8_i4: if (v1->i8 > v2->i4) goto TRUE; goto FALSE;
                                case StackValueTypeCompare.i8_i8: if (v1->i8 > v2->i8) goto TRUE; goto FALSE;
                                case StackValueTypeCompare.i8_u4: if (v1->i8 > v2->u4) goto TRUE; goto FALSE;
                                case StackValueTypeCompare.i8_u8: if (v1->i8 > 0 && v1->u8 > v2->u8) goto TRUE; goto FALSE;
                                case StackValueTypeCompare.i8_r4: if (v1->i8 > v2->r4) goto TRUE; goto FALSE;
                                case StackValueTypeCompare.i8_r8: if (v1->i8 > v2->r8) goto TRUE; goto FALSE;
                                case StackValueTypeCompare.u4_i4: if (v1->u4 > v2->i4) goto TRUE; goto FALSE;
                                case StackValueTypeCompare.u4_i8: if (v1->u4 > v2->i8) goto TRUE; goto FALSE;
                                case StackValueTypeCompare.u4_u4: if (v1->u4 > v2->u4) goto TRUE; goto FALSE;
                                case StackValueTypeCompare.u4_u8: if (v1->u4 > v2->u8) goto TRUE; goto FALSE;
                                case StackValueTypeCompare.u4_r4: if (v1->u4 > v2->r4) goto TRUE; goto FALSE;
                                case StackValueTypeCompare.u4_r8: if (v1->u4 > v2->r8) goto TRUE; goto FALSE;
                                case StackValueTypeCompare.u8_i4: if (v2->i4 < 0 || v1->u8 > v2->u4) goto TRUE; goto FALSE;
                                case StackValueTypeCompare.u8_i8: if (v2->i8 < 0 || v1->u8 > v2->u8) goto TRUE; goto FALSE;
                                case StackValueTypeCompare.u8_u4: if (v1->u8 > v2->u4) goto TRUE; goto FALSE;
                                case StackValueTypeCompare.u8_u8: if (v1->u8 > v2->u8) goto TRUE; goto FALSE;
                                case StackValueTypeCompare.u8_r4: if (v1->u8 > v2->r4) goto TRUE; goto FALSE;
                                case StackValueTypeCompare.u8_r8: if (v1->u8 > v2->r8) goto TRUE; goto FALSE;
                                case StackValueTypeCompare.r4_i4: if (v1->r4 > v2->i4) goto TRUE; goto FALSE;
                                case StackValueTypeCompare.r4_i8: if (v1->r4 > v2->i8) goto TRUE; goto FALSE;
                                case StackValueTypeCompare.r4_u4: if (v1->r4 > v2->u4) goto TRUE; goto FALSE;
                                case StackValueTypeCompare.r4_u8: if (v1->r4 > v2->u8) goto TRUE; goto FALSE;
                                case StackValueTypeCompare.r4_r4: if (v1->r4 > v2->r4) goto TRUE; goto FALSE;
                                case StackValueTypeCompare.r4_r8: if (v1->r4 > v2->r8) goto TRUE; goto FALSE;
                                case StackValueTypeCompare.r8_i4: if (v1->r8 > v2->i4) goto TRUE; goto FALSE;
                                case StackValueTypeCompare.r8_i8: if (v1->r8 > v2->i8) goto TRUE; goto FALSE;
                                case StackValueTypeCompare.r8_u4: if (v1->r8 > v2->u4) goto TRUE; goto FALSE;
                                case StackValueTypeCompare.r8_u8: if (v1->r8 > v2->u8) goto TRUE; goto FALSE;
                                case StackValueTypeCompare.r8_r4: if (v1->r8 > v2->r4) goto TRUE; goto FALSE;
                                case StackValueTypeCompare.r8_r8: if (v1->r8 > v2->r8) goto TRUE; goto FALSE;
                                default: throw new Exception("Invalid operation");
                            }
                        TRUE:
                            v2->type = StackValueType.i4;
                            v2->value = 1;
                        FALSE:
                            v2->type = StackValueType.i4;
                            v2->value = 0;
                        }
                        goto NEXT;
                    case ILOpCode.Clt:
                    case ILOpCode.Clt_un:
                        {
                            v1 = --stack;
                            v2 = (stack - 1);
                            switch ((StackValueTypeCompare)(v1->typeNum * COMPARE_TYPE_MAX + v2->typeNum))
                            {
                                case StackValueTypeCompare.i4_i4: if (v1->i4 < v2->i4) goto TRUE; goto FALSE;
                                case StackValueTypeCompare.i4_i8: if (v1->i4 < v2->i8) goto TRUE; goto FALSE;
                                case StackValueTypeCompare.i4_u4: if (v1->i4 < v2->u4) goto TRUE; goto FALSE;
                                case StackValueTypeCompare.i4_u8: if (v1->i4 < 0 || v1->u4 < v2->u8) goto TRUE; goto FALSE;
                                case StackValueTypeCompare.i4_r4: if (v1->i4 < v2->r4) goto TRUE; goto FALSE;
                                case StackValueTypeCompare.i4_r8: if (v1->i4 < v2->r8) goto TRUE; goto FALSE;
                                case StackValueTypeCompare.i8_i4: if (v1->i8 < v2->i4) goto TRUE; goto FALSE;
                                case StackValueTypeCompare.i8_i8: if (v1->i8 < v2->i8) goto TRUE; goto FALSE;
                                case StackValueTypeCompare.i8_u4: if (v1->i8 < v2->u4) goto TRUE; goto FALSE;
                                case StackValueTypeCompare.i8_u8: if (v1->i8 < 0 || v1->u8 < v2->u8) goto TRUE; goto FALSE;
                                case StackValueTypeCompare.i8_r4: if (v1->i8 < v2->r4) goto TRUE; goto FALSE;
                                case StackValueTypeCompare.i8_r8: if (v1->i8 < v2->r8) goto TRUE; goto FALSE;
                                case StackValueTypeCompare.u4_i4: if (v1->u4 < v2->i4) goto TRUE; goto FALSE;
                                case StackValueTypeCompare.u4_i8: if (v1->u4 < v2->i8) goto TRUE; goto FALSE;
                                case StackValueTypeCompare.u4_u4: if (v1->u4 < v2->u4) goto TRUE; goto FALSE;
                                case StackValueTypeCompare.u4_u8: if (v1->u4 < v2->u8) goto TRUE; goto FALSE;
                                case StackValueTypeCompare.u4_r4: if (v1->u4 < v2->r4) goto TRUE; goto FALSE;
                                case StackValueTypeCompare.u4_r8: if (v1->u4 < v2->r8) goto TRUE; goto FALSE;
                                case StackValueTypeCompare.u8_i4: if (v2->i4 > 0 && v1->u8 < v2->u4) goto TRUE; goto FALSE;
                                case StackValueTypeCompare.u8_i8: if (v2->i8 > 0 && v1->u8 < v2->u8) goto TRUE; goto FALSE;
                                case StackValueTypeCompare.u8_u4: if (v1->u8 < v2->u4) goto TRUE; goto FALSE;
                                case StackValueTypeCompare.u8_u8: if (v1->u8 < v2->u8) goto TRUE; goto FALSE;
                                case StackValueTypeCompare.u8_r4: if (v1->u8 < v2->r4) goto TRUE; goto FALSE;
                                case StackValueTypeCompare.u8_r8: if (v1->u8 < v2->r8) goto TRUE; goto FALSE;
                                case StackValueTypeCompare.r4_i4: if (v1->r4 < v2->i4) goto TRUE; goto FALSE;
                                case StackValueTypeCompare.r4_i8: if (v1->r4 < v2->i8) goto TRUE; goto FALSE;
                                case StackValueTypeCompare.r4_u4: if (v1->r4 < v2->u4) goto TRUE; goto FALSE;
                                case StackValueTypeCompare.r4_u8: if (v1->r4 < v2->u8) goto TRUE; goto FALSE;
                                case StackValueTypeCompare.r4_r4: if (v1->r4 < v2->r4) goto TRUE; goto FALSE;
                                case StackValueTypeCompare.r4_r8: if (v1->r4 < v2->r8) goto TRUE; goto FALSE;
                                case StackValueTypeCompare.r8_i4: if (v1->r8 < v2->i4) goto TRUE; goto FALSE;
                                case StackValueTypeCompare.r8_i8: if (v1->r8 < v2->i8) goto TRUE; goto FALSE;
                                case StackValueTypeCompare.r8_u4: if (v1->r8 < v2->u4) goto TRUE; goto FALSE;
                                case StackValueTypeCompare.r8_u8: if (v1->r8 < v2->u8) goto TRUE; goto FALSE;
                                case StackValueTypeCompare.r8_r4: if (v1->r8 < v2->r4) goto TRUE; goto FALSE;
                                case StackValueTypeCompare.r8_r8: if (v1->r8 < v2->r8) goto TRUE; goto FALSE;
                                default: throw new Exception("Invalid operation");
                            }
                        TRUE:
                            v2->type = StackValueType.i4;
                            v2->value = 1;
                        FALSE:
                            v2->type = StackValueType.i4;
                            v2->value = 0;
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

                        //case ILOpCode.Custom_StLoc_0_Ldc_i4_0: local[0] = (void*)0; goto NEXT;
                        //case ILOpCode.Custom_StLoc_0_Ldc_i4_1: local[0] = (void*)1; goto NEXT;
                        //case ILOpCode.Custom_StLoc_0_Ldc_i4_2: local[0] = (void*)2; goto NEXT;
                        //case ILOpCode.Custom_StLoc_0_Ldc_i4_3: local[0] = (void*)3; goto NEXT;
                        //case ILOpCode.Custom_StLoc_0_Ldc_i4_4: local[0] = (void*)4; goto NEXT;
                        //case ILOpCode.Custom_StLoc_0_Ldc_i4_5: local[0] = (void*)5; goto NEXT;
                        //case ILOpCode.Custom_StLoc_0_Ldc_i4_6: local[0] = (void*)6; goto NEXT;
                        //case ILOpCode.Custom_StLoc_0_Ldc_i4_7: local[0] = (void*)7; goto NEXT;
                        //case ILOpCode.Custom_StLoc_0_Ldc_i4_8: local[0] = (void*)8; goto NEXT;
                        //case ILOpCode.Custom_StLoc_0_Ldc_i4_9: local[0] = (void*)9; goto NEXT;
                        //case ILOpCode.Custom_StLoc_1_Ldc_i4_0: local[1] = (void*)0; goto NEXT;
                        //case ILOpCode.Custom_StLoc_1_Ldc_i4_1: local[1] = (void*)1; goto NEXT;
                        //case ILOpCode.Custom_StLoc_1_Ldc_i4_2: local[1] = (void*)2; goto NEXT;
                        //case ILOpCode.Custom_StLoc_1_Ldc_i4_3: local[1] = (void*)3; goto NEXT;
                        //case ILOpCode.Custom_StLoc_1_Ldc_i4_4: local[1] = (void*)4; goto NEXT;
                        //case ILOpCode.Custom_StLoc_1_Ldc_i4_5: local[1] = (void*)5; goto NEXT;
                        //case ILOpCode.Custom_StLoc_1_Ldc_i4_6: local[1] = (void*)6; goto NEXT;
                        //case ILOpCode.Custom_StLoc_1_Ldc_i4_7: local[1] = (void*)7; goto NEXT;
                        //case ILOpCode.Custom_StLoc_1_Ldc_i4_8: local[1] = (void*)8; goto NEXT;
                        //case ILOpCode.Custom_StLoc_1_Ldc_i4_9: local[1] = (void*)9; goto NEXT;
                        //case ILOpCode.Custom_StLoc_2_Ldc_i4_0: local[2] = (void*)0; goto NEXT;
                        //case ILOpCode.Custom_StLoc_2_Ldc_i4_1: local[2] = (void*)1; goto NEXT;
                        //case ILOpCode.Custom_StLoc_2_Ldc_i4_2: local[2] = (void*)2; goto NEXT;
                        //case ILOpCode.Custom_StLoc_2_Ldc_i4_3: local[2] = (void*)3; goto NEXT;
                        //case ILOpCode.Custom_StLoc_2_Ldc_i4_4: local[2] = (void*)4; goto NEXT;
                        //case ILOpCode.Custom_StLoc_2_Ldc_i4_5: local[2] = (void*)5; goto NEXT;
                        //case ILOpCode.Custom_StLoc_2_Ldc_i4_6: local[2] = (void*)6; goto NEXT;
                        //case ILOpCode.Custom_StLoc_2_Ldc_i4_7: local[2] = (void*)7; goto NEXT;
                        //case ILOpCode.Custom_StLoc_2_Ldc_i4_8: local[2] = (void*)8; goto NEXT;
                        //case ILOpCode.Custom_StLoc_2_Ldc_i4_9: local[2] = (void*)9; goto NEXT;
                        //case ILOpCode.Custom_StLoc_3_Ldc_i4_0: local[3] = (void*)0; goto NEXT;
                        //case ILOpCode.Custom_StLoc_3_Ldc_i4_1: local[3] = (void*)1; goto NEXT;
                        //case ILOpCode.Custom_StLoc_3_Ldc_i4_2: local[3] = (void*)2; goto NEXT;
                        //case ILOpCode.Custom_StLoc_3_Ldc_i4_3: local[3] = (void*)3; goto NEXT;
                        //case ILOpCode.Custom_StLoc_3_Ldc_i4_4: local[3] = (void*)4; goto NEXT;
                        //case ILOpCode.Custom_StLoc_3_Ldc_i4_5: local[3] = (void*)5; goto NEXT;
                        //case ILOpCode.Custom_StLoc_3_Ldc_i4_6: local[3] = (void*)6; goto NEXT;
                        //case ILOpCode.Custom_StLoc_3_Ldc_i4_7: local[3] = (void*)7; goto NEXT;
                        //case ILOpCode.Custom_StLoc_3_Ldc_i4_8: local[3] = (void*)8; goto NEXT;
                        //case ILOpCode.Custom_StLoc_3_Ldc_i4_9: local[3] = (void*)9; goto NEXT;

                        //case ILOpCode.Custom_Add_0: *(stack - 1) = *(stack - 1) + 0; goto NEXT;
                        //case ILOpCode.Custom_Add_1: *(stack - 1) = *(stack - 1) + 1; goto NEXT;
                        //case ILOpCode.Custom_Add_2: *(stack - 1) = *(stack - 1) + 2; goto NEXT;
                        //case ILOpCode.Custom_Add_3: *(stack - 1) = *(stack - 1) + 3; goto NEXT;
                        //case ILOpCode.Custom_Add_4: *(stack - 1) = *(stack - 1) + 4; goto NEXT;
                        //case ILOpCode.Custom_Add_5: *(stack - 1) = *(stack - 1) + 5; goto NEXT;
                        //case ILOpCode.Custom_Add_6: *(stack - 1) = *(stack - 1) + 6; goto NEXT;
                        //case ILOpCode.Custom_Add_7: *(stack - 1) = *(stack - 1) + 7; goto NEXT;
                        //case ILOpCode.Custom_Add_8: *(stack - 1) = *(stack - 1) + 8; goto NEXT;
                        //case ILOpCode.Custom_Add_9: *(stack - 1) = *(stack - 1) + 9; goto NEXT;

                        //case ILOpCode.Custom_Ldloc_s_Ldc_s_Add_StLoc_s:
                        //    local[op[4]] = (void*)((long)local[*opv] + op[3]);
                        //    goto NEXT;

                        //case ILOpCode.Custom_Ldloc_s_Ldc_s_Blt_s:
                        //    if ((long)local[*opv] < op[3])
                        //    {
                        //        ops = (seek + op[4]);
                        //    }
                        //    goto NEXT;

                        //case ILOpCode.Custom_Ldloc_s_Ldc_Blt_s:
                        //    if ((long)local[*opv] < *(int*)(op + 3))
                        //    {
                        //        ops = (seek + op[7]);
                        //    }
                        //    goto NEXT;

                        //case ILOpCode.Custom_StLoc_s_Ldc_i4_s:
                        //    {
                        //        local[*opv] = (void*)*(op + 3);
                        //    }
                        //    goto NEXT;
                }
            }
        }
    }
}
