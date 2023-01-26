using Bot;
using Models;
using Telegram.Bot.Types;

public class StartCommand : ICommandProcessor
{
    public bool CanProcess(ICommand command)
    {
        return command.CommandName == @"/start";
    }

    public CommandResult ProcessCommand(ICommand command)
    {
        if (!CanProcess(command)) throw new ArgumentException(nameof(command));

        User user = command.User;
        Player? player;

        using (ApplicationDbContext context = new ApplicationDbContext())
        {
            player = context.Players.FirstOrDefault(x => x.Name == user.FirstName && x.Id == user.Id) ?? CreatePlayer();

            if (player is null)
            {
                player = new Player()
                {
                    Id = user.Id,
                    Name = user.FirstName,
                    Rank = context.Players.Max(x => x.Rank) + 1,
                    Rating = 0,
                    SolvedPuzzles = 0
                };
            }
        }

        string commandResultText = $"{command.CommandName} Executed.";

        return new CommandResult(commandResultText);
    }
    
    private Player CreatePlayer()
    {
        return new Player();
    }
}