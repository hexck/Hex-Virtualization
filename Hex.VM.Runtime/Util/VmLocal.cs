using System.Collections.Generic;

namespace Hex.VM.Runtime.Util
{
    public class VmLocal
    {
        public Dictionary<int, Value> Vars { get; }

        public VmLocal()
        {
            Vars = new Dictionary<int, Value>();
            for(var i = 0;i<50; i++)
                Vars.Add(i, null);
        }

        public void Update(int index, Value value)
        {
            Vars[index] = value;
        }

        public Value Get(int index)
        {
            return !Vars.ContainsKey(index) ? null : Vars[index];
        }
    }
}