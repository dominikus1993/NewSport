namespace NewSport.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addAuthorIdField : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Posts", name: "Author_Id", newName: "AuthorId");
            RenameIndex(table: "dbo.Posts", name: "IX_Author_Id", newName: "IX_AuthorId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Posts", name: "IX_AuthorId", newName: "IX_Author_Id");
            RenameColumn(table: "dbo.Posts", name: "AuthorId", newName: "Author_Id");
        }
    }
}
