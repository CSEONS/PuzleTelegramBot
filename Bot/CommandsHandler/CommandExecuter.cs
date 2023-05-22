using Bot.CommandsHandler.Commands;
using Bot.Data;
using Bot.Domain;
using Bot.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace Bot.CommandsHandler
{
    public class CommandExecuter
    {
        public static CommandResult NoCommand => new CommandResult(MuzzlePuzzleMessage.GetInformationString(MuzzlePuzzleMessage.InformationType.WrongCommand));
        private static Regex _commandMask = new Regex(@"^[$\/](.*)");

        public static readonly Dictionary<string, ICommandProcessor> Commands = new()
        {
            {StartCommand.CommandName, new StartCommand()},
            {StatusCommand.CommandName, new StatusCommand()},
            {GetPuzzleCommand.CommandName, new GetPuzzleCommand()},
            {DisplaySolvedPuzzlesCommand.CommandName, new DisplaySolvedPuzzlesCommand()},
            {HelpCommand.CommandName, new HelpCommand()},
            {AddPuzzleCommand.CommandName, new AddPuzzleCommand()},
        };

        public static CommandResult ExecuteCommand(Command command)
        {
            if (IsCommandForm(command) is false)
                return new CommandResult(MuzzlePuzzleMessage.GetInformationString(MuzzlePuzzleMessage.InformationType.WrongCommand));

            if (Commands.ContainsKey(command.CommandName) is false)
                return new CommandResult(MuzzlePuzzleMessage.GetInformationString(MuzzlePuzzleMessage.InformationType.WrongCommand));


            ICommandProcessor commandProcessor = Commands.FirstOrDefault(x => x.Key.ToLower() == _commandMask.Match(command.CommandName).ToString().ToLower()).Value;

            if (commandProcessor is null)
                return CommandResult.Empty;

            if (HavePermission(command, commandProcessor) is false)
                return new CommandResult(MuzzlePuzzleMessage.GetInformationString(MuzzlePuzzleMessage.InformationType.NotPermissions));

            return commandProcessor.ProcessCommand(command);
        }

        public static bool HavePermission(Command command, ICommandProcessor commandProcessor)
        {
            if (command.CommandName == StartCommand.CommandName)
                return true;

            using var context = new MuzzlePuzzleDBContext();

            Player? player = context.Players.FirstOrDefault(x => x.TelegramIdentifier == command.User.Id);

            if (player == null)
                throw new ArgumentNullException(nameof(player));

            return player.Permission <= commandProcessor.Permission;
        }

        public static bool IsCommandForm(Command command)
        {
            return _commandMask.IsMatch(command.FullCommand);
        }
    }
}
