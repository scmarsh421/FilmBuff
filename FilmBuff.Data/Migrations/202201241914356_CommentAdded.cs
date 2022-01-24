namespace FilmBuff.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CommentAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comment",
                c => new
                    {
                        CommentId = c.Int(nullable: false, identity: true),
                        OwnerId = c.Guid(nullable: false),
                        ReviewId = c.Int(nullable: false),
                        Content = c.String(nullable: false),
                        CreatedUtc = c.DateTimeOffset(nullable: false, precision: 7),
                        ModifiedUtc = c.DateTimeOffset(precision: 7),
                    })
                .PrimaryKey(t => t.CommentId)
                .ForeignKey("dbo.Review", t => t.ReviewId, cascadeDelete: true)
                .Index(t => t.ReviewId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comment", "ReviewId", "dbo.Review");
            DropIndex("dbo.Comment", new[] { "ReviewId" });
            DropTable("dbo.Comment");
        }
    }
}
