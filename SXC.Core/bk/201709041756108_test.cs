namespace SXC.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
    {
        public override void Up()
        {
            //CreateTable(
            //    "dbo.Sxc_Agent",
            //    c => new
            //        {
            //            ID = c.Int(nullable: false, identity: true),
            //            PID = c.Int(),
            //            AgentID = c.Guid(nullable: false),
            //            Code = c.String(maxLength: 20),
            //            Type = c.Int(nullable: false),
            //            Level = c.Int(nullable: false),
            //            State = c.Int(nullable: false),
            //            Commission = c.Decimal(nullable: false, storeType: "money"),
            //            IsValid = c.Boolean(),
            //            CreateTime = c.DateTime(),
            //            Area_ID = c.Int(),
            //        })
            //    .PrimaryKey(t => t.ID)
            //    .ForeignKey("dbo.Sxc_Base_Area", t => t.Area_ID)
            //    .ForeignKey("dbo.Sxc_Agent", t => t.PID)
            //    .Index(t => t.PID)
            //    .Index(t => t.Code, unique: true)
            //    .Index(t => t.Area_ID);
            
            //CreateTable(
            //    "dbo.Sxc_Base_Area",
            //    c => new
            //        {
            //            ID = c.Int(nullable: false, identity: true),
            //            PID = c.Int(nullable: false),
            //            Name = c.String(maxLength: 20),
            //            Level = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => t.ID);
            
            //CreateTable(
            //    "dbo.Sxc_User",
            //    c => new
            //        {
            //            ID = c.Int(nullable: false),
            //            UserName = c.String(maxLength: 50),
            //            Password = c.String(maxLength: 50),
            //            AuthID = c.Guid(nullable: false),
            //            IsValid = c.Boolean(),
            //            CreateTime = c.DateTime(nullable: false),
            //        })
            //    .PrimaryKey(t => t.ID)
            //    .ForeignKey("dbo.Sxc_Agent", t => t.ID)
            //    .Index(t => t.ID);
            
            //CreateTable(
            //    "dbo.Sxc_UserProfile",
            //    c => new
            //        {
            //            ID = c.Int(nullable: false),
            //            NickName = c.String(maxLength: 50),
            //            RealName = c.String(maxLength: 50),
            //            Gender = c.Int(),
            //            Age = c.Int(),
            //            Email = c.String(maxLength: 50),
            //            Address = c.String(maxLength: 50),
            //            MobilePhone = c.String(maxLength: 50),
            //            IDCard = c.String(maxLength: 50),
            //            IsAgent = c.Boolean(nullable: false),
            //            CreateTime = c.DateTime(),
            //            IsVerified = c.Boolean(),
            //        })
            //    .PrimaryKey(t => t.ID)
            //    .ForeignKey("dbo.Sxc_User", t => t.ID)
            //    .Index(t => t.ID);
            
            //CreateTable(
            //    "dbo.Sxc_Promotion",
            //    c => new
            //        {
            //            ID = c.Int(nullable: false, identity: true),
            //            Name = c.String(nullable: false, maxLength: 50),
            //            Desc = c.String(maxLength: 50),
            //            Pic = c.String(),
            //            Location = c.String(),
            //            BeginTime = c.DateTime(),
            //            EndTime = c.DateTime(),
            //            Content = c.String(),
            //            IsValid = c.Boolean(),
            //            CreateTime = c.DateTime(),
            //            Article_ID = c.Int(),
            //        })
            //    .PrimaryKey(t => t.ID)
            //    .ForeignKey("dbo.Sxc_Article", t => t.Article_ID)
            //    .Index(t => t.Article_ID);
            
            //CreateTable(
            //    "dbo.Sxc_UserAuth",
            //    c => new
            //        {
            //            ID = c.Int(nullable: false, identity: true),
            //            IdentityType = c.String(maxLength: 50),
            //            Identifier = c.String(maxLength: 50),
            //            Credential = c.String(maxLength: 100),
            //            CreateTime = c.DateTime(),
            //            IsValid = c.Boolean(),
            //            LastActiveTime = c.DateTime(),
            //            User_ID = c.Int(),
            //        })
            //    .PrimaryKey(t => t.ID)
            //    .ForeignKey("dbo.Sxc_User", t => t.User_ID)
            //    .Index(t => new { t.IdentityType, t.Identifier }, unique: true, name: "UserAuthIdentity")
            //    .Index(t => t.User_ID);
            
            //AddColumn("dbo.Sxc_Article", "Title", c => c.String(maxLength: 200));
            //AddColumn("dbo.Sxc_Teacher", "Character", c => c.String(maxLength: 200));
            //DropColumn("dbo.Sxc_Article", "Desc");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Sxc_Article", "Desc", c => c.String(maxLength: 200));
            DropForeignKey("dbo.Sxc_UserAuth", "User_ID", "dbo.Sxc_User");
            DropForeignKey("dbo.Sxc_Promotion", "Article_ID", "dbo.Sxc_Article");
            DropForeignKey("dbo.Sxc_User", "ID", "dbo.Sxc_Agent");
            DropForeignKey("dbo.Sxc_UserProfile", "ID", "dbo.Sxc_User");
            DropForeignKey("dbo.Sxc_Agent", "PID", "dbo.Sxc_Agent");
            DropForeignKey("dbo.Sxc_Agent", "Area_ID", "dbo.Sxc_Base_Area");
            DropIndex("dbo.Sxc_UserAuth", new[] { "User_ID" });
            DropIndex("dbo.Sxc_UserAuth", "UserAuthIdentity");
            DropIndex("dbo.Sxc_Promotion", new[] { "Article_ID" });
            DropIndex("dbo.Sxc_UserProfile", new[] { "ID" });
            DropIndex("dbo.Sxc_User", new[] { "ID" });
            DropIndex("dbo.Sxc_Agent", new[] { "Area_ID" });
            DropIndex("dbo.Sxc_Agent", new[] { "Code" });
            DropIndex("dbo.Sxc_Agent", new[] { "PID" });
            DropColumn("dbo.Sxc_Teacher", "Character");
            DropColumn("dbo.Sxc_Article", "Title");
            DropTable("dbo.Sxc_UserAuth");
            DropTable("dbo.Sxc_Promotion");
            DropTable("dbo.Sxc_UserProfile");
            DropTable("dbo.Sxc_User");
            DropTable("dbo.Sxc_Base_Area");
            DropTable("dbo.Sxc_Agent");
        }
    }
}
