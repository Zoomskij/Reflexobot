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
namespace Reflexobot.API
{
    public class TelegramBackgroundService : BackgroundService
    {
        private readonly IConfiguration _configuration;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly string token = string.Empty;

        public TelegramBackgroundService(IServiceScopeFactory scopeFactory, IConfiguration configuration)
        {
            _scopeFactory = scopeFactory;
            _configuration = configuration;
            token = configuration.GetSection("Token").Value;
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
                var studentService = scope.ServiceProvider.GetRequiredService<IStudentService>();
                var noteService = scope.ServiceProvider.GetRequiredService<INoteService>();
                var scenarioService = scope.ServiceProvider.GetRequiredService<IScenarioService>();

                var botClient = new TelegramBotClient(token);
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
                                    UpdateEntity updateEntity = new UpdateEntity
                                    {
                                        Id = update == null ? 0 : update.Id,
                                        Type = update == null ? string.Empty : update.Type.ToString(),
                                        MessageId = message.MessageId,
                                        Message = new Entities.Telegram.MessageEntity()
                                        {
                                            ChatId = message.From == null ? 0 : message.From.Id,
                                            MessageId = message.MessageId,
                                            Text = message.Text ?? string.Empty,
                                            Date = message.Date,
                                            From = string.Empty,
                                            Chat = new ChatEntity()
                                            {
                                                Id = message.Chat.Id,
                                                FirstName = message.Chat.FirstName ?? string.Empty,
                                                LastName = message.Chat.LastName ?? string.Empty,
                                                Username = message.Chat.Username ?? string.Empty,
                                            }
                                        }
                                    };
                                    await receiverService.AddUpdate(updateEntity);

                                    await new HandeUpdateMessage().HandeUpdateMessageAsync(botClient, message, receiverService, courseService, studentService, noteService, scenarioService, cancellationToken);
                                }
                                return;

                            case UpdateType.CallbackQuery:
                                CallbackQuery? callbackQuery = update?.CallbackQuery;
                                if (callbackQuery != null)
                                    await new HandleUpdateCallBack(courseService, receiverService, studentService, scenarioService).HandleUpdateCallBackAsync(botClient, callbackQuery, cancellationToken);
                                return;
                        }
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
    }
}
