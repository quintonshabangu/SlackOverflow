namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeSlackUserId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "SlackUserId", c => c.String(nullable: false));
            DropColumn("dbo.Posts", "ApplicationUserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Posts", "ApplicationUserId", c => c.Long(nullable: false));
            DropColumn("dbo.Posts", "SlackUserId");
        }
    }
}
