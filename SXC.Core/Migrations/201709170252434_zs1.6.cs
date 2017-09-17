namespace SXC.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class zs16 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Sxc_Cooperation", new[] { "AreaID" });
            AlterColumn("dbo.Sxc_Cooperation", "AreaID", c => c.Int());
            CreateIndex("dbo.Sxc_Cooperation", "AreaID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Sxc_Cooperation", new[] { "AreaID" });
            AlterColumn("dbo.Sxc_Cooperation", "AreaID", c => c.Int(nullable: false));
            CreateIndex("dbo.Sxc_Cooperation", "AreaID");
        }
    }
}
