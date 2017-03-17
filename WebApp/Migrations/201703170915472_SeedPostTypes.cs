namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class SeedPostTypes : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO PostTypes VALUES (0, 'Question')");
            Sql("INSERT INTO PostTypes VALUES (1, 'Answer')");
            Sql("INSERT INTO PostTypes VALUES (2, 'Comment')");
        }

        public override void Down()
        {

        }
    }
}
