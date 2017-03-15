namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedTimeStampAndSlackId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "SlackId", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "SlackId");
        }
    }
}
