namespace StackUndertow.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedDbSetForScoreLogs : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ScoreLogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TargetId = c.String(maxLength: 128),
                        Points = c.Int(nullable: false),
                        Event = c.String(),
                        EventOwnerId = c.String(),
                        Timestamp = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.TargetId)
                .Index(t => t.TargetId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ScoreLogs", "TargetId", "dbo.AspNetUsers");
            DropIndex("dbo.ScoreLogs", new[] { "TargetId" });
            DropTable("dbo.ScoreLogs");
        }
    }
}
