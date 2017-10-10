using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SXC.Code.Log
{
    public class LogHelper
    {
        private static readonly Log logerror = LogFactory.GetLogger("logerror");
        private static readonly Log loginfo = LogFactory.GetLogger("loginfo");
        private static readonly Log logmonitor = LogFactory.GetLogger("logmonitor");

        public static void Error(string ErrorMsg, Exception ex = null)
        {
            logerror.Error(ErrorMsg, ex);
        }

        public static void Info(string Msg)
        {
            loginfo.Info(Msg);
        }

        public static void Monitor(string Msg)
        {
            logmonitor.Info(Msg);
        }
    }
}
