using Microsoft.Data.Sqlite;
using JobsPlatform.BusinessLogic.CRUD;
using JobsPlatform.Models;
using JobsPlatform.Enums;

namespace JobsPlatform.BusinessLogic.DataLayer
{

    public static class Database
    {
        public static SqliteConnection conn = new SqliteConnection("Data Source=database.db");
        public static List<Admin> admins;
        public static List<Employeer> employeers;
        public static List<User> users;
        public static List<Job> jobs;
        public static List<Benefit> benefits;
        public static List<JobBenefits> jobBenefits;
        public static List<UserJobs> userJobs;

        static Database()
        {
            InitialiseContainers();

        }
        private static void InitialiseContainers()
        {
            admins = new();
            employeers = new();
            users = new();
            jobs = new();
            benefits = new();
            jobBenefits = new();
            userJobs = new();
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
                 BenefitCrud.ReadBenefitTable(),
                UserJobsCrud.ReadUserJobsTable(),
                JobBenefitsCrud.ReadJobBenefitTable()

            });
        }

    }
}
