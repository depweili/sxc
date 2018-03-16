namespace SXC.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class zs41 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sxc_UserAccount", "BranchBankName", c => c.String(maxLength: 50));
            AddColumn("dbo.Sxc_UserAccount", "MobilePhone", c => c.String(maxLength: 50));
            AddColumn("dbo.Sxc_AccountWithdraw", "BranchBankName", c => c.String(maxLength: 50));
            AddColumn("dbo.Sxc_AccountWithdraw", "MobilePhone", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sxc_AccountWithdraw", "MobilePhone");
            DropColumn("dbo.Sxc_AccountWithdraw", "BranchBankName");
            DropColumn("dbo.Sxc_UserAccount", "MobilePhone");
            DropColumn("dbo.Sxc_UserAccount", "BranchBankName");
        }
    }
}
