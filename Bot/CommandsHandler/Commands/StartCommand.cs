using Bot;
using Models;
using Telegram.Bot.Types;

public class StartCommand : ICommandProcessor
{
    public bool CanProcess(ICommand command)
    {
        return command.CommandName == @"/start";
    }

    public CommandResult ProcessCommand(ICommand command, Update update)
    {
        if (!CanProcess(command)) throw new ArgumentException(nameof(command));

        User user = update.Message.From;
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

        return new CommandResult()
        {
            Text = "/start executed"
        };
    }private Player CreatePlayer()
    {
        throw new NotImplementedException();
        return new Player();
    }
}