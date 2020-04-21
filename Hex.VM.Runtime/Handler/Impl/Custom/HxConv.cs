using System;
using Hex.VM.Runtime.Util;

namespace Hex.VM.Runtime.Handler.Impl.Custom
{
    public class HxConv : HxOpCode
    {
        public override void Execute(Context vmContext, HxInstruction instruction)
        {
            var id = (int) instruction.Operand.GetObject();
            dynamic item = vmContext.Stack.Pop().GetObject();
            // should work
            switch (id)
            {
                case 0: // convert to float   Conv_R4
                    vmContext.Stack.Push(Convert.ToSingle(item));
                    break;
                case 1: // convert to double   Conv_R8
                    vmContext.Stack.Push(Convert.ToDouble(item));
                    break;
                default:
                    break;
            }

            vmContext.Index++;
        }
    }
}