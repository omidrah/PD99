namespace DataLayer.Context
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPrice : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DoorehaDorouse", "Cost", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.DoorehaDorouse", "Cost");
        }
    }
}
