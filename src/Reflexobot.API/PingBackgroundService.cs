namespace Reflexobot.API
{
    public class PingBackgroundService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        public PingBackgroundService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken token)
        {
#if !DEBUG
            await Task.Yield();

            while (token.IsCancellationRequested == false)
            {
                using var client = new HttpClient();
                var result = await client.GetAsync("http://zoomskij-001-site1.ctempurl.com/courses");
                Console.WriteLine(result.StatusCode);

                await Task.Delay(60000, token);

            }
#endif
        }
    }
}
