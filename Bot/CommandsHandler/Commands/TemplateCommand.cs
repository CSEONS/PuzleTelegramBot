public class TemplateCommand : ICommandProcessor
{
    public bool CanProcess(ICommand command)
    {
        return command.CommandName == "/mycommand";
    }

    public CommandResult ProcessCommand(ICommand command)
    {
        if (!CanProcess(command)) throw new ArgumentException(nameof(command));

        string commandResultText = $"{command.CommandName} executed";

        return new CommandResult(commandResultText);
    }
}