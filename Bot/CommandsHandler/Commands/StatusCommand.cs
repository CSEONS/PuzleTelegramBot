using Bot;
using Bot.Data;
using Bot.Models;
using System.Text;
using Telegram.Bot.Types;

namespace Bot.CommandsHandler.Commands
{
    public class StatusCommand : ICommandProcessor
    {
        public static string CommandName => _commandName;
        public Player.Permissions Permission => Player.Permissions.User;

        private static string _commandName = @"/status";

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

            string commandResultText;

            using (var context = new MuzzlePuzzleDBContext())
            {
                commandResultText = context.Players.FirstOrDefault(x => x.TelegramIdentifier == command.User.Id)?.ToString();
            }

            return new CommandResult(commandResultText);
        }
    }
}