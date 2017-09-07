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
                        Title = c.String(maxLength: 200),
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
                "dbo.Sxc_Promotion",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Desc = c.String(maxLength: 50),
                        Pic = c.String(),
                        Location = c.String(),
                        BeginTime = c.DateTime(),
                        EndTime = c.DateTime(),
                        Content = c.String(),
                        IsValid = c.Boolean(),
                        CreateTime = c.DateTime(),
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
            
            CreateTable(
                "dbo.Sxc_UserAuth",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        IdentityType = c.String(maxLength: 50),
                        Identifier = c.String(maxLength: 50),
                        Credential = c.String(maxLength: 100),
                        CreateTime = c.DateTime(),
                        IsValid = c.Boolean(),
                        LastActiveTime = c.DateTime(),
                        User_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Sxc_User", t => t.User_ID)
                .Index(t => t.User_ID);
            
            CreateTable(
                "dbo.Sxc_User",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserName = c.String(maxLength: 50),
                        Password = c.String(maxLength: 50),
                        AuthID = c.Guid(nullable: false),
                        IsValid = c.Boolean(),
                        CreateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Sxc_UserProfile",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        NickName = c.String(maxLength: 50),
                        RealName = c.String(maxLength: 50),
                        Gender = c.Int(),
                        Age = c.Int(),
                        Email = c.String(maxLength: 50),
                        Address = c.String(maxLength: 50),
                        MobilePhone = c.String(maxLength: 50),
                        IDCard = c.String(maxLength: 50),
                        AgentCode = c.String(maxLength: 50),
                        IsAgent = c.Boolean(nullable: false),
                        CreateTime = c.DateTime(),
                        IsVerified = c.Boolean(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Sxc_User", t => t.ID)
                .Index(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sxc_UserAuth", "User_ID", "dbo.Sxc_User");
            DropForeignKey("dbo.Sxc_UserProfile", "ID", "dbo.Sxc_User");
            DropForeignKey("dbo.Sxc_Teacher", "Article_ID", "dbo.Sxc_Article");
            DropForeignKey("dbo.Sxc_Promotion", "Article_ID", "dbo.Sxc_Article");
            DropForeignKey("dbo.Sxc_Navigation", "Article_ID", "dbo.Sxc_Article");
            DropForeignKey("dbo.Sxc_Course", "Article_ID", "dbo.Sxc_Article");
            DropIndex("dbo.Sxc_UserProfile", new[] { "ID" });
            DropIndex("dbo.Sxc_UserAuth", new[] { "User_ID" });
            DropIndex("dbo.Sxc_Teacher", new[] { "Article_ID" });
            DropIndex("dbo.Sxc_Promotion", new[] { "Article_ID" });
            DropIndex("dbo.Sxc_Navigation", new[] { "Article_ID" });
            DropIndex("dbo.Sxc_Course", new[] { "Article_ID" });
            DropTable("dbo.Sxc_UserProfile");
            DropTable("dbo.Sxc_User");
            DropTable("dbo.Sxc_UserAuth");
            DropTable("dbo.Sxc_Teacher");
            DropTable("dbo.Sxc_Promotion");
            DropTable("dbo.Sxc_Navigation");
            DropTable("dbo.Sxc_Course");
            DropTable("dbo.Sxc_Article");
        }
    }
}
