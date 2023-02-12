using Bot.Data;
using Bot.Data.Models;
using Bot.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot.CommandsHandler.Commands
{
    public class DisplaySolvedPuzzlesCommand : ICommandProcessor
    {
        public static string CommandName => _commandName;
        public Player.Permissions Permission => Player.Permissions.User;

        private static string _commandName = @"/displaysolved";

        public bool CanProcess(ICommand command)
        {
            return command.CommandName.ToLower() == _commandName.ToLower();
        }

        public CommandResult ProcessCommand(Command command)
        {
            if (!CanProcess(command)) throw new ArgumentException(nameof(command));

            using (var context = new MuzzlePuzzleDBContext())
            {
                IQueryable<SolvedPuzzle> solvedPuzzles = context.SolvedPuzzles.Include(x => x.Puzzle)?.Where(x => x.Player.TelegramIdentifier == command.User.Id);

                StringBuilder stringBuilder = new StringBuilder();

                if (solvedPuzzles.Count() == 0)
                    return new CommandResult(MuzzlePuzzleMessage.GetInformationString(MuzzlePuzzleMessage.InformationType.NotSolvedPuzzles));


                foreach (var solvedPuzzle in solvedPuzzles)
                {
                    stringBuilder.Append($"{solvedPuzzle.Puzzle.Id}\n{solvedPuzzle.Puzzle.Text}\n{solvedPuzzle.Puzzle.Answers}\n{new string('#', 10)}\n");
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
