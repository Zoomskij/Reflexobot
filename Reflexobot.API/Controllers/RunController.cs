using Microsoft.AspNetCore.Mvc;
using Reflexobot.Services.Inerfaces;

namespace Reflexobot.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RunController : Controller
    {
        private readonly IReceiverService _receiverService;
        public RunController(IReceiverService receiverService)
        {
            _receiverService = receiverService;
        }

        [HttpGet(Name = "Start")]
        public IActionResult Index()
        {
            
            return Ok();
        }
    }
}
