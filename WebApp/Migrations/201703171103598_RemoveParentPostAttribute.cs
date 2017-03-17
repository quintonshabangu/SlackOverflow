namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveParentPostAttribute : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Posts", "ParentPost_Id", "dbo.Posts");
            DropIndex("dbo.Posts", new[] { "ParentPost_Id" });
            DropColumn("dbo.Posts", "ParentPost_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Posts", "ParentPost_Id", c => c.Long());
            CreateIndex("dbo.Posts", "ParentPost_Id");
            AddForeignKey("dbo.Posts", "ParentPost_Id", "dbo.Posts", "Id");
        }
    }
}
