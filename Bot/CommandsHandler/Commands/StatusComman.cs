using Telegram.Bot.Types;

public class StatusCommand : ICommandProcessor
{
    public bool CanProcess(ICommand command)
    {
        return command.CommandName == "/status";
    }

    public CommandResult ProcessCommand(ICommand command, Update update)
    {
        if (!CanProcess(command)) throw new ArgumentException(nameof(command));
        Console.WriteLine($"{command.CommandName} command processed");
        return new CommandResult()
        {
            Text = "/status executed"
        };
    }
}