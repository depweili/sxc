namespace SXC.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class zs33 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sxc_Base_Area", "PCode", c => c.String(maxLength: 20));
            AddColumn("dbo.Sxc_Base_Area", "Area", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sxc_Base_Area", "Area");
            DropColumn("dbo.Sxc_Base_Area", "PCode");
        }
    }
}
