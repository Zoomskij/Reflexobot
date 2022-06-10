using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace Reflexobot.API
{
    public class Common
    {
        private int _currentChoice = 1;
        
        public Common() { }

        public async Task ChooseTeacher(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
        {
            Dictionary<int, string> phrases = GetStartPhrases();
            if (phrases != null)
            {
                List<List<InlineKeyboardButton>> inLineList = new List<List<InlineKeyboardButton>>();
                List<InlineKeyboardButton> inLineRow = new List<InlineKeyboardButton>();

                InlineKeyboardButton inLineKeyboard = InlineKeyboardButton.WithCallbackData(text: "Выбрать", callbackData: _currentChoice.ToString());
                InlineKeyboardButton inLineKeyboardNext = InlineKeyboardButton.WithCallbackData(text: "Дальше", callbackData: "NextReason;1");
                inLineRow.Add(inLineKeyboard);
                inLineRow.Add(inLineKeyboardNext);

                InlineKeyboardMarkup inlineKeyboardMarkup = new InlineKeyboardMarkup(inLineRow);

                await botClient.SendTextMessageAsync(
                chatId: message.Chat.Id,
                text: $"{phrases[1]}",
                replyMarkup: inlineKeyboardMarkup,
                cancellationToken: cancellationToken);
            }
        }

        public async Task ChooseTeacher(ITelegramBotClient botClient, CallbackQuery callbackQuery, int currentTeacher, CancellationToken cancellationToken)
        {
            Dictionary<int, string> phrases = GetStartPhrases();
            if (phrases != null)
            {
                List<List<InlineKeyboardButton>> inLineList = new List<List<InlineKeyboardButton>>();
                List<InlineKeyboardButton> inLineRow = new List<InlineKeyboardButton>();
                if (currentTeacher > 1)
                {
                    InlineKeyboardButton inLineKeyboardPrev = InlineKeyboardButton.WithCallbackData(text: "Назад", callbackData: $"PrevReason;{currentTeacher-1}");
                    inLineRow.Add(inLineKeyboardPrev);
                }
                InlineKeyboardButton inLineKeyboard = InlineKeyboardButton.WithCallbackData(text: "Выбрать", callbackData: $"{currentTeacher}");
                inLineRow.Add(inLineKeyboard);
                if (currentTeacher < phrases.Count())
                {
                    InlineKeyboardButton inLineKeyboardNext = InlineKeyboardButton.WithCallbackData(text: "Дальше", callbackData: $"NextReason;{currentTeacher + 1}");
                    inLineRow.Add(inLineKeyboardNext);
                }
                
                InlineKeyboardMarkup inlineKeyboardMarkup = new InlineKeyboardMarkup(inLineRow);
                var chatId = callbackQuery.Message.Chat.Id;
                var messageId = callbackQuery.Message.MessageId;

                await botClient.EditMessageTextAsync(chatId, messageId, $"{phrases[currentTeacher]}", replyMarkup: inlineKeyboardMarkup);
            }
        }


        public Dictionary<int, string> GetStartPhrases()
        {
            Dictionary<int, string> phrases = new Dictionary<int, string>();
            phrases.Add(1, "Ответ 1 из 4\n\nЯ люблю осознанно подходить к любому процессу, размышляю и во всем ищу смысл. Медитация и философия помогают мне во всем");
            phrases.Add(2, "Ответ 2 из 4\n\nМне нравится держать ритм и соревноваться, поддерживаю спортивный интерес даже в обучении.Самодисциплина - моя основа");
            phrases.Add(3, "Ответ 3 из 4\n\nЯ всегда просчитываю варианты и выгоды, видение будущего успеха помогает мне. Меня вдохновляют истории успеха");
            phrases.Add(4, "Ответ 4 из 4\n\nГлавное для меня- это побороть свою лень и прокрастинацию. Моя основа и причина успехов- это внутренняя работа с собой");
            return phrases;
        }
    }
}
