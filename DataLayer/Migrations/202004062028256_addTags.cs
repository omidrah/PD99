namespace DataLayer.Context
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addTags : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        TagId = c.Int(nullable: false, identity: true),
                        tagTitle = c.String(),
                        CreatedBy = c.Int(),
                        CreatedDate = c.DateTime(),
                        Modifyby = c.Int(),
                        ModifiedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.TagId);
            
            CreateTable(
                "dbo.TagsItems",
                c => new
                    {
                        TagItemId = c.Int(nullable: false, identity: true),
                        tagId = c.Int(nullable: false),
                        itemId = c.Int(nullable: false),
                        itemType = c.String(),
                        CreatedBy = c.Int(),
                        CreatedDate = c.DateTime(),
                        Modifyby = c.Int(),
                        ModifiedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.TagItemId)
                .ForeignKey("dbo.Tags", t => t.tagId, cascadeDelete: true)
                .Index(t => t.tagId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TagsItems", "tagId", "dbo.Tags");
            DropIndex("dbo.TagsItems", new[] { "tagId" });
            DropTable("dbo.TagsItems");
            DropTable("dbo.Tags");
        }
    }
}
