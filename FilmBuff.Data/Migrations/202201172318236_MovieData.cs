namespace FilmBuff.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MovieData : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Movie",
                c => new
                    {
                        MovieId = c.Int(nullable: false, identity: true),
                        OwnerId = c.Guid(nullable: false),
                        Title = c.String(nullable: false),
                        Year = c.String(nullable: false, maxLength: 4),
                        DirectedBy = c.String(nullable: false),
                        CreatedUtc = c.DateTimeOffset(nullable: false, precision: 7),
                        ModifiedUtc = c.DateTimeOffset(precision: 7),
                    })
                .PrimaryKey(t => t.MovieId);
            
            AddColumn("dbo.Review", "Movie_MovieId", c => c.Int());
            CreateIndex("dbo.Review", "Movie_MovieId");
            AddForeignKey("dbo.Review", "Movie_MovieId", "dbo.Movie", "MovieId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Review", "Movie_MovieId", "dbo.Movie");
            DropIndex("dbo.Review", new[] { "Movie_MovieId" });
            DropColumn("dbo.Review", "Movie_MovieId");
            DropTable("dbo.Movie");
        }
    }
}
