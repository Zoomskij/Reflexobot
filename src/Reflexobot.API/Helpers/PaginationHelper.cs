using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;

namespace Reflexobot.API.Helpers
{
    public static class PaginationHelper
    {
        public async static Task Pagination(ITelegramBotClient botClient, string[] items, long chatId, int messageId, string command, string selectCommand, string secondCommand, int current = 0, bool isNew = false)
        {
            if (items != null)
            {
                List<List<InlineKeyboardButton>> inLineList = new List<List<InlineKeyboardButton>>();
                List<InlineKeyboardButton> inLineRow = new List<InlineKeyboardButton>();
                if (current > 0)
                {
                    InlineKeyboardButton inLineKeyboardPrev = InlineKeyboardButton.WithCallbackData(text: "Назад", callbackData: $"{command};{current - 1}");
                    inLineRow.Add(inLineKeyboardPrev);
                }

                if (!string.IsNullOrWhiteSpace(selectCommand))
                {
                    InlineKeyboardButton inLineKeyboard = InlineKeyboardButton.WithCallbackData(text: "Выбрать", callbackData: $"{selectCommand};{current}");
                    inLineRow.Add(inLineKeyboard);
                }

                if (current < items.Count()-1)
                {
                    InlineKeyboardButton inLineKeyboardNext = InlineKeyboardButton.WithCallbackData(text: "Дальше", callbackData: $"{command};{current + 1}");
                    inLineRow.Add(inLineKeyboardNext);
                }
                else 
                {
                    if (string.IsNullOrWhiteSpace(selectCommand))
                    {
                        InlineKeyboardButton inLineKeyboardNext = InlineKeyboardButton.WithCallbackData(text: "Дальше", callbackData: $"{secondCommand}");
                        inLineRow.Add(inLineKeyboardNext);
                    }

                }
                var text = items[current];
                InlineKeyboardMarkup inlineKeyboardMarkup = new InlineKeyboardMarkup(inLineRow);
                
                if (isNew)
                    await botClient.SendTextMessageAsync(chatId, $"{text}", replyMarkup: inlineKeyboardMarkup, parseMode: Telegram.Bot.Types.Enums.ParseMode.Html);
                else
                    await botClient.EditMessageTextAsync(chatId, messageId, $"{text}", replyMarkup: inlineKeyboardMarkup, parseMode: Telegram.Bot.Types.Enums.ParseMode.Html);
            }
        }
    }
}
