namespace Bot.Configuraion
{
    public class BotConfiguration
    {
        public static string TOKEN = "5335512657:AAGLjrdcpXLT98ZdkoBNTWDkaRXafEVt1Zk";
    }

    public static class BotCommands
    {
        static Dictionary<Command, ICommandProcessor> Commands = new Dictionary<Command, ICommandProcessor>()
        {
            [new Command(@"/start")] = new StartCommand(),
            [new Command(@"/status")] = new StatusCommand()
        };

        public static CommandResult ExecuteCommend(Command command)
        {
            ICommandProcessor commandProcessor = Commands.FirstOrDefault(x => x.Key.CommandName == command.CommandName).Value;
            if (commandProcessor is null)
                return CommandResult.Null;

            return commandProcessor.ProcessCommand(command);
        }
    }
}
