namespace StackUndertow.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedCreatedDateToQuestionAndAnswer : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Answers", "Created", c => c.DateTime(nullable: false));
            AddColumn("dbo.Questions", "Created", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Questions", "Created");
            DropColumn("dbo.Answers", "Created");
        }
    }
}
