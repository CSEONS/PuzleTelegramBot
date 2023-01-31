using Bot.Data.Handler;
using Bot.Data.Models;
using Microsoft.EntityFrameworkCore;
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

        
        public readonly static Puzzle DefaultPuzzle = new()
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

        public static Puzzle For(Player player)
        {
            if (player is null) throw new ArgumentNullException($"{player}");

            using var context = new PlayerDBContext();
            if (player.SolvedPuzzles is null)
                return context.Puzzles.First();

            if (!context.Puzzles.Any())
            {
                context.Puzzles.Add(Puzzle.DefaultPuzzle);
                context.SaveChanges();
            }

            return context.Puzzles.FirstOrDefault(x => !player.SolvedPuzzles.Contains(x));
        }

        public static CommandResult ParseAnswerForPlayer(Command command)
        {
            using (var context = new PlayerDBContext())
            {
                Player player = context.Players.Include(x => x.CurrentPuzzle).FirstOrDefault(x => x.TelegramIdentifier == command.User.Id);

                if (player is null)
                    return CommandResult.Empty;

                if (player.CurrentPuzzle is null)
                {
                    player.CurrentPuzzle = Puzzle.DefaultPuzzle;
                    context.Update(player);
                    context.SaveChanges();
                }

                foreach (var answer in player.CurrentPuzzle.Answers.Split())
                {
                    if (command.FullCommand.ToLower() == answer.ToLower())
                    {
                        if (player.SolvedPuzzles is null)
                            player.SolvedPuzzles = new List<Puzzle>();

                        player.SolvedPuzzles.Add(player.CurrentPuzzle);
                        player.CurrentPuzzle = context.Puzzles.First(x => !player.SolvedPuzzles.Contains(x));

                        context.Update(player);
                        context.SaveChanges();
                        return new CommandResult(string.Format(PuzzleInformationMessage.PuzzleInformation[PuzzleInformationMessage.InformationType.CorrectAnswer], answer));
                    }
                }

                return new CommandResult(string.Format(PuzzleInformationMessage.PuzzleInformation[PuzzleInformationMessage.InformationType.WrongAnswer], player.Name));
            }
        }
    }
}