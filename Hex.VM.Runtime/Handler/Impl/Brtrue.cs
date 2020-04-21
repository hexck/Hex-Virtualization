using Hex.VM.Runtime.Util;

namespace Hex.VM.Runtime.Handler.Impl
{
    public class Brtrue : HxOpCode
    {
        public override void Execute(Context vmContext, HxInstruction instruction)
        {
            var v = vmContext.Stack.Pop();
            int x;

            if (v.IsBool())
                x = (bool) v.GetObject() ? 1 : 0;
            else
                x = (int)v.GetObject();
            
            if (x == 1)
                vmContext.Index = (int)instruction.Operand.GetObject();
            else
                vmContext.Index++;
        }
    }
}