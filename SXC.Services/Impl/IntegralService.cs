using SXC.Services.Business;
using SXC.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SXC.Services.Impl
{
    public class IntegralService : ServiceBase
    {
        public int? IntNull = null;

        public IntegralDto DailySignIn(Guid uid)
        {
            string res = string.Empty;
            using (var db = base.NewDB())
            {
                var dbitem = db.Users.FirstOrDefault(t => t.IsValid == true && t.AuthID == uid);

                if (dbitem == null)
                {
                    throw new Exception("用户异常");
                }

                var bus = new IntegralBus(db);
                var data = bus.IntegralProcess(dbitem.UserIntegral,"每日签到",null);

                var ir = new IntegralRecordDto
                {
                    points = data.Points,
                    totalpoints = data.UserIntegral.TotalPoints,
                    currentpoints = data.UserIntegral.CurrentPoints,

                };

                var integraldto = new IntegralDto {
                    message = bus.Message,
                    detail = new IntegralRecordDto
                    {
                        points = data.Points,
                        totalpoints = data.UserIntegral.TotalPoints,
                        currentpoints = data.UserIntegral.CurrentPoints,

                    }

                };

                return integraldto;
            }
        }


    }
}
