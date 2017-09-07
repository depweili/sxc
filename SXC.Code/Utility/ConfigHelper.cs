using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SXC.Code.Utility
{
    public class ConfigHelper
    {
        public static string GetSetting(string key)
        {
            return GetSetting(key, string.Empty);
        }
        public static string GetSetting(string key, string defalut)
        {
            object value = ConfigurationManager.AppSettings[key];
            if (value == null || value == string.Empty) return defalut;
            return (string)value;
        }
    }
}
