namespace DataLayer.Context
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IsMadrakTaeedcustomer : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "IsMadrakTaeed", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customers", "IsMadrakTaeed");
        }
    }
}
