namespace DataLayer.Context
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNews : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.News",
                c => new
                    {
                        NewsId = c.Int(nullable: false, identity: true),
                        ParentId = c.Int(),
                        GroupmanagerId = c.Int(),
                        NewsTitle = c.String(maxLength: 100),
                        NewsBody = c.String(),
                        IsAuthenticated = c.Boolean(),
                        IsDeleted = c.Boolean(),
                        MasterPicPathThumb = c.String(maxLength: 100),
                        MasterPicPath = c.String(maxLength: 100),
                        CreatedBy = c.Int(),
                        ModifiedBy = c.Int(),
                        CreatedDate = c.DateTime(),
                        ModifiedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.NewsId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.News");
        }
    }
}
