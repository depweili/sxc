using SXC.Code.Log;
using SXC.Code.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace SXC.WebApi
{
    public class ApiControllerBase : ApiController
    {
        protected int _cacheabsoluteminutes = ConfigHelper.GetConfigInt("CacheAbsoluteMinutes", 5);
        protected int _cacheslidingminutes = ConfigHelper.GetConfigInt("CacheSlidingMinutes", 2);
    }
}