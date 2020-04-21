using System.Reflection;
using Hex.VM.Runtime.Util;

namespace Hex.VM.Runtime.Handler.Impl
{
    
    public class Ldtoken : HxOpCode
    {
        public override void Execute(Context vmContext, HxInstruction instruction)
        {
            var str = (string) instruction.Operand.GetObject();

            var prefix = Helper.ReadPrefix(str);
            var mdtoken = int.Parse(str.Substring(1));
                
            switch (prefix)
            {
                // method
                case 0:
                    vmContext.Stack.Push(typeof(Ldtoken).Module.ResolveMethod(mdtoken));
                    break;
                // member ref
                case 1:
                    vmContext.Stack.Push(((MethodBase)typeof(Ldtoken).Module.ResolveMember(mdtoken)).MethodHandle);
                    break;
                // ifield
                case 2:
                    vmContext.Stack.Push(typeof(Ldtoken).Module.ResolveType(mdtoken));
                    break;
                // itypedeforref
                case 3:
                    vmContext.Stack.Push(typeof(Ldtoken).Module.ResolveField(mdtoken).FieldHandle);
                    break;
            }

            vmContext.Index++;
        }
    }
}