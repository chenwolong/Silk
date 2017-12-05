using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace WeChat.Common
{
    /// <summary>
    /// 枚举的资源名称描述-
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class EnumCode : Attribute
    {
        private string _code;
        private FieldInfo _fieldInfo;

        public EnumCode(string code)
        {
            _code = code;
        }

        public string FieldCode
        {
            get { return _code; }
        }

        public int FieldValue
        {
            get { return (int)_fieldInfo.GetValue(null); }
        }

        public string FieldName
        {
            get { return _fieldInfo.Name; }
        }

        static Dictionary<string, List<EnumCode>> _cached = new Dictionary<string, List<EnumCode>>();

        public static string GetFieldCode(object enumValue)
        {
            List<EnumCode> codes = GetEnumCode(enumValue.GetType());
            foreach (EnumCode code in codes)
            {
                if (code.FieldName == enumValue.ToString())
                {
                    return code.FieldCode;
                }
            }
            return string.Empty;
        }

        public static List<EnumCode> GetEnumCode(Type enumType)
        {
            List<EnumCode> result = null;
            if (!_cached.ContainsKey(enumType.FullName))
            {
                result = new List<EnumCode>();
                FieldInfo[] fields = enumType.GetFields();
                foreach (FieldInfo field in fields)
                {
                    object[] objs = field.GetCustomAttributes(typeof(EnumCode), false);
                    if (objs.Length != 1)
                    {
                        continue;
                    }
                    ((EnumCode)objs[0])._fieldInfo = field;
                    result.Add((EnumCode)objs[0]);
                }
                _cached[enumType.FullName] = result;
            }
            result = _cached[enumType.FullName];
            return result;
        }
    }
}
