namespace DataLayer.Context
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IsMadrakCheckedToCustomers : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "IsMadrakChecked", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customers", "IsMadrakChecked");
        }
    }
}
