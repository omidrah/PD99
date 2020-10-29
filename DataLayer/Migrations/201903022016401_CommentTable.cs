namespace DataLayer.Context
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CommentTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        NewsID = c.Int(nullable: false),
                        ParentID = c.Int(nullable: false),
                        Title = c.String(),
                        Name = c.String(),
                        Email = c.String(),
                        Text = c.String(),
                        IP = c.String(),
                        Date = c.DateTime(),
                        Confirm = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        Read = c.Boolean(nullable: false),
                        Like = c.Int(nullable: false),
                        Dislike = c.Int(nullable: false),
                        CreatedBy = c.Int(),
                        ModifiedBy = c.Int(),
                        CreatedDate = c.DateTime(),
                        ModifiedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.News", t => t.NewsID, cascadeDelete: true)
                .Index(t => t.NewsID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "NewsID", "dbo.News");
            DropIndex("dbo.Comments", new[] { "NewsID" });
            DropTable("dbo.Comments");
        }
    }
}
