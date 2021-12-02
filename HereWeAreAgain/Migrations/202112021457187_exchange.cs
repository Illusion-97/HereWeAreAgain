namespace HereWeAreAgain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class exchange : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BookExchanges",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreationDate = c.DateTime(nullable: false),
                        Book_Id = c.Int(),
                        OldOwner_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Books", t => t.Book_Id)
                .ForeignKey("dbo.Users", t => t.OldOwner_Id)
                .Index(t => t.Book_Id)
                .Index(t => t.OldOwner_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BookExchanges", "OldOwner_Id", "dbo.Users");
            DropForeignKey("dbo.BookExchanges", "Book_Id", "dbo.Books");
            DropIndex("dbo.BookExchanges", new[] { "OldOwner_Id" });
            DropIndex("dbo.BookExchanges", new[] { "Book_Id" });
            DropTable("dbo.BookExchanges");
        }
    }
}
