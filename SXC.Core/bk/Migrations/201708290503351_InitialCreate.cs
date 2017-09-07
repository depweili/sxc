namespace SXC.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Sxc_Article",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Type = c.Int(nullable: false),
                        Desc = c.String(maxLength: 200),
                        Content = c.String(storeType: "ntext"),
                        IsValid = c.Boolean(),
                        CreateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Sxc_Course",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Desc = c.String(maxLength: 50),
                        Pic = c.String(maxLength: 50),
                        Content = c.String(maxLength: 1000),
                        Order = c.Int(nullable: false),
                        IsValid = c.Boolean(),
                        CreateTime = c.DateTime(nullable: false),
                        Article_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Sxc_Article", t => t.Article_ID)
                .Index(t => t.Article_ID);
            
            CreateTable(
                "dbo.Sxc_Navigation",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Desc = c.String(nullable: false, maxLength: 50),
                        Pic = c.String(maxLength: 50),
                        Target = c.String(maxLength: 100),
                        Order = c.Int(nullable: false),
                        IsValid = c.Boolean(),
                        CreateTime = c.DateTime(nullable: false),
                        Article_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Sxc_Article", t => t.Article_ID)
                .Index(t => t.Article_ID);
            
            CreateTable(
                "dbo.Sxc_Teacher",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        IdentifyCode = c.String(maxLength: 50),
                        Type = c.Int(nullable: false),
                        Title = c.String(maxLength: 50),
                        Introduction = c.String(maxLength: 1000),
                        Pic = c.String(maxLength: 50),
                        Order = c.Int(nullable: false),
                        IsValid = c.Boolean(),
                        Article_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Sxc_Article", t => t.Article_ID)
                .Index(t => t.Article_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sxc_Teacher", "Article_ID", "dbo.Sxc_Article");
            DropForeignKey("dbo.Sxc_Navigation", "Article_ID", "dbo.Sxc_Article");
            DropForeignKey("dbo.Sxc_Course", "Article_ID", "dbo.Sxc_Article");
            DropIndex("dbo.Sxc_Teacher", new[] { "Article_ID" });
            DropIndex("dbo.Sxc_Navigation", new[] { "Article_ID" });
            DropIndex("dbo.Sxc_Course", new[] { "Article_ID" });
            DropTable("dbo.Sxc_Teacher");
            DropTable("dbo.Sxc_Navigation");
            DropTable("dbo.Sxc_Course");
            DropTable("dbo.Sxc_Article");
        }
    }
}
