﻿using Bot;
using Models;
using Newtonsoft.Json;
using Telegram.Bot.Types;

public class StartCommand : ICommandProcessor
{
    public bool CanProcess(ICommand command)
    {
        return command.CommandName == @"/start";
    }

    public CommandResult ProcessCommand(ICommand command)
    {
        if (!CanProcess(command)) throw new ArgumentException(nameof(command));

        User user = command.User;
        Player? player;

        using (ApplicationDbContext context = new ApplicationDbContext())
        {
            player = context.Players.FirstOrDefault(x => x.UserId == user.Id);

            if (player is null)
            {
                player = new Player()
                {
                    UserId = user.Id,
                    Name = user.Username
                };
                context.Players.Add(player);
                context.SaveChanges();
            }
        }

        string commandResultText = $"{command.CommandName} executed";

        return new CommandResult(commandResultText);
    }
}