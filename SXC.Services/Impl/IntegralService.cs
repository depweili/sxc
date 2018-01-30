using SXC.Core.Models;
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


        public LotteryDto GetLottery1(Guid uid, int type)
        {
            using (var db = base.NewDB())
            {
                var list = new List<PrizeDto> { 
                        new PrizeDto{ id=1,imgurl="https://www.hexieyinan.com/SxcWebApi/Images/Prize/1.jpg",name="奖品1"},
                        new PrizeDto{ id=2,imgurl="https://www.hexieyinan.com/SxcWebApi/Images/Prize/2.jpg",name="奖品2"},
                        new PrizeDto{ id=3,imgurl="https://www.hexieyinan.com/SxcWebApi/Images/Prize/3.jpg",name="奖品3"},
                        new PrizeDto{ id=4,imgurl="https://www.hexieyinan.com/SxcWebApi/Images/Prize/4.jpg",name="奖品4"},
                        new PrizeDto{ id=5,imgurl="https://www.hexieyinan.com/SxcWebApi/Images/Prize/5.jpg",name="奖品5"},
                        new PrizeDto{ id=6,imgurl="https://www.hexieyinan.com/SxcWebApi/Images/Prize/6.jpg",name="奖品6"},
                        new PrizeDto{ id=7,imgurl="https://www.hexieyinan.com/SxcWebApi/Images/Prize/7.jpg",name="奖品7"},
                        new PrizeDto{ id=8,imgurl="https://www.hexieyinan.com/SxcWebApi/Images/Prize/8.jpg",name="奖品8"},
                    };

                var res = new LotteryDto
                {
                    id = 1,
                    begintime = DateTime.Now.Date,
                    endtime = DateTime.Now.Date.AddDays(1).AddSeconds(-1),
                    chance = 3,
                    isvalid = true,
                    prizes = list.OrderBy(t=>Guid.NewGuid()).ToList()
                };
                return res;
            }
        }

        public LotteryDto GetLottery(Guid uid, int type)
        {
            using (var db = base.NewDB())
            {
                var bus = new IntegralBus(db);

                var dblottery = db.Lotterys.Single(t => t.IsValid == true && t.Type == type);

                var dbuser = db.Users.Single(t => t.AuthID == uid);

                var dbul = bus.GetUserLottery(dbuser, dblottery);

                db.SaveChanges();

                var res = new LotteryDto
                {
                    id = dbul.LotteryID,
                    begintime = DateTime.Now.Date,
                    endtime = DateTime.Now.Date.AddDays(1).AddSeconds(-1),
                    costpoints = dblottery.CostPoints,
                    chance = dbul.Chance,
                    isvalid = dbul.IsValid,
                    prizes = GetPrizeDtos(dblottery)
                };
                return res;
            }
        }

        public List<PrizeDto> GetPrizeDtos(Lottery lo)
        {
            var list = lo.Prizes.Where(t => t.IsValid == true).Select(t => new { t.ID, t.Image, t.Name }).ToList();

            var res = new List<PrizeDto>();

            foreach (var p in list)
            {
                res.Add(new PrizeDto
                {
                    id = p.ID,
                    name = p.Name,
                    imgurl = Function.GetStaticPicUrl(p.Image, "Prize")
                });
            }

            return res.OrderBy(t => Guid.NewGuid()).ToList();
        }

        public WinPrizeDto GetWinPrize(Guid uid,int lotteryid)
        {
            using (var db = base.NewDB())
            {
                var bus = new IntegralBus(db);

                var res = new WinPrizeDto();

                var dbuser = db.Users.Single(t => t.AuthID == uid);

                var dbui = dbuser.UserIntegral;

                var dblo = db.Lotterys.Single(t => t.ID == lotteryid);

                var dbul = db.UserLotterys.Single(t => t.UserID == dbuser.ID && t.LotteryID == lotteryid);

                if (dbui.CurrentPoints < dblo.CostPoints)
                {
                    res.remainingchance = dbul.Chance;
                    res.prize = null;
                    res.message = "您的积分不足";

                    return res;

                    //throw new Exception("您的积分不足");
                }

                if (dbul.Chance > 0)
                {
                    var busres = bus.GetWinPrize(dbul);

                    var winprize = busres.detail;

                    //按照积分记录有效期扣除
                    if (busres.issave)
                    {
                        db.SaveChanges();
                    }
                    else
                    {
                        res.remainingchance = dbul.Chance;
                        res.prize = null;
                        res.message = bus.Message;

                        return res;
                    }

                    res.remainingchance = dbul.Chance;

                    if (winprize != null)
                    {
                        res.prize = new PrizeDto
                        {
                            id = winprize.ID,
                            name = winprize.Name
                        };
                        res.message = string.Empty;
                    }
                    else
                    {
                        res.prize = null;
                        res.message = "未中奖";
                    }
                    
                }
                else
                {
                    res.remainingchance = 0;
                    res.prize = null;
                    res.message = "您的机会已经用完";
                }

                return res;
            }
            

            //if (CheckLotteryChance(uid, lotteryid))
            //{
            //    res.remainingchance = 1;
            //    res.prize = new PrizeDto
            //    {
            //        id = 4,
            //        name = "奖品4"
            //    };
            //    res.message = string.Empty;
            //}
            //else
            //{
            //    res.remainingchance = 0;
            //    res.prize = null;
            //    res.message = "您的机会已经用完";
            //}
            //return res;

        }

        public bool CheckLotteryChance(Guid uid, int lotteryid)
        {
            Random rd = new Random((int)DateTime.Now.Ticks);
            int num = rd.Next(0, 10);
            return num > 5;
        }


    }
}
