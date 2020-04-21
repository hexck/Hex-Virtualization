using Hex.VM.Runtime.Util;

namespace Hex.VM.Runtime.Handler.Impl
{
    public class Not : HxOpCode
    {
        public override void Execute(Context vmContext, HxInstruction instruction)
        {
            var x = vmContext.Stack.Pop();

            if (x.IsInt16())
                vmContext.Stack.Push(~(short) x.GetObject());
            else if (x.IsInt32())
                vmContext.Stack.Push(~(int) x.GetObject());
            else if (x.IsInt64())
                vmContext.Stack.Push(~(long) x.GetObject());
            else if (x.IsUInt16())
                vmContext.Stack.Push(~(ushort) x.GetObject());
            else if (x.IsUInt32())
                vmContext.Stack.Push(~(uint) x.GetObject());
            else if (x.IsUInt64())
                vmContext.Stack.Push(~(ulong) x.GetObject());
            else if (x.IsByte())
                vmContext.Stack.Push(~(byte) x.GetObject());
            else if (x.IsSByte())
                vmContext.Stack.Push(~(sbyte) x.GetObject());

            vmContext.Index++;
        }
    }
}