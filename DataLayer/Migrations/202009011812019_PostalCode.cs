namespace DataLayer.Context
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PostalCode : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.User", "PostalCode", c => c.String(maxLength: 10));
            AddColumn("dbo.Orders", "IsTransfered", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "IsTransfered");
            DropColumn("dbo.User", "PostalCode");
        }
    }
}
