public class CommandResult
{
    public string Text => _text;
    public static CommandResult Empty => new CommandResult(string.Empty);

    private string _text;

    public CommandResult(string text)
    {
        _text = text;
    }
}