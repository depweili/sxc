using SXC.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SXC.Core.Mappings
{
    public class CooperationMap : EntityTypeConfiguration<Cooperation>
    {
        public CooperationMap()
        {
            this.Property(t => t.Address).HasMaxLength(50);
            this.Property(t => t.Memo).HasMaxLength(100);
            this.Property(t => t.MobilePhone).HasMaxLength(20);
            this.Property(t => t.Name).HasMaxLength(10);
            this.Property(t => t.ProcessDetail).HasMaxLength(500);
            this.Property(t => t.AreaInfo).HasMaxLength(50);
            this.Property(t => t.AgentAreaInfo).HasMaxLength(50);
        }
    }
}
