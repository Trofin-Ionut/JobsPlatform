using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLitePCL;
using System.IO;
using JobsPlatform.BusinessLogic.CRUD;
using JobsPlatform.Interfaces;
using JobsPlatform.Models;
using JobsPlatform.Enums;

namespace JobsPlatform.BusinessLogic.DataLayer
{

    public static class Database
    {
        public static SqliteConnection conn = new SqliteConnection("Data Source=database.db");
        public static List<IStuff> everything;

        static Database()
        {
            everything = new List<IStuff>();
        }
        public static async Task CreateDatabase()
        {
            //remember, sqlite database is created when you connect to it or when you create it's tables.
            await Task.WhenAll(new Task[]
                {
                CreateTableS(),
                ReadDatabase()
                });
        }
        public static async Task CreateTableS()
        {
            await Task.WhenAll(new Task[]
            {
                AdminCrud.CreateAdminTable(),
                EmployeerCrud.CreateEmployeerTable(),
                UserCrud.CreateUserTable(),
                JobCrud.CreateJobTable(),
                UserJobsCrud.CreateUserJobsTable(),
                JobBenefitsCrud.CreateJobBenefitTable(),
                BenefitCrud.CreateBenefitTable()
            });

        }
        public static async Task ReadDatabase()
        {
            await Task.WhenAll(new Task[]
            {
                AdminCrud.ReadAdminTable(),
                EmployeerCrud.ReadEmployeerTable(),
                UserCrud.ReadUserTable(),
                JobCrud.ReadJobTable(),
                UserJobsCrud.ReadUserJobsTable(),
                JobBenefitsCrud.ReadJobBenefitTable(),
                BenefitCrud.ReadBenefitTable()
            });
        }

        public static async Task UpdateTable(IStuff person)
        {
            switch (person.Identifier)
            {
                case (int)Role.ADMIN:
                    Admin admin = (Admin)person;
                    await AdminCrud.UpdateAdminTable(admin.Name, admin.UserID, admin.Id);
                    break;
                case (int)Role.EMPLOYEER:
                    Employeer employee = (Employeer)person;
                    await EmployeerCrud.UpdateEmployeerTable(employee.Name, employee.UserID, employee.Id);
                    break;
                case (int)Role.USER:
                    User user = (User)person;
                    await UserCrud.UpdateUserTable(user.Name, user.Id);
                    break;
                case (int)Role.JOB:
                    Job job = (Job)person;
                    await JobCrud.UpdateJobTable(job.Name, job.Industry, job.MinSalary, job.MaxSalary, job.Id);
                    break;
                case (int)Role.BENEFIT:
                    Benefit benefit = (Benefit)person;
                    await BenefitCrud.UpdateBenefitTable(benefit.Name, benefit.Id);
                    break;
                case (int)Role.JOB_BENEFIT:
                    JobBenefits jb = (JobBenefits)person;
                    await JobBenefitsCrud.UpdateJobBenefitTable(jb.JobID, jb.BenefitID, jb.Id);
                    break;
                case (int)Role.USER_JOBS:
                    UserJobs uj = (UserJobs)person;
                    await UserJobsCrud.UpdateUserJobsTable(uj.Id,uj.UserID,uj.JobID);
                    break;


            }
        }
        public static async Task DeleteTable(IStuff person)
        {
            switch (person.Identifier)
            {
                case (int?)Role.ADMIN:
                    Admin admin = (Admin)person;
                    await AdminCrud.DeleteAdminTable(admin.Id);
                    break;
                case (int?)Role.EMPLOYEER:
                    Employeer employee = (Employeer)person;
                    await EmployeerCrud.DeleteEmployeerTable(employee.Id);
                    break;
                case (int?)Role.USER:
                    User user = (User)person;
                    await UserCrud.DeleteUserTable(user.Id);
                    break;
                case (int?)Role.JOB:
                    Job job = (Job)person;
                    await JobCrud.DeleteJobTable(job.Id);
                    break;
                case (int)Role.BENEFIT:
                    Benefit benefit = (Benefit)person;
                    await BenefitCrud.DeleteBenefitTable(benefit.Id);
                    break;
                case (int)Role.JOB_BENEFIT:
                    JobBenefits jb = (JobBenefits)person;
                    await JobBenefitsCrud.DeleteJobBenefitTable(jb.Id);
                    break;
                case (int)Role.USER_JOBS:
                    UserJobs uj = (UserJobs)person;
                    await UserJobsCrud.DeleteUserJobsTable(uj.Id);
                    break;

            }
        }
        public static async Task InsertData(IStuff person)
        {
            switch (person.Identifier)
            {
                case (int?)Role.ADMIN:
                    Admin admin = (Admin)person;
                    await AdminCrud.InsertAdmin(admin.Name, admin.Identifier, admin.UserID);
                    break;
                case (int?)Role.EMPLOYEER:
                    Employeer employee = (Employeer)person;
                    await EmployeerCrud.InsertEmployeer(employee.Name, employee.Identifier, employee.UserID);
                    break;
                case (int?)Role.USER:
                    User user = (User)person;
                    await UserCrud.InsertUser(user.Name, user.Identifier);
                    break;
                case (int?)Role.JOB:
                    Job job = (Job)person;
                    await JobCrud.InsertJob(job.Name, job.Id, job.Industry, job.MinSalary, job.MaxSalary, job.Identifier);
                    break;
                case (int)Role.BENEFIT:
                    Benefit benefit = (Benefit)person;
                    await BenefitCrud.InsertBenefit(benefit.Name, benefit.Id);
                    break;
                case (int)Role.JOB_BENEFIT:
                    JobBenefits jb = (JobBenefits)person;
                    await JobBenefitsCrud.InsertJobBenefit(jb.JobID, jb.BenefitID, jb.Id);
                    break;
                case (int)Role.USER_JOBS:
                    UserJobs uj = (UserJobs)person;
                    await UserJobsCrud.InsertUserJobs(uj.Id, uj.UserID, uj.JobID);
                    break;

            }
        }

    }
}
