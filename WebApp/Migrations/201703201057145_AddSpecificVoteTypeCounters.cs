namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSpecificVoteTypeCounters : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "UpVotes", c => c.Int(nullable: false));
            AddColumn("dbo.Posts", "DownVotes", c => c.Int(nullable: false));
            DropColumn("dbo.Posts", "Votes");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Posts", "Votes", c => c.Int(nullable: false));
            DropColumn("dbo.Posts", "DownVotes");
            DropColumn("dbo.Posts", "UpVotes");
        }
    }
}
