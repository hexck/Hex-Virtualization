using Hex.VM.Runtime.Util;

namespace Hex.VM.Runtime.Handler.Impl
{
    
    public class Shl : HxOpCode
    {
        public override void Execute(Context vmContext, HxInstruction instruction)
        {
            var x = vmContext.Stack.Pop();
            var y = vmContext.Stack.Pop();

            if (x.IsInt16())
                vmContext.Stack.Push((short) x.GetObject() << (short) y.GetObject());
            else if (x.IsInt32())
                vmContext.Stack.Push((int) x.GetObject() << (int) y.GetObject());
            else if (x.IsUInt16())
                vmContext.Stack.Push((ushort) x.GetObject() << (ushort) y.GetObject());
            else if (x.IsByte())
                vmContext.Stack.Push((byte) x.GetObject() << (byte) y.GetObject());
            else if (x.IsSByte())
                vmContext.Stack.Push((sbyte) x.GetObject() << (sbyte) y.GetObject());
                
            vmContext.Index++;
        }
    }

}