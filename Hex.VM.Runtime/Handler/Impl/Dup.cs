using Hex.VM.Runtime.Util;

namespace Hex.VM.Runtime.Handler.Impl
{
    public class Dup : HxOpCode
    {
        public override void Execute(Context vmContext, HxInstruction instruction)
        {
            vmContext.Stack.Push(vmContext.Stack.Peek());
            vmContext.Index++;
        }
    }
}