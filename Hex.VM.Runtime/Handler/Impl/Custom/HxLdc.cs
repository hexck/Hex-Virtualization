using Hex.VM.Runtime.Util;

namespace Hex.VM.Runtime.Handler.Impl.Custom
{
   
    public class HxLdc : HxOpCode
    {
        public override void Execute(Context vmContext, HxInstruction instruction)
        {
            vmContext.Stack.Push(instruction.Operand);
            vmContext.Index++;
        }
    }

}