using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SXC.Core.Models
{
    public class Lottery
    {
        public int ID { get; set; }

        public Guid LotteryUID { get; set; }

        public int Type { get; set; }

        public int Chance { get; set; }

        public string Name { get; set; }

        public bool? IsValid { get; set; }

        public DateTime? CreateTime { get; set; }

        public virtual ICollection<Prize> Prizes { get; set; }
        
    }

    public class LotteryRule
    {
        public int ID { get; set; }

        public int Type { get; set; }

        public int Chance { get; set; }

    }

    public class UserLottery
    {
        public UserLottery()
        {
            IsValid = true;
            CreateTime = DateTime.Now;
            LastTime = DateTime.Now;
        }
        public int ID { get; set; }

        public int UserID { get; set; }
        public virtual User User { get; set; }

        public int LotteryID { get; set; }
        public virtual Lottery Lottery { get; set; }

        public int Chance { get; set; }

        public bool IsValid { get; set; }

        public DateTime? CreateTime { get; set; }

        public DateTime LastTime { get; set; }

        
    }

    public class Prize
    {
        public Prize()
        {
            IsValid = true;
            CreateTime = DateTime.Now;
        }
        public int ID { get; set; }

        public int LotteryID { get; set; }
        public virtual Lottery Lottery { get; set; }

        public int Type { get; set; }

        public string Image { get; set; }

        public string Name { get; set; }

        public double WinRate { get; set; }

        public int? Level { get; set; }

        public int? Points { get; set; }

        public bool? IsValid { get; set; }

        public DateTime? CreateTime { get; set; }

        public int? CouponID { get; set; }
        public virtual Coupon Coupon { get; set; }

        

    }

    public class Coupon
    {
        public int ID { get; set; }

        public int Type { get; set; }

        public string Name { get; set; }

        public string Image { get; set; }

        public string Description { get; set; }

        public double? Discount { get; set; }//折扣

        public int? Deduction { get; set; }//直接优惠

        public DateTime? ExpiredTime { get; set; }

        public int? ValidDays { get; set; }
    }

    public class CouponCommodity
    {

    }




    public class LotteryRecord
    {
        public LotteryRecord()
        {
            RecordTime = DateTime.Now;
        }
        public int ID { get; set; }

        public int LotteryID { get; set; }
        public virtual Lottery Lottery { get; set; }

        public int UserID { get; set; }
        public virtual User User { get; set; }


        public int PrizeID { get; set; }
        public virtual Prize Prize { get; set; }

        public DateTime RecordTime { get; set; }

        public string Memo { get; set; }

    }

    public class UserCoupon
    {
        public UserCoupon()
        {
            UserCouponCode = Guid.NewGuid();
            IsValid = true;
            GetTime = DateTime.Now;
            State = 0;
        }
        public int ID { get; set; }

        public Guid UserCouponCode { get; set; }

        public int UserID { get; set; }
        public virtual User User { get; set; }

        public int CouponID { get; set; }
        public virtual Coupon Coupon { get; set; }

        public DateTime GetTime { get; set; }

        public DateTime? ExpiredTime { get; set; }

        public bool IsValid { get; set; }

        public int State { get; set; }

        public string Memo { get; set; }

    }

}
