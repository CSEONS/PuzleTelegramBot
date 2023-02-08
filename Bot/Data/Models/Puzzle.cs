using Bot.Data;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Bot.Models
{
    public class Puzzle
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string Answers { get; set; }

        public static CommandResult ParesAnswer(string answer, Puzzle puzzle)
        {
            if (puzzle is null)
                throw new ArgumentNullException(nameof(puzzle));

            foreach (var correctAnswer in puzzle.Answers.Split())
            {
                if (answer == correctAnswer)
                    return new CommandResult(PuzzleInformationMessage.Information[PuzzleInformationMessage.InformationType.CorrectAnswer]);
            }

            return new CommandResult(PuzzleInformationMessage.Information[PuzzleInformationMessage.InformationType.WrongAnswer]);
        }

        public static Puzzle? GetFirstUnsolvedFor(Player player)
        {
            var context = new PlayerDBContext();

            if (player.SolvedPuzzles is null)
                throw new NotImplementedException($"The solved puzzles is null: {JsonConvert.SerializeObject(player, Formatting.Indented)}");

            foreach (var puzzle in context.Puzzles)
            {
                if (player.SolvedPuzzles.Any(x => x == puzzle))
                    continue;

                return puzzle;
            }

            return null;
        }
    }
}