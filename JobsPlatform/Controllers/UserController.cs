using JobsPlatform.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace JobsPlatform.Controllers
{
    public class UserController : Controller
    {
        UltimateWrapperModel model= new UltimateWrapperModel();
        [Route("User/Home")]
        public IActionResult Users(UltimateWrapperModel model)
        {
            return View("UserView",model);
        }
        [Route("User/ShowUsers")]
        public IActionResult ShowUsers(UltimateWrapperModel model)
        {
            return View("Users",model);
        }
    }
}
