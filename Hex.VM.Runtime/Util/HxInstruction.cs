using Hex.VM.Runtime.Handler;

namespace Hex.VM.Runtime.Util
{
    public class HxInstruction
    {
        public HxOpCodes OpCode { get; }
        public Value Operand { get; }

        public HxInstruction(HxOpCodes opcode, Value value)
        {
            OpCode = opcode;
            if (value == null)
                Operand = null;
            else
                Operand = value;
        }

        public HxInstruction(HxOpCodes opcode)
        {
            OpCode = opcode;
            Operand = null;
        }
    }
}