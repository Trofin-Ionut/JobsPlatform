using JobsPlatform.Interfaces;

namespace JobsPlatform.Models
{
    public class Job:User
    {
        public string? Industry { get;set; }
        public  int? MinSalary { get;set; }
        public int? MaxSalary { get;set; }
        public string? Name { get;set; } 

        public Job(string?name , int?id,  string? industry, int? minSalary,int?maxSalary,int?identifier):base(name,identifier,id)
        {
            Id = id;
            Name = name;
            MinSalary= minSalary;
            MaxSalary=maxSalary;
            Industry = industry;
            Identifier = identifier;
        }
    }
}
