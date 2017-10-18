namespace SXC.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class zs27 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Sxc_UserCoupon", "CouponID", "dbo.Sxc_User");
            DropIndex("dbo.Sxc_UserCoupon", new[] { "CouponID" });
            DropColumn("dbo.Sxc_UserCoupon", "CouponID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Sxc_UserCoupon", "CouponID", c => c.Int(nullable: false));
            CreateIndex("dbo.Sxc_UserCoupon", "CouponID");
            AddForeignKey("dbo.Sxc_UserCoupon", "CouponID", "dbo.Sxc_User", "ID");
        }
    }
}
