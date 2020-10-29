namespace DataLayer.Context
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Ad_OFFPercent : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Dooreha", "OffPercent", c => c.Byte());
            AddColumn("dbo.Packages", "OffPercent", c => c.Byte());
            AddColumn("dbo.Products", "OffPercent", c => c.Byte());
            AddColumn("dbo.Kelass", "OffPercent", c => c.Byte());
            AddColumn("dbo.Orders", "OffPercent", c => c.Byte());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "OffPercent");
            DropColumn("dbo.Kelass", "OffPercent");
            DropColumn("dbo.Products", "OffPercent");
            DropColumn("dbo.Packages", "OffPercent");
            DropColumn("dbo.Dooreha", "OffPercent");
        }
    }
}
