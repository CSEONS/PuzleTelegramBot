using Bot.Data.Models;
using Bot.Models;
using Microsoft.EntityFrameworkCore;

namespace Bot.Data
{
    public class MuzzlePuzzleDBContext : DbContext
    {
        public DbSet<Player> Players { get; set; }
        public DbSet<Puzzle> Puzzles { get; set; }
        public DbSet<SolvedPuzzle> SolvedPuzzles { get; set; }

        public MuzzlePuzzleDBContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=MuzzlePuzzle;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.UseIdentityColumns(0, 1);

            modelBuilder.Entity<SolvedPuzzle>(builder =>
            {
                builder.HasOne(p => p.Player);
                builder.HasOne(p => p.Puzzle);
            });

            modelBuilder.Entity<Player>()
                .HasOne(p => p.CurrentPuzzle);
        }
    }
}
