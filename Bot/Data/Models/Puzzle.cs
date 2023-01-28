using Bot.Data.Handler;
using System.Linq;
using System.Runtime.InteropServices;

namespace Bot.Models.Data
{
    public class Puzzle
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string Text { get; set; }
        public string Answers { get; set; }

        
        public readonly static Puzzle DefaultPuzzle = new Puzzle()
        {
            Name = "Зеленое чудо",
            Text = "В лесу она родилась, в лесу она росла.",
            Answers = "елочка ёлочка елка ёлка"
        };

        public bool CheckAnswer(string userAnswer)
        {
            foreach (var answer in Answers.Split())
            {
                if (userAnswer == answer)
                    return true;
            }

            return false;
        }

        public static Puzzle? For(Player player)
        {
            if (player is null) throw new ArgumentNullException($"Invalid player Reference: {player}");

            using (var context = new PlayerDBContext())
            {
                if (player.SolvedPuzzles is null)
                    return context.Puzzles.FirstOrDefault();

                if (context.Puzzles.Count() == 0)
                {
                    context.Puzzles.Add(Puzzle.DefaultPuzzle);
                    context.SaveChanges();
                }

                return context.Puzzles.FirstOrDefault(x => !player.SolvedPuzzles.Contains(x));
            }
        }
    }
}