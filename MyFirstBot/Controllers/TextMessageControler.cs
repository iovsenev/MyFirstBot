using MyFirstBot.Services;
using MyFirstBot.Сonstants;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace MyFirstBot.Controllers
{
    internal class TextMessageControler
    {
        readonly ITelegramBotClient _telegramBotClient;
        readonly TextMessageHandler _textMessageHandler;

        public TextMessageControler(
            ITelegramBotClient telegramBotClient,
            TextMessageHandler textMessageHandler)
        {
            _telegramBotClient = telegramBotClient;
            _textMessageHandler = textMessageHandler;
        }

        public async Task GetLenghtText(Message message, CancellationToken token)
        {
            switch (message.Text)
            {
                case "/start":
                    var bottons = new List<InlineKeyboardButton[]>();
                    bottons.Add(new[]
                    {
                        InlineKeyboardButton.WithCallbackData($"Длина текста", CallbackDatas.TextLenght),
                        InlineKeyboardButton.WithCallbackData($"Сложение чисел", CallbackDatas.Adder),
                        InlineKeyboardButton.WithCallbackData($"В разработке", CallbackDatas.InWork)
                    });
                    await _telegramBotClient.SendTextMessageAsync(message.Chat.Id,
                        $"<b>Бот обрабатывает текстовые сообщения.</b>{Environment.NewLine}" +
                        $"{Environment.NewLine}Можно выбрать посчитать количество символов " +
                        $"или сумму введенный чисел.{Environment.NewLine}", cancellationToken: token, parseMode: ParseMode.Html,
                        replyMarkup: new InlineKeyboardMarkup(bottons));
                    break;
                default:
                    await _textMessageHandler.Handle(message, token);
                    break;
            }
        }
    }
}
