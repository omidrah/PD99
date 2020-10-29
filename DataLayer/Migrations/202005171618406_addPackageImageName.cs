namespace DataLayer.Context
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addPackageImageName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Packages", "ImageName", c => c.String(maxLength: 300));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Packages", "ImageName");
        }
    }
}
