using Microsoft.AspNetCore.Mvc;
using Telegram.Bot;

namespace Reflexobot.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TelegramController : Controller
    {
        private readonly IConfiguration _configuration;
        public TelegramController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        [Route("status")]
        public async Task<IActionResult> GetStatus()
        {
            var token = _configuration.GetSection("Token");
            var botClient = new TelegramBotClient(token.Value);
            using var cts = new CancellationTokenSource();
            var result = await botClient.GetMeAsync();

            return Ok(result);
        }
    }
}
