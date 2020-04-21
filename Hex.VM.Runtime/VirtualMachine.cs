using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Hex.VM.Runtime.Handler;
using Hex.VM.Runtime.Util;

// github.com/hexck
namespace Hex.VM.Runtime
{
    public class VirtualMachine
    {
        private static byte[] Extract(string resourceName, int key)
        {
            using (var stream = typeof(VirtualMachine).Assembly.GetManifestResourceStream(resourceName))
            {
                var bytes = new byte[stream.Length];
                stream.Read(bytes, 0, bytes.Length);
                return bytes.Select(bb => (byte) (bb ^ key)).ToArray();
            }
        }
        
        public static object RunVM(object[] pm)
        {
            var attr = new StackTrace().GetFrame(1).GetMethod().GetCustomAttributes(true).OfType<IdAttribute>()
                .FirstOrDefault();
            var code = Extract(attr?.Id, attr.Key);
            
            var ms = new MemoryStream(code);
            var br = new BinaryReader(ms);
            var count = br.ReadInt32();
            var instrs = new List<HxInstruction>();
            
            for (var i = 0; i < count; i++)
            {
                var opcode = (HxOpCodes) br.ReadInt32();
                Value operand = null;
                if (br.ReadBoolean()) // has operand
                {
                    var type = br.ReadInt32();

                    switch (type)
                    {
                        case 0:
                            operand = new Value(br.ReadString());
                            break;
                        case 1:
                            operand = new Value(br.ReadInt16());
                            break;
                        case 2:
                            operand = new Value(br.ReadInt32());
                            break;
                        case 3:
                            operand = new Value(br.ReadInt64());
                            break;
                        case 4:
                            operand = new Value(br.ReadUInt16());
                            break;
                        case 5:
                            operand = new Value(br.ReadUInt32());
                            break;
                        case 6:
                            operand = new Value(br.ReadUInt64());
                            break;
                        case 7:
                            operand = new Value(br.ReadDouble());
                            break;
                        case 8:
                            operand = new Value(br.ReadDecimal());
                            break;
                        case 9:
                            operand = new Value(br.ReadByte());
                            break;
                        case 10:
                            operand = new Value(br.ReadSByte());
                            break;
                        case 11:
                            operand = new Value(br.ReadSingle());
                            break;
                        case 12:
                            operand = new Value(null);
                            break;
                    }
                }
                
                // maybe is better to do this in real time as it may be a bad idea to store all of the instructions in an list like this because it would be easier to devirtualize
                instrs.Add(new HxInstruction(opcode, operand));
            }
            
            var ctx = new Context(instrs, pm);
            return ctx.Run().GetObject();
        }
    }
}