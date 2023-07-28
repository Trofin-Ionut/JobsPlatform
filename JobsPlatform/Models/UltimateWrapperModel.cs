using JobsPlatform.Interfaces;

namespace JobsPlatform.Models
{
    public class UltimateWrapperModel
    {
        public string? Name { get; set; }
        public string? Company { get; set; }
        public string?Industry { get; set; }
        public int?MinSalary { get; set; }
        public int? MaxSalary { get; set; }
        public string? Password { get; set; }
        public int Option { get; set; }
    }
}
