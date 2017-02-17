namespace StackUndertow.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedReferenceToImgUploads : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.ImgUploads", name: "Owner_Id", newName: "OwnerId");
            RenameIndex(table: "dbo.ImgUploads", name: "IX_Owner_Id", newName: "IX_OwnerId");
            AddColumn("dbo.ImgUploads", "Type", c => c.String());
            AddColumn("dbo.ImgUploads", "RefId", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ImgUploads", "RefId");
            DropColumn("dbo.ImgUploads", "Type");
            RenameIndex(table: "dbo.ImgUploads", name: "IX_OwnerId", newName: "IX_Owner_Id");
            RenameColumn(table: "dbo.ImgUploads", name: "OwnerId", newName: "Owner_Id");
        }
    }
}
