using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using dnlib.DotNet;
using dnlib.DotNet.Emit;
using Hex.VM.Runtime.Handler;
using Hex.VM.Runtime.Util;

// github.com/hexck
namespace Hex.VM.Core.Protections.Impl.Virtualization
{
    public class Converter
    {
        public MethodDef Method { get; }
        
        public bool Compatible { get; set; }
        
        public int Key { get; }
        
        public string Name { get; }

        private BinaryWriter _binaryWriter;

        private MemoryStream _memoryStream;

        public Converter(MethodDef methodDef, string name, int key)
        {
            Method = methodDef;
            Name = name;
            Key = key;
            Compatible = true;
            _memoryStream = new MemoryStream();
            _binaryWriter = new BinaryWriter(_memoryStream);
        }
        
        public byte[] ConvertMethod()
        {
            var instrs = AsHxInstructions();

            _binaryWriter.Write(instrs.Count);
            foreach (var i in instrs)
            {
                _binaryWriter.Write((int) i.OpCode);
                _binaryWriter.Write(i.Operand != null);

                if (i.Operand == null) continue;
                
                var value = i.Operand;
                if (value.IsString())
                {
                    _binaryWriter.Write(0);
                    _binaryWriter.Write((string) value.GetObject());
                }
                else if (value.IsInt16())
                {
                    _binaryWriter.Write(1);
                    _binaryWriter.Write((short) value.GetObject());
                }
                else if (value.IsInt32())
                {
                    _binaryWriter.Write(2);
                    _binaryWriter.Write((int) value.GetObject());
                }
                else if (value.IsInt64())
                {
                    _binaryWriter.Write(3);
                    _binaryWriter.Write((long) value.GetObject());
                }
                else if (value.IsUInt16())
                {
                    _binaryWriter.Write(4);
                    _binaryWriter.Write((ushort) value.GetObject());
                }
                else if (value.IsUInt32())
                {
                    _binaryWriter.Write(5);
                    _binaryWriter.Write((uint) value.GetObject());
                }
                else if (value.IsUInt64())
                {
                    _binaryWriter.Write(6);
                    _binaryWriter.Write((ulong) value.GetObject());
                }
                else if (value.IsDouble())
                {
                    _binaryWriter.Write(7);
                    _binaryWriter.Write((double) value.GetObject());
                }
                else if (value.IsDecimal())
                {
                    _binaryWriter.Write(8);
                    _binaryWriter.Write((decimal) value.GetObject());
                }
                else if (value.IsByte())
                {
                    _binaryWriter.Write(9);
                    _binaryWriter.Write((byte) value.GetObject());
                }
                else if (value.IsSByte())
                {
                    _binaryWriter.Write(10);
                    _binaryWriter.Write((sbyte) value.GetObject());
                }
                else if (value.IsFloat())
                {
                    _binaryWriter.Write(11);
                    _binaryWriter.Write((float) value.GetObject());
                }
               else if (value.IsNull())
                {
                    _binaryWriter.Write(12);
                }
               
            }

            return _memoryStream.ToArray();
        }

        public List<HxInstruction> AsHxInstructions()
        {
            Method.Body.OptimizeBranches();
            Method.Body.OptimizeMacros();
            Method.Body.SimplifyBranches();
            Method.Body.SimplifyMacros(Method.Parameters);
            
            var instrs = Method.Body.Instructions.ToList();
            
            var conv = new List<HxInstruction>();
            foreach (var i in instrs)
            {
                var x = Enum.GetNames(typeof(HxOpCodes)).ToList().FirstOrDefault(
                    name => string.Equals(name.Substring(1), i.OpCode.Name, StringComparison.CurrentCultureIgnoreCase));
                

                // simple stuff, no operand
                if (x != null)
                {
                    var opc = (HxOpCodes) Enum.Parse(typeof(HxOpCodes), x, true);
                    conv.Add(new HxInstruction(opc));
                    continue;
                }

                if (i.IsLdcI4() || i.OpCode == OpCodes.Ldc_R4 || i.OpCode == OpCodes.Ldc_R8 ||
                    i.OpCode == OpCodes.Ldc_I8 || i.OpCode == OpCodes.Ldc_I4_M1 || i.OpCode == OpCodes.Ldnull ||
                    i.OpCode == OpCodes.Ldstr)
                {
                    conv.Add(new HxInstruction(HxOpCodes.HxLdc,
                        new Value(i.OpCode == OpCodes.Ldnull ? null : i.Operand)));
                }
                else if (i.OpCode == OpCodes.Ldtoken)
                {
                    // eh string is fine here, we can just do substring later
                    var value = new Value("");
                    if (i.Operand is MethodDef)
                        value = new Value("0" + ((MethodDef) i.Operand).MDToken.ToInt32());
                    else if (i.Operand is MemberRef)
                        value = new Value("1" + ((MemberRef) i.Operand).MDToken.ToInt32());
                    else if (i.Operand is IField)
                        value = new Value("2" + ((IField) i.Operand).MDToken.ToInt32());
                    else if (i.Operand is ITypeDefOrRef)
                        value = new Value("3" + ((ITypeDefOrRef) i.Operand).MDToken.ToInt32());

                    conv.Add(new HxInstruction(HxOpCodes.Ldtoken, value));
                }
                else if (i.OpCode == OpCodes.Call || i.OpCode == OpCodes.Callvirt)
                {
                    var op = (IMethod) i.Operand;
                    if (op.Name == ".ctor" || op.Name == ".cctor")
                        conv.Add(new HxInstruction(HxOpCodes.HxCall, new Value((i.OpCode == OpCodes.Callvirt ? "0":"1") + "0" + op.MDToken.ToInt32())));
                    else if (op.IsMethodDef)
                        conv.Add(new HxInstruction(HxOpCodes.HxCall, new Value((i.OpCode == OpCodes.Callvirt ? "0":"1") +"1" + op.MDToken.ToInt32())));
                    else
                        conv.Add(new HxInstruction(HxOpCodes.HxCall, new Value((i.OpCode == OpCodes.Callvirt ? "0":"1") +"2" + op.MDToken.ToInt32())));
                }
                else if (i.IsBr() || i.IsBrtrue() || i.IsBrfalse())
                {
                    var val = new Value(Method.Body.Instructions.IndexOf((Instruction) i.Operand));
                    if (i.IsBrtrue())
                    {
                        conv.Add(new HxInstruction(HxOpCodes.Brtrue, val));
                    }
                    else if (i.IsBrfalse())
                    {
                        conv.Add(new HxInstruction(HxOpCodes.Brfalse, val));
                    }
                    else
                    {
                        conv.Add(new HxInstruction(HxOpCodes.Br, val));
                    }
                }
                else if (i.OpCode == OpCodes.Box)
                {
                    conv.Add(new HxInstruction(HxOpCodes.Box,
                        new Value(((ITypeDefOrRef) i.Operand).FullName)));
                    //   conv.Add(new HxInstruction(HxOpCodes.Box,  new Value(((ITypeDefOrRef)i.Operand).MDToken.ToInt32())));
                }
                else if (i.OpCode.Name.StartsWith("ldelem") || i.OpCode.Name.StartsWith("stelem"))
                {
                    conv.Add(new HxInstruction(HxOpCodes.HxArray, new Value(i.OpCode.Name.StartsWith("lde") ? 0 : 1)));
                }
                else if (i.IsLdloc() || i.IsStloc())
                {
                    conv.Add(new HxInstruction(HxOpCodes.HxLoc,
                        new Value((i.IsLdloc() ? "0" : "1") + Method.Body.Variables.IndexOf((Local) i.Operand))));
                }
                else if (i.IsLdarg() || i.IsStarg())
                {
                    conv.Add(new HxInstruction(HxOpCodes.HxArg,
                        new Value((i.IsLdarg() ? "0" : "1") + Method.Parameters.IndexOf((Parameter) i.Operand))));
                }
                else if (i.OpCode == OpCodes.Ldfld || i.OpCode == OpCodes.Ldflda || i.OpCode == OpCodes.Ldsfld || i.OpCode == OpCodes.Ldsflda)
                {
                    conv.Add(new HxInstruction(HxOpCodes.HxFld,
                        new Value((i.OpCode.Name.StartsWith("ldf") ? "0" : "1") + ((IField) i.Operand).MDToken.ToInt32() )));
                }
                else if (i.OpCode == OpCodes.Conv_R4)
                {
                    conv.Add(new HxInstruction(HxOpCodes.HxConv,  new Value(0)));
                }
                else if (i.OpCode == OpCodes.Conv_R8)
                {
                    conv.Add(new HxInstruction(HxOpCodes.HxConv,  new Value(1)));
                }
                else if (i.OpCode == OpCodes.Newobj)
                {
                    conv.Add(new HxInstruction(HxOpCodes.Newobj, new Value(((IMethod)i.Operand).MDToken.ToInt32())));
                }
                else
                {
                    Compatible = false;
                    Context.Instance.Log.Warning($"Unsupported opcode: {i.OpCode}");
                }
            }

            return conv;
        }
        
        public void Save()
        {
            Context.Instance.Module.Resources.Add(new EmbeddedResource(Name, ConvertMethod().Select(bb => (byte)(bb  ^ Key)).ToArray() ));
        }
    }
}
