namespace DataLayer.Context
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class orderSum : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "Sum", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "Sum");
        }
    }
}
