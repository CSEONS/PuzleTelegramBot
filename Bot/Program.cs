using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Polling;
using Bot.Configuraion;

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