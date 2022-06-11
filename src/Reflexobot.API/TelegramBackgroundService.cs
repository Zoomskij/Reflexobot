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
        private readonly IConfiguration _configuration;
        private readonly IServiceScopeFactory _scopeFactory;

        public TelegramBackgroundService(IServiceScopeFactory scopeFactory, IConfiguration configuration)
        {
            _scopeFactory = scopeFactory;
            _configuration = configuration;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return Task.Factory.StartNew(() => DoWork(stoppingToken));
        }

        private async Task DoWork(CancellationToken ct)
        {
            var token = _configuration.GetSection("Token");

            using (var scope = _scopeFactory.CreateScope())
            {
                var receiverService = scope.ServiceProvider.GetRequiredService<IReceiverService>();
                var courseService = scope.ServiceProvider.GetRequiredService<ICourseService>();
                var userService = scope.ServiceProvider.GetRequiredService<IStudentService>();

                var botClient = new TelegramBotClient(token.Value);
                using var cts = new CancellationTokenSource();

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
                                {
                                    await new HandeUpdateMessage().HandeUpdateMessageAsync(botClient, message, cancellationToken);

                                    UpdateEntity updateEntity = new UpdateEntity
                                    {
                                        Id = update.Id,
                                        Type = update.Type.ToString(),
                                        MessageId = message.MessageId,
                                        Message = new Entities.Telegram.MessageEntity()
                                        {
                                            ChatId = message.From.Id,
                                            MessageId = message.MessageId,
                                            Text = message.Text,
                                            Date = message.Date,
                                            From = String.Empty,
                                            Chat = new ChatEntity()
                                            {
                                                Id = message.Chat.Id,
                                                FirstName = message.Chat.FirstName,
                                                LastName = message.Chat.LastName,
                                                Username = message.Chat.Username,
                                            }
                                        }
                                    };

                                    await receiverService.AddUpdate(updateEntity);
                                }
                                return;

                            case UpdateType.CallbackQuery:
                                CallbackQuery? callbackQuery = update?.CallbackQuery;
                                if (callbackQuery != null)
                                    await new HandleUpdateCallBack(courseService, receiverService, userService).HandleUpdateCallBackAsync(botClient, callbackQuery, cancellationToken);
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
                        //////



                    }
                    catch (Exception ex)
                    {
                        return;
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
