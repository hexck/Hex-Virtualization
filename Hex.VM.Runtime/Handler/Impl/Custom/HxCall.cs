using System;
using System.Linq;
using System.Reflection;
using Hex.VM.Runtime.Util;

namespace Hex.VM.Runtime.Handler.Impl.Custom
{
    public class HxCall : HxOpCode
    {
        public override void Execute(Context vmContext, HxInstruction instruction)
        {
            var str = (string) instruction.Operand.GetObject();

            var type = Helper.ReadPrefix(str);
            var prefix = Helper.ReadPrefix(str, 1);
            var mdtoken = int.Parse(str.Substring(2));
            switch (prefix)
            {
                // constructor 
                case 0:
                {
                    var m = ForceResolveConstructor(mdtoken);
                    var pm = Helper.GetMethodParameters(vmContext, m.GetParameters());
                    var inst = m.Invoke(pm);

                    if (inst != null)
                        vmContext.Stack.Push(inst);
                    break;
                }

                //  method
                case 1:
                {
                    var m = ForceResolveMethod(mdtoken);
                    var pm = Helper.GetMethodParameters(vmContext, m.GetParameters());

                    object target = null;
                    if (!m.IsStatic)
                        target =vmContext.Stack.Pop().GetObject();
                    
                    var ret = m.Invoke(target, pm);
                    
                    if (ret != null)
                        vmContext.Stack.Push(ret);

                    break;
                }

                // member ref
                case 2:
                {
                    var m = ForceResolveMember(mdtoken);
                    var pm = Helper.GetMethodParameters(vmContext, m.GetParameters());
                    
                    object target = null;
                    if (!m.IsStatic)
                        target = vmContext.Stack.Pop().GetObject();

                    var ret = m.Invoke(target, pm);
                    
                    if (ret != null)
                        vmContext.Stack.Push(ret);

                    break;
                }
            }

            vmContext.Index++;
        }
    }
}