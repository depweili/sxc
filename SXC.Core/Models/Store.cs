using System;
using System.Collections.Generic;
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

        public virtual ICollection<Commodity> Commodities { get; set; }

    }

    public class Commodity
    {
        public Commodity()
        {
            CommodityUID = Guid.NewGuid();
            IsValid = true;
            CreateTime = DateTime.Now;
        }

        public int ID { get; set; }

        public Guid CommodityUID { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Details { get; set; }

        public decimal Price { get; set; }

        public int Points { get; set; }

        public int Stock { get; set; }

        public string Pic { get; set; }

        public string Memo { get; set; }

        public Nullable<bool> IsValid { get; set; }

        public Nullable<DateTime> CreateTime { get; set; }

        public int CategoryID { get; set; }
        public virtual Category Category { get; set; }
    }

    public class CommodityImage
    {
        public int ID { get; set; }

        public string Memo { get; set; }

        public string Pic { get; set; }

        public Nullable<bool> IsValid { get; set; }

        public Nullable<DateTime> CreateTime { get; set; }
    }

    public class Order
    {
        public int ID { get; set; }

        public int State { get; set; }

        public Nullable<bool> IsValid { get; set; }

        public Nullable<DateTime> CreateTime { get; set; }

        public virtual Category Category { get; set; }
    }
}
