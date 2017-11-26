using SXC.Core.Mappings;
using SXC.Core.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Infrastructure.Annotations;
using SXC.Code.Utility;

namespace SXC.Core.Data
{
    public class SxcDbContext : DbContext
    {
        public SxcDbContext()
            : base("SxcDbContext")
        {
            //Database.SetInitializer(new SxcDbContextInitializer());
            Database.SetInitializer<SxcDbContext>(null);

            //this.Configuration.ProxyCreationEnabled = false;
            // 禁用延迟加载
            //this.Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<Navigation> Navigations { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Promotion> Promotions { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<UserAuth> UserAuths { get; set; }
        public DbSet<Agent> Agents { get; set; }

        public DbSet<Base_Area> Base_Areas { get; set; }

        public DbSet<Cooperation> Cooperations { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<ReservationCourse> ReservationCourses { get; set; }
        //public DbSet<LearnPurpose> LearnPurposes { get; set; }

        public DbSet<UserIntegral> UserIntegrals { get; set; }
        public DbSet<IntegralGrade> IntegralGrades { get; set; }
        public DbSet<IntegralActivity> IntegralActivitys { get; set; }
        public DbSet<IntegralRule> IntegralRules { get; set; }
        public DbSet<IntegralRecord> IntegralRecords { get; set; }
        public DbSet<IntegralSignIn> IntegralSignIns { get; set; }
        public DbSet<IntegralUserActivity> IntegralUserActivitys { get; set; }


        public DbSet<Category> Categorys { get; set; }
        public DbSet<Commodity> Commoditys { get; set; }
        public DbSet<OrderInfo> OrderInfos { get; set; }
        public DbSet<OrderCommodity> OrderCommoditys { get; set; }

        public DbSet<OrderIntegralRecord> OrderIntegralRecords { get; set; }

        public DbSet<VideoSeries> VideoSeries { get; set; }
        public DbSet<VideoInfo> VideoInfos { get; set; }
        public DbSet<CommodityVideoSeries> CommodityVideoSeries { get; set; }

        ////////////////////////////

        public DbSet<Lottery> Lotterys { get; set; }
        public DbSet<UserLottery> UserLotterys { get; set; }
        public DbSet<Prize> Prizes { get; set; }
        public DbSet<Coupon> Coupons { get; set; }
        public DbSet<LotteryRecord> LotteryRecords { get; set; }
        public DbSet<UserCoupon> UserCoupons { get; set; }
        ////////////////////


        public DbSet<UserPayment> UserPayments { get; set; }
        public DbSet<CommissionRecord> CommissionRecords { get; set; }
        public DbSet<PaymentItem> PaymentItems { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            string tbPrefix = ConfigHelper.GetConfig("tablePrefix");//ConfigurationManager.AppSettings["tablePrefix"];

            if (!string.IsNullOrEmpty(tbPrefix))
            {
                modelBuilder.Types().Configure(f => f.ToTable(tbPrefix + f.ClrType.Name));
            }

            modelBuilder.Configurations.Add(new NavigationMap());
            modelBuilder.Configurations.Add(new CourseMap());
            modelBuilder.Configurations.Add(new TeacherMap());
            modelBuilder.Configurations.Add(new ArticleMap());
            modelBuilder.Configurations.Add(new PromotionMap());
            modelBuilder.Configurations.Add(new UserMap());
            modelBuilder.Configurations.Add(new UserProfileMap());
            modelBuilder.Configurations.Add(new UserAuthMap());
            modelBuilder.Configurations.Add(new AgentMap());
            modelBuilder.Configurations.Add(new Base_AreaMap());

            modelBuilder.Configurations.Add(new CooperationMap());
            modelBuilder.Configurations.Add(new ReservationMap());
            modelBuilder.Configurations.Add(new ReservationCourseMap());
            //modelBuilder.Configurations.Add(new LearnPurposeMap());

            modelBuilder.Configurations.Add(new UserIntegralMap());
            modelBuilder.Configurations.Add(new IntegralGradeMap());
            modelBuilder.Configurations.Add(new IntegralActivityMap());
            modelBuilder.Configurations.Add(new IntegralRuleMap());
            modelBuilder.Configurations.Add(new IntegralRecordMap());
            modelBuilder.Configurations.Add(new IntegralUserActivityMap());

            modelBuilder.Configurations.Add(new CategoryMap());
            modelBuilder.Configurations.Add(new CommodityMap());

            modelBuilder.Configurations.Add(new OrderInfoMap());
            modelBuilder.Configurations.Add(new OrderCommodityMap());

            modelBuilder.Configurations.Add(new VideoSeriesMap());
            modelBuilder.Configurations.Add(new VideoInfoMap());
            //////////////////////////////////////////////

            modelBuilder.Configurations.Add(new LotteryMap());
            modelBuilder.Configurations.Add(new PrizeMap());
            modelBuilder.Configurations.Add(new CouponMap());
            modelBuilder.Configurations.Add(new LotteryRecordMap());
            modelBuilder.Configurations.Add(new UserCouponMap());

            //////////////////////////////////////////////
            modelBuilder.Configurations.Add(new UserPaymentMap());
            modelBuilder.Configurations.Add(new CommissionRecordMap());
            modelBuilder.Configurations.Add(new PaymentItemMap());

            //////////////////////////////////////////////

            // 禁用默认表名复数形式
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            // 禁用一对多级联删除
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            // 禁用多对多级联删除
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
        }
    }
}
