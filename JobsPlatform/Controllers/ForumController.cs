using Microsoft.AspNetCore.Mvc;

namespace JobsPlatform.Controllers
{
    public class ForumController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
