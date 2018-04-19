using SXC.Code.Utility;
using SXC.Core.Data;
using SXC.Core.Models;
using SXC.Services.Dto;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityFramework.Extensions;

namespace SXC.Services.Business
{
    public class StoreBus : BusinessBase
    {
        //private StoreResultDto _result;

        public StoreBus(SxcDbContext DbContext)
            : base(DbContext)
        {
        }

        public BusinessResultDto SaveExchangeOrder(OrderInfo orderinfo)
        {
            try
            {
                if(CheckOrder(orderinfo))
                {
                    orderinfo = _context.OrderInfos.Add(orderinfo);

                    var bus = new IntegralBus(_context);

                    dynamic extdata = new ExpandoObject();

                    extdata.Points = -orderinfo.TotalPoints;

                    IntegralRecord ir = bus.IntegralProcess(orderinfo.UserIntegral, "积分兑换", extdata);

                    //orderinfo.OrderIntegralRecords.Add(new OrderIntegralRecord
                    //{
                    //    IntegralRecord = ir
                    //});
                    _context.OrderIntegralRecords.Add(new OrderIntegralRecord
                    {
                        IntegralRecord = ir,
                        OrderInfo = orderinfo
                    });

                    _resultdto.issave = bus.IsSave;
                    _resultdto.message = bus.Message;

                    _resultdto.detail = new
                    {
                        orderinfo.OrderSn,
                        orderinfo.ID,
                        orderinfo.OrderUID
                    };
                }

                return _resultdto;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool CheckOrder(OrderInfo orderinfo)
        {
            bool check = true;
            var ui = orderinfo.UserIntegral;

            if (ui.CurrentPoints < orderinfo.TotalPoints)
            {
                _resultdto.message = "积分不足";
                _resultdto.issave = false;
                return false;
            }

            var checkshort = orderinfo.OrderCommoditys.Where(t => t.Quantity > t.Commodity.Stock).Count();

            if (checkshort > 0)
            {
                _resultdto.message = "商品缺货";
                _resultdto.issave = false;
                return false;
            }
            else
            {
                //_context.Commoditys.Where(t=>orderinfo.OrderCommoditys.Where())
                foreach (var oc in orderinfo.OrderCommoditys)
                {
                    oc.Commodity.Stock -= oc.Quantity;
                }

            }

            return check;
        }

        public void ProcessOrder(OrderInfo orderinfo)
        {
            var content = orderinfo.OrderCommoditys.Aggregate("", (a, b) => a + b.Commodity.Name + ":" + b.Quantity + ";").TrimEnd(';');

            orderinfo.ShortContent = content;
        }



        /////////////////

        internal int CheckStock(int p, Commodity c, Guid authid, out string msglimit)
        {
            int res = p;
            msglimit = string.Empty;

            try
            {
                var ui = _context.UserIntegrals.Single(t => t.User.AuthID == authid);

                foreach (var i in this._context.CommodityLimits.Where(t => t.CommodityID == c.ID && (t.IsValid ?? true) == true))
                {
                    switch (i.Granularity.ToLower())
                    {
                        case "day":
                            var ygm_d = _context.OrderCommoditys.Where(t => t.CommodityID == c.ID && t.OrderInfo.UserIntegral.ID == ui.ID && t.OrderInfo.CreateTime.Value.Date == DateTime.Now.Date).Sum(t => (int?)t.Quantity).GetValueOrDefault();

                            if (ygm_d >= i.MaxQuantity)
                            {
                                res = -1;
                                msglimit = "已达到日限制量";

                                return res;
                            }
                            else
                            {
                                var kgm = i.MaxQuantity - ygm_d;
                                if (kgm < res)
                                {
                                    res = kgm;
                                }
                            }
                            break;
                        case "week":
                            var monday = DateTime.Now.AddDays(1 - Convert.ToInt32(DateTime.Now.DayOfWeek)).Date;
                            var ygm_w = _context.OrderCommoditys.Where(t => t.CommodityID == c.ID && t.OrderInfo.UserIntegral.ID == ui.ID && t.OrderInfo.CreateTime.Value.Date >= monday).Sum(t => (int?)t.Quantity).GetValueOrDefault();
                            if (ygm_w >= i.MaxQuantity)
                            {
                                res = -1;
                                msglimit = "已达到周限制量";

                                return res;
                            }
                            else
                            {
                                var kgm = i.MaxQuantity - ygm_w;
                                if (kgm < res)
                                {
                                    res = kgm;
                                }
                            }
                            break;
                        case "month":
                            var monthfirstday = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                            var ygm_m = _context.OrderCommoditys.Where(t => t.CommodityID == c.ID && t.OrderInfo.UserIntegral.ID == ui.ID && t.OrderInfo.CreateTime.Value >= monthfirstday).Sum(t => (int?)t.Quantity).GetValueOrDefault();
                            if (ygm_m >= i.MaxQuantity)
                            {
                                res = -1;
                                msglimit = "已达到月限制量";

                                return res;
                            }
                            else
                            {
                                var kgm = i.MaxQuantity - ygm_m;
                                if (kgm < res)
                                {
                                    res = kgm;
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception)
            {
                
                throw;
            }

            return res;
        }
    }
}
