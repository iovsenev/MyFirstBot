using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Requests;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Polling;
using Microsoft.Extensions.Hosting;
using MyFirstBot.Controllers;
using MyFirstBot.Services;

namespace MyFirstBot
{
    internal class Bot : BackgroundService
    {
        ITelegramBotClient _telegramBotClient;
        TextMessageControler _textMessageControler;
        DefaultMessageControler _defaultMessageControler;
        InlineKeyboardControler _inlineKeyboardControler;
        TextMessageHandler _textMessageHandler;

        public Bot(
            ITelegramBotClient telegramBotClient, 
            TextMessageControler textMessageControler,
            DefaultMessageControler defaultMessageControler,
            InlineKeyboardControler inlineKeyboardControler,
            TextMessageHandler textMessageHandler)
        {
            _telegramBotClient = telegramBotClient;
            _textMessageControler = textMessageControler;
            _defaultMessageControler = defaultMessageControler;
            _inlineKeyboardControler = inlineKeyboardControler;
            _textMessageHandler = textMessageHandler;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _telegramBotClient.StartReceiving(
                HandleUpdateAsync,
                HandleExceptionAsync,
                new ReceiverOptions() { AllowedUpdates = { } },
                cancellationToken: stoppingToken);
            Console.WriteLine("Bot is ready");
        }

        async Task HandleUpdateAsync(ITelegramBotClient botClient,
            Update update, CancellationToken cancellationToken)
        {
            if (update.Type == UpdateType.CallbackQuery)
            {
                await _inlineKeyboardControler.Handle(update.CallbackQuery,
                    cancellationToken);
                return;
            }
            if (update.Type == UpdateType.Message)
            {
                switch (update.Message!.Type)
                {
                    case MessageType.Text:
                        await _textMessageControler.GetLenghtText(
                            update.Message, cancellationToken);
                        return;
                    default:
                        await _defaultMessageControler.Handle(
                            update.Message, 
                            cancellationToken);
                        return;

                }
            }

        }
        Task HandleExceptionAsync(ITelegramBotClient botClient,
            Exception exception, CancellationToken cancellationToken)
        {
            var errorMessage = exception switch
            {
                ApiRequestException apiRequestException =>
                $"Telegram API Error:\n" +
                $"{apiRequestException.ErrorCode}\n" +
                $"{apiRequestException.Message}",
                _ => exception.ToString()
            };

            Console.WriteLine(errorMessage);

            Console.WriteLine("Ожидаем 10 секунд перед повторным подключением.");
            Thread.Sleep(10000);

            return Task.CompletedTask;
        }
    }
}
