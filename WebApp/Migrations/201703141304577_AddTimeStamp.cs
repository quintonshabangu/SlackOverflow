namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTimeStamp : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "TimeStamp", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Posts", "TimeStamp");
        }
    }
}
