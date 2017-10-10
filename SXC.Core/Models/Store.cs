using SXC.Code.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SXC.Core.Models
{
    public class Category
    {
        public Category()
        {
            CatUID = Guid.NewGuid();
            IsValid = true;
            CreateTime = DateTime.Now;
        }
        public int ID { get; set; }

        public int? PID { get; set; }

        public Guid CatUID { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Memo { get; set; }

        public int Order { get; set; }

        public Nullable<bool> IsValid { get; set; }

        public Nullable<DateTime> CreateTime { get; set; }

        public virtual ICollection<Commodity> Commoditys { get; set; }

    }

    public class Commodity
    {
        public Commodity()
        {
            CommodityUID = Guid.NewGuid();
            IsValid = true;
            CreateTime = DateTime.Now;
        }

        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid CommodityUID { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Details { get; set; }

        public decimal Price { get; set; }

        public int Points { get; set; }

        public int Stock { get; set; }

        public string Pic { get; set; }

        public bool? IsReal { get; set; }

        public bool HasVideo { get; set; }

        public string Memo { get; set; }

        public Nullable<bool> IsValid { get; set; }

        public Nullable<DateTime> CreateTime { get; set; }

        public int CategoryID { get; set; }
        public virtual Category Category { get; set; }

        //public virtual ICollection<Commodity> CommodityGallerys { get; set; }

        public virtual ICollection<CommodityVideoSeries> CommodityVideoSeries { get; set; }
    }

    public class CommodityGallery
    {
        public int ID { get; set; }

        public string Img { get; set; }

        public string ImgUrl { get; set; }

        public string ImgDesc { get; set; }

        public Nullable<bool> IsValid { get; set; }

        public Nullable<DateTime> CreateTime { get; set; }

        public int CommodityID { get; set; }
        public virtual Commodity Commodity { get; set; }
    }

    public class OrderInfo
    {
        public OrderInfo()
        {
            OrderSn = Constant.SNPREFIX_ORDER + RandomHelper.GetTimeRandom1();
            OrderUID = Guid.NewGuid();
            IsValid = true;
            CreateTime = DateTime.Now;
        }
        public int ID { get; set; }

        public Guid OrderUID { get; set; }

        public string OrderSn { get; set; }

        public string ShortContent { get; set; }

        public int OrderStatus { get; set; }

        public int StoreStatus { get; set; }

        public int PayStatus { get; set; }

        public string Consignee { get; set; }

        public string AddressRegion { get; set; }

        public string AddressDetail { get; set; }

        public string Zipcode { get; set; }

        public string Telephone { get; set; }

        public string UserNote { get; set; }

        public decimal TotalAmount { get; set; }

        public int TotalPoints { get; set; }

        public decimal PayFee { get; set; }

        public decimal DeliverFee { get; set; }

        public decimal Tax { get; set; } 

        public string ToUserNote { get; set; }

        public Nullable<bool> IsValid { get; set; }

        public Nullable<DateTime> CreateTime { get; set; }

        public int? UserIntegralID { get; set; }
        public virtual UserIntegral UserIntegral { get; set; }

        public virtual ICollection<OrderCommodity> OrderCommoditys { get; set; }

        public virtual ICollection<OrderIntegralRecord> OrderIntegralRecords { get; set; }
    }

    public class OrderCommodity 
    {
        public int ID { get; set; }

        public string ShortContent { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public int Points { get; set; }

        public decimal TotalAmount { get; set; }

        public int TotalPoints { get; set; }

        public bool? IsReal { get; set; }

        public string CommodityAttrsStr { get; set; }

        public int OrderInfoID { get; set; }
        public virtual OrderInfo OrderInfo { get; set; }

        public int CommodityID { get; set; }
        public virtual Commodity Commodity { get; set; }

    }

    public class OrderPay
    {
        public int ID { get; set; }

        public int PayType { get; set; }


        public int OrderInfoID { get; set; }
        public virtual OrderInfo OrderInfo { get; set; }
    }

    public class OrderIntegralRecord
    {
        public OrderIntegralRecord()
        {
            CreateTime = DateTime.Now;
        }

        public int ID { get; set; }

        public int OrderInfoID { get; set; }
        public virtual OrderInfo OrderInfo { get; set; }

        public int IntegralRecordID{ get; set; }
        public virtual IntegralRecord IntegralRecord { get; set; }

        public DateTime CreateTime { get; set; }
    }


}
