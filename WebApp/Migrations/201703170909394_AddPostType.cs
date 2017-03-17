namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPostType : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PostTypes",
                c => new
                    {
                        Id = c.Byte(nullable: false),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Posts", "PostType_Id", c => c.Byte());
            CreateIndex("dbo.Posts", "PostType_Id");
            AddForeignKey("dbo.Posts", "PostType_Id", "dbo.PostTypes", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Posts", "PostType_Id", "dbo.PostTypes");
            DropIndex("dbo.Posts", new[] { "PostType_Id" });
            DropColumn("dbo.Posts", "PostType_Id");
            DropTable("dbo.PostTypes");
        }
    }
}
