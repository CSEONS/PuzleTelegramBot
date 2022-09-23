public class Command : ICommand
{
    public Command(string name)
    {
        CommandName = name;
    }

    public string CommandName { get; private set; }
}