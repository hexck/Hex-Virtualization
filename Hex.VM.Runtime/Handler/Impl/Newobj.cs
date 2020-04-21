using System;
using Hex.VM.Runtime.Util;

namespace Hex.VM.Runtime.Handler.Impl
{
    public class Newobj : HxOpCode
    {
        public override void Execute(Context vmContext, HxInstruction instruction)
        {
            var m = ForceResolveConstructor((int)instruction.Operand.GetObject());
            var pm = Helper.GetMethodParameters(vmContext, m.GetParameters());
            
            var inst = m.Invoke(pm);
            if (inst != null)
                vmContext.Stack.Push(inst);
            
            vmContext.Index++;
        }
    }
}