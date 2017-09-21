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
    }
}
