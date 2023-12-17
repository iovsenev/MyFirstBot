using MyFirstBot.Сonstants;
using Telegram.Bot;

namespace MyFirstBot.models
{
    internal static class InstructionEnter
    {
        public static async Task SendInstruction(
            ITelegramBotClient botClient,
            long id,
            Session session,
            CancellationToken token)
        {
            switch (session.Choise)
            {
                case CallbackDatas.TextLenght:
                await botClient.SendTextMessageAsync(id,
                    "Введите текстовое сообщение длину которого необходимо посчитать.",
                    cancellationToken: token);
                break;
                case CallbackDatas.Adder:
                await botClient.SendTextMessageAsync(id,
                    "Введите числа через пробел которые необходимо сложить.",
                    cancellationToken: token);
                break;
                default:
                    await botClient.SendTextMessageAsync(id, 
                        "В разработке. Пожалуйста выберите другой пункт меню",
                        cancellationToken: token);
                    break;
            }
        }
    }
}
