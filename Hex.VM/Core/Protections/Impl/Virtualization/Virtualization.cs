using System;
using System.Collections.Generic;
using System.Linq;
using dnlib.DotNet;
using dnlib.DotNet.Emit;
using Hex.VM.Core.Helper;
using Hex.VM.Runtime;
using Hex.VM.Runtime.Util;

// github.com/hexck
namespace Hex.VM.Core.Protections.Impl.Virtualization
{
	public class Virtualization : IProtection
	{
		public MethodDef FindMethod(string name)
		{

			foreach (var type in Context.Instance.Module.Types)
			{
				if (!type.Namespace.StartsWith("Hex.VM.Runtime"))
					continue;
				foreach (var method in type.Methods)
				{
					if (method.Name == name)
						return method;
				}
			}

			return null;
		}
		
		public override void Execute(Context context)
		{

			var runVm = FindMethod("RunVM");
			foreach (var type in context.Module.Types)
			{
				if (type.FullName.StartsWith("Hex.VM.Runtime"))
					continue;
				
				Context.Instance.Log.Information($"Virtualizing type: {type.FullName}");
				
				foreach (var method in type.Methods)
				{
					if (!method.HasBody || method.IsConstructor || method.IsRuntime || method.IsVirtual||method.IsGetter || method.IsSetter)
						continue;
					
					Context.Instance.Log.Information($"Virtualizing method: {method.FullName}");
					
					// method.MDToken.ToInt32().ToString()
				//	string name = method.MDToken.ToInt32().ToString();
					var name = Generator.RandomAlphabet();
					var key = Generator.NextInt(1, 10000);
					
					var conv = new Converter(method, name, key);
					conv.Save();
					
					if (!conv.Compatible)
					{
						Context.Instance.Log.Warning($"Skipped method because of incompatibilities: {method.FullName}");
						continue;
					}
					
					method.Body = new CilBody();
					
					var attributeType = context.Module.Import(typeof(IdAttribute).GetConstructor(new[] { typeof(string), typeof(int) })) as ICustomAttributeType;
					var test = new CustomAttribute(attributeType, new[]
					{
						new CAArgument(context.Module.CorLibTypes.GetTypeRef("System", "String").ToTypeSig(), name),
						new CAArgument(context.Module.CorLibTypes.GetTypeRef("System", "Int32").ToTypeSig(), key),
					});
					
					method.CustomAttributes.Add(test);
					
					//method.Body.Instructions.Add(new Instruction(OpCodes.Ldstr, name));
					method.Body.Instructions.Add(new Instruction(OpCodes.Ldc_I4, method.Parameters.Count));
					method.Body.Instructions.Add(OpCodes.Newarr.ToInstruction(context.Module.CorLibTypes.Object));

					for (var i = 0; i < method.Parameters.Count; i++)
					{
						method.Body.Instructions.Add(new Instruction(OpCodes.Dup));
						method.Body.Instructions.Add(new Instruction(OpCodes.Ldc_I4, i));
						method.Body.Instructions.Add(new Instruction(OpCodes.Ldarg, i));
						method.Body.Instructions.Add(new Instruction(OpCodes.Box, method.Parameters[i].Type.ToTypeDefOrRef()));
						method.Body.Instructions.Add(new Instruction(OpCodes.Stelem_Ref));
					}
					
					method.Body.Instructions.Add(new Instruction(OpCodes.Call, runVm));
					
					
					if (method.HasReturnType)
						method.Body.Instructions.Add(new Instruction(OpCodes.Unbox_Any, method.ReturnType.ToTypeDefOrRef()));
					else
						method.Body.Instructions.Add(OpCodes.Pop.ToInstruction());
					
					method.Body.Instructions.Add(new Instruction(OpCodes.Ret));
					
					method.Body.UpdateInstructionOffsets();
				}
			}
		}
	}
}
