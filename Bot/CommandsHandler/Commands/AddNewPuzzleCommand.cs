using Bot.Data;
using Bot.Models;
using System.Text;
using System.Text.RegularExpressions;

namespace Bot.CommandsHandler.Commands
{
    public class AddPuzzleCommand : ICommandProcessor
    {
        public static string CommandName => _commandName;
        public Player.Permissions Permission => Player.Permissions.Administrator;

        private static string _commandName = @"/add";

        private Regex _puzzleTextMask = new Regex("(?<=\\[)(.*)(?=\\])");
        private Regex _answerMask = new Regex("(?<=\\()(.*)(?=\\))");

        public bool CanProcess(ICommand command)
        {
            return command.CommandName.ToLower() == _commandName.ToLower();
        }

        public string GetDescription()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine(_commandName);
            stringBuilder.AppendLine(MuzzlePuzzleMessage.GetDescriptionString(this));

            return stringBuilder.ToString();
        }

        public CommandResult ProcessCommand(Command command)
        {
            if (!CanProcess(command)) throw new ArgumentException(nameof(command));

            string text = _puzzleTextMask.Match(command.FullCommand).Value;
            string answers = _answerMask.Match(command.FullCommand).Value;

            if (string.IsNullOrEmpty(text) || string.IsNullOrEmpty(answers))
                return new CommandResult(MuzzlePuzzleMessage.GetInformationString(MuzzlePuzzleMessage.InformationType.WrongPuzzle));

            Puzzle puzzle = new Puzzle()
            {
                Answers = answers,
                Text = text
            };

            using (var context = new MuzzlePuzzleDBContext())
            {
                context.Puzzles.Add(puzzle);
                context.SaveChanges();
            }

            return new CommandResult(MuzzlePuzzleMessage.GetInformationString(MuzzlePuzzleMessage.InformationType.PuzzleAdded));
        }
    }
}