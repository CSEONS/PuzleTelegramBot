using Telegram.Bot.Types;

public interface ICommand
{
    string CommandName { get; }
    User User { get; }
}