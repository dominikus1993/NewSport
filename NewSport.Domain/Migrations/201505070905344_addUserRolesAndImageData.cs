namespace NewSport.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addUserRolesAndImageData : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Posts", "ImageData", c => c.Binary());
            AddColumn("dbo.Posts", "ImageMimeType", c => c.String());
            AddColumn("dbo.Users", "RoleId", c => c.Int());
            AddColumn("dbo.Users", "Avatar", c => c.Byte(nullable: false));
            AddColumn("dbo.Users", "AvatarMimeType", c => c.String());
            CreateIndex("dbo.Users", "RoleId");
            AddForeignKey("dbo.Users", "RoleId", "dbo.Roles", "Id");
            DropColumn("dbo.Users", "Roles");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "Roles", c => c.String());
            DropForeignKey("dbo.Users", "RoleId", "dbo.Roles");
            DropIndex("dbo.Users", new[] { "RoleId" });
            DropColumn("dbo.Users", "AvatarMimeType");
            DropColumn("dbo.Users", "Avatar");
            DropColumn("dbo.Users", "RoleId");
            DropColumn("dbo.Posts", "ImageMimeType");
            DropColumn("dbo.Posts", "ImageData");
            DropTable("dbo.Roles");
        }
    }
}
