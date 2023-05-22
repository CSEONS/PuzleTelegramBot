using Bot.Data;
using Bot.Domain.Entities;
using System.Text;
using Telegram.Bot.Types;

namespace Bot.CommandsHandler.Commands
{
    public class StartCommand : ICommandProcessor
    {
        public static string CommandName => _commandName;
        public Player.Permissions Permission => Player.Permissions.User;

        private static string _commandName = @"/start";

        public bool CanProcess(ICommand command)
        {
            return command.CommandName.ToLower() == _commandName.ToLower();
        }

        public CommandResult ProcessCommand(Command command)
        {
            if (!CanProcess(command)) throw new ArgumentException(nameof(command));

            if (Player.Exist(command.User) is false)
                Player.CreateNew(command.User.Username, command.User.Id);

            return new CommandResult(MuzzlePuzzleMessage.GetInformationString(MuzzlePuzzleMessage.InformationType.Start));
        }

        public string GetDescription()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine(_commandName);
            stringBuilder.AppendLine(MuzzlePuzzleMessage.GetDescriptionString(this));

            return stringBuilder.ToString();
        }
    }
}