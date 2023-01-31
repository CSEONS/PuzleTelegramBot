using Bot.Data.Models;
using System.Text.RegularExpressions;

namespace Bot.CommandsHandler.Commands
{
    public class ChangeMessage : ICommandProcessor
    {
        private Regex _regex = new Regex("(?<=\\[).*(?<!\\])");

        public bool CanProcess(ICommand command)
        {
            return command.CommandName.ToLower() == "/changemessage".ToLower();
        }

        public CommandResult ProcessCommand(Command command)
        {
            if (!CanProcess(command)) throw new ArgumentException(nameof(command));

            string changedMessageType = command.FullCommand.Split()[1];

            if (Enum.TryParse<PuzzleInformationMessage.InformationType>(changedMessageType, out PuzzleInformationMessage.InformationType informationType))
                PuzzleInformationMessage.PuzzleInformation[informationType] = _regex.Match(command.FullCommand).Value;
            else
                return CommandResult.Empty;

            string commandResultText = $"{command.CommandName} executed";

            return new CommandResult(commandResultText);
        }
    }
}
