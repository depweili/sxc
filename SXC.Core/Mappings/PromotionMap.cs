using SXC.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SXC.Core.Mappings
{
    class PromotionMap : EntityTypeConfiguration<Promotion>
    {
        public PromotionMap()
        {
            this.Property(t => t.Name).IsRequired().HasMaxLength(50);
            this.Property(t => t.Desc).HasMaxLength(200);
            this.Property(t => t.Location).HasMaxLength(100);
            this.Property(t => t.Pic).HasMaxLength(100);
            this.Property(t => t.Content).HasMaxLength(2000);

            //this.HasOptional(t => t.Article).WithMany().HasForeignKey(t => t.Article_ID);
        }
    }
}
