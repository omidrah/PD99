namespace DataLayer.Context
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mob : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.User", "Mobile", c => c.String(maxLength: 15));
        }
        
        public override void Down()
        {
            DropColumn("dbo.User", "Mobile");
        }
    }
}
