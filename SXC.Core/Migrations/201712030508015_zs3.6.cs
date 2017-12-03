namespace SXC.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class zs36 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sxc_Cooperation", "State", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sxc_Cooperation", "State");
        }
    }
}
