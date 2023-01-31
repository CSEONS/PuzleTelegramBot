using Bot.Models.Data;
using Microsoft.EntityFrameworkCore;

namespace Bot.Data.Handler
{
    public class PlayerDBContext : DbContext
    {
        public DbSet<Player> Players { get; set; }
        public DbSet<Puzzle> Puzzles { get; internal set; }

        public PlayerDBContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=Puzzle;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseIdentityColumns(0, 1);
        }
    }
}
