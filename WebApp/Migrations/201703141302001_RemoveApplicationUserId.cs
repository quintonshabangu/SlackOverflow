namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveApplicationUserId : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Posts", "ApplicationUserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Posts", "ApplicationUserId", c => c.Int(nullable: false));
        }
    }
}
