namespace DataLayer.Context
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DoorehOnline : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Dooreha", "HasOnline", c => c.Boolean());
            AddColumn("dbo.Dooreha", "HasHozori", c => c.Boolean());
            AddColumn("dbo.Dooreha", "ImageName", c => c.String(maxLength: 350));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Dooreha", "ImageName");
            DropColumn("dbo.Dooreha", "HasHozori");
            DropColumn("dbo.Dooreha", "HasOnline");
        }
    }
}
