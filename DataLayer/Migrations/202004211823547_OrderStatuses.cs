namespace DataLayer.Context
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrderStatuses : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.OrderDetails", "ProductID", "dbo.Products");
            DropIndex("dbo.OrderDetails", new[] { "ProductID" });
            CreateTable(
                "dbo.OrderStatus",
                c => new
                    {
                        Id = c.Byte(nullable: false, identity: true),
                        Title = c.String(maxLength: 50),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.OrderDetails", "ItemId", c => c.Int(nullable: false));
            AddColumn("dbo.OrderDetails", "ItemType", c => c.String());
            AddColumn("dbo.OrderDetails", "IsTransfered", c => c.Boolean(nullable: false));
            AddColumn("dbo.OrderDetails", "Price", c => c.Int(nullable: false));
            AddColumn("dbo.Orders", "OrderStatusId", c => c.Int(nullable: false));
            AddColumn("dbo.Orders", "OrderStatus_Id", c => c.Byte());
            CreateIndex("dbo.Orders", "OrderStatus_Id");
            AddForeignKey("dbo.Orders", "OrderStatus_Id", "dbo.OrderStatus", "Id");
            DropColumn("dbo.OrderDetails", "ProductID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.OrderDetails", "ProductID", c => c.Int(nullable: false));
            DropForeignKey("dbo.Orders", "OrderStatus_Id", "dbo.OrderStatus");
            DropIndex("dbo.Orders", new[] { "OrderStatus_Id" });
            DropColumn("dbo.Orders", "OrderStatus_Id");
            DropColumn("dbo.Orders", "OrderStatusId");
            DropColumn("dbo.OrderDetails", "Price");
            DropColumn("dbo.OrderDetails", "IsTransfered");
            DropColumn("dbo.OrderDetails", "ItemType");
            DropColumn("dbo.OrderDetails", "ItemId");
            DropTable("dbo.OrderStatus");
            CreateIndex("dbo.OrderDetails", "ProductID");
            AddForeignKey("dbo.OrderDetails", "ProductID", "dbo.Products", "ProductId", cascadeDelete: true);
        }
    }
}
