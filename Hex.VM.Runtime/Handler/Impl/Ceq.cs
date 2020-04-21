using Hex.VM.Runtime.Util;

namespace Hex.VM.Runtime.Handler.Impl
{
    public class Ceq : HxOpCode
    {
        public override void Execute(Context vmContext, HxInstruction instruction)
        {
            var y = vmContext.Stack.Pop().GetObject();
            var x = vmContext.Stack.Pop().GetObject();
            vmContext.Stack.Push(y.Equals(x));
                
            vmContext.Index++;
        }
    }
}