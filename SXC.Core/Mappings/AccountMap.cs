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
    public class UserAccountMap : EntityTypeConfiguration<UserAccount>
    {
        public UserAccountMap()
        {
            this.Property(t => t.PassWword).HasMaxLength(20);
            this.Property(t => t.Balance).HasColumnType("MONEY");
            this.Property(t => t.LockBalance).HasColumnType("MONEY");
            this.Property(t => t.Cash).HasColumnType("MONEY");
            this.Property(t => t.Expense).HasColumnType("MONEY");
            this.Property(t => t.BankCard).HasMaxLength(20);

            this.HasRequired(t => t.User).WithRequiredDependent(t => t.UserAccount);
        }
    }

    public class AccountRecordMap : EntityTypeConfiguration<AccountRecord>
    {
        public AccountRecordMap()
        {
            this.Property(t => t.AccountRecordSn).HasMaxLength(50).IsUnique().IsRequired(); ;
            this.Property(t => t.AfterBalance).HasColumnType("MONEY");
            this.Property(t => t.Amount).HasColumnType("MONEY");
            this.Property(t => t.BeforeBalance).HasColumnType("MONEY");
            this.Property(t => t.ShortMark).HasMaxLength(20);
            this.Property(t => t.Memo).HasMaxLength(200);
        }
    }

    public class AccountWithdrawMap : EntityTypeConfiguration<AccountWithdraw>
    {
        public AccountWithdrawMap()
        {
            this.Property(t => t.Amount).HasColumnType("MONEY");
            this.Property(t => t.Memo).HasMaxLength(100);
            this.Property(t => t.Name).HasMaxLength(20);
            this.Property(t => t.BankCard).HasMaxLength(20);
        }
    }
}
