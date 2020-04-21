using Hex.VM.Runtime.Util;

namespace Hex.VM.Runtime.Handler.Impl
{
    public class Nop : HxOpCode
    {
        public override void Execute(Context vmContext, HxInstruction instruction)
        {
            // do nothing really
            vmContext.Index++;
        }
    }
}