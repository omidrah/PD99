namespace DataLayer.Context
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class useraddconfirmCode : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.User", "confirmCode", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.User", "confirmCode");
        }
    }
}
