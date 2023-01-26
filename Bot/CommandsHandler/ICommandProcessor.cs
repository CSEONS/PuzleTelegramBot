using Telegram.Bot.Types;

public interface ICommandProcessor
{
    CommandResult ProcessCommand(ICommand command);
    bool CanProcess(ICommand command);
}