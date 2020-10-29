namespace DataLayer.Context
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateNews : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.News", "HeadTitle", c => c.String());
            AddColumn("dbo.News", "Visit", c => c.Long(nullable: false));
            AddColumn("dbo.News", "Like", c => c.Int(nullable: false));
            AddColumn("dbo.News", "Dislike", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.News", "Dislike");
            DropColumn("dbo.News", "Like");
            DropColumn("dbo.News", "Visit");
            DropColumn("dbo.News", "HeadTitle");
        }
    }
}
