using MyFirstBot.models;
using MyFirstBot.Services;
using MyFirstBot.Сonstants;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace MyFirstBot.Controllers
{
    internal class InlineKeyboardControler
    {
        readonly ITelegramBotClient _botClient;
        readonly IStorage _memory;

        public InlineKeyboardControler(ITelegramBotClient botClient,
            IStorage memory)
        {
            _botClient = botClient;
            _memory = memory;
        }

        public async Task Handle(CallbackQuery? callbackQuery,
            CancellationToken token)
        {
            if (callbackQuery.Data == null)
                return;
            _memory.GetSession(callbackQuery.From.Id).Choise =
                callbackQuery.Data;

            string choise = callbackQuery.Data switch
            {
                CallbackDatas.TextLenght => "Посчитать длину текста",
                CallbackDatas.Adder => "Сложить числа",
                _ => string.Empty
            };

            await _botClient.SendTextMessageAsync(callbackQuery.From.Id,
                $"<b>Вы выбрали \"{choise}\"</b>.{Environment.NewLine}" +
                $"{Environment.NewLine}Выбор можно поменять в главном меню.",
                cancellationToken: token, parseMode: ParseMode.Html);
            await InstructionEnter.SendInstruction(
                _botClient,
                callbackQuery.From.Id,
                _memory.GetSession(callbackQuery.From.Id),
                token);
        }
    }
}
