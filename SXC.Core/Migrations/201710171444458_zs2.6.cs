namespace SXC.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class zs26 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Sxc_Coupon",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Type = c.Int(nullable: false),
                        Name = c.String(maxLength: 50),
                        Image = c.String(maxLength: 50),
                        Description = c.String(maxLength: 50),
                        Discount = c.Double(),
                        Deduction = c.Int(),
                        ExpiredTime = c.DateTime(),
                        ValidDays = c.Int(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Sxc_LotteryRecord",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        LotteryID = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                        PrizeID = c.Int(nullable: false),
                        RecordTime = c.DateTime(nullable: false),
                        Memo = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Sxc_Lottery", t => t.LotteryID)
                .ForeignKey("dbo.Sxc_Prize", t => t.PrizeID)
                .ForeignKey("dbo.Sxc_User", t => t.UserID)
                .Index(t => t.LotteryID)
                .Index(t => t.UserID)
                .Index(t => t.PrizeID);
            
            CreateTable(
                "dbo.Sxc_Lottery",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        LotteryUID = c.Guid(nullable: false),
                        Type = c.Int(nullable: false),
                        Chance = c.Int(nullable: false),
                        Name = c.String(maxLength: 50),
                        IsValid = c.Boolean(),
                        CreateTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Sxc_Prize",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        LotteryID = c.Int(nullable: false),
                        Type = c.Int(nullable: false),
                        Image = c.String(maxLength: 50),
                        Name = c.String(maxLength: 50),
                        WinRate = c.Double(nullable: false),
                        Level = c.Int(),
                        Points = c.Int(),
                        IsValid = c.Boolean(),
                        CreateTime = c.DateTime(),
                        CouponID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Sxc_Coupon", t => t.CouponID)
                .ForeignKey("dbo.Sxc_Coupon", t => t.LotteryID)
                .ForeignKey("dbo.Sxc_Lottery", t => t.LotteryID)
                .Index(t => t.LotteryID)
                .Index(t => t.CouponID);
            
            CreateTable(
                "dbo.Sxc_UserCoupon",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserCouponCode = c.Guid(nullable: false),
                        UserID = c.Int(nullable: false),
                        CouponID = c.Int(nullable: false),
                        GetTime = c.DateTime(nullable: false),
                        ExpiredTime = c.DateTime(),
                        IsValid = c.Boolean(nullable: false),
                        State = c.Int(nullable: false),
                        Memo = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Sxc_User", t => t.CouponID)
                .ForeignKey("dbo.Sxc_User", t => t.UserID)
                .Index(t => t.UserID)
                .Index(t => t.CouponID);
            
            CreateTable(
                "dbo.Sxc_UserLottery",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        LotteryID = c.Int(nullable: false),
                        Chance = c.Int(nullable: false),
                        IsValid = c.Boolean(nullable: false),
                        CreateTime = c.DateTime(),
                        LastTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Sxc_Lottery", t => t.LotteryID)
                .ForeignKey("dbo.Sxc_User", t => t.UserID)
                .Index(t => t.UserID)
                .Index(t => t.LotteryID);
            
            AlterColumn("dbo.Sxc_VideoSeries", "Total", c => c.Int());
            AlterColumn("dbo.Sxc_VideoInfo", "Length", c => c.Double());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sxc_UserLottery", "UserID", "dbo.Sxc_User");
            DropForeignKey("dbo.Sxc_UserLottery", "LotteryID", "dbo.Sxc_Lottery");
            DropForeignKey("dbo.Sxc_UserCoupon", "UserID", "dbo.Sxc_User");
            DropForeignKey("dbo.Sxc_UserCoupon", "CouponID", "dbo.Sxc_User");
            DropForeignKey("dbo.Sxc_LotteryRecord", "UserID", "dbo.Sxc_User");
            DropForeignKey("dbo.Sxc_LotteryRecord", "PrizeID", "dbo.Sxc_Prize");
            DropForeignKey("dbo.Sxc_LotteryRecord", "LotteryID", "dbo.Sxc_Lottery");
            DropForeignKey("dbo.Sxc_Prize", "LotteryID", "dbo.Sxc_Lottery");
            DropForeignKey("dbo.Sxc_Prize", "LotteryID", "dbo.Sxc_Coupon");
            DropForeignKey("dbo.Sxc_Prize", "CouponID", "dbo.Sxc_Coupon");
            DropIndex("dbo.Sxc_UserLottery", new[] { "LotteryID" });
            DropIndex("dbo.Sxc_UserLottery", new[] { "UserID" });
            DropIndex("dbo.Sxc_UserCoupon", new[] { "CouponID" });
            DropIndex("dbo.Sxc_UserCoupon", new[] { "UserID" });
            DropIndex("dbo.Sxc_Prize", new[] { "CouponID" });
            DropIndex("dbo.Sxc_Prize", new[] { "LotteryID" });
            DropIndex("dbo.Sxc_LotteryRecord", new[] { "PrizeID" });
            DropIndex("dbo.Sxc_LotteryRecord", new[] { "UserID" });
            DropIndex("dbo.Sxc_LotteryRecord", new[] { "LotteryID" });
            AlterColumn("dbo.Sxc_VideoInfo", "Length", c => c.Double(nullable: false));
            AlterColumn("dbo.Sxc_VideoSeries", "Total", c => c.Int(nullable: false));
            DropTable("dbo.Sxc_UserLottery");
            DropTable("dbo.Sxc_UserCoupon");
            DropTable("dbo.Sxc_Prize");
            DropTable("dbo.Sxc_Lottery");
            DropTable("dbo.Sxc_LotteryRecord");
            DropTable("dbo.Sxc_Coupon");
        }
    }
}
