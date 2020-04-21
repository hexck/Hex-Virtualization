using System;

namespace Hex.VM.Runtime.Util
{
    [AttributeUsage(AttributeTargets.Method)]  
    public class IdAttribute : Attribute  
    {  
        public string Id { get; }
        public int Key { get; }
        
        public IdAttribute(string val, int key)  
        {  
            Id = val;
            Key = key;
        }  
    }  
}