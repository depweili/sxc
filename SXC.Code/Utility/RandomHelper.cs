using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SXC.Code.Utility
{
    public class RandomHelper
    {
        private static object _lock = new object();
        private static int count = 1;

        /// <summary>
        /// yyyyMMddHHmmss+0000
        /// </summary>
        /// <returns></returns>
        public static string GetTimeRandom1()
        {
            lock (_lock)
            {
                if (count >= 9999)
                {
                    count = 1;
                }
                var number = DateTime.Now.ToString("yyyyMMddHHmmss") + count.ToString("0000");
                count++;
                return number;
            }
        }
    }
}
