namespace SXC.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class zs17 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Sxc_Category",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PID = c.Int(nullable: false),
                        CatUID = c.Guid(nullable: false),
                        Code = c.String(maxLength: 50),
                        Name = c.String(nullable: false, maxLength: 50),
                        Description = c.String(maxLength: 100),
                        Memo = c.String(maxLength: 50),
                        Order = c.Int(nullable: false),
                        IsValid = c.Boolean(),
                        CreateTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Sxc_Commodity",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CommodityUID = c.Guid(nullable: false),
                        Code = c.String(maxLength: 50),
                        Name = c.String(nullable: false, maxLength: 50),
                        Description = c.String(maxLength: 100),
                        Details = c.String(maxLength: 500),
                        Price = c.Decimal(nullable: false, storeType: "money"),
                        Points = c.Int(nullable: false),
                        Stock = c.Int(nullable: false),
                        Pic = c.String(maxLength: 50),
                        Memo = c.String(maxLength: 50),
                        IsValid = c.Boolean(),
                        CreateTime = c.DateTime(),
                        CategoryID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Sxc_Category", t => t.CategoryID)
                .Index(t => t.CategoryID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sxc_Commodity", "CategoryID", "dbo.Sxc_Category");
            DropIndex("dbo.Sxc_Commodity", new[] { "CategoryID" });
            DropTable("dbo.Sxc_Commodity");
            DropTable("dbo.Sxc_Category");
        }
    }
}
