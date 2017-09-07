namespace SXC.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class zs12 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sxc_Article", "Author", c => c.String(maxLength: 40));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sxc_Article", "Author");
        }
    }
}
