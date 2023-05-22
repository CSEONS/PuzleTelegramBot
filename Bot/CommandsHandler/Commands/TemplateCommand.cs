using Bot.Data;
using Bot.Domain.Entities;
using System.Text;

namespace Bot.CommandsHandler.Commands
{
    public class TemplateCommand : ICommandProcessor
    {
        public static string _commandName => @"/mycommand";

        public Player.Permissions Permission => throw new NotImplementedException();

        public bool CanProcess(ICommand command)
        {
            return command.CommandName.ToLower() == _commandName.ToLower();
        }

        public string GetDescription()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine(_commandName);
            stringBuilder.AppendLine(MuzzlePuzzleMessage.GetDescriptionString(this));

            return stringBuilder.ToString();
        }

        public CommandResult ProcessCommand(Command command)
        {
            if (!CanProcess(command)) throw new ArgumentException(nameof(command));

            string commandResultText = $"{command.CommandName} executed";

            return new CommandResult(commandResultText);
        }
    }
}