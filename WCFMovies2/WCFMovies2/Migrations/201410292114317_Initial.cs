namespace WCFMovies2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Casts",
                c => new
                    {
                        CastId = c.Int(nullable: false, identity: true),
                        CastName = c.String(),
                        CastAge = c.Int(nullable: false),
                        CastGender = c.String(),
                    })
                .PrimaryKey(t => t.CastId);
            
            CreateTable(
                "dbo.CastMovies",
                c => new
                    {
                        CastMovieId = c.Int(nullable: false, identity: true),
                        Role = c.Int(nullable: false),
                        Cast_CastId = c.Int(),
                        Movies_MovieId = c.Int(),
                    })
                .PrimaryKey(t => t.CastMovieId)
                .ForeignKey("dbo.Casts", t => t.Cast_CastId)
                .ForeignKey("dbo.Movies", t => t.Movies_MovieId)
                .Index(t => t.Cast_CastId)
                .Index(t => t.Movies_MovieId);
            
            CreateTable(
                "dbo.Movies",
                c => new
                    {
                        MovieId = c.Int(nullable: false, identity: true),
                        MovieName = c.String(),
                        MovieGenre = c.String(),
                        MovieRating = c.String(),
                    })
                .PrimaryKey(t => t.MovieId);
            
            CreateTable(
                "dbo.TheaterMovies",
                c => new
                    {
                        TheaterMovieId = c.Int(nullable: false, identity: true),
                        Showtime = c.DateTime(nullable: false),
                        TicketPrice = c.Single(nullable: false),
                        MovieShowings_MovieId = c.Int(),
                        Theaters_TheaterId = c.Int(),
                    })
                .PrimaryKey(t => t.TheaterMovieId)
                .ForeignKey("dbo.Movies", t => t.MovieShowings_MovieId)
                .ForeignKey("dbo.Theaters", t => t.Theaters_TheaterId)
                .Index(t => t.MovieShowings_MovieId)
                .Index(t => t.Theaters_TheaterId);
            
            CreateTable(
                "dbo.Theaters",
                c => new
                    {
                        TheaterId = c.Int(nullable: false, identity: true),
                        TheaterName = c.String(),
                        TheaterAddress = c.String(),
                    })
                .PrimaryKey(t => t.TheaterId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TheaterMovies", "Theaters_TheaterId", "dbo.Theaters");
            DropForeignKey("dbo.TheaterMovies", "MovieShowings_MovieId", "dbo.Movies");
            DropForeignKey("dbo.CastMovies", "Movies_MovieId", "dbo.Movies");
            DropForeignKey("dbo.CastMovies", "Cast_CastId", "dbo.Casts");
            DropIndex("dbo.TheaterMovies", new[] { "Theaters_TheaterId" });
            DropIndex("dbo.TheaterMovies", new[] { "MovieShowings_MovieId" });
            DropIndex("dbo.CastMovies", new[] { "Movies_MovieId" });
            DropIndex("dbo.CastMovies", new[] { "Cast_CastId" });
            DropTable("dbo.Theaters");
            DropTable("dbo.TheaterMovies");
            DropTable("dbo.Movies");
            DropTable("dbo.CastMovies");
            DropTable("dbo.Casts");
        }
    }
}
