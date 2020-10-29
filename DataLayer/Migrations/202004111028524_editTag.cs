namespace DataLayer.Context
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editTag : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TagsItems", "tagId", "dbo.Tags");
            DropIndex("dbo.TagsItems", new[] { "tagId" });
            AddColumn("dbo.Tags", "itemId", c => c.Int(nullable: false));
            AddColumn("dbo.Tags", "tagConstant", c => c.String(maxLength: 150));
            AlterColumn("dbo.Tags", "tagTitle", c => c.String(maxLength: 150));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Tags", "tagTitle", c => c.String());
            DropColumn("dbo.Tags", "tagConstant");
            DropColumn("dbo.Tags", "itemId");
            CreateIndex("dbo.TagsItems", "tagId");
            AddForeignKey("dbo.TagsItems", "tagId", "dbo.Tags", "TagId", cascadeDelete: true);
        }
    }
}
