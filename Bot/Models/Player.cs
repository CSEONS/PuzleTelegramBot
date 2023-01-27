namespace Models
{
    public class Player
    {
        public long Id { get; set; }
        public long TelegramIdentifier { get; set; }
        public string? Name { get; set; }
        public int Rating { get; set; }
        public int Rank { get; set; }
        public Puzzle CurrentPuzzle { get; set; }
        public ICollection<Puzzle> SolvedPuzzles { get; set; }

        public string DisplayParams =>
        $"ID {Id}\n" +
        $"Name {Name}\n" +
        $"Solved puzzles {SolvedPuzzles.Count}\n" +
        $"Rating {Rating}\n" +
        $"Rank {Rank}\n";
    }
}