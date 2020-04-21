using System;
using System.Reflection.Emit;
using Hex.VM.Runtime.Util;

namespace Hex.VM.Runtime.Handler.Impl
{
    public class Box : HxOpCode
    {
        public override void Execute(Context vmContext, HxInstruction instruction)
        {
            // hmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm

            vmContext.Index++;
        }
    }
}