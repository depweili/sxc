using SXC.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SXC.Core.Mappings
{
    class Base_AreaMap : EntityTypeConfiguration<Base_Area>
    {
        public Base_AreaMap()
        {
            this.Property(t => t.Name).HasMaxLength(20);
        }
    }
}
