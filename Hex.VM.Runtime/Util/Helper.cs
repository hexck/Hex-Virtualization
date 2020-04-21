using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Hex.VM.Runtime.Handler;

namespace Hex.VM.Runtime.Util
{
    public class Helper
    {
        public static object[] GetMethodParameters(Context ctx,  ParameterInfo[] pi)
        {
            if (pi.Length == 0)
                return null;
            
            var ret = new object[pi.Length];

            for (var i = pi.Length - 1; i >= 0; i--)
            {
                var type = pi[i].ParameterType;
                var val = ctx.Stack.Pop().GetObject();
                
                if (type == typeof(bool))
                {
                    ret[i] = Convert.ToBoolean(val);
                }
                else if (type == typeof(short))
                {
                    ret[i] = Convert.ToInt16(val);
                }
                else if (type == typeof(int))
                {
                    ret[i] = Convert.ToInt32(val);
                }
                else if (type == typeof(long))
                {
                    ret[i] = Convert.ToInt64(val);
                }
                else if (type == typeof(ushort))
                {
                    ret[i] = Convert.ToUInt16(val);
                }
                else if (type == typeof(uint))
                {
                    ret[i] = Convert.ToUInt32(val);
                }
                else if (type == typeof(ulong))
                {
                    ret[i] = Convert.ToUInt64(val);
                }
                else if (type == typeof(byte))
                {
                    ret[i] = Convert.ToByte(val);
                }
                else if (type == typeof(sbyte))
                {
                    ret[i] = Convert.ToSByte(val);
                }
                else if (type == typeof(string))
                {
                    ret[i] = Convert.ToString(val);
                }
                else if (type == typeof(double))
                {
                    ret[i] = Convert.ToDouble(val);
                }
                else if (type == typeof(decimal))
                {
                    ret[i] = Convert.ToDecimal(val);
                }
                else if (type == typeof(float))
                {
                    ret[i] = Convert.ToSingle(val);
                }
                else
                {
                    ret[i] = val;
                }
            }

            return ret;
        }
        
        public static int ReadPrefix(string txt)
        {
            return int.Parse(txt[0].ToString());
        }
        
        public static int ReadPrefix(string txt, int idx)
        {
            return int.Parse(txt[idx].ToString());
        }
     
    }
}