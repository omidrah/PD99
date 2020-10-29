namespace DataLayer.Context
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSettingTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Settings",
                c => new
                    {
                        Id = c.Byte(nullable: false, identity: true),
                        PostBasePrice = c.Int(nullable: false),
                        PostPricePerUnit = c.Int(nullable: false),
                        IsDeleted = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Products", "Weight", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "Weight");
            DropTable("dbo.Settings");
        }
    }
}
