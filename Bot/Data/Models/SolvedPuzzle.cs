using Bot.Models;

namespace Bot.Data
{
    public class SolvedPuzzle
    {
        public int Id { get; set; }
        public int PlayerId { get; set; }
        public int PuzzleId { get; set; }
        public virtual Player Player { get; set; }
        public virtual Puzzle Puzzle { get; set; }

        public static void Add(Player player, Puzzle puzzle)
        {
            using (var context = new PlayerDBContext())
            {
                SolvedPuzzle solvedPuzzle = new SolvedPuzzle()
                {
                    Player = player,
                    Puzzle = puzzle
                };

                context.SolvedPuzzles.Add(solvedPuzzle);
            }
        }
    }
}
