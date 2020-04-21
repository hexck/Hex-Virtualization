using System;
using System.Linq;
using System.Reflection;
using Hex.VM.Runtime.Util;

namespace Hex.VM.Runtime.Handler.Impl.Custom
{
    public class HxFld : HxOpCode
    {
        public override void Execute(Context vmContext, HxInstruction instruction)
        {
            var str = (string) instruction.Operand.GetObject();

            var id = Helper.ReadPrefix(str);
            var mdtoken = int.Parse(str.Substring(1));

            var fi = ForceResolveField(mdtoken);
            var v = fi.GetValue(id == 0 ? vmContext.Stack.Pop().GetObject() : null);
            vmContext.Stack.Push(v);
            vmContext.Index++;
        }
    }
}