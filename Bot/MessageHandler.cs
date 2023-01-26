﻿using PuzleBot;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TelegramBotExperiments;

public class MessageHandler
{
    public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        if (update?.Message?.Text == null)
            return;

        if (update?.Message?.From == null)
            return;

        string clientMessage = update.Message.Text;
        User user = update.Message.From;
        long chatId = update.Message.Chat.Id;

        Command command = new Command(clientMessage, user);
        CommandResult commandResult = CommandResult.Empty;

        if (CommandExecuter.TryParse(clientMessage, out ICommandProcessor commandProcessor))
        {
            commandResult = commandProcessor.ProcessCommand(command);
        };

        await botClient.SendTextMessageAsync(chatId, commandResult.Text);
    }

    public static Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(exception));
        return Task.CompletedTask;
    }
}
