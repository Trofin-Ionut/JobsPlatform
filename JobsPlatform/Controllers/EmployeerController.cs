using JobsPlatform.Models;
using Microsoft.AspNetCore.Mvc;
using System.Resources;

namespace JobsPlatform.Controllers
{
    public class EmployeerController : Controller
    {
        [Route("Employeer/Home")]
        public IActionResult Employeer(UltimateWrapperModel model)
        {
            return View("EmployeerView",model);
        }
        [Route("Employeer/ShowEmployeers")]
        public IActionResult ShowEmployeers(UltimateWrapperModel model) 
        {
            return View("Employeers",model);
        }
    }
}
