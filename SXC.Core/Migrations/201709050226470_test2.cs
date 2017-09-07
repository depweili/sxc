namespace SXC.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test2 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Sxc_UserProfile", "IsAgent");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Sxc_UserProfile", "IsAgent", c => c.Boolean(nullable: false));
        }
    }
}
