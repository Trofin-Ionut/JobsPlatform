using Microsoft.AspNetCore.Mvc;
using JobsPlatform.BusinessLogic.DataLayer;
using System.Text.RegularExpressions;
using JobsPlatform.Models;
using JobsPlatform.Enums;
using JobsPlatform.BusinessLogic.CRUD;
using System.Linq;
using JobsPlatform.Interfaces;

namespace JobsPlatform.Controllers
{
    public class HomeController : Controller
    {
        UltimateWrapperModel model;
        [Route("/")]
        [Route("Home")]
        public async Task<IActionResult> Home()
        {
            await Database.CreateDatabase();
            model = new UltimateWrapperModel();
            ViewData["model"] = "Welcome to JobsPlatform";
            return View(model);
        }
        [HttpPost]
        [Route("Home/LogInModal")]
        public async Task<IActionResult> LogIn(UltimateWrapperModel model)
        {
            string pattern = "^\\[A-Za-z]*$";
            Regex regex = new Regex(pattern);
            if (regex.IsMatch(model.Name))
            {
                return BadRequest($"Invalid name, letters only.This: {model.Name} is rejected");
            }
            int role = (int)Role.USER;
           foreach(IStuff stuff in Database.everything)
            {
                User user = (User)stuff;
                if(user.Name.Equals(model.Name))
                {
                    return View("Forum", model);
                }
            }

            return BadRequest("Account doesn't exist");
        }
        [HttpPost]
        [Route("Home/CreateAccountModal")]
        public async Task<IActionResult> CreateAccount(UltimateWrapperModel model)
        {
            string pattern = "^\\[A-Za-z]*$";
            Regex regex = new Regex(pattern);
            if (regex.IsMatch(model.Name))
            {
                return BadRequest($"Invalid name, letters only.This: {model.Name} ia rejected");
            }
            model.Password = Request.Form["password"];
            switch (model.Option)
            {
                case 5:
                    var userID = Database.everything.Where(user => user.Identifier == (int?)Role.USER);
                    var result = userID.Count(folks => folks.Name.Equals(model.Name));
                    if (result > 0)
                    {
                        return BadRequest("User already exists");
                    }
                    Database.everything.Add(new User(model.Name, (int)Role.EMPLOYEER, userID.Count() + 1, model.Password));
                    await UserCrud.InsertUser(model.Name, (int)Role.EMPLOYEER, model.Password);
                    return View("Forum", model);
                case 6:
                    var userID1 = Database.everything.Where(user => user.Identifier == (int?)Role.USER);
                    var result1 = userID1.Count(folks => folks.Name.Equals(model.Name));
                    if (result1 > 0)
                    {
                        return BadRequest("Employeer already exists");
                    }
                    var count1 = Database.everything.Count(user => user.Identifier == (int?)Role.USER);
                    Database.everything.Add(new Employeer(model.Name, 0, (int)Role.EMPLOYEER, userID1.Count() + 1, model.Password));
                    await EmployeerCrud.InsertEmployeer(model.Name, (int)Role.EMPLOYEER, userID1.Count() + 1, model.Password);
                    return View("Forum", model);
                case 7:
                    var userID2 = Database.everything.Where(user => user.Identifier == (int?)Role.USER);
                    var result2 = userID2.Count(folks => folks.Name.Equals(model.Name));
                    if (result2 > 0)
                    {
                        return BadRequest("Admin already exists");
                    }
                    Database.everything.Add(new Admin(model.Name, userID2.Count() + 1, (int)Role.EMPLOYEER, userID2.Count() + 1, model.Password));
                    await AdminCrud.InsertAdmin(model.Name, (int)Role.ADMIN, userID2.Count() + 1, model.Password);
                    return View("Forum", model);
                default:
                    return BadRequest("No option selected");

            }
        }

    }
}
