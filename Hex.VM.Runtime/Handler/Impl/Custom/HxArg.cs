using System;
using Hex.VM.Runtime.Util;

namespace Hex.VM.Runtime.Handler.Impl.Custom
{
    public class HxArg : HxOpCode
    {
        public override void Execute(Context vmContext, HxInstruction instruction)
        {
            var str = (string) instruction.Operand.GetObject();
            var prefix = Helper.ReadPrefix(str);
            var idx = int.Parse(str.Substring(1));
            if (prefix == 0)
            {
                vmContext.Stack.Push(vmContext.Args.Get(idx));                          
            }
            else
            {
                var item = vmContext.Stack.Pop();
                vmContext.Args.Update(idx, item);
            }
                
            vmContext.Index++;
        }
    }
}