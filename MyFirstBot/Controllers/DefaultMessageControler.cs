using Telegram.Bot;
using Telegram.Bot.Types;

namespace MyFirstBot.Controllers
{
    internal class DefaultMessageControler
    {
        ITelegramBotClient _botClient;

        public DefaultMessageControler(ITelegramBotClient botClient)
        {
            _botClient = botClient;
        }

        public async Task Handle(Message message, CancellationToken token)
        {
            await _botClient.SendTextMessageAsync(message.Chat.Id,
                "Не поддерживаемый тип сообщения. Введите текст!",
                cancellationToken: token);
        }
    }
}
