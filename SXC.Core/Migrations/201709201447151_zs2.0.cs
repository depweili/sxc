namespace SXC.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class zs20 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Sxc_OrderCommodity",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ShortContent = c.String(maxLength: 50),
                        Quantity = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, storeType: "money"),
                        Points = c.Int(nullable: false),
                        TotalAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalPoints = c.Int(nullable: false),
                        IsReal = c.Boolean(),
                        CommodityAttrsStr = c.String(maxLength: 200),
                        OrderInfoID = c.Int(nullable: false),
                        CommodityID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Sxc_Commodity", t => t.CommodityID)
                .ForeignKey("dbo.Sxc_OrderInfo", t => t.OrderInfoID)
                .Index(t => t.OrderInfoID)
                .Index(t => t.CommodityID);
            
            CreateTable(
                "dbo.Sxc_OrderInfo",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        OrderUID = c.Guid(nullable: false),
                        OrderSn = c.String(nullable: false, maxLength: 50),
                        ShortContent = c.String(maxLength: 100),
                        OrderStatus = c.Int(nullable: false),
                        StoreStatus = c.Int(nullable: false),
                        PayStatus = c.Int(nullable: false),
                        Consignee = c.String(nullable: false, maxLength: 20),
                        AddressRegion = c.String(nullable: false, maxLength: 100),
                        AddressDetail = c.String(nullable: false, maxLength: 100),
                        Zipcode = c.String(maxLength: 10),
                        Telephone = c.String(nullable: false, maxLength: 20),
                        UserNote = c.String(maxLength: 200),
                        TotalAmount = c.Decimal(nullable: false, storeType: "money"),
                        TotalPoints = c.Int(nullable: false),
                        PayFee = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DeliverFee = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Tax = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ToUserNote = c.String(maxLength: 200),
                        IsValid = c.Boolean(),
                        CreateTime = c.DateTime(),
                        UserIntegralID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Sxc_UserIntegral", t => t.UserIntegralID)
                .Index(t => t.OrderSn, unique: true)
                .Index(t => t.UserIntegralID);
            
            AddColumn("dbo.Sxc_IntegralRecord", "TotalPoints", c => c.Int(nullable: false));
            AddColumn("dbo.Sxc_IntegralRecord", "CurrentPoints", c => c.Int(nullable: false));
            AddColumn("dbo.Sxc_Commodity", "IsReal", c => c.Boolean());
            AlterColumn("dbo.Sxc_Commodity", "CommodityUID", c => c.Guid(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sxc_OrderInfo", "UserIntegralID", "dbo.Sxc_UserIntegral");
            DropForeignKey("dbo.Sxc_OrderCommodity", "OrderInfoID", "dbo.Sxc_OrderInfo");
            DropForeignKey("dbo.Sxc_OrderCommodity", "CommodityID", "dbo.Sxc_Commodity");
            DropIndex("dbo.Sxc_OrderInfo", new[] { "UserIntegralID" });
            DropIndex("dbo.Sxc_OrderInfo", new[] { "OrderSn" });
            DropIndex("dbo.Sxc_OrderCommodity", new[] { "CommodityID" });
            DropIndex("dbo.Sxc_OrderCommodity", new[] { "OrderInfoID" });
            AlterColumn("dbo.Sxc_Commodity", "CommodityUID", c => c.Guid(nullable: false, identity: true));
            DropColumn("dbo.Sxc_Commodity", "IsReal");
            DropColumn("dbo.Sxc_IntegralRecord", "CurrentPoints");
            DropColumn("dbo.Sxc_IntegralRecord", "TotalPoints");
            DropTable("dbo.Sxc_OrderInfo");
            DropTable("dbo.Sxc_OrderCommodity");
        }
    }
}
