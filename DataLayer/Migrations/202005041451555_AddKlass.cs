namespace DataLayer.Context
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddKlass : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.KelassDooreh",
                c => new
                    {
                        KelassDoorehId = c.Int(nullable: false, identity: true),
                        KelassId = c.Int(nullable: false),
                        DoorehaId = c.Int(nullable: false),
                        Cost = c.Int(),
                        StartDate = c.DateTime(),
                        EndDate = c.DateTime(),
                        HasVideo = c.Boolean(),
                        IsActive = c.Boolean(),
                        IsDeleted = c.Boolean(),
                        CreatedBy = c.Int(),
                        CreatedDate = c.DateTime(),
                        ModifiedBy = c.Int(),
                        ModifiedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.KelassDoorehId)
                .ForeignKey("dbo.Dooreha", t => t.DoorehaId, cascadeDelete: true)
                .ForeignKey("dbo.Kelass", t => t.KelassId, cascadeDelete: true)
                .Index(t => t.KelassId)
                .Index(t => t.DoorehaId);
            
            CreateTable(
                "dbo.Kelass",
                c => new
                    {
                        KelassId = c.Int(nullable: false, identity: true),
                        DourseId = c.Int(nullable: false),
                        MasterId = c.Int(nullable: false),
                        MahaleBargozariId = c.Int(),
                        Cost = c.Int(),
                        StartDate = c.DateTime(),
                        EndDate = c.DateTime(),
                        HasVideo = c.Boolean(),
                        IsActive = c.Boolean(),
                        IsDeleted = c.Boolean(),
                        CreatedBy = c.Int(),
                        CreatedDate = c.DateTime(),
                        ModifiedBy = c.Int(),
                        ModifiedDate = c.DateTime(),
                        Doruses_Id = c.Int(),
                        Masters_UserId = c.Int(),
                    })
                .PrimaryKey(t => t.KelassId)
                .ForeignKey("dbo.Doruses", t => t.Doruses_Id)
                .ForeignKey("dbo.MahaleBargozari", t => t.MahaleBargozariId)
                .ForeignKey("dbo.Masters", t => t.Masters_UserId)
                .Index(t => t.MahaleBargozariId)
                .Index(t => t.Doruses_Id)
                .Index(t => t.Masters_UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Kelass", "Masters_UserId", "dbo.Masters");
            DropForeignKey("dbo.Kelass", "MahaleBargozariId", "dbo.MahaleBargozari");
            DropForeignKey("dbo.KelassDooreh", "KelassId", "dbo.Kelass");
            DropForeignKey("dbo.Kelass", "Doruses_Id", "dbo.Doruses");
            DropForeignKey("dbo.KelassDooreh", "DoorehaId", "dbo.Dooreha");
            DropIndex("dbo.Kelass", new[] { "Masters_UserId" });
            DropIndex("dbo.Kelass", new[] { "Doruses_Id" });
            DropIndex("dbo.Kelass", new[] { "MahaleBargozariId" });
            DropIndex("dbo.KelassDooreh", new[] { "DoorehaId" });
            DropIndex("dbo.KelassDooreh", new[] { "KelassId" });
            DropTable("dbo.Kelass");
            DropTable("dbo.KelassDooreh");
        }
    }
}
