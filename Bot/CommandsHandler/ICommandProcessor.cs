using Telegram.Bot.Types;

public interface ICommandProcessor
{
    CommandResult ProcessCommand(ICommand command, Update update);
    bool CanProcess(ICommand command);
}