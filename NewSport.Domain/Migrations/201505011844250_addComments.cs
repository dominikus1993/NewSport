namespace NewSport.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addComments : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Message = c.String(),
                        CommentsDate = c.DateTime(nullable: false),
                        AuthorId = c.Int(),
                        PostId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.AuthorId)
                .ForeignKey("dbo.Posts", t => t.PostId)
                .Index(t => t.AuthorId)
                .Index(t => t.PostId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "PostId", "dbo.Posts");
            DropForeignKey("dbo.Comments", "AuthorId", "dbo.Users");
            DropIndex("dbo.Comments", new[] { "PostId" });
            DropIndex("dbo.Comments", new[] { "AuthorId" });
            DropTable("dbo.Comments");
        }
    }
}
