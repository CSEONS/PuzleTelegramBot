using Telegram.Bot.Types;

public class Command : ICommand
{
    public string FullCommand => _fullCommand;
    public string CommandName => _commandName;
    public User User => _user;


    private string _fullCommand;
    private string _commandName;
    private User _user;

    public Command(string fullCommand, User user)
    {
        _fullCommand = fullCommand;
        _commandName = _fullCommand.Split().First();
        _user = user;
    }
}