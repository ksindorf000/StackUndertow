namespace StackUndertow.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedImgUpload : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ImgUploads",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Caption = c.String(),
                        File = c.String(),
                        Owner_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Owner_Id)
                .Index(t => t.Owner_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ImgUploads", "Owner_Id", "dbo.AspNetUsers");
            DropIndex("dbo.ImgUploads", new[] { "Owner_Id" });
            DropTable("dbo.ImgUploads");
        }
    }
}
