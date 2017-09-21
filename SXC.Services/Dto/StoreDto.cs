using Newtonsoft.Json;
using SXC.Code.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SXC.Services.Dto
{
    public class CategoryDto
    {
    }

    public class CommodityDetailDto
    {
        public int id { get; set; }

        public Guid commodityuid { get; set; }

        public string code { get; set; }

        public string name { get; set; }

        public string description { get; set; }

        public string details { get; set; }

        public decimal price { get; set; }

        public int points { get; set; }

        public int stock { get; set; }

        public string pic { get; set; }

        public string memo { get; set; }

        public Nullable<bool> isvalid { get; set; }

        public List<CommodityAttrDto> detailAttrs { get; set; }
    }

    public class CommodityDto
    {
        public int id { get; set; }

        public Guid commodityuid { get; set; }

        public string name { get; set; }

        public int points { get; set; }

        public string pic { get; set; }
    }

    public class CommodityAttrDto
    {
        public string key { get; set; }

        public string value { get; set; }
    }


    public class StoreResultDto
    {
        public string message { get; set; }

        public dynamic detail { get; set; }
    }

    public class ExchangeOrderDto
    {
        //地址
        public string consignee { get; set; }

        public string addressregion { get; set; }

        public string addressdetail { get; set; }

        public string telephone { get; set; }

        public string usernote { get; set; }

        //商品
        //public int commodityid { get; set; }

        public Guid commodityuid { get; set; }

        public int points { get; set; }

        public int quantity { get; set; }

        public List<CommodityAttrDto> commodityattrs { get; set; }

        //账号
        //public Guid integralid { get; set; }

        public Guid authid { get; set; }


        //方法
        public string GetCommodityAttrsStr()
        {
            var res = commodityattrs.Aggregate("", (a, b) => a + b.key + ":" + b.value + ";");

            return res.TrimEnd(';');
        }
    }



    public class UserExchangeOrderDto
    {
        public Guid orderuid { get; set; }

        public string ordersn { get; set; }

        public string shortcontent { get; set; }

        public int orderstatus { get; set; }

        public string consignee { get; set; }

        public string addressregion { get; set; }

        public string addressdetail { get; set; }

        public string telephone { get; set; }

        public string usernote { get; set; }

        public string tousernote { get; set; }

        public string commodityname { get; set; }

        public string commodityimgurl { get; set; }

        public int points { get; set; }

        public int quantity { get; set; }

        public int totalpoints { get; set; }

        [JsonConverter(typeof(CommonDateTimeConverter))]
        public DateTime? createtime { get; set; }
    }


    //////////////
}
