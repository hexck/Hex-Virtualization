using System;

namespace Hex.VM.Runtime.Util
{
        public class Value
        {
            public object GetObject() => _obj;
            
            private object _obj;
            public Value(object obj)
            {
                _obj = obj;
            }

            public bool IsByte()
            {
                if (_obj == null) return false;
                return Type.GetTypeCode(_obj.GetType()) == TypeCode.Byte;
            }

            public bool IsSByte()
            {
                if (_obj == null) return false;
                return Type.GetTypeCode(_obj.GetType()) == TypeCode.SByte;
            }

            public bool IsUInt16()
            {
                if (_obj == null) return false;
                return Type.GetTypeCode(_obj.GetType()) == TypeCode.UInt16;
            }

            public bool IsUInt32()
            {
                if (_obj == null) return false;
                return Type.GetTypeCode(_obj.GetType()) == TypeCode.UInt32;
            }

            public bool IsUInt64()
            {
                if (_obj == null) return false;
                return Type.GetTypeCode(_obj.GetType()) == TypeCode.UInt64;
            }

            public bool IsInt16()
            {
                if (_obj == null) return false;
                return Type.GetTypeCode(_obj.GetType()) == TypeCode.Int16;
            }
            
            public bool Same(Value other)
            {
                if (_obj == null) return false;
                return Type.GetTypeCode(_obj.GetType()) == Type.GetTypeCode(other.GetObject().GetType());
            }

            public bool IsInt32()
            {
                if (_obj == null) return false;
                return Type.GetTypeCode(_obj.GetType()) == TypeCode.Int32;
            }

            public bool IsInt64()
            {
                if (_obj == null) return false;
                return Type.GetTypeCode(_obj.GetType()) == TypeCode.Int64;
            }

            public bool IsDecimal()
            {
                if (_obj == null) return false;
                return Type.GetTypeCode(_obj.GetType()) == TypeCode.Decimal;
            }

            public bool IsDouble()
            {
                if (_obj == null) return false;
                return Type.GetTypeCode(_obj.GetType()) == TypeCode.Double;
            }
            
            public bool IsBool()
            {
                if (_obj == null) return false;
                try
                {
                    var b = (bool) _obj;
                    return true;
                }
                catch
                {
                    // ignored
                }

                return false;
            }

            public bool IsString()
            {
                if (_obj == null) return false;
                return Type.GetTypeCode(_obj.GetType()) == TypeCode.String;
            }

            public bool IsNull()
            {
                return _obj == null;
            }
            
            public bool IsFloat()
            {
                if (_obj == null) return false;
                return Type.GetTypeCode(_obj.GetType()) == TypeCode.Single;
            }

            public bool IsNumeric()
            {
                if (_obj == null) return false;
                switch (Type.GetTypeCode(_obj.GetType()))
                {
                    case TypeCode.Byte:
                    case TypeCode.SByte:
                    case TypeCode.UInt16:
                    case TypeCode.UInt32:
                    case TypeCode.UInt64:
                    case TypeCode.Int16:
                    case TypeCode.Int32:
                    case TypeCode.Int64:
                    case TypeCode.Decimal:
                    case TypeCode.Double:
                    case TypeCode.Single:
                        return true;
                    default:
                        return false;
                }
            }
        }
}