namespace DataLayer.Context
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddImageTitle : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Images", "Title", c => c.String(maxLength: 250));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Images", "Title");
        }
    }
}
