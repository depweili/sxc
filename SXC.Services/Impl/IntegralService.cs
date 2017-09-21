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

        public UserIntegralDto GetUserIntegral(Guid authid)
        {
            using (var db = base.NewDB())
            {
                var dbitem = db.Users.FirstOrDefault(t => t.IsValid == true && t.AuthID == authid);

                if (dbitem == null)
                {
                    throw new Exception("用户异常");
                }

                var ui = dbitem.UserIntegral;

                var uidto = new UserIntegralDto
                {
                    integralid = ui.IntegralID,
                    gradetitle = ui.IntegralGrade.Title,
                    totalpoints = ui.TotalPoints,
                    currentpoints = ui.CurrentPoints,
                    totalexpense = ui.TotalExpense
                };

                return uidto;
            }
        }

        public IntegralActionResultDto DailySignIn(Guid uid)
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

                db.SaveChanges();

                var integraldto = new IntegralActionResultDto
                {
                    message = bus.Message,
                    detail = new IntegralChangeDto
                    {
                        points = data.Points,
                        totalpoints = data.UserIntegral.TotalPoints,
                        currentpoints = data.UserIntegral.CurrentPoints
                    }

                };

                return integraldto;
            }
        }


        public List<IntegralRecordDto> GetIntegralRecords(Guid uid)
        {
            using (var db = base.NewDB())
            {

                var dblist = db.IntegralRecords.Where(t => t.UserIntegral.IntegralID == uid).OrderByDescending(t => t.RecordTime);

                var res = new List<IntegralRecordDto>();
                foreach (var item in dblist)
                {
                    res.Add(new IntegralRecordDto
                    {
                        shortmark = item.ShortMark,
                        points = item.Points,
                        recordtime = item.RecordTime
                    });
                }
                return res;
            }
        }
    }
}
