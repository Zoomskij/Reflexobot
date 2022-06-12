using Reflexobot.Services.Inerfaces;
using Telegram.Bot;

namespace Reflexobot.API
{
    public class NotifyBackgroundService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IConfiguration _configuration;
        public NotifyBackgroundService(IServiceScopeFactory scopeFactory, IConfiguration configuration)
        {
            _scopeFactory = scopeFactory;
            _configuration = configuration;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            //#if !DEBUG
            //TODO: Закомментировал, чтобы не было рассылок от бота, севрис рабочий.

            //while (cancellationToken.IsCancellationRequested == false)
            //{
            //    var token = _configuration.GetSection("Token");
            //    using (var scope = _scopeFactory.CreateScope())
            //    {
            //        var receiverService = scope.ServiceProvider.GetRequiredService<IReceiverService>();
            //        var chats = receiverService.GetChats();
            //        var botClient = new TelegramBotClient(token.Value);
            //        using var cts = new CancellationTokenSource();
            //        List<ChatEntityDto> groupedChats = new List<ChatEntityDto>();
            //        foreach (var chat in chats.GroupBy(x => x.Id))
            //        {
            //            groupedChats.Add(new ChatEntityDto
            //            {
            //                Id = chat.Key
            //            });

            //            foreach (var groupedChat in groupedChats)
            //            {
            //                await botClient.SendTextMessageAsync(groupedChat.Id, "<b>Медитация</b> «Если вы думаете, что способны выполнить что-то, или думаете, что не способны на это, вы правы в обоих случаях», — Генри Форд.", Telegram.Bot.Types.Enums.ParseMode.Html);
            //            }
            //        }
            //        await Task.Delay(6000000, cancellationToken);
            //    }
            //}
            //#endif
        }
        public class ChatEntityDto
        {
            public long Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Username { get; set; }
        }
    }
}
