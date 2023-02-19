using Bot.Data;
using Telegram.Bot;
using Telegram.Bot.Polling;

namespace TelegramBotExperiments
{

    class Program
    {
        public static ITelegramBotClient bot = new TelegramBotClient(BotConfiguration.TOKEN);

        static void Main(string[] args)
        {
            var cts = new CancellationTokenSource();
            var cancellationToken = cts.Token;
            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = { },
            };

            bot.StartReceiving(
                MessageHandler.HandleUpdateAsync,
                MessageHandler.HandleErrorAsync,
                receiverOptions,
                cancellationToken
            );

            Console.ReadLine();
        }
    }
}