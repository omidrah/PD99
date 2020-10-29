namespace DataLayer.Context
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SettingAddCreatebye : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Settings", "CreateDate", c => c.DateTime());
            AddColumn("dbo.Settings", "ModifiedDate", c => c.DateTime());
            AddColumn("dbo.Settings", "CreatedBy", c => c.Int());
            AddColumn("dbo.Settings", "ModifiedBy", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Settings", "ModifiedBy");
            DropColumn("dbo.Settings", "CreatedBy");
            DropColumn("dbo.Settings", "ModifiedDate");
            DropColumn("dbo.Settings", "CreateDate");
        }
    }
}
