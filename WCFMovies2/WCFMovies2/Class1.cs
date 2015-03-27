using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Infrastructure;
using System.Data.Services.Common;

//http://localhost:19508/WcfDataService1.svc/Movies(1)
//http://localhost:19508/WcfDataService1.svc/GetAllMovies
//http://localhost:19508/WcfDataService1.svc/GetMovieById(1)

namespace WCFMoviesLib2
{
    public enum Role { director, producer, actor, actress, crew };

    public class MovieInfoContext : DbContext
    {
        public MovieInfoContext()
            : base("Server=.\\SQLEXPRESS;Database=MovieDB;User=DBUser;Password=Password00")
        {

        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Cast> CastPeople { get; set; }
        public DbSet<Theater> Theaters { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<Movie>().Ignore(m => m.MovieName);
            //modelBuilder.Entity<Movie>().Property(m => m.MovieName).HasColumnName("fdsfas");
        }
    }

    [EntitySet("Movies")]
    public class Movie
    {
        public Movie()
        {
            this.CastMembers = new List<CastMovie>();
            this.TheaterShowing = new List<TheaterMovie>();
        }

        [Key]
        public int MovieId { get; set; }

        public string MovieName { get; set; }

        public string MovieGenre { get; set; }

        public string MovieRating { get; set; }

        public virtual ICollection<CastMovie> CastMembers { get; set; }

        public virtual ICollection<TheaterMovie> TheaterShowing { get; set; }
    }

    public class CastMovie
    {
        [Key]
        public int CastMovieId { get; set; }

        public Cast Cast { get; set; }

        public Movie Movies { get; set; }

        public int Role { get; set; }
    }

    [EntitySet("Casts")]
    public class Cast
    {
        public Cast() { }

        [Key]
        public int CastId { get; set; }

        public string CastName { get; set; }

        public int CastAge { get; set; }

        public string CastGender { get; set; }

        public ICollection<CastMovie> CastMovies { get; set; }
    }

    public class TheaterMovie
    {
        [Key]
        public int TheaterMovieId { get; set; }

        public Movie MovieShowings { get; set; }

        public Theater Theaters { get; set; }

        public DateTime Showtime { get; set; }

        public float TicketPrice { get; set; }
    }

    public class Theater
    {
        public Theater()
        {

            this.MovieTheaters = new List<TheaterMovie>();
        }

        [Key]
        public int TheaterId { get; set; }

        public string TheaterName { get; set; }

        public string TheaterAddress { get; set; }

        public virtual ICollection<TheaterMovie> MovieTheaters { get; set; }
    }
}
