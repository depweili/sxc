namespace SXC.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class zs38 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Sxc_AccountWithdraw", new[] { "AccountRecordID" });
            AlterColumn("dbo.Sxc_AccountWithdraw", "AccountRecordID", c => c.Int());
            CreateIndex("dbo.Sxc_AccountWithdraw", "AccountRecordID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Sxc_AccountWithdraw", new[] { "AccountRecordID" });
            AlterColumn("dbo.Sxc_AccountWithdraw", "AccountRecordID", c => c.Int(nullable: false));
            CreateIndex("dbo.Sxc_AccountWithdraw", "AccountRecordID");
        }
    }
}
