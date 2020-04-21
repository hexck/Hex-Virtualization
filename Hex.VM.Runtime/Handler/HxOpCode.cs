using System;
using System.Linq;
using System.Reflection;
using Hex.VM.Runtime.Util;

// github.com/hexck
namespace Hex.VM.Runtime.Handler
{
    public abstract class HxOpCode
    {
        public abstract void Execute(Context vmContext, HxInstruction instruction);

        //probably useless but lets keep it like this
        protected MethodInfo ForceResolveMethod(int mdtoken)
        {
            foreach(var module in Assembly.GetEntryAssembly().Modules)
            {
                try
                {
                    var mi = (MethodInfo) module.ResolveMethod(mdtoken);
                    return mi;
                }
                catch
                {
                    // ignored
                }
            }

            return null;
        }

        protected ConstructorInfo ForceResolveConstructor(int mdtoken)
        {
            foreach(var module in Assembly.GetEntryAssembly().Modules)
            {
                try
                {
                    var mi = (ConstructorInfo) module.ResolveMethod(mdtoken);
                    return mi;
                }
                catch
                {
                    // ignored
                }
            }

            return null;
        }

        protected MethodBase ForceResolveMember(int mdtoken)
        {
            foreach(var module in Assembly.GetEntryAssembly().Modules)
            {
                try
                {
                    var mi = (MethodBase) module.ResolveMember(mdtoken);
                    return mi;
                }
                catch
                {
                    // ignored
                }
            }

            return null;
        }

        protected FieldInfo ForceResolveField(int mdtoken)
        {
            foreach(var module in Assembly.GetEntryAssembly().Modules)
            {
                try
                {
                    var mi = module.ResolveField(mdtoken);
                    return mi;
                }
                catch
                {
                    // ignored
                }
            }

            return null;
        }
    }
}