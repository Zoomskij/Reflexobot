namespace Reflexobot.API
{
    public class TelegramBackgroundService : BackgroundService
    {
        public TelegramBackgroundService()
        {

        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return Task.Factory.StartNew(() => DoWork(stoppingToken));
        }

        private async Task DoWork(CancellationToken ct)
        {
          
        }
    }
}
