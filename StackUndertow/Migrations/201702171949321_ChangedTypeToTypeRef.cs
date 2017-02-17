namespace StackUndertow.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedTypeToTypeRef : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ImgUploads", "TypeRef", c => c.String());
            DropColumn("dbo.ImgUploads", "Type");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ImgUploads", "Type", c => c.String());
            DropColumn("dbo.ImgUploads", "TypeRef");
        }
    }
}
