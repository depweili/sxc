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
    public class AgentMap : EntityTypeConfiguration<Agent>
    {
        public AgentMap()
        {
            this.Property(t => t.Code).HasMaxLength(20).IsUnique();
            this.Property(t => t.Commission).HasColumnType("MONEY");

            this.HasOptional(t => t.ParentAgent).WithMany(t => t.ChildAgents).HasForeignKey(t => t.PID);

            this.HasRequired(t => t.User).WithRequiredDependent(t => t.Agent);

            this.HasOptional(t => t.Area).WithMany().HasForeignKey(t=>t.Area_ID);
        }
    }

    public class UserPaymentMap : EntityTypeConfiguration<UserPayment>
    {
        public UserPaymentMap()
        {
            this.Property(t => t.PaySN).HasMaxLength(20).IsUnique();
            this.Property(t => t.Memo).HasMaxLength(50);
            this.Property(t => t.Amount).HasColumnType("MONEY");
            this.Property(t => t.DistrAmount).HasColumnType("MONEY");
            this.Property(t => t.Commission).HasColumnType("MONEY");
            this.Property(t => t.FinalAmount).HasColumnType("MONEY");

            this.Property(t => t.OperatorID).HasMaxLength(20);
            this.Property(t => t.OperatorName).HasMaxLength(10);

        }
    }

    public class CommissionRecordMap : EntityTypeConfiguration<CommissionRecord>
    {
        public CommissionRecordMap()
        {
            this.Property(t => t.Memo).HasMaxLength(50);
        }
    }

    public class PaymentItemMap : EntityTypeConfiguration<PaymentItem>
    {
        public PaymentItemMap()
        {
            this.Property(t => t.Name).HasMaxLength(20);
        }
    }
}
