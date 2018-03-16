namespace SXC.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class zs42 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sxc_UserAccount", "PassWord", c => c.String(maxLength: 20));
            DropColumn("dbo.Sxc_UserAccount", "PassWword");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Sxc_UserAccount", "PassWword", c => c.String(maxLength: 20));
            DropColumn("dbo.Sxc_UserAccount", "PassWord");
        }
    }
}
