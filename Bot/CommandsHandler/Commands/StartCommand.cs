using Bot.Data;
using Bot.Models;
using Telegram.Bot.Types;

namespace Bot.CommandsHandler.Commands
{
    public class StartCommand : ICommandProcessor
    {
        public static string CommandName => @"/start";

        public bool CanProcess(ICommand command)
        {
            return command.CommandName.ToLower() == CommandName.ToLower();
        }

        public CommandResult ProcessCommand(Command command)
        {
            if (!CanProcess(command)) throw new ArgumentException(nameof(command));

            Player.TryCreateNew(command.User.Username, command.User.Id);

            return new CommandResult(PuzzleMessage.GetInformationString(PuzzleMessage.InformationType.Start));
        }
    }
}