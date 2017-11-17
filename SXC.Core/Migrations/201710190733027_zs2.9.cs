namespace SXC.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class zs29 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sxc_Commodity", "ArticleID", c => c.Int());
            CreateIndex("dbo.Sxc_Commodity", "ArticleID");
            AddForeignKey("dbo.Sxc_Commodity", "ArticleID", "dbo.Sxc_Article", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sxc_Commodity", "ArticleID", "dbo.Sxc_Article");
            DropIndex("dbo.Sxc_Commodity", new[] { "ArticleID" });
            DropColumn("dbo.Sxc_Commodity", "ArticleID");
        }
    }
}
