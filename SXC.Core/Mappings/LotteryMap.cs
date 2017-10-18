using SXC.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SXC.Core.Mappings
{
    public class LotteryMap : EntityTypeConfiguration<Lottery>
    {
        public LotteryMap()
        {
            this.Property(t => t.Name).HasMaxLength(50);
        }
    }


    public class PrizeMap : EntityTypeConfiguration<Prize>
    {
        public PrizeMap()
        {
            this.Property(t => t.Name).HasMaxLength(50);
            this.Property(t => t.Image).HasMaxLength(50);
        }
    }

    public class CouponMap : EntityTypeConfiguration<Coupon>
    {
        public CouponMap()
        {
            this.Property(t => t.Name).HasMaxLength(50);
            this.Property(t => t.Image).HasMaxLength(50);
            this.Property(t => t.Description).HasMaxLength(50);
        }
    }

    public class LotteryRecordMap : EntityTypeConfiguration<LotteryRecord>
    {
        public LotteryRecordMap()
        {
            this.Property(t => t.Memo).HasMaxLength(50);
        }
    }

    public class UserCouponMap : EntityTypeConfiguration<UserCoupon>
    {
        public UserCouponMap()
        {
            this.Property(t => t.Memo).HasMaxLength(50);
        }
    }

}
