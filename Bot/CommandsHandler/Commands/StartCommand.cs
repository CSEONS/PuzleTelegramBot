using Bot.Data.Handler;
using Bot.Models.Data;
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

        using (PlayerDBContext context = new PlayerDBContext())
        {
            player = context.Players.FirstOrDefault(x => x.TelegramIdentifier == user.Id);

            if (player is null)
            {
                player = new Player(user.Id, user.Username ?? Player.defaultName);

                player.CurrentPuzzle ??= Puzzle.For(player);
                context.Players.Add(player);
                context.SaveChanges();
            }

            if (player.CurrentPuzzle is null)
                player.CurrentPuzzle ??= Puzzle.For(player);
        }

        string commandResultText = $"{player.CurrentPuzzle?.Text ?? string.Empty}";

        return new CommandResult(commandResultText);
    }
}