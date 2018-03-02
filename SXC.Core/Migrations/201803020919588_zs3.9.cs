namespace SXC.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class zs39 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sxc_AccountWithdraw", "Name", c => c.String(maxLength: 20));
            AddColumn("dbo.Sxc_AccountWithdraw", "BankCard", c => c.String(maxLength: 20));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sxc_AccountWithdraw", "BankCard");
            DropColumn("dbo.Sxc_AccountWithdraw", "Name");
        }
    }
}
