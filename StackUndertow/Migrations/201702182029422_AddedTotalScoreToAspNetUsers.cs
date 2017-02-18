namespace StackUndertow.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedTotalScoreToAspNetUsers : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "TotalScore", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "TotalScore");
        }
    }
}
