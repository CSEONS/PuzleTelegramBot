using Bot.Data;
using Bot.Domain;
using Bot.Domain.Entities;
using System.Text;

namespace Bot.CommandsHandler.Commands
{
    public class HelpCommand : ICommandProcessor
    {
        public static string CommandName => _commandName;
        public Player.Permissions Permission => Player.Permissions.User;


        public static string _commandName = @"/help";

        public bool CanProcess(ICommand command)
        {
            return command.CommandName.ToLower() == _commandName.ToLower();
        }

        public CommandResult ProcessCommand(Command command)
        {
            if (!CanProcess(command)) throw new ArgumentException(nameof(command));

            using (var context = new MuzzlePuzzleDBContext())
            {
                Player? player = context.Players.FirstOrDefault(x => x.TelegramIdentifier == command.User.Id);

                if (player is null)
                    throw new ArgumentException(nameof(player));

                StringBuilder stringBuilder = new StringBuilder();

                foreach (var executedCommand in CommandExecuter.Commands)
                {
                    if (CommandExecuter.HavePermission(command, executedCommand.Value))
                    {
                        stringBuilder = stringBuilder.AppendLine(executedCommand.Value.GetDescription());
                    }
                }

                return new CommandResult(stringBuilder.ToString());
            }
        }

        public string GetDescription()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine(_commandName);
            stringBuilder.AppendLine(MuzzlePuzzleMessage.GetDescriptionString(this));

            return stringBuilder.ToString();
        }
    }
}
