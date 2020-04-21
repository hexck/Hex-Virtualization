using System.Collections.Generic;
using System.Linq;

namespace Hex.VM.Runtime.Util
{
    public class VmStack
    {
        public Stack<Value> Stack { get; }
        
        public int Count => Stack.Count;

        public VmStack()
        {
            Stack = new Stack<Value>();
        }

        public void Push(object obj) => Stack.Push(new Value(obj));

        public void Push(Value value) => Stack.Push(value);

        public Value Get(int i) => Stack.ToList()[i];
        public Value Peek() => Stack.Peek();
        public Value Pop() => Stack.Pop();
    }
}