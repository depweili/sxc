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
    class AgentMap : EntityTypeConfiguration<Agent>
    {
        public AgentMap()
        {
            this.Property(t => t.Code).HasMaxLength(20).IsUnique();
            this.Property(t => t.Commission).HasColumnType("MONEY");

            this.HasOptional(t => t.ParentAgent).WithMany(t => t.ChildAgents).HasForeignKey(t => t.PID);

            this.HasRequired(t => t.User).WithRequiredDependent(t => t.Agent);
        }
    }
}
