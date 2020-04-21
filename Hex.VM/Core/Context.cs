using System;
using System.Collections.Generic;
using dnlib.DotNet;
using Hex.VM.Core.Helper;
using Hex.VM.Core.Protections;
using Hex.VM.Core.Protections.Impl.Virtualization;
using Serilog;
using Serilog.Core;
using Serilog.Sinks.SystemConsole.Themes;

namespace Hex.VM.Core
{
    public class Context
    {
        public static Context Instance { get; private set; }

        public ModuleDefMD Module { get; }

        public List<IProtection> Protections { get; }
        
        public Logger Log { get; }

        public Context(string name)
        {
            Log = new LoggerConfiguration()
                .WriteTo.Console(theme: AnsiConsoleTheme.Code)
                .CreateLogger();
            
            Instance = this;
            Module = ModuleDefMD.Load(name);
            Protections = new List<IProtection>()
            {
                new Virtualization()
            };
        }
    }
}
