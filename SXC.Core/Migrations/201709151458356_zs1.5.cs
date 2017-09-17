namespace SXC.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class zs15 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Sxc_IntegralUserActivity", new[] { "IntegralActivityID" });
            AddColumn("dbo.Sxc_IntegralUserActivity", "Memo", c => c.String(maxLength: 50));
            AlterColumn("dbo.Sxc_IntegralUserActivity", "IntegralActivityID", c => c.Int(nullable: false));
            CreateIndex("dbo.Sxc_IntegralUserActivity", "IntegralActivityID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Sxc_IntegralUserActivity", new[] { "IntegralActivityID" });
            AlterColumn("dbo.Sxc_IntegralUserActivity", "IntegralActivityID", c => c.Int());
            DropColumn("dbo.Sxc_IntegralUserActivity", "Memo");
            CreateIndex("dbo.Sxc_IntegralUserActivity", "IntegralActivityID");
        }
    }
}
