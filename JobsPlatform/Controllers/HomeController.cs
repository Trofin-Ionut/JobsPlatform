using Microsoft.AspNetCore.Mvc;
using JobsPlatform.BusinessLogic.DataLayer;
namespace JobsPlatform.Controllers
{
    public class HomeController : Controller
    {
        [Route("/")]
        [Route("Home")]
        public async Task<IActionResult> Home()
        {
            await Database.CreateDatabase();
            return View();
        }
    }
}
