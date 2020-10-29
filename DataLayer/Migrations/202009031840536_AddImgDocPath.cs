namespace DataLayer.Context
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddImgDocPath : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "ImgDocPath", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customers", "ImgDocPath");
        }
    }
}
