namespace SXC.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class zs30 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sxc_Lottery", "CostPoints", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sxc_Lottery", "CostPoints");
        }
    }
}
