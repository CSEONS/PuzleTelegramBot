using Bot.Models;
using Microsoft.EntityFrameworkCore;

namespace Bot.Data
{
    public class PlayerDBContext : DbContext
    {
        public DbSet<Player> Players { get; set; }
        public DbSet<Puzzle> Puzzles { get; set; }
        public DbSet<SolvedPuzzle> SolvedPuzzles { get; set; }

        public PlayerDBContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=MuzzlePuzzle;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseIdentityColumns(0, 1);
        }
    }
}
