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
        public static string GetConfig(string key)
        {
            return GetConfig(key, string.Empty);
        }
        public static string GetConfig(string key, string defalut)
        {
            object value = ConfigurationManager.AppSettings[key];
            if (value == null || value == string.Empty) return defalut;
            return (string)value;
        }

        public static bool GetConfigBool(string key, bool defaultvalue = false)
        {
            bool result = defaultvalue;
            string cfgVal = GetConfig(key);
            if (null != cfgVal && string.Empty != cfgVal)
            {
                try
                {
                    result = bool.Parse(cfgVal);
                }
                catch (FormatException)
                {
                    // Ignore format exceptions.
                }
            }
            return result;
        }

        public static decimal GetConfigDecimal(string key, decimal defaultvalue = 0)
        {
            decimal result = defaultvalue;
            string cfgVal = GetConfig(key);
            if (null != cfgVal && string.Empty != cfgVal)
            {
                try
                {
                    result = decimal.Parse(cfgVal);
                }
                catch (FormatException)
                {
                    // Ignore format exceptions.
                }
            }

            return result;
        }

        public static int GetConfigInt(string key, int defaultvalue = 0)
        {
            int result = defaultvalue;
            string cfgVal = GetConfig(key);
            if (null != cfgVal && string.Empty != cfgVal)
            {
                try
                {
                    result = int.Parse(cfgVal);
                }
                catch (FormatException)
                {
                    // Ignore format exceptions.
                }
            }

            return result;
        }


    }
}
