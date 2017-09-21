using SXC.Core.Data;
using SXC.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SXC.Services.Business
{
    public class IntegralBus : BusinessBase
    {
        private string _message;

        private string _error;

        private bool _isSave;

        public string Message {
            get
            {
                return _message;
            }
        }

        public string Error
        {
            get
            {
                return _error;
            }
        }

        public IntegralBus(SxcDbContext DbContext)
            : base(DbContext)
        {
            _isSave = true;
        }

        public IntegralRecord SaveRecord(IntegralRecord record)
        {
            try
            {
                if(_isSave)
                {
                    record.UserIntegral.CurrentPoints += record.Points;
                    
                    if (record.Points > 0)
                    {
                        record.UserIntegral.IntegralUserActivitys.FirstOrDefault(t => t.IntegralActivity == record.IntegralActivity).TotalPoints += record.Points;
                        record.UserIntegral.TotalPoints += record.Points;
                        record.ValidPoints = record.Points;
                    }

                    if (record.Points < 0)
                    {
                        record.UserIntegral.TotalExpense += (-record.Points);

                        IntegralRecordsExpense(record.UserIntegral, record.Points);
                    }

                    record.TotalPoints = record.UserIntegral.TotalPoints;

                    record.CurrentPoints = record.UserIntegral.CurrentPoints;

                    record = _context.IntegralRecords.Add(record);

                    //_context.SaveChanges();//不允许启动新事务，因为有其他线程正在该会话中运行 外部可能还在循环中
                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return record;
        }

        public int GetDailySignInPoints(UserIntegral ui, IntegralActivity activity)
        {
            int pointmark = 0;
            int points = 0;

            var rule = activity.IntegralRule;

            var usersign = _context.IntegralSignIns.FirstOrDefault(t => t.UserIntegral.ID == ui.ID);

            if (usersign == null)
            {
                usersign = new IntegralSignIn
                {
                    UserIntegral = ui
                };

                usersign = _context.IntegralSignIns.Add(usersign);
            }

            if (usersign.LastTime == null)
            {
                usersign.DurationDays = 1;
                pointmark = 1;
            }
            else
            {
                if (usersign.LastTime.Value.Date == DateTime.Now.Date)
                {
                    _message = "请勿重复签到";
                    _isSave = false;
                    pointmark = 0;
                    return pointmark;
                }
                else
                {
                    if (rule.CycleType == "Month" && DateTime.Now.Day == 1)
                    {
                        usersign.DurationDays = 1;
                        pointmark = 1;
                    }
                    else
                    {
                        if (usersign.LastTime.Value.AddDays(1).Date == DateTime.Now.Date)
                        {
                            usersign.DurationDays += 1;
                            pointmark = usersign.DurationDays;
                        }
                        else
                        {
                            usersign.DurationDays = 1;
                            pointmark = 1;
                        }
                    }
                }
            }

            usersign.LastTime = DateTime.Now;

            if (pointmark != 0)
            {
                points = rule.StepPoints.Value * pointmark;

                if (rule.MaxPoints != null)
                {
                    points = points > rule.MaxPoints.Value ? rule.MaxPoints.Value : points;
                }
            }

            return points;
        }

        public IntegralRecord ShareEntrance(Guid authid, User newuser)
        {
            return null;
            //try
            //{
            //    var ui = _context.UserIntegrals.FirstOrDefault(t => t.User.AuthID == authid);

            //    return IntegralProcess(ui, "分享有礼", newuser);
            //}
            //catch (Exception ex)
            //{
            //}
        }

        public IntegralRecord IntegralProcess(UserIntegral ui, string activityname, dynamic extdata)
        {
            try
            {
                var activity = _context.IntegralActivitys.FirstOrDefault(t => t.Name == activityname);

                var rule = activity.IntegralRule;

                var useractivity = ui.IntegralUserActivitys.FirstOrDefault(t => t.IntegralActivityID == activity.ID);

                if (useractivity == null)
                {
                    //_context.IntegralUserActivitys.

                    ui.IntegralUserActivitys.Add(new IntegralUserActivity
                    {
                        IntegralActivity = activity
                    });
                }


                IntegralRecord res = new IntegralRecord
                {
                    IntegralActivity = activity,
                    UserIntegral = ui,
                    ShortMark = activityname,
                    Points = GetPoints(ui, activity, extdata)
                    //Content = BuildRecordContent(activityname, extdata)
                };

                return SaveRecord(res);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private int GetPoints(UserIntegral ui, IntegralActivity activity, dynamic extdata)
        {
            int res = 0;
            if (activity.Name == "每日签到")
            {
                return GetDailySignInPoints(ui, activity);
            }
            else
            {
                var rule = activity.IntegralRule;
                switch (rule.Type)
                {
                    case 1://单次获得积分
                        res = rule.Points.Value;
                        break;
                    case 2://外部获取积分
                        res = extdata.Points;
                        break;
                    case 3://持续获得积分
                        break;
                    case 4://外部消耗积分
                        res = extdata.Points;
                        break;
                    default:
                        break;
                }
            }

            return res;
        }

        private void IntegralRecordsExpense(UserIntegral ui, int Points)
        {
            var ps = -Points;
            var list=_context.IntegralRecords.Where(t=>t.UserIntegral.ID==ui.ID&&t.ValidPoints>0&&ui.IsValid==true).OrderBy(t=>t.ExpiredTime).ThenBy(t=>t.RecordTime);

            foreach (var r in list)
            {
                if (r.ValidPoints >= ps)
                {
                    r.ValidPoints = r.ValidPoints - ps;
                    r.ExpensePoints = r.ExpensePoints + ps;
                    ps = 0;
                    break;
                }
                else
                {
                    ps = ps - r.ValidPoints;
                    r.ExpensePoints = r.ExpensePoints + r.ValidPoints;
                    r.ValidPoints = 0;
                }
            }

            if (ps > 0)
            {
                _isSave = false;
                _error = "积分数据异常";
                _message = "积分兑换失败";
            }

        }

        public string BuildRecordContent(string activityname, dynamic extdata)
        {
            try
            {
                string res = string.Empty;

                switch (activityname)
                {
                    case "分享有礼":
                        res = string.Format(@"分享用户：{0}", (extdata as User).AuthID);
                        break;
                    default:
                        break;
                }

                return res;

            }
            catch (Exception)
            {
                
                throw;
            }
        }
    }
}
