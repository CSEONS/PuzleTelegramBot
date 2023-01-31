using Bot;
using Bot.Data.Handler;
using Telegram.Bot.Types;

namespace Bot.CommandsHandler.Commands
{
    public class StatusCommand : ICommandProcessor
    {
        public bool CanProcess(ICommand command)
        {
            return command.CommandName.ToLower() == "/status".ToLower();
        }

        public CommandResult ProcessCommand(Command command)
        {
            if (!CanProcess(command)) throw new ArgumentException(nameof(command));

            string commandResultText;

            using (var context = new PlayerDBContext())
            {
                commandResultText = context.Players.FirstOrDefault(x => x.TelegramIdentifier == command.User.Id).DisplayParams;
            }

            return new CommandResult(commandResultText);
        }
    }
}