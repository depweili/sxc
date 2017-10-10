namespace SXC.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class zs23 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Sxc_Course", "Period", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Sxc_Course", "Period", c => c.Double());
        }
    }
}
