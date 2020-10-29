namespace DataLayer.Context
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class orderDetailShowCusumer : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderDetails", "OrderDetailParenID", c => c.Int());
            AddColumn("dbo.OrderDetails", "ShowForCusomer", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrderDetails", "ShowForCusomer");
            DropColumn("dbo.OrderDetails", "OrderDetailParenID");
        }
    }
}
