namespace SXC.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class zs19 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Sxc_Commodity", "CommodityUID", c => c.Guid(nullable: false, identity: true));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Sxc_Commodity", "CommodityUID", c => c.Guid(nullable: false));
        }
    }
}
