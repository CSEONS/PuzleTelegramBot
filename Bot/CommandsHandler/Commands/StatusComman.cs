using Telegram.Bot.Types;

public class StatusCommand : ICommandProcessor
{
    public bool CanProcess(ICommand command)
    {
        return command.CommandName == "/status";
    }

    public CommandResult ProcessCommand(ICommand command)
    {
        if (!CanProcess(command)) throw new ArgumentException(nameof(command));
        Console.WriteLine($"{command.CommandName} command processed");

        string commandResultText = $"{command.CommandName} executed";

        return new CommandResult(commandResultText);
    }
}