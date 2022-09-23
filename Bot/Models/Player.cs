namespace Models
{
    public class Player
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int SolvedPuzzles { get; set; }
        public int Rating { get; set; }
        public int Rank { get; set; }
    }
}