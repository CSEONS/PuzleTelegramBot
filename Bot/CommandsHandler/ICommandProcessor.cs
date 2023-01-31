using Telegram.Bot.Types;

public interface ICommandProcessor
{
    CommandResult ProcessCommand(Command command);
    bool CanProcess(ICommand command);
}