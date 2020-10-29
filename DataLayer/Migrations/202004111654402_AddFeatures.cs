namespace DataLayer.Context
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFeatures : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductFeatures",
                c => new
                    {
                        ProductFeatureValueID = c.Int(nullable: false, identity: true),
                        FeatueId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        FeatureValue = c.String(),
                        IsActive = c.Boolean(),
                        IsDeleted = c.Boolean(),
                        CreatedBy = c.Int(),
                        CreatedDate = c.DateTime(),
                        ModifiedBy = c.Int(),
                        ModifiedDate = c.DateTime(),
                        Features_FeatureId = c.Int(),
                    })
                .PrimaryKey(t => t.ProductFeatureValueID)
                .ForeignKey("dbo.Features", t => t.Features_FeatureId)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId)
                .Index(t => t.Features_FeatureId);
            
            CreateTable(
                "dbo.Features",
                c => new
                    {
                        FeatureId = c.Int(nullable: false, identity: true),
                        FeaturesName = c.String(nullable: false, maxLength: 250),
                        featuresDispaly = c.String(maxLength: 250),
                        IsActive = c.Boolean(),
                        IsDeleted = c.Boolean(),
                        CreateDate = c.DateTime(),
                        ModifyDate = c.DateTime(),
                        CreatedBy = c.Int(),
                        ModifyBy = c.Int(),
                    })
                .PrimaryKey(t => t.FeatureId);
            
            CreateTable(
                "dbo.ProductGroupFeatures",
                c => new
                    {
                        ProductGroupFeatuesId = c.Int(nullable: false, identity: true),
                        ProductGroupId = c.Int(nullable: false),
                        FeaturesId = c.Int(nullable: false),
                        IsActive = c.Boolean(),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.Int(),
                        ModifiedDate = c.DateTime(),
                        ModifiedBy = c.Int(),
                        Features_FeatureId = c.Int(),
                    })
                .PrimaryKey(t => t.ProductGroupFeatuesId)
                .ForeignKey("dbo.Features", t => t.Features_FeatureId)
                .ForeignKey("dbo.ProductGroup", t => t.ProductGroupId, cascadeDelete: true)
                .Index(t => t.ProductGroupId)
                .Index(t => t.Features_FeatureId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductFeatures", "ProductId", "dbo.Products");
            DropForeignKey("dbo.ProductGroupFeatures", "ProductGroupId", "dbo.ProductGroup");
            DropForeignKey("dbo.ProductGroupFeatures", "Features_FeatureId", "dbo.Features");
            DropForeignKey("dbo.ProductFeatures", "Features_FeatureId", "dbo.Features");
            DropIndex("dbo.ProductGroupFeatures", new[] { "Features_FeatureId" });
            DropIndex("dbo.ProductGroupFeatures", new[] { "ProductGroupId" });
            DropIndex("dbo.ProductFeatures", new[] { "Features_FeatureId" });
            DropIndex("dbo.ProductFeatures", new[] { "ProductId" });
            DropTable("dbo.ProductGroupFeatures");
            DropTable("dbo.Features");
            DropTable("dbo.ProductFeatures");
        }
    }
}
