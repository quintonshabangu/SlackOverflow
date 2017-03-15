namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedParentPost : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "ParentPost_Id", c => c.Long());
            CreateIndex("dbo.Posts", "ParentPost_Id");
            AddForeignKey("dbo.Posts", "ParentPost_Id", "dbo.Posts", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Posts", "ParentPost_Id", "dbo.Posts");
            DropIndex("dbo.Posts", new[] { "ParentPost_Id" });
            DropColumn("dbo.Posts", "ParentPost_Id");
        }
    }
}
