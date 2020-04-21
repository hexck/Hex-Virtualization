using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using dnlib.DotNet;
using dnlib.DotNet.Writer;
using Hex.VM.Core.Helper;
using Serilog;

namespace Hex.VM.Core
{
	public class Engine
	{
		private Context Context { get; set; }

		public void Run(string[] args)
		{
			Context = new Context(args[0]);
			
			Context.Log.Information("Injection phase..");
			var names = InjectHelper.InjectNamespaces(new[]{ "Hex.VM.Runtime", "Hex.VM.Runtime.Handler", "Hex.VM.Runtime.Handler.Impl",  "Hex.VM.Runtime.Handler.Impl.Custom", "Hex.VM.Runtime.Util"});
			foreach(var name in names) 
				Context.Log.Information($"Injected {name}");
			Console.WriteLine();
			Context.Log.Information("Virtualization phase..");
			try
			{
				Context.Protections[0].Execute(Context);
			}
			catch (Exception exc)
			{
				Context.Log.Error($"Something went wrong while applying virtualization: {exc.Message}");
			}

			Save(Path.GetFileNameWithoutExtension(args[0])+"-obf.exe");
		}

		private void Save(string sp)
		{
			Console.WriteLine();
			try
			{
				var opts = new ModuleWriterOptions(Context.Module)
				{
					Logger = DummyLogger.NoThrowInstance,
				};
				
				opts.MetaDataOptions.Flags = MetaDataFlags.PreserveAll;
				Context.Module.Write(sp, opts);
				if (File.Exists(sp))
					Context.Log.Information($"Obfuscated file saved as {sp}");
			}
			catch (Exception exc)
			{
				Context.Log.Fatal("Error, could not save: " + exc.Message);
			}

			Console.ReadKey();
		}
	}
}
