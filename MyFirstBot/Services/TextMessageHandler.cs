using MyFirstBot.Сonstants;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace MyFirstBot.Services
{
    internal class TextMessageHandler
    {
        readonly ITelegramBotClient _botClient;
        readonly IStorage _memory;

        public TextMessageHandler(ITelegramBotClient botClient, IStorage memory)
        {
            _botClient = botClient;
            _memory = memory;
        }

        public async Task Handle(Message message, CancellationToken token)
        {
            switch (_memory.GetSession(message.Chat.Id).Choise)
            {
                case CallbackDatas.TextLenght:
                    await _botClient.SendTextMessageAsync(message.Chat.Id,
                        $"Длина текста составляет {GetLenght(message.Text)} символов.",
                        cancellationToken: token);
                    break;
                case CallbackDatas.Adder:
                    await GetSumm(message, token);
                    break;
                default:
                    await _botClient.SendTextMessageAsync(message.Chat.Id,
                        "Еще в разработке. Пожалуйста выберите другой пункт.",
                        cancellationToken: token);
                    break;
            }
        }

        string GetLenght(string text)
        {
            return text.Length.ToString();
        }

        async Task GetSumm(Message message, CancellationToken token)
        {
            var nums = message.Text.Split(" ");
            var sum = 0;
            try
            {
                foreach (var num in nums)
                {
                    sum += Convert.ToInt32(num);
                }
                await _botClient.SendTextMessageAsync(message.Chat.Id,
                    $"Cумма чисел: {sum.ToString()}.",
                    cancellationToken:token);
            }
            catch (Exception ex)
            {
                await _botClient.SendTextMessageAsync(message.Chat.Id,
                    $"Не верный формат сообщения.",
                    cancellationToken: token);
            }
        }
    }
}
