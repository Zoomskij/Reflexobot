﻿using Reflexobot.Common;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;

namespace Reflexobot.API.Helpers
{
    public static class NavigationHelper
    {
        public async static Task Navigation(ITelegramBotClient botClient, NavigationModel model)
        {
            if (model.Items != null)
            {
                List<List<InlineKeyboardButton>> inLineList = new List<List<InlineKeyboardButton>>();
                List<InlineKeyboardButton> inLineRow = new List<InlineKeyboardButton>();
                if (model.CurrentPosition > 0)
                {
                    InlineKeyboardButton inLineKeyboardPrev = InlineKeyboardButton.WithCallbackData(text: "Назад", callbackData: $"{model.NavigationCommand};{model.CurrentPosition - 1}");
                    inLineRow.Add(inLineKeyboardPrev);
                }

                if (!string.IsNullOrWhiteSpace(model.SelectCommand))
                {
                    InlineKeyboardButton inLineKeyboard = InlineKeyboardButton.WithCallbackData(text: "Выбрать", callbackData: $"{model.SelectCommand};{model.CurrentPosition}");
                    inLineRow.Add(inLineKeyboard);
                }

                if (model.CurrentPosition < model.Items.Count()-1)
                {
                    InlineKeyboardButton inLineKeyboardNext = InlineKeyboardButton.WithCallbackData(text: "Дальше", callbackData: $"{model.NavigationCommand};{model.CurrentPosition + 1}");
                    inLineRow.Add(inLineKeyboardNext);
                }
                else 
                {
                    if (!string.IsNullOrWhiteSpace(model.NextStepCommand))
                    {
                        InlineKeyboardButton inLineKeyboardNext = InlineKeyboardButton.WithCallbackData(text: "Дальше", callbackData: $"{model.NextStepCommand}");
                        inLineRow.Add(inLineKeyboardNext);
                    }

                }
                var text = model.Items[model.CurrentPosition];
                InlineKeyboardMarkup inlineKeyboardMarkup = new InlineKeyboardMarkup(inLineRow);
                
                if (model.IsNew)
                    await botClient.SendTextMessageAsync(model.ChatId, $"{text}", replyMarkup: inlineKeyboardMarkup, parseMode: Telegram.Bot.Types.Enums.ParseMode.Html);
                else
                    await botClient.EditMessageTextAsync(model.ChatId, model.MessageId, $"{text}", replyMarkup: inlineKeyboardMarkup, parseMode: Telegram.Bot.Types.Enums.ParseMode.Html);
            }
        }
    }
}