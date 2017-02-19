namespace StackUndertow.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedEventOwnerIdToName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ScoreLogs", "EventOwnerName", c => c.String());
            DropColumn("dbo.ScoreLogs", "EventOwnerId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ScoreLogs", "EventOwnerId", c => c.String());
            DropColumn("dbo.ScoreLogs", "EventOwnerName");
        }
    }
}
