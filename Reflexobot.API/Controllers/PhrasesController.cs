using Microsoft.AspNetCore.Mvc;
using Reflexobot.Services.Inerfaces;

namespace Reflexobot.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PhrasesController : Controller
    {
        private readonly IReceiverService _receiverSerive;
        public PhrasesController(IReceiverService receiverSerive)
        {
            _receiverSerive = receiverSerive;
        }

        [HttpGet]
        [Route("")]
        public IActionResult GetPhrases()
        {
            var phrases = _receiverSerive.GetPhrases();
            return Ok(phrases);
        }

        [HttpPost]
        [Route("{teacherId}")]
        public async Task<IActionResult> AddPhrase(int teacherId, string phrase)
        {
            await _receiverSerive.AddPhrase(teacherId, phrase);
            return Ok();
        }
    }
}

