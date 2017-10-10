namespace SXC.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class zs25 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Sxc_CommodityVideoSeries",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CommodityID = c.Int(nullable: false),
                        VideoSeriesID = c.Int(nullable: false),
                        IsValid = c.Boolean(),
                        CreateTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Sxc_Commodity", t => t.CommodityID)
                .ForeignKey("dbo.Sxc_VideoSeries", t => t.VideoSeriesID)
                .Index(t => t.CommodityID)
                .Index(t => t.VideoSeriesID);
            
            CreateTable(
                "dbo.Sxc_VideoSeries",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        VideoSeriesUID = c.Guid(nullable: false),
                        Title = c.String(maxLength: 50),
                        Cover = c.String(maxLength: 100),
                        Folder = c.String(maxLength: 100),
                        Introduction = c.String(maxLength: 200),
                        Total = c.Int(nullable: false),
                        IsValid = c.Boolean(),
                        CreateTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Sxc_VideoInfo",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        VideoUID = c.Guid(nullable: false),
                        Title = c.String(maxLength: 50),
                        Introduction = c.String(maxLength: 100),
                        File = c.String(maxLength: 100),
                        Source = c.String(maxLength: 100),
                        Snapshot = c.String(maxLength: 100),
                        Length = c.Double(nullable: false),
                        Order = c.Int(nullable: false),
                        IsValid = c.Boolean(),
                        CreateTime = c.DateTime(),
                        VideoSeriesID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Sxc_VideoSeries", t => t.VideoSeriesID)
                .Index(t => t.VideoSeriesID);
            
            AddColumn("dbo.Sxc_Commodity", "HasVideo", c => c.Boolean(nullable: false));
            AddColumn("dbo.Sxc_Promotion", "Type", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sxc_CommodityVideoSeries", "VideoSeriesID", "dbo.Sxc_VideoSeries");
            DropForeignKey("dbo.Sxc_VideoInfo", "VideoSeriesID", "dbo.Sxc_VideoSeries");
            DropForeignKey("dbo.Sxc_CommodityVideoSeries", "CommodityID", "dbo.Sxc_Commodity");
            DropIndex("dbo.Sxc_VideoInfo", new[] { "VideoSeriesID" });
            DropIndex("dbo.Sxc_CommodityVideoSeries", new[] { "VideoSeriesID" });
            DropIndex("dbo.Sxc_CommodityVideoSeries", new[] { "CommodityID" });
            DropColumn("dbo.Sxc_Promotion", "Type");
            DropColumn("dbo.Sxc_Commodity", "HasVideo");
            DropTable("dbo.Sxc_VideoInfo");
            DropTable("dbo.Sxc_VideoSeries");
            DropTable("dbo.Sxc_CommodityVideoSeries");
        }
    }
}
