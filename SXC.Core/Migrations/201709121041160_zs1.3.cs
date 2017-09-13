namespace SXC.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class zs13 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sxc_Course", "Price", c => c.Decimal(storeType: "money"));
            AddColumn("dbo.Sxc_Course", "Period", c => c.Double());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sxc_Course", "Period");
            DropColumn("dbo.Sxc_Course", "Price");
        }
    }
}
