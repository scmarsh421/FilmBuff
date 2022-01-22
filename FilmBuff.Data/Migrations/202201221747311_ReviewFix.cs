namespace FilmBuff.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReviewFix : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Review", "Movie_MovieId", "dbo.Movie");
            DropIndex("dbo.Review", new[] { "Movie_MovieId" });
            RenameColumn(table: "dbo.Review", name: "Movie_MovieId", newName: "MovieId");
            AlterColumn("dbo.Review", "MovieId", c => c.Int(nullable: false));
            CreateIndex("dbo.Review", "MovieId");
            AddForeignKey("dbo.Review", "MovieId", "dbo.Movie", "MovieId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Review", "MovieId", "dbo.Movie");
            DropIndex("dbo.Review", new[] { "MovieId" });
            AlterColumn("dbo.Review", "MovieId", c => c.Int());
            RenameColumn(table: "dbo.Review", name: "MovieId", newName: "Movie_MovieId");
            CreateIndex("dbo.Review", "Movie_MovieId");
            AddForeignKey("dbo.Review", "Movie_MovieId", "dbo.Movie", "MovieId");
        }
    }
}
