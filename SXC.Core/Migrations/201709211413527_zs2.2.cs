namespace SXC.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class zs22 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sxc_Course", "HasVideo", c => c.Boolean());
            AddColumn("dbo.Sxc_Course", "HasFreeVideo", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sxc_Course", "HasFreeVideo");
            DropColumn("dbo.Sxc_Course", "HasVideo");
        }
    }
}
