using Bot;
using Bot.Data;
using Telegram.Bot.Types;

namespace Bot.CommandsHandler.Commands
{
    public class StatusCommand : ICommandProcessor
    {
        public static string CommandName => @"/status";

        public bool CanProcess(ICommand command)
        {
            return command.CommandName.ToLower() == CommandName.ToLower();
        }

        public CommandResult ProcessCommand(Command command)
        {
            if (!CanProcess(command)) throw new ArgumentException(nameof(command));

            string commandResultText;

            using (var context = new PlayerDBContext())
            {
                commandResultText = context.Players.FirstOrDefault(x => x.TelegramIdentifier == command.User.Id)?.ToString();
            }

            return new CommandResult(commandResultText);
        }
    }
}