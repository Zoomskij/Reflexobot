using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace Reflexobot.API
{
    public class HandeUpdateMessage
    {

        public async Task HandeUpdateMessageAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
        {
            if (message == null)
                return;

            switch (message.Type)
            {
                case MessageType.Text:
                    if (string.IsNullOrWhiteSpace(message.Text))
                        return;

                    if (message.Text.Equals("/start") || message.Text.Equals("Старт 🏁"))
                    {
                        await botClient.SendTextMessageAsync(
                            chatId: message.Chat.Id,
                            text: $@"Привет, {message.Chat.FirstName}! Я твой личный помощник и ментор в процессе обучения в Нетологии. Я помогу тебе фокусироваться на цели твоего обучения, буду поддерживать тебя во всем, помогать заглянуть внутрь себя и увидеть твои достижения, для того, чтобы обучение на курсе прошло для тебя максимально эффективно!",
                            cancellationToken: cancellationToken);

                        await Task.Delay(800);

                        await botClient.SendTextMessageAsync(
                            chatId: message.Chat.Id,
                            text: $@"Чтобы предложить тебе максимально комфортную для тебя поддержку, давай  познакомимся поближе?",
                            cancellationToken: cancellationToken);

                        await Task.Delay(800);

                        await botClient.SendTextMessageAsync(
                            chatId: message.Chat.Id,
                            text: $"Вопрос 1 из 3\n\nРасскажи, что мотивирует конкретно тебя не терять интерес и фокусироваться на цели?",
                            cancellationToken: cancellationToken);
                       
                        await Task.Delay(800);

                        await new Common().ChooseTeacher(botClient, message, cancellationToken);
                        break;

                    }

                    // Создание нижней клавиатуры
                    ReplyKeyboardMarkup replyKeyboardMarkup = new(new[]
                    {
                            new KeyboardButton[] { "Старт 🏁" },
                    })
                    {
                        //ResizeKeyboard = true
                    };
                    await botClient.SendTextMessageAsync(
                        chatId: message.Chat.Id,
                        text: "Выберите действие",
                        replyMarkup: replyKeyboardMarkup,
                        cancellationToken: cancellationToken);

                    return;
            }

            return;
        }


    }
}
