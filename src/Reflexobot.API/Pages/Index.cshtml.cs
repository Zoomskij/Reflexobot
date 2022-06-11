using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using Reflexobot.Services.Interfaces;

namespace Reflexobot.API.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ICourseService _courseService;
        public IndexModel(ILogger<IndexModel> logger, ICourseService courseService)
        {
            _logger = logger;
            _courseService = courseService;
        }

        public void OnGet()
        {
            var courses = _courseService.GetCourses();
        }
    }
}
