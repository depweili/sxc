using SXC.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SXC.Core.Mappings
{
    public class NavigationMap : EntityTypeConfiguration<Navigation>
    {
        public NavigationMap()
        {
            this.Property(t => t.Desc).IsRequired().HasMaxLength(50);
            this.Property(t => t.Pic).HasMaxLength(50);
            this.Property(t => t.Target).HasMaxLength(100);
        }
    }
}
