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
    public class DisplaySolvedPuzzles : ICommandProcessor
    {
        public static string CommandName => @"/displaysolved";

        public bool CanProcess(ICommand command)
        {
            return command.CommandName.ToLower() == CommandName.ToLower();
        }

        public CommandResult ProcessCommand(Command command)
        {
            if (!CanProcess(command)) throw new ArgumentException(nameof(command));

            using (var context = new MuzzlePuzzleDBContext())
            {
                IQueryable<SolvedPuzzle> solvedPuzzles = context.SolvedPuzzles.Include(x => x.Puzzle).Where(x => x.Player.TelegramIdentifier == command.User.Id);

                StringBuilder outputMessage = new StringBuilder();

                if (solvedPuzzles.Count() == 0)
                    return new CommandResult(PuzzleMessage.GetInformationString(PuzzleMessage.InformationType.NotSolvedPuzzles));


                foreach (var solvedPuzzle in solvedPuzzles)
                {
                    outputMessage.Append($"{solvedPuzzle.Puzzle.Id}\n{solvedPuzzle.Puzzle.Text}\n{solvedPuzzle.Puzzle.Answers}\n{new string('#', 10)}\n");
                }
        
                return new CommandResult(outputMessage.ToString());
            }

        }
    }
}
