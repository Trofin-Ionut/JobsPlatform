using JobsPlatform.BusinessLogic.CRUD;
using JobsPlatform.BusinessLogic.DataLayer;
using JobsPlatform.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using System.Data.Common;
using System.Globalization;
using System.Text.Json;

namespace JobsPlatform.Controllers
{
    public class AdminController : Controller
    {
        [Route("Admin")]
        public async Task<IActionResult>Admin(UltimateWrapperModel model)
        {
            if (model.Role != HomeController._model.Role)
            {
                model = HomeController._model;
            }
            return View("AdminView",model);
        }
        [Route("Admin/json")]
        public async Task<IActionResult>AdminJson()
        {
            return View("AdminView",HomeController._model);
        }
        [HttpPost]
        [Route("Admin/AddJob")]
        public async Task<IActionResult> InsertJob(UltimateWrapperModel model)
        {
            if(model.Industry==null) 
            {
                model = HomeController._model;
            }
           
            DateTime? created = DateTime.ParseExact(model.Time, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None);
            Database.jobs.Add(new Job(model.Name,Database.jobs.Count()+1,model.Industry,model.MinSalary,model.MaxSalary,created));
            await JobCrud.InsertJob(model.Name, model.Industry, model.MinSalary, model.MaxSalary, created.Value.ToString("dd/MM/yyyy"));
            foreach(string benId in Request.Form["benefit"])
            {
                int?id= Database.jobBenefits.Count() + 1;
                int? benefitId = Convert.ToInt32(benId);
                int? jobId = Database.jobs.Last().Id;
                Database.jobBenefits.Add(new JobBenefits(id,benefitId ,jobId));
                await JobBenefitsCrud.InsertJobBenefit(benefitId, jobId);
            }
            model=HomeController._model;
            return View("AdminView",model);
        }
        [HttpPost]
        [Route("Admin/DeleteJob")]
        public async Task<IActionResult>DeleteJob(UltimateWrapperModel model)
        {
            if (model.Role!=HomeController._model.Role)
            {
                model = HomeController._model;
            }
            try
            {
                Job job = new();
                int jobID = Convert.ToInt32(Request.Form["jobID"]);
                foreach(Job job1 in Database.jobs) 
                {
                    if(job1.Id == jobID)
                    job = job1;
                }
                
                Database.jobs.Remove(job);
                await JobCrud.DeleteJobTable(job.Id.Value);
            }
            catch(Exception) 
            {
                return RedirectToAction("Admin", new {ErrorMessage="Job doesn't exist"}); };
            return View("AdminView",model);
        }
        [HttpPut] //good for update database methods
        [Route("Admin/UpdateJob")]
        public async Task<IActionResult>UpdateJob(UltimateWrapperModel model)
        {
            if (model.Role != HomeController._model.Role)
            {
                model = HomeController._model;
            }
            try
            {
                Job job = Database.jobs[model.jobID.Value - 1];
                await JobCrud.UpdateJobTable(job.Name, job.Industry, job.MinSalary, job.MaxSalary, job.Id, job.Created.Value.ToString("dd-MM-yyyy"));
                Database.jobs[job.Id.Value].Name = job.Name;
                Database.jobs[job.Id.Value].Industry = job.Industry;
                Database.jobs[job.Id.Value].MinSalary = job.MinSalary;
                Database.jobs[job.Id.Value].MaxSalary = job.MaxSalary;
                Database.jobs[job.Id.Value].Id = job.Id;
                Database.jobs[job.Id.Value].Created = job.Created;
                
            }
            catch(Exception) 
            {
                return RedirectToAction("Admin", new { ErrorMesage = "The job doesn't exist" }); };
            return View("AdminView",model);
        }
        [HttpPost]
        [Route("Admin/AddBenefits")]
        public async Task<IActionResult>AddBenefits(UltimateWrapperModel model)
        {
            if (model.Role != HomeController._model.Role)
            {
               model = HomeController._model;
            }
            foreach (Benefit ben in Database.benefits)
            {
                if(ben.Name.Equals(model.BenefitName))
                {
                    return RedirectToAction("Admin");
                }
            }
            Benefit benefit = new Benefit(Database.benefits.Count() + 1, Convert.ToString(Request.Form["BenefitName"]));
            Database.benefits.Add(benefit);
            await BenefitCrud.InsertBenefit(benefit.Name);
            return View("AdminView",model);
        }
        [Route("Admin/ShowAdmins")]
        public IActionResult ShowAdmins(UltimateWrapperModel model)
        {
            return View("Admins");
        }
    }
}
