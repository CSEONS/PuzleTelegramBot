using Bot.Data.Handler;
using Bot.Data.Models;
using Bot.Models.Data;
using System.Numerics;
using Telegram.Bot.Types;

namespace Bot.CommandsHandler.Commands
{
    public class StartCommand : ICommandProcessor
    {
        public bool CanProcess(ICommand command)
        {
            return command.CommandName.ToLower() == @"/start".ToLower();
        }

        public CommandResult ProcessCommand(Command command)
        {
            if (!CanProcess(command)) throw new ArgumentException(nameof(command));

            User user = command.User;
            Player? player;

            using (PlayerDBContext context = new PlayerDBContext())
            {
                player = context.Players.FirstOrDefault(x => x.TelegramIdentifier == user.Id);

                if (player is null)
                    player = Player.CreateInDB(user, context);

                if (player.CurrentPuzzle is null)
                {
                    player.CurrentPuzzle ??= Puzzle.For(player);
                    context.Update(player);
                    context.SaveChanges();
                }
            }

            string commandResultText = string.Format(PuzzleInformationMessage.PuzzleInformation[PuzzleInformationMessage.InformationType.Start], player.Name);

            return new CommandResult(commandResultText);
        }
    }
}