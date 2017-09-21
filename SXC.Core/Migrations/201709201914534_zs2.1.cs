namespace SXC.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class zs21 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Sxc_OrderIntegralRecord",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        OrderInfoID = c.Int(nullable: false),
                        IntegralRecordID = c.Int(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Sxc_IntegralRecord", t => t.IntegralRecordID)
                .ForeignKey("dbo.Sxc_OrderInfo", t => t.OrderInfoID)
                .Index(t => t.OrderInfoID)
                .Index(t => t.IntegralRecordID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sxc_OrderIntegralRecord", "OrderInfoID", "dbo.Sxc_OrderInfo");
            DropForeignKey("dbo.Sxc_OrderIntegralRecord", "IntegralRecordID", "dbo.Sxc_IntegralRecord");
            DropIndex("dbo.Sxc_OrderIntegralRecord", new[] { "IntegralRecordID" });
            DropIndex("dbo.Sxc_OrderIntegralRecord", new[] { "OrderInfoID" });
            DropTable("dbo.Sxc_OrderIntegralRecord");
        }
    }
}
