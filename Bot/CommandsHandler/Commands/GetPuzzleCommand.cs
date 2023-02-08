using Bot.Data;
using Bot.Models;

namespace Bot.CommandsHandler.Commands
{
    internal class GetPuzzleCommand : ICommandProcessor
    {
        public static string CommandName => @"/get";

        public bool CanProcess(ICommand command)
        {
            return command.CommandName.ToLower() == CommandName.ToLower();
        }

        public CommandResult ProcessCommand(Command command)
        {
            if (!CanProcess(command)) throw new ArgumentException(nameof(command));

            using (var context = new PlayerDBContext())
            {
                
            }

            return CommandResult.Empty;
        }
    }
}