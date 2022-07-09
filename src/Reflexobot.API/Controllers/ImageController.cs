using Microsoft.AspNetCore.Mvc;
using Reflexobot.Services.Interfaces;

namespace Reflexobot.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ImageController : Controller
    {
        private readonly IImageService _imageService;
        private readonly IAchievmentService _achievementService;
        public ImageController(IImageService imageService, IAchievmentService achievmentService)
        {
            _imageService = imageService;
            _achievementService = achievmentService;
        }

        [HttpGet]
        [Route("generate")]
        public IActionResult Generate()
        {
            var achievments = _achievementService.GetAchievments();
            var image = _imageService.GenerateImage(200, 200, achievments);

            return base.File(image, "image/jpeg");
        }
    }
}
