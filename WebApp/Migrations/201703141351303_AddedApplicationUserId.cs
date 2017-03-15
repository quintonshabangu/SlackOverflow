namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedApplicationUserId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "ApplicationUserId", c => c.Long(nullable: false));
            AlterColumn("dbo.Posts", "TimeStamp", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Posts", "TimeStamp", c => c.String());
            DropColumn("dbo.Posts", "ApplicationUserId");
        }
    }
}
