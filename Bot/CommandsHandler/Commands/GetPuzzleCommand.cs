using Bot.Data;
using Bot.Data.Models;
using Bot.Models;
using Microsoft.EntityFrameworkCore;

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

            using (var context = new MuzzlePuzzleDBContext())
            {
                Player player = context.Players.Include(x => x.CurrentPuzzle).FirstOrDefault(x => x.TelegramIdentifier == command.User.Id);

                IQueryable<SolvedPuzzle> playerSolvedPuzzles = context.SolvedPuzzles.Where(x => x.Player.TelegramIdentifier == player.TelegramIdentifier);

                if (player is null)
                {
                    return CommandResult.Empty;
                }

                if (player.CurrentPuzzle != null)
                {
                    return new CommandResult(PuzzleMessage.GetInformationString(PuzzleMessage.InformationType.HasPuzzle, player.CurrentPuzzle.Text));
                }

                using (var puzzleContext = new MuzzlePuzzleDBContext())
                {
                    foreach (var puzzle in puzzleContext.Puzzles)
                    {
                        if (playerSolvedPuzzles.Any(x => x.Puzzle == puzzle))
                            continue;

                        player.CurrentPuzzle = puzzle;

                        context.SaveChanges();
                    }
                }

                if (player.CurrentPuzzle is null)
                {
                    return new CommandResult(PuzzleMessage.GetInformationString(PuzzleMessage.InformationType.NoPuzzle));
                }

                return new CommandResult(PuzzleMessage.GetInformationString(PuzzleMessage.InformationType.GetPuzzle, player.CurrentPuzzle.Text));
            }
        }
    }
}