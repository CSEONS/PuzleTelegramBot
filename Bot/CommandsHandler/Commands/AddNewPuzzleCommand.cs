using Bot.Data;
using Bot.Models;
using System.Text;

namespace Bot.CommandsHandler.Commands
{
    public class AddPuzzleCommand : ICommandProcessor
    {
        public static string CommandName => _commandName;
        public Player.Permissions Permission => Player.Permissions.Administrator;

        private static string _commandName = @"/add";

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

            string commandResultText = $"{command.CommandName} executed";

            return new CommandResult(commandResultText);
        }
    }
}