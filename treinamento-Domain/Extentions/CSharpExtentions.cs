using Newtonsoft.Json;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace treinamento_Domain.Extentions
{
    public static class CSharpExtentions
    {
        #region Convert 


        public static bool IsNumeric(this object valor)
        {
            try
            {
                Convert.ToInt64(valor);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool ToBoolean(this object _obj)
        {
            try
            {
                return Convert.ToBoolean(_obj);
            }
            catch
            {
                return false;
            }
        }

        public static DateTime ToDateTime(this object _obj)
        {
            try
            {
                if (_obj == null)
                {
                    return DateTime.MinValue;
                }
                else
                {
                    return Convert.ToDateTime(_obj);
                }
            }
            catch
            {
                return DateTime.MinValue;
            }
        }

        public static DateTime? ToNDateTime(this object _obj)
        {
            try
            {
                if (_obj == null)
                {
                    return null;
                }
                else
                {
                    return Convert.ToDateTime(_obj);
                }
            }
            catch
            {
                return null;
            }
        }

        public static decimal ToDecimal(this object _obj)
        {
            try
            {
                if (_obj == null)
                {
                    return 0;
                }
                else
                {
                    return Convert.ToDecimal(_obj);
                }
            }
            catch
            {
                return 0;
            }
        }

        public static double ToDouble(this object _obj)
        {
            try
            {
                if (_obj == null)
                {
                    return 0;
                }
                else
                {
                    return Convert.ToDouble(_obj);
                }
            }
            catch
            {
                return 0;
            }
        }

        public static byte ToByte(this object _obj)
        {
            try
            {
                if (_obj == null)
                {
                    return 0;
                }
                else
                {
                    return Convert.ToByte(_obj);
                }
            }
            catch
            {
                return 0;
            }
        }
        public static byte? ToNByte(this object _obj)
        {
            try
            {
                if (_obj == null)
                {
                    return null;
                }
                else
                {
                    return Convert.ToByte(_obj);
                }
            }
            catch
            {
                return null;
            }
        }

        public static bool? ToNBoolean(this object _obj)
        {
            try
            {
                if (_obj == null ||
                   !(_obj.ToString().Equals("0") ||
                    _obj.ToString().Equals("1")))
                {
                    return null;
                }
                else
                {
                    return Convert.ToBoolean(_obj);
                }
            }
            catch
            {
                return null;
            }
        }

        public static byte ToByte16(this string _obj, int fromBase = 16)
        {
            try
            {
                if (_obj == null)
                {
                    return 0;
                }
                else
                {
                    return Convert.ToByte(_obj, fromBase);
                }
            }
            catch
            {
                return 0;
            }
        }
        public static short ToInt16(this object _obj)
        {
            try
            {
                if (_obj == null)
                {
                    return 0;
                }
                else
                {
                    return Convert.ToInt16(_obj);
                }
            }
            catch
            {
                return 0;
            }
        }

        public static ushort ToUInt16(this object _obj)
        {
            try
            {
                if (_obj == null)
                {
                    return 0;
                }
                else
                {
                    return Convert.ToUInt16(_obj);
                }
            }
            catch
            {
                return 0;
            }
        }

        public static short? ToNInt16(this object _obj)
        {
            try
            {
                if (_obj == null)
                {
                    return null;
                }
                else
                {
                    return Convert.ToInt16(_obj);
                }
            }
            catch
            {
                return null;
            }
        }

        public static int ToInt32(this object _obj)
        {
            try
            {
                if (_obj == null)
                {
                    return 0;
                }
                else
                {
                    return Convert.ToInt32(_obj);
                }
            }
            catch
            {
                return 0;
            }
        }

        public static int? ToNInt32(this object _obj)
        {
            try
            {
                if (_obj == null)
                {
                    return null;
                }
                else
                {
                    return Convert.ToInt32(_obj);
                }
            }
            catch
            {
                return null;
            }
        }

        public static uint ToUInt32(this object _obj)
        {
            try
            {
                if (_obj == null)
                {
                    return 0;
                }
                else
                {
                    return Convert.ToUInt32(_obj);
                }
            }
            catch
            {
                return 0;
            }
        }

        public static long ToInt64(this object _obj)
        {
            try
            {
                if (_obj == null)
                {
                    return 0;
                }
                else
                {
                    return Convert.ToInt64(_obj);
                }
            }
            catch
            {
                return 0;
            }
        }

        public static long? ToNInt64(this object _obj)
        {
            try
            {
                if (_obj == null)
                {
                    return null;
                }
                else
                {
                    return Convert.ToInt64(_obj);
                }
            }
            catch
            {
                return null;
            }
        }

        public static ulong ToUInt64(this object _obj)
        {
            try
            {
                if (_obj == null)
                {
                    return 0;
                }
                else
                {
                    return Convert.ToUInt64(_obj);
                }
            }
            catch
            {
                return 0;
            }
        }

        public static char ToChar(this object _obj)
        {
            try
            {
                if (_obj == null)
                {
                    return char.MinValue;
                }
                else
                {
                    return Convert.ToChar(_obj);
                }
            }
            catch
            {
                return char.MinValue;
            }
        }
        public static float ToSingle(this object _obj)
        {
            try
            {
                if (_obj == null)
                {
                    return float.NaN;
                }
                else
                {
                    return Convert.ToSingle(_obj);
                }
            }
            catch
            {
                return float.NaN;
            }
        }

        public static T Clone<T>(this T source)
        {
            if (!typeof(T).IsSerializable)
            {
                throw new ArgumentException("The type must be serializable.", "source");
            }

            if (Object.ReferenceEquals(source, null))
            {
                return default(T);
            }

            using (MemoryStream stream = new MemoryStream())
            {
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, source);
                stream.Seek(0, SeekOrigin.Begin);

                return (T)formatter.Deserialize(stream);
            }
        }

        public static string Serialize(this object _obj)
        {
            try
            {
                if (_obj is null)
                {
                    return string.Empty;
                }
                else
                {
                    return JsonConvert.SerializeObject(_obj);
                }
            }
            catch
            {
                return string.Empty;
            }
        }

        public static T Deserialize<T>(this string _obj)
        {
            if (_obj is null || string.IsNullOrWhiteSpace(_obj))
                return (T)(object)null;

            return JsonConvert.DeserializeObject<T>(_obj);
        }
        #endregion Convert
    }
}
