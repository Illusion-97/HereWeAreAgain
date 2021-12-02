namespace HereWeAreAgain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Books",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ISBN = c.String(),
                        Title = c.String(),
                        Author = c.String(),
                        Price = c.Double(nullable: false),
                        EditionDate = c.DateTime(nullable: false),
                        State = c.Int(nullable: false),
                        PointsValue = c.Int(nullable: false),
                        Owner_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Owner_Id)
                .Index(t => t.Owner_Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Email = c.String(),
                        Password = c.String(),
                        TotalPoint = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Books", "Owner_Id", "dbo.Users");
            DropIndex("dbo.Books", new[] { "Owner_Id" });
            DropTable("dbo.Users");
            DropTable("dbo.Books");
        }
    }
}
