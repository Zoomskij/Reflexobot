
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Microsoft.Extensions.DependencyInjection;
using Reflexobot.Services.Inerfaces;
using Reflexobot.Services;
using Reflexobot.Entities;
using Microsoft.Extensions.Hosting;
using Reflexobot.Repositories.Interfaces;
using Reflexobot.Repositories;
using Reflexobot.Data;
using Microsoft.Extensions.Configuration;
using Reflexobot;

static void Main(string[] args, IConfiguration configuration)
{
    //setup our DI
    var serviceProvider = new ServiceCollection()
         .AddLogging()
         .AddTransient<IReceiverService, ReceiverService>()
         .AddTransient<IUpdateRepository, UpdateRepository>()
         .Configure<Settings>(configuration.GetSection("Token"))
         .BuildServiceProvider();


    Settings settings = configuration.GetSection("Token").Get<Settings>();
    //do the actual work here
    var reseiverService = serviceProvider.GetService<IReceiverService>();
}


var serviceProvider = new ServiceCollection()
     .AddTransient<IReceiverService, ReceiverService>()
     .AddTransient<IUpdateRepository, UpdateRepository>()
     .AddDbContext<ReflexobotContext>()
     .BuildServiceProvider();

var config = new ConfigurationBuilder()
        .AddJsonFile("appsettings.local.json", optional: false)
        .Build();


// Add services to the container.
var reseiverService = serviceProvider.GetService<IReceiverService>();

const string Token = "";

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
    var chatId = update.Message?.Chat.Id;
    var messageText = update.Message?.Text;
    var channelPost = update.ChannelPost;
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

    Console.WriteLine($"Received a '{messageText}' message in chat {chatId}.");


    UpdateEntity updateEntity = new UpdateEntity
    {
        Id = update.Id,
        Type = update.Type.ToString(),
        MessageId = (int)update.Message?.MessageId,
        Message = new Reflexobot.Entities.MessageEntity()
        {
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
    await reseiverService.AddUpdate(updateEntity);

    Message sentMessage = await botClient.SendTextMessageAsync(
    chatId: chatId,
    text: phrases[0],
    cancellationToken: cancellationToken);
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
