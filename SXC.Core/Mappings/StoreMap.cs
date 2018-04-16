using SXC.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SXC.Code.Extensions;

namespace SXC.Core.Mappings
{
    public class CategoryMap : EntityTypeConfiguration<Category>
    {
        public CategoryMap()
        {
            this.Property(t => t.Name).IsRequired().HasMaxLength(50);
            this.Property(t => t.Description).HasMaxLength(100);
            this.Property(t => t.Memo).HasMaxLength(50);
            this.Property(t => t.Code).HasMaxLength(50);
        }
    }

    public class CommodityMap : EntityTypeConfiguration<Commodity>
    {
        public CommodityMap()
        {
            this.Property(t => t.Name).IsRequired().HasMaxLength(50);
            this.Property(t => t.Description).HasMaxLength(100);
            this.Property(t => t.Details).HasMaxLength(500);
            this.Property(t => t.Memo).HasMaxLength(50);
            this.Property(t => t.Code).HasMaxLength(50);
            this.Property(t => t.Price).HasColumnType("MONEY");
            this.Property(t => t.Pic).HasMaxLength(50);
            
        }
    }

    public class OrderInfoMap : EntityTypeConfiguration<OrderInfo>
    {
        public OrderInfoMap()
        {
            this.Property(t => t.AddressRegion).HasMaxLength(100).IsRequired();
            this.Property(t => t.AddressDetail).HasMaxLength(100).IsRequired();
            this.Property(t => t.Consignee).HasMaxLength(20).IsRequired();
            this.Property(t => t.OrderSn).HasMaxLength(50).IsUnique().IsRequired();
            this.Property(t => t.Telephone).HasMaxLength(20).IsRequired();
            this.Property(t => t.ToUserNote).HasMaxLength(200);
            this.Property(t => t.UserNote).HasMaxLength(200);
            this.Property(t => t.Zipcode).HasMaxLength(10);
            this.Property(t => t.TotalAmount).HasColumnType("MONEY");
            this.Property(t => t.ShortContent).HasMaxLength(100);
        }
    }

    public class OrderCommodityMap : EntityTypeConfiguration<OrderCommodity>
    {
        public OrderCommodityMap()
        {
            this.Property(t => t.ShortContent).HasMaxLength(50);
            this.Property(t => t.CommodityAttrsStr).HasMaxLength(200);
            this.Property(t => t.Price).HasColumnType("MONEY");
        }
    }

    public class CommodityLimitMap : EntityTypeConfiguration<CommodityLimit>
    {
        public CommodityLimitMap()
        {
            this.Property(t => t.Granularity).HasMaxLength(50);
            this.Property(t => t.Memo).HasMaxLength(50);
        }
    }


}
