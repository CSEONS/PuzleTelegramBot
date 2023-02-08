namespace Bot.CommandsHandler.Commands
{
    public class AddPuzzleCommand : ICommandProcessor
    {
        public static string CommandName => @"/mycommand";

        public bool CanProcess(ICommand command)
        {
            return command.CommandName.ToLower() == CommandName.ToLower();
        }

        public CommandResult ProcessCommand(Command command)
        {
            if (!CanProcess(command)) throw new ArgumentException(nameof(command));

            string commandResultText = $"{command.CommandName} executed";

            return new CommandResult(commandResultText);
        }
    }
}