namespace StackUndertow.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MadeCreatedNullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Answers", "Created", c => c.DateTime());
            AlterColumn("dbo.Questions", "Created", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Questions", "Created", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Answers", "Created", c => c.DateTime(nullable: false));
        }
    }
}
