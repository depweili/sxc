namespace SXC.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class zs43 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Sxc_CommodityLimit",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Granularity = c.String(maxLength: 50),
                        CommodityID = c.Int(nullable: false),
                        MaxQuantity = c.Int(nullable: false),
                        Memo = c.String(maxLength: 50),
                        IsValid = c.Boolean(),
                        CreateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Sxc_Commodity", t => t.CommodityID)
                .Index(t => t.CommodityID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sxc_CommodityLimit", "CommodityID", "dbo.Sxc_Commodity");
            DropIndex("dbo.Sxc_CommodityLimit", new[] { "CommodityID" });
            DropTable("dbo.Sxc_CommodityLimit");
        }
    }
}
