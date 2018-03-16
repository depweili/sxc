namespace SXC.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class zs40 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sxc_UserAccount", "BankName", c => c.String(maxLength: 50));
            AddColumn("dbo.Sxc_AccountWithdraw", "BankName", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sxc_AccountWithdraw", "BankName");
            DropColumn("dbo.Sxc_UserAccount", "BankName");
        }
    }
}
