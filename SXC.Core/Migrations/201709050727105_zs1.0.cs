namespace SXC.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class zs10 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Sxc_Agent",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        PID = c.Int(),
                        AgentID = c.Guid(nullable: false),
                        Code = c.String(maxLength: 20),
                        Type = c.Int(nullable: false),
                        Level = c.Int(nullable: false),
                        State = c.Int(nullable: false),
                        Commission = c.Decimal(nullable: false, storeType: "money"),
                        IsValid = c.Boolean(),
                        CreateTime = c.DateTime(),
                        Area_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Sxc_Base_Area", t => t.Area_ID)
                .ForeignKey("dbo.Sxc_Agent", t => t.PID)
                .ForeignKey("dbo.Sxc_User", t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.PID)
                .Index(t => t.Code, unique: true)
                .Index(t => t.Area_ID);
            
            CreateTable(
                "dbo.Sxc_Base_Area",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PID = c.Int(nullable: false),
                        Name = c.String(maxLength: 20),
                        Level = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Sxc_Teacher", "Character", c => c.String(maxLength: 200));
            CreateIndex("dbo.Sxc_UserAuth", new[] { "IdentityType", "Identifier" }, unique: true, name: "UserAuthIdentity");
            DropColumn("dbo.Sxc_UserProfile", "AgentCode");
            DropColumn("dbo.Sxc_UserProfile", "IsAgent");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Sxc_UserProfile", "IsAgent", c => c.Boolean(nullable: false));
            AddColumn("dbo.Sxc_UserProfile", "AgentCode", c => c.String(maxLength: 50));
            DropForeignKey("dbo.Sxc_Agent", "ID", "dbo.Sxc_User");
            DropForeignKey("dbo.Sxc_Agent", "PID", "dbo.Sxc_Agent");
            DropForeignKey("dbo.Sxc_Agent", "Area_ID", "dbo.Sxc_Base_Area");
            DropIndex("dbo.Sxc_UserAuth", "UserAuthIdentity");
            DropIndex("dbo.Sxc_Agent", new[] { "Area_ID" });
            DropIndex("dbo.Sxc_Agent", new[] { "Code" });
            DropIndex("dbo.Sxc_Agent", new[] { "PID" });
            DropIndex("dbo.Sxc_Agent", new[] { "ID" });
            DropColumn("dbo.Sxc_Teacher", "Character");
            DropTable("dbo.Sxc_Base_Area");
            DropTable("dbo.Sxc_Agent");
        }
    }
}
