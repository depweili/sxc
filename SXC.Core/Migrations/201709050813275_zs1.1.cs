namespace SXC.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class zs11 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Sxc_Course", name: "Article_ID", newName: "ArticleID");
            RenameColumn(table: "dbo.Sxc_Navigation", name: "Article_ID", newName: "ArticleID");
            RenameColumn(table: "dbo.Sxc_Promotion", name: "Article_ID", newName: "ArticleID");
            RenameColumn(table: "dbo.Sxc_Teacher", name: "Article_ID", newName: "ArticleID");
            RenameIndex(table: "dbo.Sxc_Course", name: "IX_Article_ID", newName: "IX_ArticleID");
            RenameIndex(table: "dbo.Sxc_Navigation", name: "IX_Article_ID", newName: "IX_ArticleID");
            RenameIndex(table: "dbo.Sxc_Promotion", name: "IX_Article_ID", newName: "IX_ArticleID");
            RenameIndex(table: "dbo.Sxc_Teacher", name: "IX_Article_ID", newName: "IX_ArticleID");
            AddColumn("dbo.Sxc_Agent", "SupAgentBindTime", c => c.DateTime());
            AddColumn("dbo.Sxc_UserProfile", "AvatarUrl", c => c.String(maxLength: 200));
            AddColumn("dbo.Sxc_Article", "Code", c => c.String(maxLength: 20));
            AlterColumn("dbo.Sxc_Promotion", "Desc", c => c.String(maxLength: 200));
            AlterColumn("dbo.Sxc_Promotion", "Pic", c => c.String(maxLength: 100));
            AlterColumn("dbo.Sxc_Promotion", "Location", c => c.String(maxLength: 100));
            AlterColumn("dbo.Sxc_Promotion", "Content", c => c.String(maxLength: 2000));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Sxc_Promotion", "Content", c => c.String());
            AlterColumn("dbo.Sxc_Promotion", "Location", c => c.String());
            AlterColumn("dbo.Sxc_Promotion", "Pic", c => c.String());
            AlterColumn("dbo.Sxc_Promotion", "Desc", c => c.String(maxLength: 50));
            DropColumn("dbo.Sxc_Article", "Code");
            DropColumn("dbo.Sxc_UserProfile", "AvatarUrl");
            DropColumn("dbo.Sxc_Agent", "SupAgentBindTime");
            RenameIndex(table: "dbo.Sxc_Teacher", name: "IX_ArticleID", newName: "IX_Article_ID");
            RenameIndex(table: "dbo.Sxc_Promotion", name: "IX_ArticleID", newName: "IX_Article_ID");
            RenameIndex(table: "dbo.Sxc_Navigation", name: "IX_ArticleID", newName: "IX_Article_ID");
            RenameIndex(table: "dbo.Sxc_Course", name: "IX_ArticleID", newName: "IX_Article_ID");
            RenameColumn(table: "dbo.Sxc_Teacher", name: "ArticleID", newName: "Article_ID");
            RenameColumn(table: "dbo.Sxc_Promotion", name: "ArticleID", newName: "Article_ID");
            RenameColumn(table: "dbo.Sxc_Navigation", name: "ArticleID", newName: "Article_ID");
            RenameColumn(table: "dbo.Sxc_Course", name: "ArticleID", newName: "Article_ID");
        }
    }
}
