using Bot.Data;
using Bot.Data.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Numerics;
using Telegram.Bot.Types;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Bot.Models
{
    public class Puzzle
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string Answers { get; set; }

        public static CommandResult ParesAnswer(Command command)
        {
            using (var context = new MuzzlePuzzleDBContext())
            {
                Player player = context.Players.Include(x => x.CurrentPuzzle).FirstOrDefault(x => x.TelegramIdentifier == command.User.Id);

                if (player == null)
                    throw new ArgumentNullException(nameof(player));

                if (player.CurrentPuzzle is null)
                    return new CommandResult(PuzzleMessage.GetInformationString(PuzzleMessage.InformationType.TryGetPuzzle));

                if (CheckAnswerForCorrectness(command.FullCommand, player.CurrentPuzzle))
                {
                    context.SolvedPuzzles.Add(new SolvedPuzzle()
                    {
                        Player = player,
                        Puzzle = player.CurrentPuzzle
                    });

                    player.CurrentPuzzle = null;
                    context.SaveChanges();

                    return new CommandResult(PuzzleMessage.GetInformationString(PuzzleMessage.InformationType.CorrectAnswer));
                }
            }

            return new CommandResult(PuzzleMessage.GetInformationString(PuzzleMessage.InformationType.WrongAnswer));
        }

        private static bool CheckAnswerForCorrectness(string userAnswer, Puzzle? currentPuzzle)
        {
            if (currentPuzzle is null)
                throw new ArgumentNullException(nameof(currentPuzzle));

            foreach (var answer in currentPuzzle.Answers.ToLower().Split())
            {
                if (userAnswer.ToLower() == answer)
                    return true;
            }

            return false;
        }

        public static Puzzle? GetFirstUnsolvedFor(Player player)
        {
            using (var context = new MuzzlePuzzleDBContext())
            {
                List<SolvedPuzzle> solvedPuzzles = context.SolvedPuzzles.Where(x => x.Player.TelegramIdentifier == player.TelegramIdentifier).ToList();

                foreach (var puzzle in context.Puzzles)
                {
                    if (solvedPuzzles.All(x => x.Puzzle != puzzle))
                        continue;

                    return puzzle;
                }

                return null;
            }
        }
    }
}