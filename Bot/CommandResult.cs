public class CommandResult
{
    public string? Text { get; set; }
    public static CommandResult Null => new CommandResult() { Text = null };
}