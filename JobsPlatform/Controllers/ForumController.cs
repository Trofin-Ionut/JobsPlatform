using JobsPlatform.BusinessLogic.CRUD;
using JobsPlatform.BusinessLogic.DataLayer;
using JobsPlatform.Enums;
using JobsPlatform.Models;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using System.Text.Json;

namespace JobsPlatform.Controllers
{
    public class ForumController : Controller
    {

        [HttpGet]
        [Route("Forum/Home")]
        public IActionResult HomeForum(UltimateWrapperModel _model)
        {
            if(_model == null) 
            {
                _model = HomeController._model;
            }
            return View("Forum", _model);
        }
        [Route("Forum/Json")]
        public IActionResult ForumJson()
        {;
            return View("Forum", HomeController._model);
        }
        [HttpPost]
        [Route("Forum/Apply")]
        public async Task<IActionResult> Apply(UltimateWrapperModel model)
        {
            try
            {
                int test=Database.userJobs.Last().Id.Value;
            }catch(Exception) 
            { goto skip; }
                List<UserJobs> userJobs = Database.userJobs.Where(user => user.Id == model.userID).ToList();
                UserJobs alreadyApplied = new UserJobs();
                alreadyApplied = userJobs.FirstOrDefault(hasApplied => hasApplied.JobID == model.jobID);
            
            
            if (alreadyApplied.Id.HasValue)
            {
                Database.userJobs.Add(alreadyApplied);
                await UserJobsCrud.InsertUserJobs(model.jobID, model.userID);
                goto skip1;
            }
        skip:
            Database.userJobs.Add(new UserJobs(1,model.jobID,HomeController._model.userID));
            await UserJobsCrud.InsertUserJobs(model.jobID, HomeController._model.userID);
            skip1:
            model = HomeController._model;
            return RedirectToAction("Users","User");
        }
        [HttpPost]
        [Route("Forum/Search")]
        public async Task<IActionResult> Search(UltimateWrapperModel model)
        {
            if(!model.userID.HasValue)
            {
                model=HomeController._model;
            }
            string jobName = Convert.ToString(Request.Form["jobName"]);
            string action = "";
            string controller = "";
            switch(model.Role)
            {
                case (int)Role.USER:
                    action = "Users";
                    controller = "User";
                    break;
                case (int)Role.EMPLOYEER:
                    action = "Employeer";
                    controller = "Employeer";
                    break;
                case (int)Role.ADMIN:
                    action = "Admin";
                    controller = "Admin";
                    break;
                default:
                    action = "Home";
                    controller = "Home";
                    break;
            }
            try
            {
                Job found = new Job();
                foreach(Job job in Database.jobs)
                {
                    string temp = job.Name.ToLower();
                    string lowercase = jobName.ToLower();
                    if (temp.Contains(lowercase)) 
                    {                      
                        found = job;
                        break;
                    }
                }
                
                model.jobName = found.Name;
                
            }
            catch (Exception) 
            { model = HomeController._model;
                return RedirectToAction(action, controller, new { ErrorMessage = "Job doesn't exist" });
            };
            
            return RedirectToAction(action,controller);
        }
    }
}
