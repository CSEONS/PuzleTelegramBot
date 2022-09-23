using Bot.Configuraion;
using Models;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TelegramBotExperiments;

public class DistributorMessageOfType
{
    public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        string? messageText = update?.Message?.Text?.ToLower();

        if(update.Message is null)
            return;

        if (update.Message?.Entities?[0].Type == MessageEntityType.BotCommand)
        {
            CommandResult result = BotCommands.ExecuteCommend(new Command(update.Message.Text));
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(result));

            if (result.Text is null)
                return;
            Console.WriteLine("Message sended");
            await botClient.SendTextMessageAsync(update.Message.Chat.Id, result.Text);
        }
            
    }

    public static async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(exception));
    }
}
