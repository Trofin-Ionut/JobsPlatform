using Microsoft.AspNetCore.Mvc;
using JobsPlatform.BusinessLogic.DataLayer;
using System.Text.RegularExpressions;
using JobsPlatform.Models;
using JobsPlatform.Enums;
using JobsPlatform.BusinessLogic.CRUD;

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
        [HttpPost]
        [Route("LogInModal")]
        public async Task<IActionResult> LogIn(string?name,int? id, string?password,int? userID)
        {
            string pattern = "^\\[A-Za-z]*$";
            Regex regex = new Regex(pattern);
            if(!regex.IsMatch(name))
            {
                return BadRequest($"Name {name} is not accepted");
            }
            if(password.Length<1) 
            {
                return BadRequest("Empty password field");
            }
            Database.everything.Add(new Employeer(name,id, (int)Role.EMPLOYEER,userID, password));
            await EmployeerCrud.InsertEmployeer(name,(int)Role.EMPLOYEER,userID,password);
            return Ok($"Account created :\n Name:{name} \n Password:{password}");
        }
    }
}
