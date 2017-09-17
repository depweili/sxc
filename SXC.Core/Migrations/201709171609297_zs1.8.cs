namespace SXC.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class zs18 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Sxc_Category", "PID", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Sxc_Category", "PID", c => c.Int(nullable: false));
        }
    }
}
