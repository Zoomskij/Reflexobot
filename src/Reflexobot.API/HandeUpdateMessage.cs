using Reflexobot.Services.Inerfaces;
using Reflexobot.Services.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace Reflexobot.API
{
    public class HandeUpdateMessage
    {

        public async Task HandeUpdateMessageAsync(ITelegramBotClient botClient, Message message, IReceiverService receiverService, ICourseService courseService, CancellationToken cancellationToken)
        {
            if (message == null)
                return;

            switch (message.Type)
            {
                case MessageType.Text:
                    if (string.IsNullOrWhiteSpace(message.Text))
                        return;

                    if (message.Text.Equals("/start"))
                    {
                        await botClient.SendTextMessageAsync(
                            chatId: message.Chat.Id,
                            text: $@"Привет, {message.Chat.FirstName}! Я твой личный помощник и ментор на курсе в Нетологии 🙌",
                            cancellationToken: cancellationToken);

                        await Task.Delay(800);


                        var helloText = $"Я помогу тебе:" +
                                        "\n✅ осознавать получаемый опыт в процессе обучения" +
                                        "\n✅ регулярно рефлексировать" +
                                        "\n✅ фокусироваться на цели обучения" +
                                        "\n✅ возвращаться в ресурсное состояние в моменты спада" +
                                        "\n✅ и сокращать прокрастинацию!";
       
                        List<InlineKeyboardButton> inLineRow = new List<InlineKeyboardButton>();
                        InlineKeyboardButton inLineKeyboardNext = InlineKeyboardButton.WithCallbackData(text: "Дальше", callbackData: "Hello;1");
                        inLineRow.Add(inLineKeyboardNext);
                        InlineKeyboardMarkup inlineKeyboardMarkup = new InlineKeyboardMarkup(inLineRow);

                        await botClient.SendTextMessageAsync(
                        chatId: message.Chat.Id,
                        text: $"{helloText}",
                        replyMarkup: inlineKeyboardMarkup,
                        cancellationToken: cancellationToken);
                        break;

                    }

                    if (message.Text.Equals("/training"))
                    {
                        await new Common().Training(botClient, message, cancellationToken);
                    }

                    if (message.Text.Equals("/lessons"))
                    {
                        var courses = courseService.GetCourses();
                        if (courses != null)
                        {
                            var firstCourseId = courses.First().Guid;
                            var lessons = courseService.GetLessonEntitiesByCourseGuid(firstCourseId);
                            List<List<InlineKeyboardButton>> inLineLessonsList = new List<List<InlineKeyboardButton>>();
                            foreach (var lesson in lessons)
                                inLineLessonsList.Add(new List<InlineKeyboardButton>() { InlineKeyboardButton.WithCallbackData(text: lesson.Name, callbackData: lesson.Guid.ToString()) });
 

                            InlineKeyboardMarkup inlineLessonKeyboard = new InlineKeyboardMarkup(inLineLessonsList);

                            await botClient.SendTextMessageAsync(message.Chat.Id, "Выберите урок: ", replyMarkup: inlineLessonKeyboard);
                            return;
                        }

                        await botClient.SendTextMessageAsync(
                            chatId: message.Chat.Id,
                            text: "Надейся и жди",
                            cancellationToken: cancellationToken);
                    }

                    if (message.Text.Equals("/help"))
                    {
                        await botClient.SendTextMessageAsync(
                            chatId: message.Chat.Id,
                            text: "Надейся и жди",
                            cancellationToken: cancellationToken);
                    }

                    if (message.Text.Equals("/meditation"))
                    {
                        var phrases = receiverService.GetPhrases().ToArray();
                        if (phrases != null)
                        {
                            Random rnd = new Random();
                            int id = rnd.Next(1, phrases.Count());

                            await botClient.SendTextMessageAsync(
                                chatId: message.Chat.Id,
                                text: phrases[id],
                                cancellationToken: cancellationToken);
                        }
                    }

                    if (message.Text.Equals("/web"))
                    {

                        InlineKeyboardMarkup inlineKeyboard = new(new[]
                            {
                                InlineKeyboardButton.WithUrl(
                                    text: "Открыть на сайте 🌐",
                                    url: $"http://zoomskij-001-site1.ctempurl.com/?uid={message.Chat.Id}"
                                )
                            }
                        );

                        Message sentMessage = await botClient.SendTextMessageAsync(
                            chatId: message.Chat.Id,
                            text: "Перейдите на Web-сайт, чтобы увидеть больше информации",
                            replyMarkup: inlineKeyboard,
                            cancellationToken: cancellationToken);
                    }

                    return;
            }

            return;
        }


    }
}
