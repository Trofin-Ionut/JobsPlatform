
namespace JobsPlatform.Models
{
    public class Job
    {
        public int? Id { get; set; }
        public string? Industry { get; set; }
        public int? MinSalary { get; set; }
        public int? MaxSalary { get; set; }
        public string? Name { get; set; }
        public DateTime? Created { get; set; }

        public Job(string? name, int? id, string? industry, int? minSalary, int? maxSalary, DateTime? time)
        {
            Id = id;
            Name = name;
            MinSalary = minSalary;
            MaxSalary = maxSalary;
            Industry = industry;
            Created = time;
        }

        public Job()
        {
        }
    }
}
