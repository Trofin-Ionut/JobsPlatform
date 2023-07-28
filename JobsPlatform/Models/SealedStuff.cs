using JobsPlatform.Interfaces;

namespace JobsPlatform.Models
{
    public abstract class SealedStuff : IStuff
    {
        public int? Id { get; set; }
        public int? Identifier { get; set; }
        public string? Name { get; set; }
        string? IStuff.Company { get; set; }
        string? IStuff.Industry { get; set; }
        int? IStuff.MinSalary { get; set; }
        int? IStuff.MaxSalary { get; set; }
        public string? Password { get; set; }
        int? IStuff.UserID { get; set; }
        int? IStuff.JobID { get; set; }
        int? IStuff.BenefitID { get; set; }
    }
}
