using Bot.Data;
using Bot.Domain.Entities;
using System.Text;

public interface ICommandProcessor
{
    public static string? CommandName { get; }
    Player.Permissions Permission { get; }
    CommandResult ProcessCommand(Command command);
    bool CanProcess(ICommand command);
    string GetDescription();
}