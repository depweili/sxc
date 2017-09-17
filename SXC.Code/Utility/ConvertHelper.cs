using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SXC.Code.Utility
{
    public class ConvertHelper
    {
        public static Guid StrToGuid(string str,Guid defValue)
        {
            Guid gv;

            if(Guid.TryParse(str,out gv))
            {
                return gv;
            }

            return defValue;
        }
    }
}
