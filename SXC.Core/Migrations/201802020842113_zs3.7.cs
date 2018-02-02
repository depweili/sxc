namespace SXC.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class zs37 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Sxc_AccountRecord",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AccountRecordSn = c.String(nullable: false, maxLength: 50),
                        UserAccountID = c.Int(nullable: false),
                        Type = c.Int(nullable: false),
                        ShortMark = c.String(maxLength: 20),
                        Amount = c.Decimal(nullable: false, storeType: "money"),
                        AfterBalance = c.Decimal(nullable: false, storeType: "money"),
                        BeforeBalance = c.Decimal(nullable: false, storeType: "money"),
                        CreateTime = c.DateTime(nullable: false),
                        Memo = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Sxc_UserAccount", t => t.UserAccountID)
                .Index(t => t.AccountRecordSn, unique: true)
                .Index(t => t.UserAccountID);
            
            CreateTable(
                "dbo.Sxc_UserAccount",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        AccountID = c.Guid(nullable: false),
                        PassWword = c.String(maxLength: 20),
                        Balance = c.Decimal(nullable: false, storeType: "money"),
                        LockBalance = c.Decimal(nullable: false, storeType: "money"),
                        Cash = c.Decimal(nullable: false, storeType: "money"),
                        Expense = c.Decimal(nullable: false, storeType: "money"),
                        BankCard = c.String(maxLength: 20),
                        IsValid = c.Boolean(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        IsVerified = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Sxc_User", t => t.ID)
                .Index(t => t.ID);
            
            CreateTable(
                "dbo.Sxc_AccountWithdraw",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserAccountID = c.Int(nullable: false),
                        Amount = c.Decimal(nullable: false, storeType: "money"),
                        CreateTime = c.DateTime(nullable: false),
                        State = c.Int(nullable: false),
                        CompleteTime = c.DateTime(),
                        Memo = c.String(maxLength: 100),
                        AccountRecordID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Sxc_AccountRecord", t => t.AccountRecordID)
                .ForeignKey("dbo.Sxc_UserAccount", t => t.UserAccountID)
                .Index(t => t.UserAccountID)
                .Index(t => t.AccountRecordID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sxc_AccountWithdraw", "UserAccountID", "dbo.Sxc_UserAccount");
            DropForeignKey("dbo.Sxc_AccountWithdraw", "AccountRecordID", "dbo.Sxc_AccountRecord");
            DropForeignKey("dbo.Sxc_UserAccount", "ID", "dbo.Sxc_User");
            DropForeignKey("dbo.Sxc_AccountRecord", "UserAccountID", "dbo.Sxc_UserAccount");
            DropIndex("dbo.Sxc_AccountWithdraw", new[] { "AccountRecordID" });
            DropIndex("dbo.Sxc_AccountWithdraw", new[] { "UserAccountID" });
            DropIndex("dbo.Sxc_UserAccount", new[] { "ID" });
            DropIndex("dbo.Sxc_AccountRecord", new[] { "UserAccountID" });
            DropIndex("dbo.Sxc_AccountRecord", new[] { "AccountRecordSn" });
            DropTable("dbo.Sxc_AccountWithdraw");
            DropTable("dbo.Sxc_UserAccount");
            DropTable("dbo.Sxc_AccountRecord");
        }
    }
}
