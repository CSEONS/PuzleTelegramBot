using Bot.CommandsHandler.Commands;
using Bot.Data;
using System.Text.RegularExpressions;

namespace Bot.CommandsHandler
{
    public class CommandExecuter
    {
        public static CommandResult NoCommand => new CommandResult(MuzzlePuzzleMessage.GetInformationString(MuzzlePuzzleMessage.InformationType.ItNoPuzzle));
        private static Regex _regex = new Regex("^[$\\/]");

        public static readonly Dictionary<string, ICommandProcessor> Commands = new()
        {
            {StartCommand.CommandName, new StartCommand()},
            {StatusCommand.CommandName, new StatusCommand()},
            {GetPuzzleCommand.CommandName, new GetPuzzleCommand()},
            {DisplaySolvedPuzzlesCommand.CommandName, new DisplaySolvedPuzzlesCommand()},
            {HelpCommand.CommandName, new HelpCommand()},
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

        public static bool IsCommandForm(Command command)
        {
            return _regex.IsMatch(command.FullCommand);
        }
    }
}
