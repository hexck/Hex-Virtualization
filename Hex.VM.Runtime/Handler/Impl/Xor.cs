using Hex.VM.Runtime.Util;

namespace Hex.VM.Runtime.Handler.Impl
{
    
    public class Xor : HxOpCode
    {
        public override void Execute(Context vmContext, HxInstruction instruction)
        {
            var y = vmContext.Stack.Pop();
            var x = vmContext.Stack.Pop();

            if (x.IsInt16())
                vmContext.Stack.Push((short) x.GetObject() ^ (short) y.GetObject());
            else if (x.IsInt32())
                vmContext.Stack.Push((int) x.GetObject() ^ (int) y.GetObject());
            else if (x.IsInt64())
                vmContext.Stack.Push((long) x.GetObject() ^ (long) y.GetObject());
            else if (x.IsUInt16())
                vmContext.Stack.Push((ushort) x.GetObject() ^ (ushort) y.GetObject());
            else if (x.IsUInt32())
                vmContext.Stack.Push((uint) x.GetObject() ^ (uint) y.GetObject());
            else if (x.IsUInt64())
                vmContext.Stack.Push((ulong) x.GetObject() ^ (ulong) y.GetObject());
            else if (x.IsByte())
                vmContext.Stack.Push((byte) x.GetObject() ^ (byte) y.GetObject());
            else if (x.IsSByte())
                vmContext.Stack.Push((sbyte) x.GetObject() ^ (sbyte) y.GetObject());

                
            vmContext.Index++;
        }
    }
}