namespace SXC.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class zs32 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Sxc_CommissionRecord",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Commission = c.Decimal(nullable: false, precision: 18, scale: 2),
                        UserPaymentID = c.Int(nullable: false),
                        AgentID = c.Int(nullable: false),
                        State = c.Int(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        CheckTime = c.DateTime(),
                        Memo = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Sxc_Agent", t => t.AgentID)
                .ForeignKey("dbo.Sxc_UserPayment", t => t.UserPaymentID)
                .Index(t => t.UserPaymentID)
                .Index(t => t.AgentID);
            
            CreateTable(
                "dbo.Sxc_UserPayment",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        PayItemID = c.Int(nullable: false),
                        PayUID = c.Guid(nullable: false),
                        PaySN = c.String(maxLength: 20),
                        IsDistr = c.Boolean(nullable: false),
                        DistrType = c.Int(nullable: false),
                        State = c.Int(nullable: false),
                        Amount = c.Decimal(nullable: false, storeType: "money"),
                        DistrAmount = c.Decimal(nullable: false, storeType: "money"),
                        Commission = c.Decimal(nullable: false, storeType: "money"),
                        FinalAmount = c.Decimal(nullable: false, storeType: "money"),
                        CreateTime = c.DateTime(nullable: false),
                        DistrTime = c.DateTime(),
                        AccountTime = c.DateTime(),
                        Memo = c.String(maxLength: 50),
                        OperatorID = c.String(maxLength: 20),
                        OperatorName = c.String(maxLength: 10),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Sxc_PaymentItem", t => t.PayItemID)
                .ForeignKey("dbo.Sxc_User", t => t.UserID)
                .Index(t => t.UserID)
                .Index(t => t.PayItemID)
                .Index(t => t.PaySN, unique: true);
            
            CreateTable(
                "dbo.Sxc_PaymentItem",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sxc_UserPayment", "UserID", "dbo.Sxc_User");
            DropForeignKey("dbo.Sxc_UserPayment", "PayItemID", "dbo.Sxc_PaymentItem");
            DropForeignKey("dbo.Sxc_CommissionRecord", "UserPaymentID", "dbo.Sxc_UserPayment");
            DropForeignKey("dbo.Sxc_CommissionRecord", "AgentID", "dbo.Sxc_Agent");
            DropIndex("dbo.Sxc_UserPayment", new[] { "PaySN" });
            DropIndex("dbo.Sxc_UserPayment", new[] { "PayItemID" });
            DropIndex("dbo.Sxc_UserPayment", new[] { "UserID" });
            DropIndex("dbo.Sxc_CommissionRecord", new[] { "AgentID" });
            DropIndex("dbo.Sxc_CommissionRecord", new[] { "UserPaymentID" });
            DropTable("dbo.Sxc_PaymentItem");
            DropTable("dbo.Sxc_UserPayment");
            DropTable("dbo.Sxc_CommissionRecord");
        }
    }
}
