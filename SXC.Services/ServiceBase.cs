using SXC.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SXC.Services
{
    public interface IServiceBase<T> where T : class
    {
        List<T> GetAll();
        T Get();
    }

    public abstract class ServiceBase
    {
        protected SxcDbContext NewDB()
        {
            return new SxcDbContext();
        }

        protected void Try(Action action)
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                //LogHelper.TryLog("ServiceBase.Try", ex);
            }
        }
    }
}
