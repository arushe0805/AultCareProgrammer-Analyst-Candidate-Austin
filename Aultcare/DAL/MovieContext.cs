using Aultcare.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Aultcare.DAL
{
    public class MovieContext : DbContext
    {

        public MovieContext() : base("MovieContext")
        {
        }

        public DbSet<Movie> Movies { get; set; }

        public DbSet<ActorMovie> ActorMovies { get; set; }

        public DbSet<Actor> Actors { get; set; }

        public DbSet<GenreMovie> GenreMovies { get; set; }

        public DbSet<Genre> Genres { get; set; }

    }

}
