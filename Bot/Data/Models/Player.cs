namespace Bot.Models.Data
{
    public class Player
    {

        public Player(long telegramIdentifier, string name)
        {
            TelegramIdentifier = telegramIdentifier;
            Name = name;
        }

        public static string defaultName = $"Player{new Random().Next(1000, 9999)}";


        public int Id { get; set; }
        public long TelegramIdentifier { get; set; }
        public string Name { get; set; }
        public int Rating { get; set; }
        public int Rank { get; set; }
        public Puzzle? CurrentPuzzle { get; set; }
        public List<Puzzle>? SolvedPuzzles { get; set; } = new();

        public string DisplayParams =>
        $"ID {Id}\n" +
        $"Name {Name}\n" +
        $"Solved puzzles {SolvedPuzzles?.Count ?? 0}\n" +
        $"Rating {Rating}\n" +
        $"Rank {Rank}\n";
    }
}