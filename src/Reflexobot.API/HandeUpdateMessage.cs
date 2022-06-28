using Reflexobot.Entities;
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

        public async Task HandeUpdateMessageAsync(ITelegramBotClient botClient, 
            Message message, 
            IReceiverService receiverService, 
            ICourseService courseService, 
            IStudentService studentService,
            INoteService noteService,
            IScenarioService scenarioService,
            CancellationToken cancellationToken)
        {
            if (message == null)
                return;
            var commands = scenarioService.Get();
            if (commands.Any(x => !string.IsNullOrWhiteSpace(x.Command) && x.Command.Equals(message.Text)))
            {
                var parrent = commands.First(x => !string.IsNullOrWhiteSpace(x.Command) && x.Command.Equals(message.Text));
                var scenarios = commands.Where(x => x.ParrentGuid == parrent.Guid).OrderBy(x => x.CreatedDate);

                foreach (var scenario in scenarios)
                {
                    // Send text with buttons
                    if (!string.IsNullOrWhiteSpace(scenario.Command))
                    {
                        switch (scenario.Command)
                        {
                            case "Next":
                                List<InlineKeyboardButton> inLineRow = new List<InlineKeyboardButton>();
                                InlineKeyboardButton inLineKeyboardNext = InlineKeyboardButton.WithCallbackData(text: "Дальше", callbackData: $"{scenario.Guid}");
                                inLineRow.Add(inLineKeyboardNext);
                                InlineKeyboardMarkup inlineKeyboardMarkup = new InlineKeyboardMarkup(inLineRow);

                                await botClient.SendTextMessageAsync(
                                    chatId: message.Chat.Id,
                                    text: scenario.Text ?? "empty message",
                                    replyMarkup: inlineKeyboardMarkup,
                                    parseMode: ParseMode.Html,
                                    cancellationToken: cancellationToken);
                                continue;
                            default:
                                continue;
                        }
                    }

                    // Send just text
                    await botClient.SendTextMessageAsync(
                        chatId: message.Chat.Id,
                        text: scenario.Text ?? "empty message",
                        parseMode: ParseMode.Html,
                        cancellationToken: cancellationToken);
                    await Task.Delay(800);
                }

                return;
            }
            switch (message.Type)
            {
                case MessageType.Text:
                    if (string.IsNullOrWhiteSpace(message.Text))
                        return;

                    if (message.Text.Equals("/start"))
                    {
                        //Если этой новый чат, созданим под него студента
                        var student = await studentService.GetStudentByChatIdAsync(message.From.Id);
                        if (student == null)
                        {
                            student = new StudentEntity
                            {
                                FirstName = message.From.FirstName,
                                LastName = message.From.LastName,
                                Username = message.From.Username,
                                ChatId = message.From.Id,
                            };
                            await studentService.AddStudentAsync(student);
                        }

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
                        parseMode: ParseMode.Html,
                        cancellationToken: cancellationToken);
                        return;

                    }

                    if (message.Text.Equals("/training"))
                    {
                        await new Common().Training(botClient, message, cancellationToken);
                    }

                    if (message.Text.Equals("/courses"))
                    {
                        List<InlineKeyboardButton> inLineRow = new List<InlineKeyboardButton>();
                        InlineKeyboardButton inLineKeyboardNext = InlineKeyboardButton.WithCallbackData(text: "Дальше ➡️", callbackData: "NavigationCourse;1");
                        inLineRow.Add(inLineKeyboardNext);
                        InlineKeyboardMarkup inlineKeyboardMarkup = new InlineKeyboardMarkup(inLineRow);
                        var courses = courseService.GetCourses();
                        var course = courses.First();

                        //Hack: Так как апишка не позвоялет нам послать одним сообщением картику и разметку к ней, 
                        //посылаем сообщение и сразу же делаем инкремент сообщения.
                        //Возможны коллизии при одновременной отправке сообщений
                        InputMediaPhoto photo = new InputMediaPhoto(course.Img);
                        var media = new IAlbumInputMedia[] { photo };
                        await botClient.SendMediaGroupAsync(message.Chat.Id, media);
                        await botClient.EditMessageMediaAsync(message.Chat.Id, message.MessageId+1, photo, replyMarkup: inlineKeyboardMarkup);
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

                    if (message.Text.Equals("/note"))
                    {
                        var notes = noteService.GetNotes();
                        if (notes != null)
                        {
                            foreach (var note in notes)
                            {
                                await botClient.SendTextMessageAsync(
                                chatId: message.Chat.Id,
                                text: note.Text,
                                cancellationToken: cancellationToken);
                            }
                        }
                        await botClient.SendTextMessageAsync(
                            chatId: message.Chat.Id,
                            text: "Добавьте замтеку: ",
                            cancellationToken: cancellationToken);
                        return;
                    }

                    //TODO: add command for reciever
                    //var currentStudent = await studentService.GetStudentByChatIdAsync(message.From.Id);
                    //await noteService.AddNoteAsync(message.Text, currentStudent.Guid);


                    return;
            }

            return;
        }


    }
}
