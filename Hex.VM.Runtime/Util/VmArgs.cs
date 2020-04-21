using System.Collections.Generic;

namespace Hex.VM.Runtime.Util
{
    public class VmArgs
    {
        public Dictionary<int, Value> Args { get; }

        public VmArgs()
        {
            Args = new Dictionary<int, Value>();
        }

        public VmArgs(object[] pm)
        {
            Args = new Dictionary<int, Value>();
            for (var i = 0; i < pm.Length; i++)
                Args[i] = new Value(pm[i]);
        }

        public void Update(int index, Value value)
        {
            Args[index] = value;
        }

        public Value Get(int index)
        {
            return Args[index];
        }
    }
}