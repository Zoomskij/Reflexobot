using Newtonsoft.Json;
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

        public TelegramBackgroundService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
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

                async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
                {
                    try
                    {
                        switch (update.Type)
                        {
                            case UpdateType.Message:
                                Message? message = update?.Message;
                                if (message != null)
                                    await new HandeUpdateMessage().HandeUpdateMessageAsync(botClient, message, cancellationToken);
                                return;

                            case UpdateType.CallbackQuery:
                                CallbackQuery? callbackQuery = update?.CallbackQuery;
                                if (callbackQuery != null)
                                    await new HandleUpdateCallBack(courseService, receiverService).HandleUpdateCallBackAsync(botClient, callbackQuery, cancellationToken);
                                return;


                        }
                    }
                    catch (Exception ex)
                    {
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
                        //Message markupMessage = await botClient.SendTextMessageAsync(
                        //    chatId: chatId,
                        //    text: "Выберите действие",
                        //    replyMarkup: replyKeyboardMarkup,
                        //    cancellationToken: cancellationToken);
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
