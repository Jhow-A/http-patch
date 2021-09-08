using Microsoft.EntityFrameworkCore;
using PatchTest.Model;

namespace PatchTest.Data
{
    public class MovieContext : DbContext
    {
        public MovieContext(DbContextOptions<MovieContext> options)
            : base(options)
        { }

        public DbSet<Movie> Movie { get; set; }
    }
}
