namespace SXC.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class zs35 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sxc_User", "SystemAccount", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sxc_User", "SystemAccount");
        }
    }
}
