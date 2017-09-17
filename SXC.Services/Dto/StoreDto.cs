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
    }

    public class CommodityDto
    {
        public int id { get; set; }

        public string name { get; set; }

        public int points { get; set; }

        public string pic { get; set; }
    }
}
