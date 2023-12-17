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
            if (session.Choise == CallbackDatas.TextLenght)
            {
                await botClient.SendTextMessageAsync(id,
                    "Введите текстовое сообщение длину которого необходимо посчитать.",
                    cancellationToken: token);
                return;
            }
            if (session.Choise == CallbackDatas.Adder)
            {
                await botClient.SendTextMessageAsync(id,
                    "Введите числа через пробел которые необходимо сложить.",
                    cancellationToken: token);
                return;
            }
        }
    }
}
