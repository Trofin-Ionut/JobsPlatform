namespace JobsPlatform.Interfaces
{
    public interface IStuff
    {
        int? Id { get; set; }
        int? Identifier { get;set; }
        public string? Name { get; set; }
        public string? Company { get; set; }
        public string? Industry { get; set; }
        public int? MinSalary { get; set; }
        public int? MaxSalary { get; set; }
        public string? Password { get;set; }
        public abstract int? UserID { get; set; }
        public abstract int? JobID { get; set; }
        public abstract int? BenefitID { get; set; }
    }
}
