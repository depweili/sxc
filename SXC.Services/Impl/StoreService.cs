using SXC.Core.Models;
using SXC.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                        name = item.Name,
                        pic = Function.GetStaticPicUrl(item.Pic),
                        points = item.Points
                    });
                }

                return res;
            }
        }
    }
}
