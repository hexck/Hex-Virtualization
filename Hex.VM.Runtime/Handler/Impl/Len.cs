using System;
using Hex.VM.Runtime.Util;

namespace Hex.VM.Runtime.Handler.Impl
{
            
    public class Len : HxOpCode
    {
        public override void Execute(Context vmContext, HxInstruction instruction)
        {
            var arr = (Array)vmContext.Stack.Pop().GetObject();
            vmContext.Stack.Push(arr.Length);
                
            vmContext.Index++;
        }
    }
}