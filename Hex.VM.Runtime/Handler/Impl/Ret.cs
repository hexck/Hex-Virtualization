using System;
using Hex.VM.Runtime.Util;

namespace Hex.VM.Runtime.Handler.Impl
{
   
    public class Ret : HxOpCode
    {
        public override void Execute(Context vmContext, HxInstruction instruction)
        {
            vmContext.Stack.Push(vmContext.Stack.Count == 0 ? new Value(null) : vmContext.Stack.Pop().GetObject());
            vmContext.Index++;
        }
    }
}