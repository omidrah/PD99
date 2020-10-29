namespace DataLayer.Context
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FeaturesCorection : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ProductFeatures", "Features_FeatureId", "dbo.Features");
            DropIndex("dbo.ProductFeatures", new[] { "Features_FeatureId" });
            RenameColumn(table: "dbo.ProductFeatures", name: "Features_FeatureId", newName: "FeatureId");
            AlterColumn("dbo.ProductFeatures", "FeatureId", c => c.Int(nullable: false));
            CreateIndex("dbo.ProductFeatures", "FeatureId");
            AddForeignKey("dbo.ProductFeatures", "FeatureId", "dbo.Features", "FeatureId", cascadeDelete: true);
            DropColumn("dbo.ProductFeatures", "FeatueId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ProductFeatures", "FeatueId", c => c.Int(nullable: false));
            DropForeignKey("dbo.ProductFeatures", "FeatureId", "dbo.Features");
            DropIndex("dbo.ProductFeatures", new[] { "FeatureId" });
            AlterColumn("dbo.ProductFeatures", "FeatureId", c => c.Int());
            RenameColumn(table: "dbo.ProductFeatures", name: "FeatureId", newName: "Features_FeatureId");
            CreateIndex("dbo.ProductFeatures", "Features_FeatureId");
            AddForeignKey("dbo.ProductFeatures", "Features_FeatureId", "dbo.Features", "FeatureId");
        }
    }
}
