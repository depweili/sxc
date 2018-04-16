using SXC.Core.Models;
using SXC.Services.Business;
using SXC.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityFramework.Extensions;
using SXC.Core.Data;

namespace SXC.Services.Impl
{
    public class StoreService : ServiceBase
    {
        public List<CommodityDto> GetCommoditys(int? top = null)
        {
            using (var db = base.NewDB())
            {
                IQueryable<Commodity> dblist;
                if (top != null)
                {
                    dblist = db.Commoditys.Where(t => t.IsValid == true).Take(top.Value);
                }
                else
                {
                    dblist = db.Commoditys.Where(t => t.IsValid == true);
                }
                

                var res = new List<CommodityDto>();
                foreach (var item in dblist)
                {
                    res.Add(new CommodityDto
                    {
                        id = item.ID,
                        commodityuid = item.CommodityUID,
                        name = item.Name,
                        pic = Function.GetStaticPicUrl(item.Pic),
                        points = item.Points,
                        isreal = item.IsReal ?? true
                    });
                }

                return res;
            }
        }

        public CommodityDetailDto GetCommodity(Guid uid, Guid authid)
        {
            using (var db = base.NewDB())
            {
                var dbitem = db.Commoditys.FirstOrDefault(t => t.IsValid == true && t.CommodityUID == uid);

                if (dbitem == null)
                {
                    return null;
                }

                var res = new CommodityDetailDto
                {
                    id = dbitem.ID,
                    commodityuid = dbitem.CommodityUID,
                    name = dbitem.Name,
                    pic = Function.GetStaticPicUrl(dbitem.Pic),
                    points = dbitem.Points,
                    description = dbitem.Description,
                    //details = dbitem.Details,
                    code = dbitem.Code,
                    memo = dbitem.Memo,
                    price = dbitem.Price,
                    isvalid = dbitem.IsValid,
                    stock = dbitem.Stock,
                    isreal = dbitem.IsReal ?? true,
                    detailAttrs = GetAttrs(dbitem.Details),
                    articleid = dbitem.ArticleID
                };

                string msglimit = string.Empty;

                var bus = new StoreBus(db);
                res.stock = bus.CheckStock(res.stock, dbitem, authid, out msglimit);
                res.msglimit = msglimit;

                return res;
            }
        }




        private List<CommodityAttrDto> GetAttrs(string strattrs)
        {
            List<CommodityAttrDto> res = new List<CommodityAttrDto>();
            if (!string.IsNullOrEmpty(strattrs))
            {
                foreach (var row in strattrs.Split(new string[] { "\r\n" }, StringSplitOptions.None))
                {
                    res.Add(new CommodityAttrDto
                    {
                        value = row
                    });
                }
            }
            return res;
        }


        public BusinessResultDto ExchangeOrder(ExchangeOrderDto orderdto)
        {
            try
            {
                using (var db = base.NewDB())
                {
                    var order = new OrderInfo
                    {
                        Consignee = orderdto.consignee,
                        AddressDetail = orderdto.addressdetail,
                        AddressRegion = orderdto.addressregion,
                        Telephone = orderdto.telephone,

                        UserNote = orderdto.usernote,
                        TotalPoints = orderdto.quantity * orderdto.points,
                        OrderCommoditys = new List<OrderCommodity> { 
                            new OrderCommodity{
                                 Commodity=db.Commoditys.FirstOrDefault(t=>t.CommodityUID==orderdto.commodityuid),
                                 Quantity=orderdto.quantity,
                                 Points=orderdto.points,
                                 TotalPoints=orderdto.quantity*orderdto.points,
                                 CommodityAttrsStr=orderdto.GetCommodityAttrsStr()
                            }
                        },
                        //UserIntegral = db.UserIntegrals.First(t => t.IntegralID == orderdto.integralid),

                        UserIntegral = db.Users.FirstOrDefault(t => t.AuthID == orderdto.authid).UserIntegral
                    };

                    var bus = new StoreBus(db);
                    var res = bus.SaveExchangeOrder(order);

                    if (res.issave)
                    {
                        db.SaveChanges();
                    }

                    return res;
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }


        public dynamic GetUserOrders(Guid authid,int? top = null)
        {
            using (var db = base.NewDB())
            {
                //IQueryable<OrderInfo> dblist;
                //EntityFramework.Future.FutureQuery<OrderInfo> dblist;

                db.Database.Log = Console.WriteLine;

                var ui = db.Users.FirstOrDefault(t => t.AuthID == authid).UserIntegral;

                var query = db.OrderInfos.Where(t => t.UserIntegralID == ui.ID).OrderByDescending(t => t.ID);
                
                if (top != null)
                {
                    query.Take(top.Value);
                }

                var dblist = query.Future();

                var ordercount = query.FutureCount().Value;

                var orderdtos = new List<UserExchangeOrderDto>();

                foreach (var item in dblist)
                {
                    var firstCommodity=item.OrderCommoditys.First();
                    orderdtos.Add(new UserExchangeOrderDto
                    {
                        ordersn = item.OrderSn,
                        orderuid = item.OrderUID,
                        orderstatus = item.OrderStatus,
                        consignee = item.Consignee,
                        addressregion = item.AddressRegion,
                        addressdetail = item.AddressDetail,
                        telephone = item.Telephone,
                        usernote = item.UserNote,
                        tousernote = item.ToUserNote,
                        commodityname = firstCommodity.Commodity.Name,
                        commodityimgurl = Function.GetStaticPicUrl(firstCommodity.Commodity.Pic),
                        points = firstCommodity.Points,
                        quantity = firstCommodity.Quantity,
                        totalpoints = item.TotalPoints,
                        createtime = item.CreateTime,
                        hasvideo = item.OrderCommoditys.First().Commodity.HasVideo

                    });
                }

                var res = new 
                {
                    ordercount = ordercount,
                    ui.TotalExpense,
                    orderdtos
                };

                return res;
            }
        }


        public VideoSeriesDto GetVideoByOrder(Guid orderuid)
        {
            try
            {
                using (var db = base.NewDB())
                {
                    var commodity = db.OrderInfos.SingleOrDefault(t => t.OrderUID == orderuid).OrderCommoditys.Single().Commodity;
                    var videos = db.CommodityVideoSeries.Single(t => t.CommodityID==commodity.ID).VideoSeries;
                    var videolist = videos.VideoInfos.OrderBy(t => t.Order);

                    List<VideoInfoDto> videodtolist = new List<VideoInfoDto>();


                    foreach (var v in videolist)
                    {
                        videodtolist.Add(new VideoInfoDto
                        {
                            id = v.ID,
                            introduction = v.Introduction,
                            order = v.Order,
                            title = v.Title,
                            //length = v.Length,
                            snapshot = v.Snapshot,
                            src = v.Source,
                            videouid = v.VideoUID
                        });
                    }

                    VideoSeriesDto res = new VideoSeriesDto
                    {
                        id = videos.ID,
                        cover = videos.Cover,
                        title = videos.Title,
                        total = videos.Total??0,
                        videoseriesuid = videos.VideoSeriesUID,
                        VideoList = videodtolist
                    };

                    return res;
                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /////////////////////

    }
}
