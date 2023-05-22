using Bot.Data;
using Bot.Domain;
using Bot.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace Bot.CommandsHandler.Commands
{
    internal class GetPuzzleCommand : ICommandProcessor
    {
        public static string CommandName => _commandName;
        public Player.Permissions Permission => Player.Permissions.User;

        private static string _commandName = @"/get";

        public bool CanProcess(ICommand command)
        {
            return command.CommandName.ToLower() == _commandName.ToLower();
        }

        public CommandResult ProcessCommand(Command command)
        {
            if (!CanProcess(command)) throw new ArgumentException(nameof(command));

            using (var playerContext = new MuzzlePuzzleDBContext())
            {
                Player player = playerContext.Players.Include(x => x.CurrentPuzzle).FirstOrDefault(x => x.TelegramIdentifier == command.User.Id);

                IQueryable<SolvedPuzzle> playerSolvedPuzzles = playerContext.SolvedPuzzles.Where(x => x.Player.TelegramIdentifier == player.TelegramIdentifier);

                if (player is null)
                {
                    return CommandResult.Empty;
                }

                if (player.CurrentPuzzle != null)
                {
                    return new CommandResult(MuzzlePuzzleMessage.GetInformationString(MuzzlePuzzleMessage.InformationType.HasPuzzle, player.CurrentPuzzle.Text));
                }

                using (var puzzleContext = new MuzzlePuzzleDBContext())
                {
                    foreach (var puzzle in puzzleContext.Puzzles)
                    {
                        if (playerSolvedPuzzles.Any(x => x.Puzzle == puzzle))
                            continue;

                        player.CurrentPuzzle = puzzle;

                        playerContext.SaveChanges();
                    }
                }

                if (player.CurrentPuzzle is null)
                {
                    return new CommandResult(MuzzlePuzzleMessage.GetInformationString(MuzzlePuzzleMessage.InformationType.NoPuzzle));
                }

                return new CommandResult(MuzzlePuzzleMessage.GetInformationString(MuzzlePuzzleMessage.InformationType.GetPuzzle, player.CurrentPuzzle.Text));
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