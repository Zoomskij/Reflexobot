﻿using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Reflexobot.Entities;
using Reflexobot.Entities.Telegram;
using Reflexobot.Services.Inerfaces;
using Reflexobot.Services.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace Reflexobot.API
{
    public class EventHandlerCallBack
    {
        public string Event { get; set; }
        public Guid Guid { get; set; }
    }
    public class TelegramBackgroundService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
       // private readonly ICourseService _courseService;

        public TelegramBackgroundService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;

            //using (var scope = _scopeFactory.CreateScope())
            //{
            //    _courseService = scope.ServiceProvider.GetRequiredService<ICourseService>();
            //}
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return Task.Factory.StartNew(() => DoWork(stoppingToken));
        }

        private async Task DoWork(CancellationToken ct)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var receiverService = scope.ServiceProvider.GetRequiredService<IReceiverService>();
                var courseService = scope.ServiceProvider.GetRequiredService<ICourseService>();

                //////////////////////
                const string Token = "5575017651:AAHbegf79LC3sg1Gqy9vG0C-NmzbNWM65T8";    //DEV
                //const string Token = "5593861941:AAEzphLQ8HTyJtQlRbASDXMNUNWp7si9Y44";  //PROD

                var botClient = new TelegramBotClient(Token);
                using var cts = new CancellationTokenSource();

                var phrases = await GetPhrases();

                var receiverOptions = new ReceiverOptions
                {
                    AllowedUpdates = Array.Empty<UpdateType>()
                };
                botClient.StartReceiving(
                    updateHandler: HandleUpdateAsync,
                    errorHandler: HandlePollingErrorAsync,
                    receiverOptions: receiverOptions,
                    cancellationToken: cts.Token
                );

                var me = await botClient.GetMeAsync();

                Console.WriteLine($"Start listening for @{me.Username}");
                Console.ReadLine();

                // Send cancellation request to stop bot
                cts.Cancel();

                async Task HandleCallBackAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
                {
                    var chatId = update.CallbackQuery.Message.Chat.Id;
                    if (update.CallbackQuery.Message.Text.Equals("Выберите курс:"))
                    {

                        var lessons = courseService.GetLessonEntitiesByCourseGuid(Guid.Parse(update.CallbackQuery.Data));

                        List<List<InlineKeyboardButton>> inLineLessonsList = new List<List<InlineKeyboardButton>>();
                        foreach (var lesson in lessons)
                        {
                            inLineLessonsList.Add(new List<InlineKeyboardButton>() { InlineKeyboardButton.WithCallbackData(text: lesson.Name, callbackData: lesson.Guid.ToString()) });
                        }
  
                        InlineKeyboardMarkup inlineLessonKeyboard = new InlineKeyboardMarkup(inLineLessonsList);

                        await botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: "Выберите урок:", 
                        replyMarkup: inlineLessonKeyboard,
                        cancellationToken: cancellationToken);
                        return;
                    }

                    if (update.CallbackQuery.Message.Text.Equals("Выберите урок:"))
                    {

                        var tasks = courseService.GetTasksByLessonGuid(Guid.Parse(update.CallbackQuery.Data));

                        List<List<InlineKeyboardButton>> inLineTasksList = new List<List<InlineKeyboardButton>>();
                        foreach (var task in tasks)
                        {
                            inLineTasksList.Add(new List<InlineKeyboardButton>() { InlineKeyboardButton.WithCallbackData(text: task.Name, callbackData: task.Guid.ToString()) });
                        }

                        InlineKeyboardMarkup inlineTaskKeyboard = new InlineKeyboardMarkup(inLineTasksList);

                        await botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: "Выберите задачу:",
                        replyMarkup: inlineTaskKeyboard,
                        cancellationToken: cancellationToken);
                        return;
                    }

                    EventHandlerCallBack data;

                   // Account account = JsonConvert.DeserializeObject<Account>(json); 
                    var teacherId = int.Parse(update.CallbackQuery.Data);
                    var teachers = receiverService.GetTeachers();

                    if (teacherId == 99)
                    {
                        var currentTeacher = await receiverService.GetPersonByUserId(update.CallbackQuery.From.Id);
                        if (currentTeacher != null)
                        {
                            await botClient.SendStickerAsync(
                                chatId: chatId,
                                sticker: currentTeacher.Img,
                                cancellationToken: cancellationToken);
                        }
                        return;
                    }

                    if (teachers != null)
                    {

                        var teacher = teachers.FirstOrDefault(x => x.Id == teacherId);
                        if (teacher != null)
                        {
                            UserPersonIds userPersonIds = new UserPersonIds
                            {
                                PersonId = teacherId,
                                UserId = update.CallbackQuery.From.Id
                            };
                            await receiverService.AddOrUpdateUserPersonId(userPersonIds);
                            await botClient.SendStickerAsync(
                                chatId: chatId,
                                sticker: teacher.Img,
                                cancellationToken: cancellationToken);
                        }
                    }
                }

                async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
                {
                    if (update.Type == UpdateType.CallbackQuery)
                    {
                        await HandleCallBackAsync(botClient, update, cancellationToken);
                        return;
                    }

                    try
                    {
                        long userId = 0;
                        var chatId = update.Message?.Chat.Id;
                        var messageText = update.Message?.Text;
                        var channelPost = update.ChannelPost;
                        if (update.Message != null && update.Message.From != null)
                        {
                            userId = update.Message.From.Id;
                        }

                        if (!string.IsNullOrWhiteSpace(channelPost?.Text) && channelPost.Text.Equals("/help"))
                        {
                            Message msg = await botClient.SendTextMessageAsync(
                            chatId: update.ChannelPost.Chat.Id,
                            text: @" ✓ ты чувствуешь, что теряешь мотивацию и тебе нужна поддержка - /guruhelp
                 ✓ ты хочешь услышать мой голос и помедитировать -/meditation
                 ✓ ты хочешь вспомнить как звучит твоя цель - /mygoal",
                            cancellationToken: cancellationToken);
                        }
                        // Only process Message updates: https://core.telegram.org/bots/api#message
                        if (update.Type != UpdateType.Message)
                            return;
                        // Only process text messages
                        if (update.Message!.Type != MessageType.Text)
                            return;

                        ////////////
                        ReplyKeyboardMarkup replyKeyboardMarkup = new(new[]
                        {
                            new KeyboardButton[] { "Получить информацию о курсе" },
                            new KeyboardButton[] { "Выбрать персонажа", "/guruhelp", "/meditation" },
                            new KeyboardButton[] { "/mygoal", "/rasp", "/achievments" },
                        })
                        {
                            ResizeKeyboard = true
                        };
                        Message markupMessage = await botClient.SendTextMessageAsync(
                            chatId: chatId,
                            text: "Выберите действие",
                            replyMarkup: replyKeyboardMarkup,
                            cancellationToken: cancellationToken);
                        ////////////////////


                        Console.WriteLine($"Received a '{messageText}' message in chat {chatId}.");

                        var teachers = receiverService.GetTeachers();
                        var teachersArray = teachers.Select(x => x.Name).ToArray();
                        var buttons = new List<KeyboardButton>();


                        List<InlineKeyboardButton> inlineKeyboardList = new List<InlineKeyboardButton>();
                        foreach (var teacher in teachers)
                        {
                            inlineKeyboardList.Add(InlineKeyboardButton.WithCallbackData(text: teacher.Name, callbackData: teacher.Id.ToString()));
                        }
                        inlineKeyboardList.Add(InlineKeyboardButton.WithCallbackData(text: "Узнать моего персонажа", callbackData: "99"));
                        InlineKeyboardMarkup inlineKeyboard = new InlineKeyboardMarkup(inlineKeyboardList);

                        if (!string.IsNullOrWhiteSpace(messageText))
                        {
                            switch (messageText)
                            {
                                case "Получить информацию о курсе":
                                    {
                                        var courses = courseService.GetCourses();
                                        List<InlineKeyboardButton> inLineCoursesList = new List<InlineKeyboardButton>();
                                        foreach (var course in courses)
                                        {
                                            EventHandlerCallBack eventHandler = new EventHandlerCallBack()
                                            {
                                                Event = "Courses",
                                                Guid = course.Guid
                                            };
                                            string callBackData = JsonConvert.SerializeObject(eventHandler);

                                            var serializeData = course.ToString();
                                            inLineCoursesList.Add(InlineKeyboardButton.WithCallbackData(text: course.Name, callbackData: course.Guid.ToString()));
                                        }
                                        InlineKeyboardMarkup inlineCoursesKeyboard = new InlineKeyboardMarkup(inLineCoursesList);

                                            await botClient.SendTextMessageAsync(
                                            chatId: chatId,
                                            text: "Выберите курс:",
                                            replyMarkup: inlineCoursesKeyboard,
                                            cancellationToken: cancellationToken);
                                        break;
                                    }
                                case "Выбрать персонажа":
                                    await botClient.SendTextMessageAsync(
                                        chatId: chatId,
                                        text: "Выберите своего персонажа:",
                                        replyMarkup: inlineKeyboard,
                                        cancellationToken: cancellationToken);
                                    break;
                                case "/guruhelp":
                                    await botClient.SendTextMessageAsync(
                                        chatId: chatId,
                                        text: @" ✓ ты чувствуешь, что теряешь мотивацию и тебе нужна поддержка - /guruhelp
                             ✓ ты хочешь услышать мой голос и помедитировать- /meditation
                             ✓ ты хочешь вспомнить как звучит твоя цель - /mygoal",
                                        cancellationToken: cancellationToken);
                                    break;
                                case "/rasp":
                                    await botClient.SendTextMessageAsync(
                                        chatId: chatId,
                                        text: @"Пн-пт: 19:45",
                                        cancellationToken: cancellationToken);
                                    break;
                                case "/meditation":
                                    var phrase = GetRandomPhrase(receiverService, userId);
                                    await botClient.SendTextMessageAsync(
                                        chatId: chatId,
                                        text: phrase,
                                        cancellationToken: cancellationToken);
                                    break;
                                case "/mygoal":
                                    await botClient.SendTextMessageAsync(
                                        chatId: chatId,
                                        text: @"Здесь будет твоя цель",
                                        cancellationToken: cancellationToken);
                                    break;
                                case "/achievments":
                                    Message message1 = await botClient.SendStickerAsync(
                                        chatId: chatId,
                                        sticker: "https://github.com/TelegramBots/book/raw/master/src/docs/sticker-fred.webp",
                                        cancellationToken: cancellationToken);
                                    break;
                            }

                        }

                        //////


                        UpdateEntity updateEntity = new UpdateEntity
                        {
                            Id = update.Id,
                            Type = update.Type.ToString(),
                            MessageId = (int)update.Message?.MessageId,
                            Message = new Entities.Telegram.MessageEntity()
                            {
                                ChatId = update.Message.From.Id,
                                MessageId = update.Message.MessageId,
                                Text = update.Message.Text,
                                Date = update.Message.Date,
                                From = String.Empty,
                                Chat = new ChatEntity()
                                {
                                    Id = update.Message.Chat.Id,
                                    FirstName = update.Message.Chat.FirstName,
                                    LastName = update.Message.Chat.LastName,
                                    Username = update.Message.Chat.Username,
                                }
                            }
                        };

                        await receiverService.AddUpdate(updateEntity);


                    }
                    catch (Exception ex)
                    {

                    }

                }

                Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
                {
                    var ErrorMessage = exception switch
                    {
                        ApiRequestException apiRequestException
                            => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                        _ => exception.ToString()
                    };

                    Console.WriteLine(ErrorMessage);
                    return Task.CompletedTask;
                }

            }
        }
        async Task<string[]> GetPhrases()
        {
            string[] phrases =
            {
                "Привет! Я - твой Гуру и буду помогать тебе постигать твой путь обучения и дойти до поставленной тобой цели!",
                "Спасибо, что выбрал меня",
                "Я знаю, что ты поставил себе большую и амбициозную цель- и я уверен, что у тебя все получится. Я буду рядом и не дам тебе остановиться на полпути!"
            };
            return phrases;
        }

        string GetRandomPhrase(IReceiverService receiverService, long userId)
        {

            var phrases = receiverService.GetPhrasesbyUserId(userId).ToArray();
            if (phrases == null)
                return "Некогда медетировать";

            Random rnd = new Random();
            int num = rnd.Next(0, phrases.Length);
            return phrases[num];
        }
    }
}