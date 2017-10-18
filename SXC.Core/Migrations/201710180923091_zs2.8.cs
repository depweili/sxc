namespace SXC.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class zs28 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sxc_UserCoupon", "CouponID", c => c.Int(nullable: false));
            CreateIndex("dbo.Sxc_UserCoupon", "CouponID");
            AddForeignKey("dbo.Sxc_UserCoupon", "CouponID", "dbo.Sxc_Coupon", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sxc_UserCoupon", "CouponID", "dbo.Sxc_Coupon");
            DropIndex("dbo.Sxc_UserCoupon", new[] { "CouponID" });
            DropColumn("dbo.Sxc_UserCoupon", "CouponID");
        }
    }
}
