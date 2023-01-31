using Bot.CommandsHandler.Commands;

namespace Bot.CommandsHandler
{
    public class CommandExecuter
    {
        static readonly Dictionary<string, ICommandProcessor> Commands = new()
        {
            {@"/start", new StartCommand()},
            {@"/status",new StatusCommand()},
            {@"/changemessage", new ChangeMessage()},
        };

        public static CommandResult ExecuteCommand(Command command)
        {
            ICommandProcessor commandProcessor = Commands.FirstOrDefault(x => x.Key.ToLower() == command.CommandName.Split().FirstOrDefault()?.ToLower()).Value;

            if (commandProcessor is null)
                return CommandResult.Empty;

            return commandProcessor.ProcessCommand(command);
        }

        public static bool TryParse(string commandName, out ICommandProcessor commandProcessor)
        {
            var command = commandName.Split().FirstOrDefault()?.ToLower();

            commandProcessor = Commands.FirstOrDefault(x => x.Key == command).Value;

            if (commandProcessor is null)
                return false;

            return true;
        }
    }
}
