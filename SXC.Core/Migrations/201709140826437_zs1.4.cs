namespace SXC.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class zs14 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Sxc_UserIntegral",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        IntegralID = c.Guid(nullable: false),
                        TotalPoints = c.Int(nullable: false),
                        CurrentPoints = c.Int(nullable: false),
                        TotalExpense = c.Int(nullable: false),
                        IsValid = c.Boolean(),
                        CreateTime = c.DateTime(),
                        IntegralGradeID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Sxc_IntegralGrade", t => t.IntegralGradeID)
                .ForeignKey("dbo.Sxc_User", t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.IntegralGradeID);
            
            CreateTable(
                "dbo.Sxc_IntegralGrade",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Grade = c.Int(nullable: false),
                        Title = c.String(maxLength: 50),
                        Icon = c.String(maxLength: 50),
                        IsValid = c.Boolean(),
                        CreateTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Sxc_IntegralRecord",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ShortMark = c.String(maxLength: 20),
                        Points = c.Int(nullable: false),
                        ValidPoints = c.Int(nullable: false),
                        LockPoints = c.Int(nullable: false),
                        ExpensePoints = c.Int(nullable: false),
                        ExpiredPoints = c.Int(nullable: false),
                        Memo = c.String(maxLength: 200),
                        ExpiredTime = c.DateTime(),
                        RecordTime = c.DateTime(nullable: false),
                        IsValid = c.Boolean(nullable: false),
                        IntegralActivityID = c.Int(),
                        UserIntegralID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Sxc_IntegralActivity", t => t.IntegralActivityID)
                .ForeignKey("dbo.Sxc_UserIntegral", t => t.UserIntegralID)
                .Index(t => t.IntegralActivityID)
                .Index(t => t.UserIntegralID);
            
            CreateTable(
                "dbo.Sxc_IntegralActivity",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Type = c.Int(nullable: false),
                        Code = c.String(maxLength: 30),
                        Name = c.String(maxLength: 50),
                        Description = c.String(maxLength: 200),
                        MinGrade = c.Int(),
                        MaxGrade = c.Int(),
                        ArticleID = c.Int(),
                        BeginTime = c.DateTime(),
                        EndTime = c.DateTime(),
                        IsValid = c.Boolean(),
                        IsOpen = c.Boolean(),
                        CreateTime = c.DateTime(),
                        IntegralRuleID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Sxc_Article", t => t.ArticleID)
                .ForeignKey("dbo.Sxc_IntegralRule", t => t.IntegralRuleID)
                .Index(t => t.ArticleID)
                .Index(t => t.IntegralRuleID);
            
            CreateTable(
                "dbo.Sxc_IntegralRule",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Type = c.Int(nullable: false),
                        Description = c.String(maxLength: 500),
                        ArticleID = c.Int(),
                        Points = c.Int(),
                        Formula = c.String(maxLength: 50),
                        StepPoints = c.Int(),
                        StepInterval = c.String(maxLength: 20),
                        CycleType = c.String(maxLength: 20),
                        MaxPoints = c.Int(),
                        MaxTotalPoints = c.Int(),
                        IsValid = c.Boolean(),
                        CreateTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Sxc_Article", t => t.ArticleID)
                .Index(t => t.ArticleID);
            
            CreateTable(
                "dbo.Sxc_IntegralUserActivity",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TotalPoints = c.Int(nullable: false),
                        UserIntegralID = c.Int(nullable: false),
                        IntegralActivityID = c.Int(),
                        State = c.Int(nullable: false),
                        CompleteTime = c.DateTime(),
                        IsValid = c.Boolean(),
                        CreateTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Sxc_IntegralActivity", t => t.IntegralActivityID)
                .ForeignKey("dbo.Sxc_UserIntegral", t => t.UserIntegralID)
                .Index(t => t.UserIntegralID)
                .Index(t => t.IntegralActivityID);
            
            CreateTable(
                "dbo.Sxc_Cooperation",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 10),
                        MobilePhone = c.String(maxLength: 20),
                        AreaInfo = c.String(maxLength: 50),
                        Address = c.String(maxLength: 50),
                        Type = c.Int(nullable: false),
                        Level = c.Int(nullable: false),
                        AgentAreaInfo = c.String(maxLength: 50),
                        Memo = c.String(maxLength: 100),
                        ProcessDetail = c.String(maxLength: 500),
                        CreateTime = c.DateTime(nullable: false),
                        UserID = c.Int(nullable: false),
                        AreaID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Sxc_Base_Area", t => t.AreaID)
                .ForeignKey("dbo.Sxc_User", t => t.UserID)
                .Index(t => t.UserID)
                .Index(t => t.AreaID);
            
            CreateTable(
                "dbo.Sxc_IntegralSignIn",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        LastTime = c.DateTime(),
                        DurationDays = c.Int(nullable: false),
                        UserIntegralID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Sxc_UserIntegral", t => t.UserIntegralID)
                .Index(t => t.UserIntegralID);
            
            CreateTable(
                "dbo.Sxc_ReservationCourse",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ReservationID = c.Int(nullable: false),
                        CourseID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Sxc_Course", t => t.CourseID)
                .ForeignKey("dbo.Sxc_Reservation", t => t.ReservationID)
                .Index(t => t.ReservationID)
                .Index(t => t.CourseID);
            
            CreateTable(
                "dbo.Sxc_Reservation",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 10),
                        MobilePhone = c.String(maxLength: 20),
                        AreaInfo = c.String(maxLength: 20),
                        Address = c.String(maxLength: 50),
                        Purpose = c.String(maxLength: 40),
                        Memo = c.String(maxLength: 50),
                        ReservedDate = c.DateTime(),
                        State = c.Int(nullable: false),
                        ProcessDetail = c.String(maxLength: 500),
                        CreateTime = c.DateTime(nullable: false),
                        UserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Sxc_User", t => t.UserID)
                .Index(t => t.UserID);
            
            AddColumn("dbo.Sxc_Base_Area", "Type", c => c.Int(nullable: false));
            AddColumn("dbo.Sxc_Base_Area", "Code", c => c.String(maxLength: 20));
            AddColumn("dbo.Sxc_User", "LastActiveTime", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sxc_Reservation", "UserID", "dbo.Sxc_User");
            DropForeignKey("dbo.Sxc_ReservationCourse", "ReservationID", "dbo.Sxc_Reservation");
            DropForeignKey("dbo.Sxc_ReservationCourse", "CourseID", "dbo.Sxc_Course");
            DropForeignKey("dbo.Sxc_IntegralSignIn", "UserIntegralID", "dbo.Sxc_UserIntegral");
            DropForeignKey("dbo.Sxc_Cooperation", "UserID", "dbo.Sxc_User");
            DropForeignKey("dbo.Sxc_Cooperation", "AreaID", "dbo.Sxc_Base_Area");
            DropForeignKey("dbo.Sxc_UserIntegral", "ID", "dbo.Sxc_User");
            DropForeignKey("dbo.Sxc_IntegralUserActivity", "UserIntegralID", "dbo.Sxc_UserIntegral");
            DropForeignKey("dbo.Sxc_IntegralUserActivity", "IntegralActivityID", "dbo.Sxc_IntegralActivity");
            DropForeignKey("dbo.Sxc_IntegralRecord", "UserIntegralID", "dbo.Sxc_UserIntegral");
            DropForeignKey("dbo.Sxc_IntegralRecord", "IntegralActivityID", "dbo.Sxc_IntegralActivity");
            DropForeignKey("dbo.Sxc_IntegralActivity", "IntegralRuleID", "dbo.Sxc_IntegralRule");
            DropForeignKey("dbo.Sxc_IntegralRule", "ArticleID", "dbo.Sxc_Article");
            DropForeignKey("dbo.Sxc_IntegralActivity", "ArticleID", "dbo.Sxc_Article");
            DropForeignKey("dbo.Sxc_UserIntegral", "IntegralGradeID", "dbo.Sxc_IntegralGrade");
            DropIndex("dbo.Sxc_Reservation", new[] { "UserID" });
            DropIndex("dbo.Sxc_ReservationCourse", new[] { "CourseID" });
            DropIndex("dbo.Sxc_ReservationCourse", new[] { "ReservationID" });
            DropIndex("dbo.Sxc_IntegralSignIn", new[] { "UserIntegralID" });
            DropIndex("dbo.Sxc_Cooperation", new[] { "AreaID" });
            DropIndex("dbo.Sxc_Cooperation", new[] { "UserID" });
            DropIndex("dbo.Sxc_IntegralUserActivity", new[] { "IntegralActivityID" });
            DropIndex("dbo.Sxc_IntegralUserActivity", new[] { "UserIntegralID" });
            DropIndex("dbo.Sxc_IntegralRule", new[] { "ArticleID" });
            DropIndex("dbo.Sxc_IntegralActivity", new[] { "IntegralRuleID" });
            DropIndex("dbo.Sxc_IntegralActivity", new[] { "ArticleID" });
            DropIndex("dbo.Sxc_IntegralRecord", new[] { "UserIntegralID" });
            DropIndex("dbo.Sxc_IntegralRecord", new[] { "IntegralActivityID" });
            DropIndex("dbo.Sxc_UserIntegral", new[] { "IntegralGradeID" });
            DropIndex("dbo.Sxc_UserIntegral", new[] { "ID" });
            DropColumn("dbo.Sxc_User", "LastActiveTime");
            DropColumn("dbo.Sxc_Base_Area", "Code");
            DropColumn("dbo.Sxc_Base_Area", "Type");
            DropTable("dbo.Sxc_Reservation");
            DropTable("dbo.Sxc_ReservationCourse");
            DropTable("dbo.Sxc_IntegralSignIn");
            DropTable("dbo.Sxc_Cooperation");
            DropTable("dbo.Sxc_IntegralUserActivity");
            DropTable("dbo.Sxc_IntegralRule");
            DropTable("dbo.Sxc_IntegralActivity");
            DropTable("dbo.Sxc_IntegralRecord");
            DropTable("dbo.Sxc_IntegralGrade");
            DropTable("dbo.Sxc_UserIntegral");
        }
    }
}
