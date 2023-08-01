using Microsoft.AspNetCore.Mvc;
using JobsPlatform.BusinessLogic.DataLayer;
using System.Text.RegularExpressions;
using JobsPlatform.Models;
using JobsPlatform.Enums;
using JobsPlatform.BusinessLogic.CRUD;
using System.Text.Json;

namespace JobsPlatform.Controllers
{
    public class HomeController : Controller
    {
        public static UltimateWrapperModel _model;
        private static bool DatabaseCreated = false;

        [Route("/")]
        [Route("Home")]
        public async Task<IActionResult> Home()
        {
            if (!DatabaseCreated)
            {
                await Database.CreateDatabase();
                DatabaseCreated = true;
            }
            _model = new UltimateWrapperModel();
            return View(_model);
        }
        [HttpPost]
        [Route("~/Home/LogInModal")]
        public async Task<IActionResult> LogIn(UltimateWrapperModel model)
        {
            string pattern = "^\\[A-Za-z]*$";
            Regex regex = new Regex(pattern);
            if (regex.IsMatch(model.Name))
            {;
                return RedirectToAction("Home", new { ErrorMessage = $"Invalid name, letters only.This: {model.Name} is rejected" });
            }
            User? user1 = new User();
            foreach (User user in Database.users)
            {
                if (user.Name.Equals(model.Name))
                { user1 = user;
                    break;
                }

            };
            model.userID = user1.Id;
            bool? isAdmin = Database.admins.Select(admin => admin.UserID == user1.Id).FirstOrDefault();
            bool? isEmployeer = Database.employeers.Select(Employeer => Employeer.UserID == user1.Id).FirstOrDefault();
            if (isAdmin.HasValue && isAdmin.Value)
            {
                model.Role = (int)Role.ADMIN;
                _model = model;
                return RedirectToAction("Admin", "Admin");
            }
            if (isEmployeer.HasValue &&isEmployeer.Value)
            {
                model.Role = (int)Role.EMPLOYEER;
                _model = model;
                return RedirectToAction("Employeer","Employeer");
            }
            if (user1.Id.HasValue && user1.Id.Value>0)
            { 
                model.Role = (int)Role.USER;
                _model = model;
                return RedirectToAction("Users", "User");
            }

            return RedirectToAction("Home", new { ErrorMessage = "Account doesn't exist" });
        }
        [HttpPost]
        [Route("~/Home/CreateAccountModal")]
        public async Task<IActionResult> CreateAccount(UltimateWrapperModel model)
        {
            string pattern = "^\\[A-Za-z]*$";
            Regex regex = new Regex(pattern);
            if (regex.IsMatch(model.Name))
            {
                return RedirectToAction("Home", new { ErrorMessage = $"Invalid name, letters only.This: {model.Name} ia rejected" });
            }
            model.Password = Request.Form["password"];
            switch (model.Option)
            {

                case 5:
                    var result = Database.users.Count(folk => folk.Name.Equals(model.Name));
                    if (result > 0)
                    {
                        return RedirectToAction("Home", new { ErrorMessage = "User already exists" });
                    }
                    if (Database.users.Count() < 1)
                    {
                        Database.users.Add(new User(model.Name, Database.users.Count() + 1, model.Password));
                    }
                    else { Database.users.Add(new User(model.Name, Database.users.Last().Id + 1, model.Password)); }
                    await UserCrud.InsertUser(model.Name, model.Password);
                    model.Role = (int)Role.USER;
                    _model = model;
                    return RedirectToAction("Users","User");
                case 6:
                    var result1 = Database.employeers.Count(folks => folks.Name.Equals(model.Name));
                    if (result1 > 0)
                    {
                        return RedirectToAction("Home", new { ErrorMessage = "Employeer already exists" });
                    }
                    await UserCrud.InsertUser(model.Name, model.Password);
                    Database.users.Add(new User(model.Name, Database.users.Last().Id + 1, model.Password));
                    if (Database.employeers.Count() < 1)
                    {
                        Database.employeers.Add(new Employeer(model.Name, Database.employeers.Count() + 1, Database.users.Last().Id, model.Password,model.Company));
                    }
                    else { Database.employeers.Add(new Employeer(model.Name, Database.employeers.Last().Id + 1, Database.users.Last().Id, model.Password,model.Company)); }
                    await EmployeerCrud.InsertEmployeer(model.Name, Database.users.Last().Id, model.Password,model.Company);
                    model.Role = (int)Role.EMPLOYEER;
                    _model = model;
                    return RedirectToAction("Employeer", "Employeer");
                case 7:
                    var result2 = Database.admins.Count(folks => folks.Name.Equals(model.Name));
                    if (result2 > 0)
                    {
                        return RedirectToAction("Home", new { ErrorMessage = "Admin already exists" });
                    }
                    await UserCrud.InsertUser(model.Name, model.Password);
                    Database.users.Add(new User(model.Name, Database.users.Last().Id + 1, model.Password));
                    if (Database.admins.Count() < 1)
                    {
                        Database.admins.Add(new Admin(model.Name, Database.admins.Count() + 1, Database.users.Last().Id, model.Password));
                    }
                    else { Database.admins.Add(new Admin(model.Name, Database.admins.Last().Id + 1, Database.users.Last().Id, model.Password)); }
                    await AdminCrud.InsertAdmin(model.Name, Database.users.Last().Id, model.Password);
                    model.Role = (int)Role.ADMIN;
                    _model = model;
                    return RedirectToAction("AdminJson", "Admin");
                default:
                    return RedirectToAction("Home", new { ErrorMessage = "No option selected" });

            }
        }
    }
}
